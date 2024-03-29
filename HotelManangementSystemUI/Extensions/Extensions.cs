using HotelManangementSystemLibrary;
using System.Collections.Generic;

namespace HotelManangementSystemUI.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<IRoomBooking> GetBookingsOf(this IRoomBookings bookings, string userid)
        {
            foreach (var item in bookings)
            {
                if (item.Guest.UserID == userid)
                    yield return item;
            }
        }//GetBookingsOf
    }//class
}//namesoaxe
