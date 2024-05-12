using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManangementSystemLibrary
{
    internal class RoomBookings: GeneralCollection<IRoomBooking>, IRoomBookings, IRoomBookingDB
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
        public override void Add(IRoomBooking item)
        { 
            if (FindIndex(item) >= 0)
            {
                //This is temporary
                item.ChangeBookingDate(item.DateBookedFor.AddDays(5), item.NumberOfDaysToStay);
            }
           item.Guest.Account.AddDept(item.BookingFee.BookingCost, "Booked room.");
                
            base._collection.Add(item);
        }//ICollectionHotel<IRoomBooking>.Add

        public void AddFromDB(IRoomBooking item)
        {
            //This will let us know that it is comming from the database and how much the user is owing
            IUserAccountDB account = (IUserAccountDB)item.Guest.Account;
            //item.Guest.Account.AddDept(item.BookingFee.AmoutToPay, "Booked room");
            account.AddDeptRemaining(item.BookingFee.AmoutToPay, "Booked room.");
            base._collection.Add(item);
        }//AddFromDB
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
        public virtual void CancelBooking(IRoomBooking booking, CancellationReason reason)
        {
            RemovedBooking?.Invoke(booking, new HotelEventArgs(booking.BookingID,reason.ToString()) { IsHandled = false });
            booking.Guest.Account.CancelBooking(booking.BookingFee);
            base.Remove(booking);
        }//CancelBooking        
        public virtual void CancelBooking(IRoomBooking booking, string reason)
        {
            RemovedBooking?.Invoke(booking, new HotelEventArgs(booking.BookingID,reason) { IsHandled = false });
            booking.Guest.Account.CancelBooking(booking.BookingFee);
            base.Remove(booking);
        }//CancelBooking
    }//class
}//namespace