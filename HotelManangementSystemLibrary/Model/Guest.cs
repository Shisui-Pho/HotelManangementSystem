using System;
namespace HotelManangementSystemLibrary
{
    internal class Guest : User, IGuest
    {
        public IUSerAccount Account { get; private set; }
        public IContactDetails ContactDetails { get;private set; }
        private static int _count = 0;
        public Guest(string _name, string _surname, DateTime _dob) : base(_name, _surname, _dob)
        {
            _count++;
            UserType = TypeOfUser.Guest;
            UserID = $"GU-895485685{_count}";
            ContactDetails = new ContactDetails();
            Account = new UserAccount();
        }//ctor 01

        public void SetEmailAddress(string email)
        {
            if (!Service.IsEmailCorrect(email) && email != "None")
                throw new ArgumentException("Email not in the correct format!!");
            ContactDetails.EmailAddress = email;
        }//SetEmailAddress

        public void SetCellNumber(string _cellnumber)
        {
            if(!Service.IsCellphoneNumberCorrect(_cellnumber) && _cellnumber != "None")
                throw new ArgumentException("Cellphone number not in the correct format!!");
            ContactDetails.CellphoneNumber = _cellnumber;
        }//SetCellNumber

        public void SetEmergencyNumber(string _emergency)
        {
            if (!Service.IsCellphoneNumberCorrect(_emergency) && _emergency != "None")
                throw new ArgumentException("Cellphone number not in the correct format!!");
            ContactDetails.EmergencyNumber = _emergency;
        }//SetEmergencyNumber

        public void SetContactDetails(IContactDetails details) => ContactDetails = details;
        public new string ToCSVFormat()
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

        public bool Equals(IGuest other)
        {
            return base.Equals(other);
        }
    }//class
}//namespace