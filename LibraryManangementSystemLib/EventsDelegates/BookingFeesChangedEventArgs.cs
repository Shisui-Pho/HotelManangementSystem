using System;

namespace HotelManangementSystemLibrary
{
    public class BookingFeesChangedEventArgs : EventArgs
    {
        public decimal AmountPaid { get; private set; }
        public decimal AmountToPay { get; private set; }
        public string BookingID { get; internal set; }
        public BookingFeesChangedEventArgs(decimal paid,decimal topay,string id)
        {
            this.AmountPaid = paid;
            this.AmountToPay = topay;
            this.BookingID = id;
        }//ctor 
    }//BookingFeesChangedEventArgs
}//namespace
