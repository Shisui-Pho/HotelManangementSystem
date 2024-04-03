namespace HotelManangementSystemLibrary
{
    public interface IOldBooking
    {
        IRoomBooking Booking { get; }
        CancellationReason? State { get; }
        string StateString { get; }
    }//
}//namespace
