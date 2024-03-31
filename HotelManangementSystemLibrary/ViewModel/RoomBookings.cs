using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    internal class RoomBookings: GeneralCollection<IRoomBooking>, IRoomBookings
    {

        public event delOnRemovedEvent RemovedBooking;

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
        void IGeneralCollection<IRoomBooking>.Add(IRoomBooking item)
        { 
            if (FindIndex(item) >= 0)
            {
                //For temp
                item.ChangeBookingDate(item.DateBookedFor.AddDays(5), item.NumberOfDaysToStay);
            }
                //throw new ArgumentException("Room has already been booked for that date.");
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
        public IEnumerable<IRoomBooking> GetBookingsOf(IGuest guest)
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

        public IEnumerable<IRoom> GetBookedRooms<T>()
            where T : IRoom
        {
            return _collection.Select(a => a.Room);
            //return _collection.Distinct()
            //                  .Where(s => s.Room is T)
            //                  .Select(s => s.Room);
        }//GetBookedRooms
        public new void Remove(IRoomBooking booking)
        {
            
            //base.Remove(booking);
        }//Remove

        public void CancelBooking(IRoomBooking booking, CancellationReason reason)
        {
            RemovedBooking?.Invoke(booking, new HotelEventArgs(booking.BookingID,reason.ToString()) { IsHandled = false });
            base.Remove(booking);
        }//CancelBooking        
        public void CancelBooking(IRoomBooking booking, string reason)
        {
            RemovedBooking?.Invoke(booking, new HotelEventArgs(booking.BookingID,reason.ToString()) { IsHandled = false });
            base.Remove(booking);
        }//CancelBooking
    }//class
}//namespace