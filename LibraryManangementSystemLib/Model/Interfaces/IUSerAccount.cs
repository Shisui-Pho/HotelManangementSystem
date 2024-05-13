namespace HotelManangementSystemLibrary
{
    public interface IUSerAccount
    {

        decimal CurrentBalance { get; }
        decimal AmountOwing { get; }
        string AccountNumber { get; }
        event delOnBalanceChanged BalanceChanged;
        event delOnTrasnaction OnTransactionEvent;
        void AddDept(decimal amount, string reason);
        bool DepositAmount(decimal amount);
        bool DepositAmount(decimal amount, string sMsg);
        decimal PayAllDepts();
        decimal PayForBooking(decimal amount);
        bool WithdrawAmount(decimal amount);
        void CancelBooking(IBookingFees bookingFee);
    }//IUserAccount
    internal interface IUserAccountDB
    {
        /// <summary>
        /// Adds a dept read from the database
        /// </summary>
        /// <param name="amount">The dept amount.</param>
        /// <param name="reason">The reason.</param>
        void AddDeptRemaining(decimal amount, string reason);
    }//IUserAccountDB
}//namespace