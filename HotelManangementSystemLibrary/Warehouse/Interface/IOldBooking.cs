namespace HotelManangementSystemLibrary
{
    public interface IOldBooking
    {
        IRoomBooking Booking { get; }
        BookingState State { get; }
    }//
}//namespace
