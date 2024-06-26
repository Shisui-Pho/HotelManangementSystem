﻿using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
using HotelManangementSystemLibrary.Logging;

namespace HotelManangementSystemLibrary
{
    internal class DBRooms : Rooms, IRooms
    {
        private bool isLoading = true;
        private readonly OleDbConnection con;
        public DBRooms(string connectionstring) : base()
        {
            con = new OleDbConnection(connectionstring);
        }//ctor main
        ~DBRooms()
        {
            con.Dispose();
        }
        internal void LoadData()
        {
            try 
            { 
                con.Open();
                string sql = "qr_LoadRooms";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rdRooms = cmd.ExecuteReader();
                if (rdRooms == default)
                    throw new ArgumentException("Data not loaded");
                while (rdRooms.Read())
                {
                    //For loading the room
                    string roomNumber = rdRooms["RoomNumber"].ToString();
                    bool isSingleRoom = bool.Parse(rdRooms["IsSingleRoom"].ToString());

                    //Load Room Booked Dates
                    sql = "qr_BookedRoomDates";
                    cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    OleDbDataReader rdBookedDates = cmd.ExecuteReader();
                    IRoomBookedDate bookedDates = RoomFactory.CreateRoomBookedDates();
                    while (rdBookedDates.Read())
                    {
                        DateTime date = DateTime.Parse(rdBookedDates["DateBookedFor"].ToString());
                        int duration = int.Parse(rdBookedDates["Duration"].ToString());
                        bookedDates.AddBookingDate(date, duration);
                    }//end bookings

                    IRoom room = RoomFactory.CreateRoom(isSingleRoom? TypeOfRoom.SingleRoom : TypeOfRoom.SharingRoom,roomNumber,bookedDates);

                    //For loading room features
                    sql = "qr_GetRoomFeatures";
                    cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    OleDbDataReader rdFeatures = cmd.ExecuteReader();
                    while (rdFeatures.Read())
                    {
                        string fname = rdFeatures["F_Name"].ToString();
                        string dsc = rdFeatures["F_Description"].ToString();
                        decimal price = decimal.Parse(rdFeatures["F_Price"].ToString());
                        Feature ft = new Feature(fname, dsc, price);
                        room.RoomFeatures.AddFeature(ft);
                    }//end loadding 
                    this.Add(room);
                }//Create objects here
            }//end using
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
            isLoading = false;
        }//LoadData
        public override void Add(IRoom item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                if (!PushToDatabase(item))
                    return;
            }//END IF
            //-Subscribe to the PropertyChangedEvent
            item.PropertyChangedEvent += Item_PropertyChangedEvent;
            item.RoomFeatures.OnFeaturesModified += RoomFeatures_OnFeaturesModified;
            base.Add(item);
        }//Add
        private bool PushToDatabase(IRoom room)
        {
            try
            {
                //Open the connection
                con.Open();
                string sql = "qr_CreateRoom";

                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //Pass the parameters
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomPrice", room.Price);
                cmd.Parameters.AddWithValue("@IsSingle", room.IsSingleRoom);

                //Execute the query
                cmd.ExecuteNonQuery();

                return true;
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }
        private void RoomFeatures_OnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args)
        {
            if (isLoading)
                return;
            //This will be executed everytime we remove or add a new feature to a room 
            try 
            { 
                con.Open();
                string sql;

                //Check which operation is being made
                if (!isAdded)//IF delete
                    sql = "qr_RemoveFeature"; 
                else//If insert
                    sql = "qr_AddRoomFeature";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //Pass the parameters
                cmd.Parameters.AddWithValue("@FeatureID", feature.FeatureID);
                cmd.Parameters.AddWithValue("@RoomNumber", args.RoomNumber);
                int count = cmd.ExecuteNonQuery();
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }//RoomFeatures_OnFeaturesModified

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            if (isLoading)
                return;
            try 
            { 
                con.Open();
                //Build the sql query to be executed
                string sql = "UPDATE tbl_Room SET " + field + " = '" + newVal + "' WHERE RoomNumber = '" + id + "'";

                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }//Item_PropertyChangedEvent

        public override void Remove(IRoom item)
        {
            if (isLoading)
                return;
            try 
            { 
                //Open connection
                con.Open();

                //Detele the room first
                string sql = "DELETE FROM tbl_Room WHERE RoomNumber = '" + item.RoomNumber + "'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();

                //Remove all the features associated this the room number
                sql = "DELETE FROM tblRoomFeature WHERE RoomNumber = '" + item.RoomNumber + "'";
                cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }

            //Remove from the collection
            base.Remove(item);
        }//Remove
    }//class
}//namespace
