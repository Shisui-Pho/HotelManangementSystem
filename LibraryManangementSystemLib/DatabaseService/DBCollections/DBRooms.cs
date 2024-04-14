﻿using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
namespace HotelManangementSystemLibrary
{
    internal class DBRooms : Rooms, IRooms
    {
        private bool isLoading = true;
        private readonly string connectionString;
        public DBRooms(string connectionstring) : base()
        {
            this.connectionString = connectionstring;
            LoadData();
        }//ctor main
        private void LoadData()
        {
            using (OleDbConnection con = new OleDbConnection(this.connectionString))
            {
                string query = "SELECT * FROM tbl_Room";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataReader rd = Execute.GetReader(con, cmd);
                if (rd == default)
                    throw new ArgumentException("Data not loaded");
                while (rd.Read())
                {
                    //For loading the room
                    string roomNumber = rd["RoomNumber"].ToString();
                    bool isSingleRoom = bool.Parse(rd["IsSingleRoom"].ToString());
                    IRoom room = RoomFactory.CreateRoom(isSingleRoom? TypeOfRoom.SingleRoom : TypeOfRoom.SharingRoom,roomNumber);
                    
                    
                    //For loading room features
                    query = "qr_GetRoomFeatures";
                    con.Open();
                    cmd = new OleDbCommand(query, con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    OleDbDataReader rd2 = Execute.GetReader(con, cmd);
                    while (rd2.Read())
                    {
                        string fname = rd2["F_Name"].ToString();
                        string dsc = rd2["F_Description"].ToString();
                        decimal price = decimal.Parse(rd2["F_Price"].ToString());
                        Feature ft = new Feature(fname, dsc, price);
                        room.RoomFeatures.AddFeature(ft);
                    }
                    this.Add(room);
                }//Create objects here
            }//end using
            isLoading = false;
        }//LoadData
        public override void Add(IRoom item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    string sql = "qr_CreateRoom";

                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomNumber", item.RoomNumber);
                    cmd.Parameters.AddWithValue("@RoomPrice", item.Price);
                    cmd.Parameters.AddWithValue("@IsSingle", item.IsSingleRoom);
                    Execute.NoneQuery(con,cmd);
                }//end using
            }//END IF
            //-Subscribe to the PropertyChangedEvent
            item.PropertyChangedEvent += Item_PropertyChangedEvent;
            item.RoomFeatures.OnFeaturesModified += RoomFeatures_OnFeaturesModified;
            base.Add(item);
        }//Add

        private void RoomFeatures_OnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args)
        {
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sql;
                if (!isAdded)
                    sql = "DELETE FROM tbl_RoomFeature WHERE FeatureID = " + feature.FeatureID + " AND RoomNumber = " + args.RoomNumber; 
                else
                    sql = "INSERT INTO tbl_RoomFeature(FeatureID,RoomNumber) VALUES ("+feature.FeatureID+","+args.RoomNumber+")";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con, cmd);
            }
        }//RoomFeatures_OnFeaturesModified

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE tbl_Room SET " + field + " = " + newVal + " WHERE RoomNumber = " + id;

                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con, cmd);
            }//end using
        }//Item_PropertyChangedEvent

        public override void Remove(IRoom item)
        {
            //Establish the database connection here
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                string sql = "DELETE FROM tbl_Room WHERE RoomNumber = " + item.RoomNumber;
                con.Open();

                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con,cmd);
            }//end using
            base.Remove(item);
        }//Remove

        public override void Update(IRoom old, IRoom _new)
        {
            //Establish the database connection here

            base.Update(old, _new);
        }//Update
    }//class
}//namespace