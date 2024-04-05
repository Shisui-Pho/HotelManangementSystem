namespace HotelManangementSystemLibrary
{
    internal class OldBooking : IOldBooking
    {
        public IRoomBooking Booking { get; private set; }
        public CancellationReason? State { get; private set; } = null;
        public string StateString { get; private set; } = "";
        public OldBooking(IRoomBooking booking,CancellationReason state)
        {
            Booking = booking;
            State = state;
        }//ctor 01
        public OldBooking(IRoomBooking booking, string state)
        {
            Booking = booking;
            StateString = state;
        }//ctor 01

        public int CompareTo(object obj)
        {
            return this.Booking.BookingID.CompareTo(((IOldBooking)obj).Booking.BookingID);
        }

        public bool Equals(IOldBooking other)
        {
            return this.Booking.BookingID == other.Booking.BookingID;
        }
    }//
}//namespace
