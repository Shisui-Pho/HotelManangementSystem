using System;
using System.Collections;
using System.Collections.Generic;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBookings : IGeneralCollection<IRoomBooking>
    {
        bool IsBooked(IRoomBooking item);
        bool IsRoomBooked(IRoom booking, DateTime date);
        IRoomBooking[] HasBookings(IRoom room);
        IEnumerator<IRoomBooking> GetBookingsOf(IGuest guest);
    }//Interface
}//namespace
