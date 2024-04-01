namespace HotelManangementSystemLibrary
{
    public interface IUSerAccount
    {

        decimal CurrentBalance { get; }
        decimal AmountOwing { get; }
        event delOnTrasnaction OnTransactionEvent;
        void AddDept(decimal amount, string reason);
        bool DepositAmount(decimal amount);
        decimal PayForBooking();
        decimal PayForBooking(decimal amount);
        bool WithdrawAmount(decimal amount);
    }//IUserAccount
}//namespace