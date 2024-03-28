using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class OldBookingsRepository:GeneralCollection<IOldBooking>, IOldBookingRepoistory, IGeneralCollection<IOldBooking>
    {
        public OldBookingsRepository() : base()
        {
        }
        public IEnumerator<IOldBooking> GetBookingsOf(IGuest guest)
        {
            foreach (IOldBooking booking in base._collection)
            {
                if (booking.Booking.Guest.UserID == guest.UserID)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf

        public IEnumerator<IOldBooking> GetBookingsOf(IRoom room)
        {
            foreach (IOldBooking booking in base._collection)
            {
                if (booking.Booking.Room.RoomNumber == room.RoomNumber)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf

        public IEnumerator<IOldBooking> GetBookingsOf(BookingState state)
        {
            foreach (IOldBooking booking in base._collection)
            {
                if (booking.State == state)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf
    }//class
}//namespace
