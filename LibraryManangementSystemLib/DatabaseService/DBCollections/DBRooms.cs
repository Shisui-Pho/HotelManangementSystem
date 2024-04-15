using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
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
                string query = "qr_LoadRooms";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = cmd.ExecuteReader();
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
                    cmd = new OleDbCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    OleDbDataReader rd2 = cmd.ExecuteReader();
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
            catch (Exception ex)
            {
                throw ex;
            }//end catch
            finally
            {
                con.Close();
            }//end finally
            isLoading = false;
        }//LoadData
        public override void Add(IRoom item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                try 
                { 
                    //Open the connection
                    con.Open();
                    string sql = "qr_CreateRoom";

                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Pass the parameters
                    cmd.Parameters.AddWithValue("@RoomNumber", item.RoomNumber);
                    cmd.Parameters.AddWithValue("@RoomPrice", item.Price);
                    cmd.Parameters.AddWithValue("@IsSingle", item.IsSingleRoom);

                    //Execute the query
                    cmd.ExecuteNonQuery();
                }//end try
                catch (Exception ex)
                {
                    throw ex;
                }//end catch
                finally
                {
                    con.Close();
                }//end finaly
            }//END IF
            //-Subscribe to the PropertyChangedEvent
            item.PropertyChangedEvent += Item_PropertyChangedEvent;
            item.RoomFeatures.OnFeaturesModified += RoomFeatures_OnFeaturesModified;
            base.Add(item);
        }//Add

        private void RoomFeatures_OnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args)
        {
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
                cmd.ExecuteNonQuery();
            }//end try
            catch (Exception ex)
            {
                throw ex;
            }//end catch
            finally
            {
                con.Close();
            }//end finally
        }//RoomFeatures_OnFeaturesModified

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
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
                throw ex;
            }//end catch 
            finally
            {
                con.Close();
            }//end finally
        }//Item_PropertyChangedEvent

        public override void Remove(IRoom item)
        {
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
                throw ex;
            }//catch
            finally
            {
                con.Close();
            }//finally

            //Remove from the collection
            base.Remove(item);
        }//Remove
    }//class
}//namespace
