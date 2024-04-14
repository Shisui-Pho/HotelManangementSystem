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

        public static IGuest CreateGuest(string _name, string _surname, DateTime dob)
        {
            return new Guest(_name, _surname, dob);
        }
        public static IGuest CreateGuest(IUser user)
        {
            Guest gs = (Guest)user;  //new Guest(user.Name, user.Surname, user.DOB);
            gs.SetPassword(user.Password);
            gs.SetUsername(user.UserName);
            gs.SetIdForExistingUser(user.UserID);
            return gs;
        }//CreateGuest
        internal static IGuest CreateGuest(IUser user, decimal amountToPay, decimal balance)
        {
            Guest gs = (Guest)user; //new Guest(user.Name, user.Surname, user.DOB);
            gs.SetPassword(user.Password);
            gs.SetUsername(user.UserName);
            gs.SetIdForExistingUser(user.UserID);
            gs.Account.AddDept(amountToPay, "Current Dept");
            gs.Account.DepositAmount(balance, "Current Balance");
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
    }//class
}//namespace
