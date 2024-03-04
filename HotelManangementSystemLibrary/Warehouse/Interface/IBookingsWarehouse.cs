using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IBookingsWarehouse
    {
        void Add(IOldBooking booking);
        IEnumerator<IOldBooking> GetBookingsOf(IGuest guest);
        IEnumerator<IOldBooking> GetBookingsOf(IRoom room);
        IEnumerator<IOldBooking> GetBookingsOf(BookingState state);
        IEnumerator<IOldBooking> GetEnumerator();
    }//class
}//namespace
