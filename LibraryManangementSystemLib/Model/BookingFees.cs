using System;
namespace HotelManangementSystemLibrary
{
    internal class BookingFees : IBookingFees
    {
        public static double RefundRate = 10d;
        private DateTime _bookingDate;
        public decimal BookingCost { get; private set; }
        public decimal AmoutToPay { get; private set; }
        public decimal AmountPaid { get; private set; }
        public BookingFees(DateTime bookingdate, decimal bookingAmount)
        {
            this._bookingDate = bookingdate;
            BookingCost = bookingAmount;
            AmoutToPay = bookingAmount;
            AmountPaid = 0m;
        }
        internal BookingFees(DateTime date, decimal cost, decimal amountoPay, decimal amountPaid)
        {
            this._bookingDate = date;
            this.BookingCost = cost;
            this.AmountPaid = amountPaid;
            this.AmoutToPay = amountoPay;
        }
        public bool PayForBooking(decimal amount, out decimal change)
        {
            change = 0;
            //if the amount is less thank the acceptable 
            if (amount < 0)
                return false;

            //Business rules applied
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
            //Get the refund amount after deducting the cancelation fee
            decimal amount = AmountPaid - GetCancellationFee();

            //If the user must pay the full amount/hasn't paid more that the cancelation fee
            if (amount <= 0)
                return 0m;

            //Return the refund
            return amount;
        }//GetRefundAmount

        public decimal GetCancellationFee()
        {
            DateTime maxDate = _bookingDate.AddDays(-2);
            //If the user decides to cancel in less than 48hours before the booked date
            //-The pay the full amount for cancellation fee
            if (DateTime.Now >= maxDate)
                return BookingCost;

            //Return the cancelation fee, in this case it is 10%
            return BookingCost * (decimal)(RefundRate / 100);
        }//GetCancellationFee
    }//class
}//namespace