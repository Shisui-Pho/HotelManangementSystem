using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
using System.Data;
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
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            isLoading = false;
        }//LoadData
        private void DBBookings_RemovedBooking(object sender, HotelEventArgs args)
        {
            //Establish a database connection here
            try 
            { 
                con.Open();

                string sql = "DELETE FROM tbl_Booking WHERE ID = " + args.Name;
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
        }//DBBookings_RemovedBooking
        public override void Add(IRoomBooking item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                try 
                {
                    con.Open();
                    //VALUES ([@GuestID], [@RoomNumber], [@BookingDate], [@Duration], [@Cost], [@Paid], [@ToPay], [@ServiceID]);
                    string sql = "qr_CreateBooking";
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GuestID", item.Guest.UserID);
                    cmd.Parameters.AddWithValue("@RoomNumber", item.Room.RoomNumber);
                    cmd.Parameters.AddWithValue("@BookingDate", item.DateBookedFor.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@Duration", item.NumberOfDaysToStay);
                    cmd.Parameters.AddWithValue("@Cost", item.BookingFee.BookingCost);
                    cmd.Parameters.AddWithValue("@Paid", item.BookingFee.AmountPaid);
                    cmd.Parameters.AddWithValue("@ToPay", item.BookingFee.AmoutToPay);
                    cmd.Parameters.AddWithValue("@ServiceID", "None");
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
            }
            //Subscibe to the OnPropertyChanged event
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            //Add to the base class
            base.Add(item);
        }//Add

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            try
            {
                con.Open();
                string sql = "UPDATE tbl_Booking SET " + field + " = '" + newVal + "' WHERE ID = '" + id +"'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.ExecuteNonQuery();
            }//end using
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