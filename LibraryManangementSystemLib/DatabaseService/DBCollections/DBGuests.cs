﻿using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
using System.Runtime.ExceptionServices;
using HotelManangementSystemLibrary.Logging;

namespace HotelManangementSystemLibrary
{
    internal class DBGuests : Guests , IGuests
    {
        //Datamembers
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
        }//destructor
        //[HandleProcessCorruptedStateExceptionsAttribute]
        internal async void LoadData()
        {
            //Handle's it nicely
            //netsh winsock reset
            if (base._collection.Count > 0)
                return;
            try
            { 
                //Open connection
                await con.OpenAsync();

                //
                string query = "qr_LoadGuests";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Get the data
                OleDbDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    //Extract the fields per record
                    string id = rd["GuestID"].ToString();
                    string cellphoneNumber = rd["CellphoneNumber"].ToString();
                    string email = rd["Email_Address"].ToString();
                    string emegencyNo = rd["Emergency_PhoneNumber"].ToString();

                    //Account details
                    string accountNumber = rd["AccountNumber"].ToString();
                    decimal owing = decimal.Parse(rd["Amount_Owing"].ToString());
                    decimal balance = decimal.Parse(rd["Balance"].ToString());

                    //Get the user profile from users collection
                    IUser user = this._users.GetUser(id);
                    IUSerAccount account = UsersFactory.CreateUserAccount(balance, owing, accountNumber);
                    IContactDetails contacts = UsersFactory.CreateContactDetails(email, cellphoneNumber, emegencyNo);

                    IGuest guest = UsersFactory.CreateGuest(user,contacts,account);
                    guest.SetCellNumber(cellphoneNumber);
                    guest.SetEmailAddress(email);
                    guest.SetEmergencyNumber(emegencyNo);

                    this.Add(guest);
                }//Create objects here
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }

            //Turn off our flag for loading
            isLoading = false;
        }//LoadData

        public override void Add(IGuest item)
        {
            //Establish the databse connection here
            if (!isLoading)
            {
                if (!PushToDataBase(item))
                    return;
            }//

            //-Subscribe to the PropertyChanged event and Balance changed
            //-This three events handlers will do the updates to the database
            item.GuestPropertyChangedEvent += Item_PropertyChangedEvent;
            item.BalanceChangedEvent += Item_BalanceChangedEvent;
            item.Account.OnTransactionEvent += Account_OnTransactionEvent;

            //Add the guest to the local collection
            base.Add(item);
        }//Add
        private bool PushToDataBase(IGuest newguest)
        {
            //To note 
            //-The user has already been created at this point at has a valid user id
            //-All we do is extend the user to create a guest and account using the same user ID
            
            //If the unexpected we can always rollback the transaction and notify the user
            OleDbTransaction trans = null;
            try
            {
                //Oppen connection
                con.Open();

                //Start a transaction since we have multiple statements
                trans = con.BeginTransaction();

                //Procedure name
                string sql = "qr_CreateGuest";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Add parameters
                cmd.Parameters.AddWithValue("@GuestID", newguest.UserID);
                cmd.Parameters.AddWithValue("@CellNumber", newguest.ContactDetails.CellphoneNumber);
                cmd.Parameters.AddWithValue("@Email", newguest.ContactDetails.EmailAddress);
                cmd.Parameters.AddWithValue("@Emergency", newguest.ContactDetails.EmergencyNumber);
                
                //Execute
                cmd.ExecuteNonQuery();


                //Create the guest account if the guest was created successfully
                sql = "qr_CreateAccount";
                cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Add Parameters
                cmd.Parameters.AddWithValue("@GuestID", newguest.UserID);
                cmd.Parameters.AddWithValue("@Owing", newguest.Account.AmountOwing);
                cmd.Parameters.AddWithValue("@Balance", newguest.Account.CurrentBalance);

                //Execute
                cmd.ExecuteNonQuery();

                //Save the changes if everthing was successful
                trans.Commit();
                return true;
            }//end try
            catch (Exception ex)
            {
                //If an error occured at any stage, rollback 
                //-Since there are only two statement, then if the first query fails nothing will be done
                //- if query 2 fails,the first query will be rolled back 
                if (trans != null)
                    trans.Rollback();
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }//end catch
            finally
            {
                con.Close();
                if(trans != null)
                    trans.Dispose();
            }//end finally
        }//PushToDataBase
        private async void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            if (isLoading)
                return;
            try 
            { 
                //Open the connection async to prevent connection delays from the UI
                await con.OpenAsync();
                string sql = "UPDATE tbl_Guest SET " + field + " = '" + newVal + "' WHERE GuestID = '" + id + "'";
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
        private async void Item_BalanceChangedEvent(BalanceChangedEventArgs args)
        {
            if (isLoading)
                return;
            try
            {
                //Open the connection async to prevent connection delays from the UI
                await con.OpenAsync();
                string sql = "qr_UpdateBalance";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Pass the parameters
                cmd.Parameters.AddWithValue("@AmountOwing", args.AmountOwing);
                cmd.Parameters.AddWithValue("@Balance", args.CurrentBalance);
                cmd.Parameters.AddWithValue("@ID", args.AccountUserID);

                //Execute asyncroneously
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }//Item_BalanceChangedEvent
        private void Account_OnTransactionEvent(TransactionArgs transaction)
        {
            if (isLoading || !transaction.CanPushToDatabase)
                return;

            try
            {
                con.Open();
                //@AccountNumber,@TimeStamp,@Message,@BalanceAffected,@Amount
                //qr_CreateTransaction
                string sql = "qr_CreateTransaction";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
                cmd.Parameters.AddWithValue("@TTimeStamp", transaction.TimeStamp.ToString());
                cmd.Parameters.AddWithValue("@Message", transaction.Message);
                cmd.Parameters.AddWithValue("@BalanceAffected", transaction.AffectedBalance.ToString());
                cmd.Parameters.AddWithValue("@Amount", transaction.Amount);

                //INSERT INTO tbl_Transaction ( AccountNumber, Time_Stamp, Message, BalanceAffected, Amount )
                //VALUES([@AccountNumber], [@TimeStamp], [@Message], [@BalanceAffected], [@Amount]);

                //string sql = $"INSERT INTO tbl_Transaction ( AccountNumber, Time_Stamp, Message, BalanceAffected, Amount) VALUES " +
                //    $"(\"{transaction.AccountNumber}\", \"{transaction.}\")";
                //OleDbCommand cmd = new OleDbCommand();

                cmd.ExecuteNonQuery();
            }//end try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }finally{con.Close();}
        }//Account_OnTransactionEvent
    }//class
}//namespace
