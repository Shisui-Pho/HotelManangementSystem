using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IBookingFees
    {
        event delOnBookingFeesChanged BookingFeesChanged;
        decimal AmountPaid { get; }
        decimal AmoutToPay { get; }
        decimal BookingCost { get; }
        bool PayForBooking(decimal amount, out decimal change);
        decimal GetRefundAmount();
        decimal GetCancellationFee();
    }//interface IBookingFee
}//namespace
