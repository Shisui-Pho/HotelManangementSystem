using HotelManangementSystemLibrary;
using System.Collections.Generic;

namespace HotelManangementControlLibrary.Utils
{
    public static class Extensions
    {
        public static IEnumerable<IRoomBooking> FindBookings(this IRoomBookings bookings,IRoom room)
        {
            foreach (IRoomBooking booking in bookings)
            {
                if (booking.Room.RoomNumber == room.RoomNumber)
                    yield return booking;
            }//end for each
        }//FindBookings
        public static IEnumerable<IGuest> GetGuests(this IGuests guests,bool isFirstName,string name)
        {
            foreach (IGuest guest in guests)
            {
                if (isFirstName && guest.Name.StartsWith(name))
                    yield return guest;
                if (!isFirstName && guest.Surname.StartsWith(name))
                    yield return guest;
            }//End forearch
        }//GetGuests
        public static IEnumerable<IGuest> GetGuests(this IGuests guests,string fname, string lname)
        {
            foreach (IGuest guest in guests)
            {
                if (guest.Name.StartsWith(fname) && guest.Surname.StartsWith(lname))
                    yield return guest;
            }
        }//GetGuests
    }//class
}//namespace
