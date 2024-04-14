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
                PushToDataBase(item);
            }//
            //-Subscribe to the PropertyChanged event
            item.GuestPropertyChangedEvent += Item_PropertyChangedEvent;
            item.BalanceChangedEvent += Item_BalanceChangedEvent;
            base.Add(item);
        }//Add
        private void PushToDataBase(IGuest newguest)
        {
            try
            {
                con.Open();
                string sql = "qr_CreateGuest";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //([@GuestID], [@CellNumber], [@Email], [@Emergency], [@Owing], [@Balance]);
                cmd.Parameters.AddWithValue("@GuestID", newguest.UserID);
                cmd.Parameters.AddWithValue("@CellNumber", newguest.ContactDetails.CellphoneNumber);
                cmd.Parameters.AddWithValue("@Email", newguest.ContactDetails.EmailAddress);
                cmd.Parameters.AddWithValue("@Emergency", newguest.ContactDetails.EmergencyNumber);
                cmd.Parameters.AddWithValue("@Owing", newguest.Account.AmountOwing);
                cmd.Parameters.AddWithValue("@Balance", newguest.Account.CurrentBalance);
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
        }//PushToDataBase
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
        private async void Item_BalanceChangedEvent(BalanceChangedEventArgs args)
        {
            try
            {
                await con.OpenAsync();
                string sql = "qr_UpdateBalance";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AmountOwing", args.AmountOwing);
                cmd.Parameters.AddWithValue("@Balance", args.CurrentBalance);
                cmd.Parameters.AddWithValue("@ID", args.AccountUserID);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }//end catch
            finally
            {
                con.Close();
            }//end finally
        }//Item_BalanceChangedEvent
    }//class
}//namespace
