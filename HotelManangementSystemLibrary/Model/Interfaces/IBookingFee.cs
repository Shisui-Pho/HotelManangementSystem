using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IBookingFee
    {
        decimal AmountPaid { get; }
        decimal AmoutToPay { get; }
        decimal BookingCost { get; }
        bool PayForBooking(decimal amount, out decimal change);
        decimal GetRefundAmount();
        decimal GetCancellationFee();
    }//interface IBookingFee
}//namespace
