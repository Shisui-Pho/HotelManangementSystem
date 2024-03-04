using System;
using System.Collections;
using System.Collections.Generic;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBookings : ICollectionHotel<IRoomBooking>
    {
        bool IsBooked(IRoomBooking item);
        bool IsRoomBooked(IRoom booking, DateTime date);
        IEnumerator<IRoomBooking> GetBookingsOf(IGuest guest);
    }//Interface
}//namespace
