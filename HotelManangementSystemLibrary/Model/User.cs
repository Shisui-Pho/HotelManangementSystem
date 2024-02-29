using System;
using System.Collections.Generic;
using System.Text;

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
        internal void SetIDReadFromTheDatabase(string _userID) => UserID = _userID;

        public bool SignIn(string _username, string _password)
        {
            return UserName == _username && Password == _password;
        }//LogIn
    }//class

    internal class Guest : User, IGuest
    {
        public IContactDetails ContactDetails { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guest(string _name, string _surname, DateTime _dob) : base(_name, _surname, _dob)
        {
            UserType = TypeOfUser.Guest;
            UserID = "GU-{1253123}";
        }//ctor 01

        public void SetEmailAddress(string email)
        {
            if (!Service.IsEmailCorrect(email))
                throw new ArgumentException("Email not in the correct format!!");
            ContactDetails.EmailAddress = email;
        }//SetEmailAddress

        public void SetCellNumber(string _cellnumber)
        {
            if(!Service.IsCellphoneNumberCorrect(_cellnumber))
                throw new ArgumentException("Cellphone number not in the correct format!!");
            ContactDetails.CellphoneNumber = _cellnumber;
        }//SetCellNumber

        public void SetEmergencyNumber(string _emergency)
        {
            if (!Service.IsCellphoneNumberCorrect(_emergency))
                throw new ArgumentException("Cellphone number not in the correct format!!");
            ContactDetails.EmergencyNumber = _emergency;
        }//SetEmergencyNumber

        public void SetContactDetails(IContactDetails details) => ContactDetails = details;
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
