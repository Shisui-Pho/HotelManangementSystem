using System;
namespace HotelManangementSystemLibrary
{
    public class TransactionArgs : EventArgs
    {
        private DateTime date;
        private BalanceAffected type;
        public string TypeOfTransaction { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date
        {
            get { return date; }
        }
        //A flag to let me know which amount it affects
        public BalanceAffected Affected => type;
        public TransactionArgs(string type, decimal amount, BalanceAffected ballance)
        {
            TypeOfTransaction = type;
            Amount = amount;
            date = DateTime.Now;
            this.type = ballance;
        }//ctor 01
        public override string ToString()
        {
            string sAmount = (Amount > 0) ? "+" + Amount.ToString("C2") : Amount.ToString("C2");
            return Date.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25) + sAmount.PadRight(15) + TypeOfTransaction +"(" +(Affected.ToString()[0].ToString()) + ")"; 
        }//ToString()
    }//class
}//namespace
