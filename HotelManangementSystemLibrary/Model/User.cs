using System;
namespace HotelManangementSystemLibrary
{
    internal abstract class User : Person, IUser
    { 
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string UserID { get; protected set; }

        public TypeOfUser UserType { get; protected set; }

        public User(string _name, string _surname, DateTime _dob) : base(_name, _surname, _dob) 
        {
        }//

        public void SetUsername(string username)
        {
            //Do some data validation here
            UserName = username;
        }//SetUsername
        public void SetPassword(string password)
        {
            //Do some data validation here
            Password = password;
        }//SetPassword
        internal void SetIdForExistingUser(string _userID) => UserID = _userID;

        public bool SignIn(string _username, string _password)
        {
            return UserName == _username && Password == _password;
        }//LogIn
        public override string ToString()
        {
            return base.Name + " " + base.Surname;
            //return String.Format($"{UserType.ToString()};{UserID};{UserName};{Password};{Name};{Surname};{DOB.ToString("dd/MM/yyyy")}");
        }//ToString

        public string ToCSVFormat()
        {
            return $"{this.UserType.ToString()},{this.UserID},{this.UserName},{this.Password},{this.Name},{this.Surname},{this.DOB.ToString("dd/MM/yyyy")}";
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.UserID.CompareTo(((IUser)obj).UserID);
        }//CompareTo

        public bool Equals(IUser other)
        {
            return other.UserID == this.UserID;
        }//
    }//class
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
            return String.Format($"{this.UserID},{this.ContactDetails.CellphoneNumber},{this.ContactDetails.EmailAddress},{this.ContactDetails.EmergencyNumber}");
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
    internal class Administrator : User, IAdministrator
    {
        public AccessRights Rights { get; private set; }
        public Administrator(string _name, string _surname, DateTime _dob) : base(_name, _surname, _dob)
        {
            UserType = TypeOfUser.Admin;
            UserID = "AD-{264654}";
            Rights = AccessRights.Restricted;
        }//ctor 01

        public void ChangeAccessRights(IAdministrator admin, AccessRights _rights)
        {
            if (admin.Rights != AccessRights.Universal)
                throw new ArgumentException("Unauthorized access");
            Rights = _rights;
        }//ChangeAccessRights
    }//Administrator
}//namespace