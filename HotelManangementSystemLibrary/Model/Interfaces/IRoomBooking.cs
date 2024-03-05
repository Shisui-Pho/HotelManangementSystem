using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBooking
    {
        string BookingID { get; }
        IGuest Guest { get; }
        IRoom Room { get; }
        DateTime DateBookedFor { get; }
        int NumberOfDaysToStay { get; }
        decimal AmoutToPay { get; }
        void ChangeBooking(DateTime date, int numberOfDays = 1);
    }//IRoomBooking
}//namespace
