using HotelManangementSystemLibrary;
using System.Collections.Generic;

namespace HotelManangementControlLibrary.Utils
{
    public static class Extensions
    {
        public static IEnumerable<IRoomBooking> FindBookings(this IRoomBookings bookings,IRoom room)
        {
            foreach (IRoomBooking booking in bookings)
            {
                if (booking.Room.RoomNumber == room.RoomNumber)
                    yield return booking;
            }//end for each
        }//FindBookings
    }//class
}//namespace
