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
            try 
            { 
                //Open the connection
                con.Open();
                string query = "qr_LoadUsers";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Load the users
                OleDbDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    //Determin the type of user first
                    TypeOfUser type = (rd["UserType"].ToString() == "Admin") ? TypeOfUser.Admin : TypeOfUser.Guest;
                    string dateString = rd["DOB"].ToString();
                    DateTime dob = DateTime.Parse(dateString);
                    string name = rd["Name"].ToString();
                    string surname = rd["Surname"].ToString();
                    string username = rd["User_Name"].ToString();
                    string password = rd["User_Password"].ToString();
                    string userId = rd["ID"].ToString();

                    //Create the user
                    IUser user = UsersFactory.CreateUser(type, name, surname, dob, userId);
                    
                    //Set the password
                    user.SetPassword(password);
                    user.SetUsername(username);

                    //Add the user to the collection
                    this.Add(user);
                }//Create objects here
            }//end try
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
        public override void Add(IUser item)
        {
            //Establish the databse connection here
            if (!isLoading)
            {
                if (!PushToDatabse(item))
                    return;
            }
            //-Subscibe to the Property changed event 
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            base.Add(item);
        }//Add
        private bool PushToDatabse(IUser user)
        {
            //If the unexpected we can always rollback the transaction and notify the user
            OleDbTransaction trans = null;
            try
            {

                con.Open();
                //First create the person

                //Start a transaction since we have multiple statements
                trans = con.BeginTransaction();
                string sql = "qr_CreatePerson";
                OleDbCommand cmd = new OleDbCommand(sql, con);

                //Pass the required parameters
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", user.UserID);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Surname", user.Surname);
                cmd.Parameters.AddWithValue("@dob", user.DOB);

                //Execute the first operation
                cmd.ExecuteNonQuery();

                //For the user table
                sql = "qr_CreateUser";
                cmd = new OleDbCommand(sql, con);
                cmd.Parameters.AddWithValue("@UserId", user.UserID);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);
                string type = (user is IAdministrator) ? "Admin" : "Guest";
                cmd.Parameters.AddWithValue("@UserType", type);
                cmd.ExecuteNonQuery();

                //Save the changes if everthing was successful
                trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                //If an error occured at any stage, rollback 
                //-Since there are only two statement, then if the first query fails nothing will be done
                //- if query 2 fails,the first query will be rolled back 
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                con.Close();
                if (trans != null)
                    trans.Dispose();
            }
        }
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
