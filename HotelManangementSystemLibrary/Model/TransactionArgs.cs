using System;
namespace HotelManangementSystemLibrary
{
    public class TransactionArgs : EventArgs
    {
        private DateTime date;
        public string TypeOfTransaction { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date
        {
            get { return date; }
        }
        public bool Cancelled { get; set; }
        public TransactionArgs(string type, decimal amount)
        {
            TypeOfTransaction = type;
            Amount = amount;
            Cancelled = false;
            date = DateTime.Now;
        }//ctor 01
    }//class
}//namespace
