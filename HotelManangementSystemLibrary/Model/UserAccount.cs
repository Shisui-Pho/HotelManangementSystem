using System;
namespace HotelManangementSystemLibrary
{
    public delegate void delOnTrasnaction(TransactionArgs transaction);
    internal class UserAccount : IUSerAccount
    {
        public decimal CurrentBalance { get; private set; }
        public decimal AmountOwing { get; private set; }
        public event delOnTrasnaction OnTransactionEvent;
        public UserAccount(decimal initialAamount = 0m)
        {
            CurrentBalance = initialAamount;
        }//ctor 01
        public bool DepositAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            TransactionArgs args = new TransactionArgs("Deposited", amount);
            OnTransactionEvent?.Invoke(args);
            if(!args.Cancelled)
            {
                CurrentBalance += amount;
                return true;
            }
            return false;
        }//Deposit amount

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The total amount in the current account.</returns>
        public decimal PayForBooking()
        {
            decimal temp = CurrentBalance;
            TransactionArgs args = new TransactionArgs("Payed for booking", (-1) *temp);
            OnTransactionEvent?.Invoke(args);
            if (!args.Cancelled)
            {
                CurrentBalance = 0m;
                return temp;
            }
            return 0;
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

            TransactionArgs args = new TransactionArgs("Payed booking", (-1) * amount);
            OnTransactionEvent?.Invoke(args);
            if (!args.Cancelled)
            {
                CurrentBalance -= amount;
                return amount;
            }
            return 0m;
        }//PayForBooking

        public bool WithdrawAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            if (amount > CurrentBalance)
                return false;

            TransactionArgs args = new TransactionArgs("Withdraw amount", (-1) * amount);
            OnTransactionEvent?.Invoke(args);
            if (!args.Cancelled)
            {
                CurrentBalance -= amount;
                return true;
            }
            return false;           
        }//WithdrawAmount
        public void AddDept(decimal amount, string reason)
        {
            if (amount < 0)
                return;
            TransactionArgs args = new TransactionArgs(reason, amount);
            OnTransactionEvent?.Invoke(args);
            if (!args.Cancelled)
            {
                AmountOwing += amount;
            }
        }//decimal void
        private void ReverseAmount(decimal amount)
        {
            TransactionArgs args = new TransactionArgs("Reversed", amount);
            OnTransactionEvent?.Invoke(args);
            if (!args.Cancelled)
            {
                AmountOwing -= amount;
                CurrentBalance += amount;
            }
        }//ReverseAmount
        public void CancelBooking(IBookingFees bookingFees)
        {
            //For now to am going to keep it simple.
            //-This will however change as time goes by.
            //decimal amountPaid = bookingFee.AmountPaid;
            ReverseAmount(bookingFees.GetRefundAmount());
            TransactionArgs args = new TransactionArgs("Cancelled Booking", (-1)*bookingFees.GetCancellationFee());
            OnTransactionEvent?.Invoke(args);
        }//CancelBooking
    }//class
}//namespace