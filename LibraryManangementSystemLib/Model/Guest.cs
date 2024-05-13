using System;
namespace HotelManangementSystemLibrary
{
    internal class Guest : User, IGuest
    {
        public IUSerAccount Account { get; private set; }
        public IContactDetails ContactDetails { get;private set; }
        private static int _count = 0;
        public event delOnPropertyChanged GuestPropertyChangedEvent;
        public event delBalanceChanged BalanceChangedEvent;

        public Guest(string _name, string _surname, DateTime _dob) : base(_name, _surname, _dob)
        {
            _count++;
            UserType = TypeOfUser.Guest;
            UserID = $"GU-895485685{_count}";
        }//ctor 01

        private void Account_BalanceChanged(decimal newBalance, decimal newAmountOwing)
        {
            BalanceChangedEventArgs args = new BalanceChangedEventArgs(newBalance, newAmountOwing, this.UserID);
            BalanceChangedEvent?.Invoke(args);
        }//Account_BalanceChanged

        public void SetEmailAddress(string email)
        {
            if (!Service.IsEmailCorrect(email) && email != "None")
                throw new ArgumentException("Email not in the correct format!!");
            if (email == this.ContactDetails.EmailAddress)
                return;
            ContactDetails.EmailAddress = email;
            GuestPropertyChangedEvent?.Invoke(this.UserID, "Email_Address", email);
        }//SetEmailAddress

        public void SetCellNumber(string _cellnumber)
        {
            if(!Service.IsCellphoneNumberCorrect(_cellnumber) && _cellnumber != "None")
                throw new ArgumentException("Cellphone number not in the correct format!!");
            if (this.ContactDetails.CellphoneNumber == _cellnumber)
                return;
            ContactDetails.CellphoneNumber = _cellnumber;
            GuestPropertyChangedEvent?.Invoke(this.UserID, "CellphoneNumber", _cellnumber);
        }//SetCellNumber

        public void SetEmergencyNumber(string _emergency)
        {
            if (!Service.IsCellphoneNumberCorrect(_emergency) && _emergency != "None")
                throw new ArgumentException("Cellphone number not in the correct format!!");
            if (this.ContactDetails.EmergencyNumber == _emergency)
                return;            
            ContactDetails.EmergencyNumber = _emergency;
            GuestPropertyChangedEvent?.Invoke(this.UserID, "Emergency_PhoneNumber", _emergency);
        }//SetEmergencyNumber
        internal void SetAccountAndContactDetails(IContactDetails contacts, IUSerAccount account)
        {
            ContactDetails = contacts;
            Account = account;
            Account.BalanceChanged += Account_BalanceChanged;
        }//SetAccountAndContactDetails
        public void SetContactDetails(IContactDetails details) => ContactDetails = details;
        public string ToGuestCSVFormat()
        {
            string balance = Service.ToStringMoney(this.Account.CurrentBalance);
            string amountToPay = Service.ToStringMoney(this.Account.AmountOwing);
            return String.Format($"{this.UserID},{this.ContactDetails.CellphoneNumber}," +
                $"{this.ContactDetails.EmailAddress},{this.ContactDetails.EmergencyNumber}," +
                $"{balance},{amountToPay}");
        }//ToCSVFormat
        public override string ToString()
        {
            return $"{this.UserID.PadRight(15)} {this.Name.PadRight(15)} {this.Surname}";
        }//ToString
    }//class
}//namespace