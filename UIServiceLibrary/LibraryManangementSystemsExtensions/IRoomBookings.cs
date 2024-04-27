using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIServiceLibrary.Extensions
{
    public static class Extensions
    {
        public static IRooms GetBookedRooms(this IRoomBookings bookings)
        {
            return RoomFactory.CreateRooms(bookings.GetBookedRooms<IRoom>().ToList().Distinct().ToList());
        }
    }
}
