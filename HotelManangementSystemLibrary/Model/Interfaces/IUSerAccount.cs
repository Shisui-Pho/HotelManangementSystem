namespace HotelManangementSystemLibrary
{
    public interface IUSerAccount
    {
        decimal CurrentBalance { get; }
        bool DepositAmount(decimal amount);
        decimal PayForBooking();
        decimal PayForBooking(decimal amount);
        bool WithdrawAmount(decimal amount);
    }//
}//namespace