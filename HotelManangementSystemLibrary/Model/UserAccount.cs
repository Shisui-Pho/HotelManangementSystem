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
            TransactionArgs args = new TransactionArgs("Deposited", amount, BalanceAffected.CurrentBalance);
            CurrentBalance += amount;
            OnTransactionEvent?.Invoke(args);
            return true;
        }//Deposit amount

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The total amount in the current account.</returns>
        public decimal PayForBooking()
        {
            decimal temp = CurrentBalance;
            TransactionArgs args = new TransactionArgs("Payed for booking", (-1) *temp, BalanceAffected.CurrentBalance);
            CurrentBalance = 0m;
            OnTransactionEvent?.Invoke(args);
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
            if (amount > CurrentBalance || amount > AmountOwing)
                return 0m;

            TransactionArgs args = new TransactionArgs("Payed booking", (-1) * amount, BalanceAffected.CurrentBalance);
            CurrentBalance -= amount;
            AmountOwing -= amount;
            OnTransactionEvent?.Invoke(args);
            return amount;
        }//PayForBooking

        public bool WithdrawAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            if (amount > CurrentBalance)
                return false;

            TransactionArgs args = new TransactionArgs("Withdraw amount", (-1) * amount, BalanceAffected.CurrentBalance);
            CurrentBalance -= amount;
            OnTransactionEvent?.Invoke(args);
            return true;      
        }//WithdrawAmount
        public void AddDept(decimal amount, string reason)
        {
            if (amount < 0)
                return;
            TransactionArgs args = new TransactionArgs(reason, amount, BalanceAffected.DeptBalance);
            AmountOwing += amount;
            OnTransactionEvent?.Invoke(args);
        }//decimal void
        private void ReverseAmount(decimal amount)
        {
            if (amount <= 0)
                return;
            TransactionArgs args = new TransactionArgs("Reversed", (-1)*amount, BalanceAffected.DeptBalance);
            //AmountOwing -= amount;
            OnTransactionEvent?.Invoke(args);
            DepositAmount(amount);
        }//ReverseAmount
        public void CancelBooking(IBookingFees bookingFees)
        {
            //For now to am going to keep it simple.
            //-This will however change as time goes by.
            //decimal amountPaid = bookingFee.AmountPaid;
            decimal mCancel = bookingFees.GetCancellationFee();
            decimal mRefund = bookingFees.GetRefundAmount();
            TransactionArgs args = new TransactionArgs("Booking cancelled", (-1)* bookingFees.AmoutToPay, BalanceAffected.DeptBalance);
            AmountOwing -= bookingFees.AmoutToPay;
            OnTransactionEvent?.Invoke(args);
            ReverseAmount(mRefund);
            AmountOwing += mCancel;
            args = new TransactionArgs("Cancelation fee", bookingFees.GetCancellationFee(), BalanceAffected.DeptBalance);
            OnTransactionEvent?.Invoke(args);
        }//CancelBooking
    }//class
}//namespace