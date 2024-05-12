using System;

namespace HotelManangementSystemLibrary
{
    public class BalanceChangedEventArgs : EventArgs
    {
        public decimal CurrentBalance { get; private set; }
        public decimal AmountOwing { get; private set; }
        public string AccountUserID { get; private set; }
        public BalanceChangedEventArgs(decimal current,decimal amountowing, string userid)
        {
            this.CurrentBalance = current;
            this.AmountOwing = amountowing;
            this.AccountUserID = userid;
        }//ctor 01
    }//BalanceChangedEvenArgs
}//namespace
