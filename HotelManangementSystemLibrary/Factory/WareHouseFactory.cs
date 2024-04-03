using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.Factory
{
    public static class WareHouseFactory
    {
        public static IOldBooking PrepareMove(IRoomBooking booking, CancellationReason state)
            => new OldBooking(booking, state);
        public static IOldBooking PrepareMove(IRoomBooking booking, string state)
            => new OldBooking(booking, state);
        public static IOldBookingRepoistory WareHouse()
            => OldBookingsRepository.GetRepoistoryInstance();
    }//WareHouseFactory
}//namespace
