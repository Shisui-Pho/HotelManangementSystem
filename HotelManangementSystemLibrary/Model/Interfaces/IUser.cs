using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IUser : IPerson, IHotelModel
    {
        string UserName { get; }
        string Password { get; }
        string UserID { get; }
        TypeOfUser UserType { get; }

        bool SignIn(string _username, string _password);
        void SetPassword(string password);
        void SetUsername(string username);
    }//Interface
}//namespace
