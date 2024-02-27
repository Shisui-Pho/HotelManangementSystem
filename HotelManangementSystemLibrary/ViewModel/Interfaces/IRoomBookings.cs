using System;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBookings : ICollectionHotel<IRoomBooking>
    {
        bool IsBooked(IRoomBooking item);
        bool IsRoomBooked(IRoom booking, DateTime date);
    }//Interface
}//namespace
