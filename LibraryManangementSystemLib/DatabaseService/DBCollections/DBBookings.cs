using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary
{
    internal class DBBookings : RoomBookings ,IRoomBookings
    {
        //Collections needed for loading the bookings
        //-They are going to be injected through the constructor
        private readonly IGuests _guests;
        private readonly IRooms _rooms;

        private bool isLoading = true;
        private readonly OleDbConnection con;
        public DBBookings(string connectionstring, IGuests guests, IRooms rooms) : base()
        {
            _guests = guests;
            _rooms = rooms;

            //Invoke the booking removed from the base collection
            //-We will user the event handler to move the booking to the old bookings table in our database
            base.RemovedBooking += DBBookings_RemovedBooking;
            con = new OleDbConnection(connectionstring);
            
        }//ctor main
        ~DBBookings()
        {
            con.Dispose();
        }
        internal void LoadData()
        {
            try
            {
                con.Open();
                string query = "qr_LoadBookings";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                OleDbDataReader rd = cmd.ExecuteReader();
                if (rd == default)
                    throw new ArgumentException("Data not loaded");
                while (rd.Read())
                {
                    //Extract the properties
                    string Id = rd["ID"].ToString();
                    string roomNumber = rd["RoomNumber"].ToString();
                    string guestID = rd["GuestID"].ToString();
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
                    IGuest guest = this._guests.FindGuest(guestID);

                    //Create the booking fees
                    IBookingFees fee = new BookingFees(dt, cost, amountToPay, amountPayed);

                    //Create the booking
                    IRoomBooking booking = BookingsFactory.CreateBookingWithFees(Id, guest, room, dt, fee, duration);

                    //Add it to the collection
                    this.Add(booking);
                }//Create objects here
            }//try
            catch(Exception ex)
            {
                throw ex;
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

                string sql = "DELETE FROM tbl_Booking WHERE ID = " + args.Name;
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//try
            catch (Exception ex)
            {
                throw ex;
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
                if (! await PushToDatabase(item))
                    return;
            }
            //Subscibe to the OnPropertyChanged event
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            //Add to the base class
            base.Add(item);
        }//Add
        private async Task<bool> PushToDatabase(IRoomBooking booking)
        {
            try
            {
                //Oppen the connection asyncroniously
                await con.OpenAsync();
                
                string sql = "qr_CreateBooking";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //Pass parameters
                cmd.Parameters.AddWithValue("@GuestID", booking.Guest.UserID);
                cmd.Parameters.AddWithValue("@RoomNumber", booking.Room.RoomNumber);
                cmd.Parameters.AddWithValue("@BookingDate", booking.DateBookedFor.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@Duration", booking.NumberOfDaysToStay);
                cmd.Parameters.AddWithValue("@Cost", booking.BookingFee.BookingCost);
                cmd.Parameters.AddWithValue("@Paid", booking.BookingFee.AmountPaid);
                cmd.Parameters.AddWithValue("@ToPay", booking.BookingFee.AmoutToPay);
                cmd.Parameters.AddWithValue("@ServiceID", "None");

                //Execute
                cmd.ExecuteNonQuery();
                
                return true;
            }//try
            catch (Exception ex)
            {
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
            catch (Exception ex)
            {
                throw ex;
            }//catch
            finally
            {
                con.Close();
            }//finally
        }//Item_PropertyChangedEvent
    }//class
}//namespace