using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    internal class RoomBookings: GeneralCollection<IRoomBooking>, IRoomBookings
    {
        public RoomBookings():base()
        {
        }//ctor 01     
        public RoomBookings(List<IRoomBooking> bookings):base(bookings)
        {
        }//ctor 02
        public RoomBookings(IRoomBooking[] bookings):base(bookings)
        {
        }//ctor 03

        public bool IsBooked(IRoomBooking booking)
        {
            return FindIndex(booking) >= 0;
        }//IsBooked
        public bool IsRoomBooked(IRoom booking, DateTime date)
        {
            for (int i = 0; i < base._collection.Count; i++)
            {
                IRoom room = base._collection[i].Room;
                if (base._collection[i].DateBookedFor == date && room.RoomNumber == booking.RoomNumber)
                    return true;
            }
            return false;
        }//IsRoomBooked
        void ICollectionHotel<IRoomBooking>.Add(IRoomBooking item)
        { 
            if (FindIndex(item) >= 0)
                throw new ArgumentException("Room has already been booked for that date.");
            base._collection.Add(item);
        }//ICollectionHotel<IRoomBooking>.Add
        private int FindIndex(IRoomBooking booking)
        {
            int i = base._collection.FindIndex(_b => booking.BookingID == _b.BookingID);
            if (i >= 0)
                return i;
            i = base._collection.FindIndex(_b => _b.DateBookedFor == booking.DateBookedFor
                                    && _b.Room.RoomNumber == booking.Room.RoomNumber);
            return i;
        }//
        public IEnumerator<IRoomBooking> GetBookingsOf(IGuest guest)
        {
            foreach (IRoomBooking booking in base._collection)
            {
                if(booking.Guest.UserID == guest.UserID)
                    yield return booking;
            }//GetBookingsOf
        }//GetBookingsOf

        public IRoomBooking[] HasBookings(IRoom room)
        {
            return base._collection.Where(b => b.Room.RoomNumber == room.RoomNumber).ToArray();
        }//HasBookings
    }//class
}//namespace