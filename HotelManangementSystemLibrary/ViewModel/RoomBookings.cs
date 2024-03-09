using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    internal class RoomBookings : IRoomBookings
    {
        private List<IRoomBooking> _bookings;
        public int Count => _bookings.Count;

        public RoomBookings()
        {
            _bookings = new List<IRoomBooking>();
        }//ctor 01     
        public RoomBookings(List<IRoomBooking> bookings)
        {
            _bookings = bookings;
        }//ctor 02
        public RoomBookings(IRoomBooking[] bookings)
        {
            _bookings = new List<IRoomBooking>();
            _bookings.AddRange(bookings);
        }//ctor 03

        public bool IsBooked(IRoomBooking booking)
        {
            return FindIndex(booking) >= 0;
        }//IsBooked
        public bool IsRoomBooked(IRoom booking, DateTime date)
        {
            for (int i = 0; i < _bookings.Count; i++)
            {
                IRoom room = _bookings[i].Room;
                if (_bookings[i].DateBookedFor == date && room.RoomNumber == booking.RoomNumber)
                    return true;
            }
            return false;
        }//IsRoomBooked
        public void Remove(IRoomBooking booking)
        {
            int i = FindIndex(booking);
            if (i < 0)
                throw new ArgumentException("No such booking was made");
            _bookings.RemoveAt(i);
        }//Remove

        public void Update(IRoomBooking old, IRoomBooking _new)
        {
            int i = FindIndex(old);
            if (i < 0)
                throw new ArgumentException("No such booking was made");
            _bookings[i] = _new;
        }//Update

        void ICollectionHotel<IRoomBooking>.Add(IRoomBooking item)
        {
            _bookings.Add(item);
            //throw new NotImplementedException();
        }//ICollectionHotel<IRoomBooking>.Add
        public IEnumerator<IRoomBooking> GetEnumerator()
        {
            foreach (IRoomBooking booking in _bookings)
            {
                yield return booking;
            }
        }//GetEnumerator
        private int FindIndex(IRoomBooking booking)
            =>_bookings.FindIndex(_b => booking.BookingID == _b.BookingID);

        public IEnumerator<IRoomBooking> GetBookingsOf(IGuest guest)
        {
            foreach (IRoomBooking booking in _bookings)
            {
                if(booking.Guest.UserID == guest.UserID)
                    yield return booking;
            }//GetBookingsOf
        }//GetBookingsOf

        public IRoomBooking[] HasBookings(IRoom room)
        {
            return _bookings.Where(b => b.Room.RoomNumber == room.RoomNumber).ToArray();
        }//HasBookings
    }//class
}//namespace