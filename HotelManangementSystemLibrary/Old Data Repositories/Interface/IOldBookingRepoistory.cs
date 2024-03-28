using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IOldBookingRepoistory : IGeneralCollection<IOldBooking>
    {
        IEnumerator<IOldBooking> GetBookingsOf(IGuest guest);
        IEnumerator<IOldBooking> GetBookingsOf(IRoom room);
        IEnumerator<IOldBooking> GetBookingsOf(BookingState state);
    }//class
}//namespace
