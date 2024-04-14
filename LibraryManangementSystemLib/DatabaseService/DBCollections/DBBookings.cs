using HotelManangementSystemLibrary.Factory;
using System;
using System.Data.OleDb;
namespace HotelManangementSystemLibrary
{
    internal class DBBookings : RoomBookings ,IRoomBookings
    {
        private string connectionString;
        private readonly IGuests _guests;
        private readonly IRooms _rooms;
        private bool isLoading = true;
        public DBBookings(string connectionstring, IGuests guests, IRooms rooms) : base()
        {
            this.connectionString = connectionstring;
            _guests = guests;
            _rooms = rooms;
            base.RemovedBooking += DBBookings_RemovedBooking;
            LoadData();
        }//ctor main
        private void LoadData()
        {
            using(OleDbConnection con = new OleDbConnection(this.connectionString))
            {
                con.Open();
                string query = "SELECT * FROM tbl_Booking";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataReader rd = Execute.GetReader(con, cmd);
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
                    DateTime dt = DateTime.Parse(rd["DateBookedFor"].ToString());

                    //Find the guest and rooms
                    IRoom room = this._rooms.FindRoom(roomNumber);
                    IGuest guest = this._guests.FindGuest(guestID);

                    //Create the booking fees
                    IBookingFees fee = new BookingFees(dt, cost, amountToPay, amountPayed);

                    //Create the booking
                    IRoomBooking booking = BookingsFactory.CreateBookingWithFees(Id,guest, room, dt, fee,duration);
                    
                    //Add it to the collection
                    this.Add(booking);
                }//Create objects here
            }//end using
            isLoading = false;
        }//LoadData
        private void DBBookings_RemovedBooking(object sender, HotelEventArgs args)
        {
            //Establish a database connection here
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();

                string sql = "DELETE FROM tbl_Booking WHERE ID = " + args.Name;
                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con, cmd);
            }
        }//DBBookings_RemovedBooking
        public override void Add(IRoomBooking item)
        {
            //Establish database connection here
            if (!isLoading)
            {
                using(OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    //VALUES ([@GuestID], [@RoomNumber], [@BookingDate], [@Duration], [@Cost], [@Paid], [@ToPay], [@ServiceID]);
                    string sql = "qr_CreateBooking";
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.Parameters.AddWithValue("@GuestID", item.Guest.UserID);
                    cmd.Parameters.AddWithValue("@RoomNumber", item.Room.RoomNumber);
                    cmd.Parameters.AddWithValue("@BookingDate", item.DateBookedFor);
                    cmd.Parameters.AddWithValue("@Duration", item.NumberOfDaysToStay);
                    cmd.Parameters.AddWithValue("@Cost", item.BookingFee.BookingCost);
                    cmd.Parameters.AddWithValue("@Paid", item.BookingFee.AmountPaid);
                    cmd.Parameters.AddWithValue("@ToPay", item.BookingFee.AmoutToPay);
                    cmd.Parameters.AddWithValue("@ServiceID", "None");

                    Execute.NoneQuery(con, cmd);
                }//end using
            }
            //Subscibe to the OnPropertyChanged event
            item.PropertyChangedEvent += Item_PropertyChangedEvent;

            //Add to the base class
            base.Add(item);
        }//Add

        private void Item_PropertyChangedEvent(string id, string field, string newVal)
        {
            using(OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE tbl_Booking SET " + field + " = " + newVal + " WHERE ID = " + id;
                OleDbCommand cmd = new OleDbCommand(sql, con);
                Execute.NoneQuery(con, cmd);
            }//end using
        }//Item_PropertyChangedEvent
    }//class
}//namespace