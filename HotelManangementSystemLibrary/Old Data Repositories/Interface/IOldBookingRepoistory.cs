using System.Collections.Generic;
namespace HotelManangementSystemLibrary
{
    public interface IOldBookingRepoistory : IGeneralCollection<IOldBooking>
    {
        IEnumerator<IOldBooking> GetBookingsOf(IGuest guest);
        IEnumerator<IOldBooking> GetBookingsOf(IRoom room);
        IEnumerator<IOldBooking> GetBookingsOf(CancellationReason state);
    }//class
}//namespace
