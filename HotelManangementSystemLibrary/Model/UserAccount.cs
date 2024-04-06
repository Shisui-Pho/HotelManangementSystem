namespace HotelManangementSystemLibrary
{
    public delegate void delOnTrasnaction(TransactionArgs transaction);
    public delegate void delOnBalanceChanged(decimal newBalance, decimal newAmountOwing);
    internal class UserAccount : IUSerAccount
    {
        public decimal CurrentBalance { get; private set; }
        public decimal AmountOwing { get; private set; }
        public event delOnTrasnaction OnTransactionEvent;
        public event delOnBalanceChanged BalanceChanged;

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
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
            return true;
        }//Deposit amount
        public bool DepositAmount(decimal amount, string sMsg)
        {
            if (amount < 0)
                return false;
            TransactionArgs args = new TransactionArgs(sMsg, amount, BalanceAffected.CurrentBalance);
            CurrentBalance += amount;
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
            return true;
        }//Deposit amount

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The total amount in the current account.</returns>
        public decimal PayAllDepts()
        {
            decimal temp = CurrentBalance;
            TransactionArgs args = new TransactionArgs("Payed for dept", (-1) *temp, BalanceAffected.CurrentBalance);
            CurrentBalance = 0m;
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
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
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
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
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
            return true;      
        }//WithdrawAmount
        public void AddDept(decimal amount, string reason)
        {
            //Prevent unnecessary transactions
            if (amount < 0)
                return;
            
            //Create a transaction for adding a dept
            TransactionArgs args = new TransactionArgs(reason, amount, BalanceAffected.DeptBalance);
            AmountOwing += amount;

            //Let the user know abou this transaction
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
        }//decimal void
        private void ReverseAmount(decimal amount)
        {
            //Prevent unnecessary transactions
            if (amount <= 0)
                return;

            //Reverse the amount
            DepositAmount(amount);
        }//ReverseAmount
        public void CancelBooking(IBookingFees bookingFees)
        {
            //For now to am going to keep it simple.
            //-This will however change as time goes by.

            //Get  the amounts for cancelation and refund
            decimal mCancel = bookingFees.GetCancellationFee();
            decimal mRefund = bookingFees.GetRefundAmount();
            
            //Create a transaction for cancelling
            TransactionArgs args = new TransactionArgs("Void", (-1)* bookingFees.AmoutToPay, BalanceAffected.DeptBalance);
            
            //Deduct the amount that has not been pay yet from the dept
            AmountOwing -= bookingFees.AmoutToPay;
            //Alert the user about this transaction
            OnTransactionEvent?.Invoke(args);

            //Refund the user
            ReverseAmount(mRefund);

            //Add the cancelation fee to the dept of the user balance
            AmountOwing += mCancel;
            //Alert the user about this transaction
            args = new TransactionArgs("Cancelation fee", bookingFees.GetCancellationFee(), BalanceAffected.DeptBalance);
            OnTransactionEvent?.Invoke(args);

            //Invoke the balance change for all listerners
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
        }//CancelBooking
    }//class
}//namespace