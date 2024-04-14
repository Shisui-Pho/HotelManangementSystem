using System.Data.OleDb;
using System.Data;
using System;
using HotelManangementSystemLibrary.Factory;

namespace HotelManangementSystemLibrary
{
    internal class DBUsers : Users, IUsers
    {
        private bool isLoading = true;
        private readonly string connectionString;
        public DBUsers(string connectionstring) : base()
        {
            this.connectionString = connectionstring;
            LoadData();
        }//ctor main
        private void LoadData()
        {
            using (OleDbConnection con = new OleDbConnection(this.connectionString))
            {
                con.Open();
                string query = "qr_LoadUsers";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = Execute.GetReader(con, cmd);

                if (rd == default)
                    throw new ArgumentException("Data not loaded");
                while (rd.Read())
                {
                    TypeOfUser type = (rd["UserType"].ToString() == "Admnin") ? TypeOfUser.Admin : TypeOfUser.Guest;
                    DateTime dob = DateTime.Parse(rd["DOB"].ToString());
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
            isLoading = false;
        }//LoadData
        public override void Add(IUser item)
        {
            //Establish the databse connection here
            if (!isLoading)
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
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

                    Execute.NoneQuery(con, cmd);

                    //For the user table
                    con.Open();
                    sql = "qr_CreateUser";
                    cmd = new OleDbCommand(sql, con);
                    //([@UserId], [@UserName], [@UserPassword], [@UserType]);
                    cmd.Parameters.AddWithValue("@UserId", item.UserID);
                    cmd.Parameters.AddWithValue("@UserName", item.UserName);
                    cmd.Parameters.AddWithValue("@UserPassword", item.Password);
                    string type = (item is IAdministrator) ? "Admin" : "Guest";
                    cmd.Parameters.AddWithValue("@UserType", type);

                    Execute.NoneQuery(con, cmd);
                }
            }
            //-Subscibe to the Property changed event 
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            base.Add(item);
        }//Add

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            //Establish the database connection here
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE tbl_User SET " + field + " = " + newVal + " WHERE UserID = " + id;
                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con, cmd);
            }
        }//Item_PropertyChangedEvent
        public override void Remove(IUser item)
        {
            //Lots of operations to be made for now we going to leave it like this
            base.Remove(item);
        }//Remove
    }//class
}//namespace
