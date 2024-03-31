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
        int NumberOfDaysToStay { get; }
        int DaysStayed { get; set; }
        decimal AmountPaid { get; }
        bool IsCheckedIn { get; }
        decimal AmoutToPay { get; }
        decimal BookingCost { get; }
        void ChangeBookingDate(DateTime date, int numberOfDays = 1);
        void ChangeRoom(IRoom room);
        void CheckIn();
        bool PayForBooking(decimal amount,out decimal change);
        decimal GetRefundAmount();
        decimal GetCancellationFee();
    }//IRoomBooking
}//namespace
