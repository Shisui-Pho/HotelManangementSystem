using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBooking : IHotelModel
    {
        string BookingID { get; }
        IGuest Guest { get; }
        IRoom Room { get; }
        DateTime DateBookedFor { get; }
        IBookingFee BookingFee { get; }
        int NumberOfDaysToStay { get; }
        int DaysStayed { get; set; }
        bool IsCheckedIn { get; }
        void ChangeBookingDate(DateTime date, int numberOfDays = 1);
        void ChangeRoom(IRoom room);
        void CheckIn();
    }//IRoomBooking
}//namespace
