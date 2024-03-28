using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.Factory
{
    public static class WareHouseFactory
    {
        public static IOldBooking PrepareMove(IRoomBooking booking, BookingState state)
            => new OldBooking(booking, state);
        public static IOldBookingRepoistory WareHouse()
            => new OldBookingsRepository();
    }//WareHouseFactory
}//namespace
