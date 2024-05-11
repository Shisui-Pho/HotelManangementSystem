using HotelManangementSystemLibrary.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
namespace HotelManangementSystemLibrary
{
    internal class RoomBookedDates : IRoomBookedDate
    {
        private readonly List<DateTime> BookedDates;

        public string RoomNumber { get; internal set; }

        public bool HasBookings => BookedDates.Count > 0;

        public RoomBookedDates()
        {
            BookedDates = new List<DateTime>();
        }//ctor main

        public bool AddBookingDate(DateTime date, int duration)
        {
            if (BookedDates.Contains(date))
            {
                ExceptionLog.Exception("Cannot booke room on this date");
                return false;
            }
            int count = 0;
            while(count <= duration)
            {
                DateTime nextdate = date.AddDays(count);
                if (BookedDates.FindIndex(d => d.Day == nextdate.Day && d.Year == nextdate.Year && d.Month == nextdate.Month) >= 0)
                {
                    ExceptionLog.Exception($"Room has been booked for the date :  {nextdate.ToShortDateString()}.");
                    return false;
                }
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
                {
                    var ex =  new KeyNotFoundException($"The specified date range was now found : Date {date.AddDays(duration).ToShortDateString()}");
                    bool handled = ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.BusinessRuleBridge);
                    if (!handled)
                        throw ex;
                    return false;
                }
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
