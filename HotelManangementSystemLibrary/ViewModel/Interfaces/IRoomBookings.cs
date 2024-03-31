using System;
using System.Collections;
using System.Collections.Generic;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBookings : IGeneralCollection<IRoomBooking>
    {
        event delOnRemovedEvent RemovedBooking;
        void CancelBooking(IRoomBooking booking, CancellationReason reason);
        void CancelBooking(IRoomBooking booking, string reason);
        bool IsBooked(IRoomBooking item);
        bool IsRoomBooked(IRoom booking, DateTime date);
        IRoomBooking[] HasBookings(IRoom room);
        IEnumerable<IRoom> GetBookedRooms<T>()
            where T : IRoom;
    }//Interface
}//namespace
