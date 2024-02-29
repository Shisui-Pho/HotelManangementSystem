using System;
namespace HotelManangementSystemLibrary.Factory
{
    public static class BookingsFactory
    {
        public static IRoomBooking CreateBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
            => new RoomBooking(guest, room, date, numberOfDays);
        public static IRoomBookings CreateBookings()
            => new RoomBookings();
    }//class
}//namesapce
