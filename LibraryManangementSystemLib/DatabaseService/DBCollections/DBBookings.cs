using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;
using HotelManangementSystemLibrary.Logging;

namespace HotelManangementSystemLibrary
{
    internal class DBBookings : RoomBookings ,IRoomBookings
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
                    IBookingFees fee = BookingsFactory.CreateBookingFee(dt, cost, amountToPay, amountPayed);

                    //Create the booking
                    IRoomBooking booking = BookingsFactory.CreateBookingWithFees(Id, guest, room, dt, fee, duration);

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
            //Add to the base class
            base.Add(item);
        }//Add
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
            
        }//RoomService_OnServiceLogging
        private void RoomService_OnTicketAdded(Ticket ticket,string serviceid)
        {
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
            
        }//RoomService_PropertyChangedEvent
    }//class
}//namespace