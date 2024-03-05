using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IUsers : ICollectionHotel<IUser>
    {
        bool UsernameExists(string username);
        bool AlreadyExist(IUser user);
        IUser GetUser(string userid);
        bool LogInUser(string username, string password, out IUser logged_in_user);
    }//IUsers
}//namespace
