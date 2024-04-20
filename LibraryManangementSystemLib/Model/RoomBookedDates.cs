using System;
using System.Collections;
using System.Collections.Generic;
namespace HotelManangementSystemLibrary
{
    internal class RoomBookedDates : IRoomBookedDate
    {
        private readonly List<DateTime> BookedDates;
        public RoomBookedDates()
        {
            BookedDates = new List<DateTime>();
        }//ctor main
        public bool AddBookingDate(DateTime date, int duration)
        {
            if (BookedDates.Contains(date))
                throw new ArgumentException("Cannot booke room on this date");
            int count = 0;
            while(count <= duration)
            {
                DateTime nextdate = date.AddDays(count);
                if (BookedDates.Contains(nextdate))
                    throw new ArgumentException($"Room has been booked for {nextdate.ToShortDateString()}.");
                BookedDates.Add(nextdate);
                count++;
            }//end while
            return true;
        }//AddBookingDate
        public bool ChangeBookingDate(DateTime olddate, int oldduration, DateTime newdate, int newduaration)
        {
            if (!RemoveBookings(olddate, oldduration))
                return false;

            if (!AddBookingDate(newdate, newduaration))
                return false;
            return true;
        }//ChangeBookingDate

        public IEnumerator<DateTime> GetEnumerator()
        {
            return BookedDates.GetEnumerator();
        }//GetEnumerator

        public bool RemoveBookings(DateTime date,int duration)
        {
            while(duration >= 0)
            {
                bool isRemoved = BookedDates.Remove(date.AddDays(duration));
                if (!isRemoved)
                    throw new KeyNotFoundException($"The specified date range was now found : Date {date.AddDays(duration).ToShortDateString()}");
                duration--;
            }//end while
            return true;
        }//RemoveBookings

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }//class
}//naespace
