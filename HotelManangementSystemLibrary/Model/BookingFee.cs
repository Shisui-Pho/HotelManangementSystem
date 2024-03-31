using System;
namespace HotelManangementSystemLibrary
{
    internal class BookingFee : IBookingFee
    {
        public static double RefundRate = 10d;
        private DateTime _bookingDate;
        public decimal BookingCost { get; private set; }
        public decimal AmoutToPay { get; private set; }
        public decimal AmountPaid { get; private set; }
        public BookingFee(DateTime bookingdate, decimal bookingAmount)
        {
            this._bookingDate = bookingdate;
            BookingCost = bookingAmount;
            AmoutToPay = bookingAmount;
            AmountPaid = 0m;
        }
        public bool PayForBooking(decimal amount, out decimal change)
        {
            change = 0;
            //if the amount is less thank the acceptable 
            if (amount < 0)
                return false;
            if (amount >= AmoutToPay)
            {
                change = amount - AmoutToPay;
                AmoutToPay = 0m;
                AmountPaid = BookingCost;
            }
            else
            {
                AmoutToPay -= amount;
                AmountPaid += amount;
            }

            return true;
        }//PayForBooking

        public decimal GetRefundAmount()
        {
            decimal amount = AmountPaid - GetCancellationFee();

            if (amount <= 0)
                return 0m;

            return amount;
        }//GetRefundAmount

        public decimal GetCancellationFee()
        {
            if (DateTime.Now.AddDays(-2) <= _bookingDate)
                return BookingCost;

            return BookingCost * (decimal)(BookingCost / 100);
        }//GetCancellationFee
    }//class
}//namespace