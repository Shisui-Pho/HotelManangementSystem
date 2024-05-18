using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;
using HotelManangementSystemLibrary.Logging;

namespace HotelManangementSystemLibrary
{
    internal class DBBookings : RoomBookings ,IRoomBookings, IRoomBookingDB
    {
        //Collections needed for loading the bookings
        //-They are going to be injected through the constructor
        private readonly IGuests _guests;
        private readonly IRooms _rooms;
        private readonly IUser _user;
        private bool isLoading = true;
        private readonly OleDbConnection con;
        public DBBookings(string connectionstring, IGuests guests, IRooms rooms, IUser user) : base()
        {
            _guests = guests;
            _rooms = rooms;
            _user = user;
            //Invoke the booking removed from the base collection
            //-We will user the event handler to move the booking to the old bookings table in our database
            base.RemovedBooking += DBBookings_RemovedBooking;
            con = new OleDbConnection(connectionstring);
            
        }//ctor main
        ~DBBookings()
        {
            con.Dispose();
        }
        internal async Task LoadData()
        {
            try
            {
                await con.OpenAsync();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (_user is IAdministrator)//Load all bookings
                {
                    cmd.CommandText = "qr_LoadBookings";
                }//end if admin
                else if (_user is IGuest)//Load guest specific bookings
                {
                    cmd.CommandText = "qr_GuestBookings";
                    cmd.Parameters.AddWithValue("@Id", _user.UserID);
                }//end if guest profile
                cmd.Connection = con;
                //cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = cmd.ExecuteReader();

                while (await rd.ReadAsync())
                {
                    //Extract the properties
                    string Id = rd["ID"].ToString();
                    string roomNumber = rd["RoomNumber"].ToString();
                    string serviceID = rd["ServiceID"].ToString();

                    //Get amounts and convert
                    decimal amountPayed = decimal.Parse(rd["AmountPaid"].ToString());
                    decimal amountToPay = decimal.Parse(rd["AmountToPay"].ToString());
                    decimal cost = decimal.Parse(rd["BookingCost"].ToString());

                    //Get remaining prop
                    int duration = int.Parse(rd["Duration"].ToString());
                    string dateString = rd["DateBookedFor"].ToString();
                    DateTime dt = DateTime.Parse(dateString);

                    //Find the guest and rooms
                    IRoom room = this._rooms.FindRoom(roomNumber);
                    IGuest guest = default; //this._guests.FindGuest(guestID);
                    if (_user is IAdministrator)
                    {
                        string guestID = rd["GuestID"].ToString();
                        guest = this._guests.FindGuest(guestID);
                    }
                    else
                        guest = this._guests.CurrentGuest;
                        
                    //Create the booking fees
                    IBookingFees fee = BookingsFactory.CreateBookingFee(dt, cost,amountPayed, amountToPay);

                    //Create the booking
                    IRoomBooking booking = BookingsFactory.CreateBookingWithFees(Id, guest, room, dt, fee, duration);

                    if (_user is IGuest)
                    {
                        //Add the top 10 transactions
                        string sql = "qr_LoadTransactions";
                        string accountNumber = guest.Account.AccountNumber;
                        cmd = new OleDbCommand(sql, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        OleDbDataReader rdtransactions = cmd.ExecuteReader();

                        IUserAccountDB accountdb = (IUserAccountDB)guest.Account;
                        while (rdtransactions.Read())
                        {
                            DateTime timestamp = DateTime.Parse(rdtransactions["TransactionTimeStamp"].ToString());
                            string message = rdtransactions["Message"].ToString();
                            decimal amount = decimal.Parse(rdtransactions["Amount"].ToString());
                            BalanceAffected affected = (BalanceAffected)Enum.Parse(typeof(BalanceAffected), rdtransactions["BalanceAffected"].ToString());

                            TransactionArgs trans = new TransactionArgs(message, amount, affected, accountNumber,timestamp);
                            accountdb.AddTransaction(trans);
                        }//end transaction reading
                    }//end if guest transactions

                    //Add it to the collection
                    this.Add(booking);
                }//Create objects here
            }//try
            catch(Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }//catch
            finally
            {
                con.Close();
            }//finally
            isLoading = false;
        }//LoadData
        private async void DBBookings_RemovedBooking(object sender, HotelEventArgs args)
        {
            if (isLoading)
                return;
            try 
            { 
                await con.OpenAsync();

                string sql = "UPDATE tbl_Booking SET IsCancelled = TRUE WHERE ID = " + args.Name;
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO tbl_CancelledBooking(BookingId,CancellationReason) VALUES(" + args.Name + ",\"" + args.Description + "\")";
                cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }//catch
            finally
            {
                con.Close();
            }//finally
        }//DBBookings_RemovedBooking
        public async override void Add(IRoomBooking item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                try
                {
                    //First check if the room can be booked on that specified amount of time/duration
                    if (!item.Room.BookedDates.AddBookingDate(item.DateBookedFor, item.NumberOfDaysToStay))
                        return;
                    if (!await PushToDatabase(item))
                        return;
                }catch(Exception ex)
                {
                    ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                    throw;
                }
            }
            //Subscibe to the OnPropertyChanged event
            item.PropertyChangedEvent += Item_PropertyChangedEvent;
            item.RoomService.PropertyChangedEvent += RoomService_PropertyChangedEvent;
            item.RoomService.OnServiceLogging += RoomService_OnServiceLogging;
            item.RoomService.OnTicketAdded += RoomService_OnTicketAdded;
            item.BookingFee.BookingFeesChanged += BookingFee_BookingFeesChanged;
            //Check 
            if (isLoading)
                base.AddFromDB(item);
            else
                base.Add(item);
        }//Add

        private void BookingFee_BookingFeesChanged(BookingFeesChangedEventArgs args)
        {

            if (isLoading)
                return;
            try
            {
                //Open an async connection
                con.OpenAsync();

                //Build the required sql based on the field changed
                string sql = "qr_UpdateBookingFees";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                // @AmountPaid , AmountToPay = @AmountToPay  WHERE BookingID = @BookingID;
                cmd.Parameters.AddWithValue("@AmountPaid", args.AmountPaid);
                cmd.Parameters.AddWithValue("@AmountToPay", args.AmountToPay);
                cmd.Parameters.AddWithValue("@BookingID",args.BookingID);
                cmd.ExecuteNonQuery();
            }//try
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }//catch
            finally { con.Close(); }//finally
        }//BookingFee_BookingFeesChanged

        private async Task<bool> PushToDatabase(IRoomBooking booking)
        {
            OleDbTransaction trans = null;
            try
            {
                //Oppen the connection asyncroniously
                await con.OpenAsync();
                trans = con.BeginTransaction();
                string sql = "qr_CreateBooking";
                OleDbCommand cmd = new OleDbCommand(sql, con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                //Pass parameters
                cmd.Parameters.AddWithValue("@GuestID", booking.Guest.UserID);
                cmd.Parameters.AddWithValue("@RoomNumber", booking.Room.RoomNumber);
                cmd.Parameters.AddWithValue("@BookingDate", booking.DateBookedFor.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@Duration", booking.NumberOfDaysToStay);
                cmd.Parameters.AddWithValue("@ServiceID", 0);

                //Execute first query
                cmd.ExecuteNonQuery();

                sql = "qr_CreateBookingFee";
                cmd = new OleDbCommand(sql, con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cost", booking.BookingFee.BookingCost);
                cmd.Parameters.AddWithValue("@Paid", booking.BookingFee.AmountPaid);
                cmd.Parameters.AddWithValue("@ToPay", booking.BookingFee.AmoutToPay);
                cmd.ExecuteNonQuery();
                trans.Commit();
                return true;
            }//try
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }//catch
            finally
            {
                con.Close();
            }//finally
        }//PushToDatabase 
        private async void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            if (isLoading)
                return;
            try
            {
                //Open an async connection
                await con.OpenAsync();

                //Build the required sql based on the field changed
                string sql = "UPDATE tbl_Booking SET " + field + " = '" + newVal + "' WHERE ID = '" + id +"'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//try
            catch(Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }//catch
            finally{con.Close();}//finally
        }//Item_PropertyChangedEvent
        private void RoomService_OnServiceLogging(ServiceLogEventArgs args)
        {
            if (isLoading)
                return;
            try
            {
                con.Open();
                string sql = "qr_CreateServiceLog";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoomServiceID", args.RoomServiceID);
                cmd.Parameters.AddWithValue("@Activity", args.Activity);
                cmd.Parameters.AddWithValue("@Timestamp", args.TimeStamp);
                //Extecute the sommnad
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }//RoomService_OnServiceLogging
        private void RoomService_OnTicketAdded(Ticket ticket,string serviceid)
        {
            if (isLoading)
                return;
            try
            {
                con.Open();
                string sql = "qr_CreateTicket";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                //VALUES (@PersonelD,@RoomServiceID, @Desc,FALSE);
                //Pass the parameters
                cmd.Parameters.AddWithValue("@PersonelD", ticket.AssignedPersonelID);
                cmd.Parameters.AddWithValue("@RoomServiceID", serviceid);
                cmd.Parameters.AddWithValue("@Desc", ticket.Description);

                //Extecute the sommnad
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw; }
            finally { con.Close(); }
        }//RoomService_OnTicketAdded
        private void RoomService_PropertyChangedEvent(string id, string field, string newVal)
        {
            if (isLoading)
                return;
            try
            {
                string service = "EndTimeStartTimePersonelID";
                con.Open();
                //BuildSql
                string sql = "";
                if (service.IndexOf(field) >= 0)
                {
                    //if we are changing the service it self
                    sql = $"UPDATE tbl_RoomService SET {field} = \"{newVal}\" WHERE ID = {id}";
                }
                else
                {
                    //We are changing the ticket
                    sql = $"UPDATE tbl_Ticket SET {field} = \"{newVal }\" WHERE TicketID = \"{id }\"";
                }
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw;
            }
            finally { con.Close(); }
        }//RoomService_PropertyChangedEvent
    }//class
}//namespace