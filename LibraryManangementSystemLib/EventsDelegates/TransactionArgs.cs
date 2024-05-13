using System;
namespace HotelManangementSystemLibrary
{
    public class TransactionArgs : EventArgs
    {
        public string Message { get; private set; }
        public decimal Amount { get; private set; }
        public BalanceAffected AffectedBalance { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string AccountNumber { get; private set; }
        public TransactionArgs(string message, decimal amount, BalanceAffected ballance,string accountNumber)
        {
            Message = message;
            Amount = amount;
            TimeStamp = DateTime.Now;
            AffectedBalance = ballance;
        }//ctor 01
        public override string ToString()
        {
            string sAmount = (Amount >= 0) ? "+" + Amount.ToString("C2") : Amount.ToString("C2");
            return TimeStamp.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25) + sAmount.PadRight(15) + Message +"(" +(AffectedBalance.ToString()[0].ToString()) + ")"; 
        }//ToString()
    }//class
}//namespace
