using System;
namespace HotelManangementSystemLibrary
{
    internal class UserAccount : IUSerAccount
    {
        public decimal CurrentBalance { get; private set; }

        public UserAccount(decimal initialAamount = 0m)
        {
            CurrentBalance = initialAamount;
        }//ctor 01
        public bool DepositAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            CurrentBalance += amount;
            return true;
        }//Deposit amount

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The total amount in the current account.</returns>
        public decimal PayForBooking()
        {
            decimal temp = CurrentBalance;
            CurrentBalance = 0m;
            return temp;
        }//PayForBooking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">The amount to be withdrawn</param>
        /// <returns>The specified amount in the current account provided that the user has that amount.</returns>
        public decimal PayForBooking(decimal amount)
        {
            if (amount < 0)
                return 0m;
            if (amount > CurrentBalance)
                return 0m;
            CurrentBalance -= amount;
            return amount;
        }//PayForBooking

        public bool WithdrawAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            if (amount > CurrentBalance)
                return false;
            CurrentBalance -= amount; 
            return true;
        }//WithdrawAmount
    }//class
}//namespace