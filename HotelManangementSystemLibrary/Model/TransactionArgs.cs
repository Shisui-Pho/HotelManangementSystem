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
        public override string ToString()
        {
            string sAmount = (Amount > 0) ? "+" + Amount.ToString("C2") : Amount.ToString("C2");
            return Date.ToString("dd/MMMM/yyyy HH:mm:ss").PadRight(25) + sAmount.PadRight(20) + TypeOfTransaction; 
        }//ToString()
    }//class
}//namespace
