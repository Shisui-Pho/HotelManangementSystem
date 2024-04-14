using System;
namespace HotelManangementSystemLibrary.Factory
{
    public static class BookingsFactory
    {
        public static IRoomBooking CreateBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
            => new RoomBooking(guest, room, date, numberOfDays);
        internal static IRoomBooking CreateBookingWithFees(string id,IGuest guest, IRoom room, DateTime date,  IBookingFees fees,int numberOfDays = 1)
        {
            RoomBooking booking = new RoomBooking(id,guest, room, date, numberOfDays);
            booking.SetBookingFees(fees);
            return booking;
        }
        public static IRoomBookings CreateBookings()
            => new RoomBookings();
    }//class
}//namesapce
