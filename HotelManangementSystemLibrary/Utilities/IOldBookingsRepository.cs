using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HotelManangementSystemLibrary.Utilities
{
    public static class IOldBookingsRepositoryFile
    {
        private static string file = "oldbookings.csv";
        public static void Save<T>(List<T> collection)
            where T : IOldBooking
        {
            if (collection is null)
                return;
            StringBuilder bl = new StringBuilder();
            using (StreamWriter wr = new StreamWriter(file, true))
            {
                foreach (T booking in collection)
                {
                    if (booking == null)
                        continue;
                    string state = (booking.State == null) ? booking.StateString : booking.State.ToString();
                    bl.AppendLine(string.Format($"{booking.Booking.BookingID},{booking.Booking.Guest.UserID},{booking.Booking.Room.RoomNumber},{booking.Booking.DateBookedFor.ToString("dd/MM/yyyy")}" +
                        $",{booking.Booking.NumberOfDaysToStay.ToString()},{booking.Booking.IsCheckedIn},{booking.Booking.DaysStayed},{state}"));
                }
                wr.WriteLine(bl.ToString());
            }
        }
    }//class
}//namespace
