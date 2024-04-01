using System;
namespace HotelManangementSystemLibrary
{
    public class TransactionArgs : EventArgs
    {
        public string TypeOfTransaction { get; private set; }
        public decimal Amount { get; private set; }
        public bool Cancelled { get; set; }
        public TransactionArgs(string type, decimal amount)
        {
            TypeOfTransaction = type;
            Amount = amount;
            Cancelled = false;
        }//ctor 01
    }//class
}//namespace
