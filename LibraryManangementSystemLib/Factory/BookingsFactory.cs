using System;
namespace HotelManangementSystemLibrary.Factory
{
    public static class BookingsFactory
    {
        public static IRoomBooking CreateBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
        {
            IBookingFees fees = CreateBookingFee(date, numberOfDays * room.Price);
            return new RoomBooking(guest, room, date, fees,numberOfDays);
        }//CreateBooking
        internal static IRoomBooking CreateBookingWithFees(string id,IGuest guest, IRoom room, DateTime date,  IBookingFees fees,int numberOfDays)
        {
            return new RoomBooking(id, guest, room, date, fees, numberOfDays);
        }//CreateBookingWithFees
        public static IRoomBookings CreateBookings()
            => new RoomBookings();
        internal static IBookingFees CreateBookingFee(DateTime bookingdate, decimal bookingAmount)
        {
            return new BookingFees(bookingdate, bookingAmount);
        }//CreateBookingFee
        internal static IBookingFees CreateBookingFee(DateTime bookingdate, decimal bookingAmount,decimal amountPaid, decimal amountToPAY)
        {
            return new BookingFees(bookingdate, bookingAmount, amountToPAY, amountPaid);
        }//CreateBookingFee
    }//class
}//namesapce
