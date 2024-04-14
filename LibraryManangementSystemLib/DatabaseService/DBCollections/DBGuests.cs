using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
namespace HotelManangementSystemLibrary
{
    internal class DBGuests : Guests , IGuests
    {
        private readonly OleDbConnection con;
        private bool isLoading = true;
        private readonly IUsers _users;
        public DBGuests(string connectionString, IUsers users) : base()
        {
            con = new OleDbConnection(connectionString);
            _users = users;
        }//ctor main
        ~DBGuests()
        {
            con.Dispose();
        }
        internal void LoadData()
        {
            try 
            { 
                con.Open();
                string query = "qr_LoadGuests";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    string id = rd["GuestID"].ToString();
                    string cellphoneNumber = rd["CellphoneNumber"].ToString();
                    string email = rd["Email_Address"].ToString();
                    string emegencyNo = rd["Emergency_PhoneNumber"].ToString();

                    decimal owing = decimal.Parse(rd["Amount_Owing"].ToString());
                    decimal balance = decimal.Parse(rd["Balance"].ToString());

                    IUser user = this._users.GetUser(id);
                    IGuest guest = UsersFactory.CreateGuest(user, owing,balance);
                    guest.SetCellNumber(cellphoneNumber);
                    guest.SetEmailAddress(email);
                    guest.SetEmergencyNumber(emegencyNo);

                    this.Add(guest);
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

        public override void Add(IGuest item)
        {
            //Establish the databse connection here
            if (!isLoading)
            {
                try 
                { 
                    con.Open();
                    string sql = "qr_CreateGuest";
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //([@GuestID], [@CellNumber], [@Email], [@Emergency], [@Owing], [@Balance]);
                    cmd.Parameters.AddWithValue("@GuestID", item.UserID);
                    cmd.Parameters.AddWithValue("@CellNumber", item.ContactDetails.CellphoneNumber);
                    cmd.Parameters.AddWithValue("@Email", item.ContactDetails.EmailAddress);
                    cmd.Parameters.AddWithValue("@Emergency", item.ContactDetails.EmergencyNumber);
                    cmd.Parameters.AddWithValue("@Owing", item.Account.AmountOwing);
                    cmd.Parameters.AddWithValue("@Balance", item.Account.CurrentBalance);
                    cmd.ExecuteNonQuery();
                    //Execute.NoneQuery(con, cmd);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }//
            //-Subscribe to the PropertyChanged event
            item.GuestPropertyChangedEvent += Item_PropertyChangedEvent;
            base.Add(item);
        }//Add

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            try 
            { 
                con.Open();

                string sql = "UPDATE tbl_Guest SET " + field + " = '" + newVal + "' WHERE GuestID = '" + id + "'";
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
    }//class
}//namespace
