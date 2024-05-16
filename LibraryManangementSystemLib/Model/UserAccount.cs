namespace HotelManangementSystemLibrary
{ 
    internal class UserAccount : IUSerAccount, IUserAccountDB
    {
        private static int CountAccounts;
        public decimal CurrentBalance { get; private set; }
        public decimal AmountOwing { get; private set; }

        public string AccountNumber { get; private set; }

        public event delOnTrasnaction OnTransactionEvent;
        public event delOnBalanceChanged BalanceChanged;

        public UserAccount(decimal initialAamount = 0m)
        {
            CurrentBalance = initialAamount;
            this.AccountNumber = CountAccounts.ToString();
            CountAccounts++;
        }//ctor default
        internal UserAccount(decimal balance, decimal amountOwing, string accountNumber)
        {
            this.AccountNumber = accountNumber;
            this.CurrentBalance = balance;
            this.AmountOwing = amountOwing;
            CountAccounts++;
        }//ctor 01
        public bool DepositAmount(decimal amount)
        {
            if (amount < 0)
                return false;
            TransactionArgs args = new TransactionArgs("Deposited", amount, BalanceAffected.CurrentBalance, this.AccountNumber);
            CurrentBalance += amount;
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
            return true;
        }//Deposit amount
        public bool DepositAmount(decimal amount, string sMsg)
        {
            if (amount < 0)
                return false;
            TransactionArgs args = new TransactionArgs(sMsg, amount, BalanceAffected.CurrentBalance, this.AccountNumber);
            CurrentBalance += amount;
            if (amount == 0)
                args.CanPushToDatabase = false;
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
            TransactionArgs args = new TransactionArgs("Payed for dept", (-1) *temp, BalanceAffected.CurrentBalance, this.AccountNumber);
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
            if (amount > CurrentBalance)
                return 0m;

            TransactionArgs args = new TransactionArgs("Payed booking", (-1) * amount, BalanceAffected.CurrentBalance, this.AccountNumber);
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

            TransactionArgs args = new TransactionArgs("Withdraw amount", (-1) * amount, BalanceAffected.CurrentBalance, this.AccountNumber);
            CurrentBalance -= amount;
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
            return true;      
        }//WithdrawAmount

        public void AddDeptRemaining(decimal amount, string reason)
        {
            //This will add the amount of dept the user still owes as read from the database
            if (amount < 0)
                return;
            //Create a transaction for adding a dept
            TransactionArgs args = new TransactionArgs(reason, amount, BalanceAffected.DeptBalance, this.AccountNumber);
            args.CanPushToDatabase = false;
            //Let the user know abou this transaction
            OnTransactionEvent?.Invoke(args);
            BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
        }//AddDeptRemaining
        public void AddDept(decimal amount, string reason)
        {
            //Prevent unnecessary transactions
            if (amount < 0)
                return;
            
            //Create a transaction for adding a dept
            TransactionArgs args = new TransactionArgs(reason, amount, BalanceAffected.DeptBalance, this.AccountNumber);
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
            DepositAmount(amount, "Reversed");
        }//ReverseAmount
        public void CancelBooking(IBookingFees fees)
        {
            //https://www.bluewatershotel.co.za/terms-conditions/guarantee-payment-cancellations-policy.html
            //Business rules
            //-If user has paid for a booking, deduct the amount needed to pay for cancelling and refund the remaining
            //-If the cancellation fee is larger than the amount paid, add the amount as dept.
            //-If the user paid for booking and there's still change after deductions, refund the user.

            decimal cancellationfee = fees.GetCancellationFee();
            TransactionArgs args;
            //If the user hasn't paid the full amount and the cancellation fees is more than the amount paid
            //-This means that the user will not get any refund.
            if (cancellationfee >= fees.AmountPaid)
            {
                this.AmountOwing -= fees.AmountPaid;

                //Raise the event handlers to alert the user about what is happening
                args = new TransactionArgs("Cancellation fee", cancellationfee, BalanceAffected.DeptBalance, this.AccountNumber);
                OnTransactionEvent?.Invoke(args);

                args = new TransactionArgs("Paid Cancelation fee", (-1) * fees.AmountPaid, BalanceAffected.DeptBalance, this.AccountNumber);
                OnTransactionEvent?.Invoke(args);

                //-Raise the balance changed event
                BalanceChanged?.Invoke(CurrentBalance, AmountOwing);
                return;
            }//end if

            //Here it means that the user has paid more and they must get a refund after deduction of the cancellation fee
            decimal refund = fees.AmountPaid - cancellationfee;

            //Deduct the refund amount from the amout they are owing
            this.AmountOwing -= refund;

            //Raise the transaction event to alert the user about what is happening
            args = new TransactionArgs("Cancelled booking", (-1) * fees.BookingCost, BalanceAffected.DeptBalance, this.AccountNumber);
            OnTransactionEvent?.Invoke(args);

            args = new TransactionArgs("Cancelletion fee", cancellationfee, BalanceAffected.DeptBalance, this.AccountNumber);
            OnTransactionEvent?.Invoke(args);

            args = new TransactionArgs("Paid Cancelation fee", (-1)*cancellationfee, BalanceAffected.DeptBalance, this.AccountNumber);
            OnTransactionEvent?.Invoke(args);
            //Reverse the amount left
            ReverseAmount(refund);
        }//CancelBooking2

        public void AddTransaction(TransactionArgs transaction)
        {
            //This will add the top 10 transation that have happened recently
            transaction.CanPushToDatabase = false;
            OnTransactionEvent?.Invoke(transaction);
        }//AddTransaction
    }//class
}//namespace