using HotelManangementSystemLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class OldBookingsRepository:GeneralCollection<IOldBooking>, IOldBookingRepoistory, IGeneralCollection<IOldBooking>
    {
        private static readonly IOldBookingRepoistory repository = new OldBookingsRepository();
        private OldBookingsRepository() : base()
        {
            //No need to load data
        }
        ~OldBookingsRepository()
        {
            IOldBookingsRepositoryFile.Save(_collection);
        }//destructor
        public static IOldBookingRepoistory GetRepoistoryInstance() => repository;
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

        public IEnumerator<IOldBooking> GetBookingsOf(CancellationReason state)
        {
            foreach (IOldBooking booking in base._collection)
            {
                if (booking.State == state)
                    yield return booking;
            }//end foreach
        }//GetBookingsOf
    }//class
}//namespace
