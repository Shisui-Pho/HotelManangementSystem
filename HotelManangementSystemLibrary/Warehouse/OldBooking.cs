namespace HotelManangementSystemLibrary
{
    internal class OldBooking : IOldBooking
    {
        public IRoomBooking Booking { get; private set; }
        public BookingState State { get; private set; }
        public OldBooking(IRoomBooking booking,BookingState state)
        {
            Booking = booking;
            State = state;
        }//ctor 01
    }//
}//namespace
