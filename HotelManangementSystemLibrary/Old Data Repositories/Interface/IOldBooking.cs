namespace HotelManangementSystemLibrary
{
    public interface IOldBooking
    {
        IRoomBooking Booking { get; }
        CancellationReason State { get; }
    }//
}//namespace
