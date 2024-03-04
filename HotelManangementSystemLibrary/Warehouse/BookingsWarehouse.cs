using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class BookingsWarehouse : IBookingsWarehouse
    {
        private List<IOldBooking> _bookings;
        public BookingsWarehouse()
        {
            _bookings = new List<IOldBooking>();
        }//
        public void Add(IOldBooking booking)
        {
            _bookings.Add(booking);
        }//Add
        public IEnumerator<IOldBooking> GetBookingsOf(IGuest guest)
        {
            foreach (IOldBooking booking in _bookings)
            {
                if (booking.Booking.Guest.UserID == guest.UserID)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf

        public IEnumerator<IOldBooking> GetBookingsOf(IRoom room)
        {
            foreach (IOldBooking booking in _bookings)
            {
                if (booking.Booking.Room.RoomNumber == room.RoomNumber)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf

        public IEnumerator<IOldBooking> GetBookingsOf(BookingState state)
        {
            foreach (IOldBooking booking in _bookings)
            {
                if (booking.State == state)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf

        public IEnumerator<IOldBooking> GetEnumerator()
        {
            foreach (IOldBooking booking in _bookings)
            {
                yield return booking;
            }
        }//GetEnumerator
    }//class
}//namespace
