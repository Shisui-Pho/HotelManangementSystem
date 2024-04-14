using System.Data.OleDb;
using System.Data;
using System;
using HotelManangementSystemLibrary.Factory;

namespace HotelManangementSystemLibrary
{
    internal class DBUsers : Users, IUsers
    {
        private bool isLoading = true;
        private readonly OleDbConnection con;
        public DBUsers(string connectionstring) : base()
        {
            con = new OleDbConnection(connectionstring);
        }//ctor main
        ~DBUsers()
        {
            con.Dispose();
        }
        internal void LoadData()
        {
            try { 
                con.Open();
                string query = "qr_LoadUsers";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = cmd.ExecuteReader();

                if (rd == default)
                    throw new ArgumentException("Data not loaded");
                while (rd.Read())
                {
                    TypeOfUser type = (rd["UserType"].ToString() == "Admnin") ? TypeOfUser.Admin : TypeOfUser.Guest;
                    string dateString = rd["DOB"].ToString();
                    DateTime dob = DateTime.Parse(dateString);
                    string name = rd["Name"].ToString();
                    string surname = rd["Surname"].ToString();
                    string username = rd["User_Name"].ToString();
                    string password = rd["User_Password"].ToString();
                    string userId = rd["ID"].ToString();
                    IUser user = UsersFactory.CreateUser(type, name, surname, dob, userId);
                    user.SetPassword(password);
                    user.SetUsername(username);

                    this.Add(user);
                }//Create objects here
            }//end using
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            isLoading = false;
        }//LoadData
        public override void Add(IUser item)
        {
            //Establish the databse connection here
            if (!isLoading)
            {
                try 
                { 
                    //First make the person
                    con.Open();
                    string sql = "qr_CreatePerson";
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", item.UserID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Surname", item.Surname);
                    cmd.Parameters.AddWithValue("@dob", item.DOB);

                    cmd.ExecuteNonQuery();

                    //For the user table
                    //con.Open();
                    sql = "qr_CreateUser";
                    cmd = new OleDbCommand(sql, con);
                    //([@UserId], [@UserName], [@UserPassword], [@UserType]);
                    cmd.Parameters.AddWithValue("@UserId", item.UserID);
                    cmd.Parameters.AddWithValue("@UserName", item.UserName);
                    cmd.Parameters.AddWithValue("@UserPassword", item.Password);
                    string type = (item is IAdministrator) ? "Admin" : "Guest";
                    cmd.Parameters.AddWithValue("@UserType", type);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            //-Subscibe to the Property changed event 
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            base.Add(item);
        }//Add

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            //Establish the database connection here
            try { 
                    con.Open();
                string sql = "UPDATE tbl_User SET " + field + " = '" + newVal + "' WHERE UserID = '" + id + "'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }//Item_PropertyChangedEvent
        public override void Remove(IUser item)
        {
            //Lots of operations to be made for now we going to leave it like this
            base.Remove(item);
        }//Remove
    }//class
}//namespace
