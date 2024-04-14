namespace HotelManangementSystemLibrary
{
    public interface IUSerAccount
    {

        decimal CurrentBalance { get; }
        decimal AmountOwing { get; }
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
}//namespace