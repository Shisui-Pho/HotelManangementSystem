using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.Factory
{
    public static class UsersFactory
    {
        public static IUser CreateUser(TypeOfUser type, string _name, string _surname, DateTime dob)
        {
            switch (type)
            {
                case TypeOfUser.Admin:
                    return new Administrator(_name,_surname,dob);
                case TypeOfUser.Guest:
                    return new Guest(_name, _surname, dob);
                default:
                    throw new NotSupportedException();
            }//end switch
        }//CreateUser
        public static IGuest CreateGuest(IUser user, IContactDetails contacts, IUSerAccount account)
        {
            Guest gs = (Guest)user;
            gs.SetPassword(user.Password);
            gs.SetUsername(user.UserName);
            gs.SetIdForExistingUser(user.UserID);
            gs.SetAccountAndContactDetails(contacts, account);
            return gs;
        }//CreateGuest
        internal static IUser CreateUser(TypeOfUser type, string _name, string _surname, DateTime dob,string userID)
        {
            switch (type)
            {
                case TypeOfUser.Admin:
                    Administrator admin = new Administrator(_name, _surname, dob);
                    admin.SetIdForExistingUser(userID);
                    return admin;

                case TypeOfUser.Guest:
                    Guest guest = new Guest(_name, _surname, dob);
                    guest.SetIdForExistingUser(userID);
                    return guest;

                default:
                    throw new NotSupportedException();
            }//end switch
        }//CreateUser
        public static IGuests CreateGuests()
            => new Guests();
        public static IUsers CreateUsers()
            => new Users();
        public static IUsers CreateUsers(IUser[] users)
            => new Users(users);
        public static IUsers CreateUsers(List<IUser> users)
            => new Users(users);
        public static IUSerAccount CreateUserAccount()
        {
            return new UserAccount();
        }//CreateUserAccount
        internal static IUSerAccount CreateUserAccount(decimal balance, decimal amout_owing, string accoutNumber)
        {
            return new UserAccount(balance, amout_owing, accoutNumber);
        }//CreateUserAccount
        public static IContactDetails CreateContactDetails()
        {
            return new ContactDetails();
        }//CreateContactDetails
        public static IContactDetails CreateContactDetails(string email, string cellNumber, string emergency_number)
        {
            return new ContactDetails(email, cellNumber, emergency_number);
        }//CreateContactDetails
    }//class
}//namespace
