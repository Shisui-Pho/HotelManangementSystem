using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class Users: GeneralCollection<IUser> , IUsers
    {
        public Users():base()
        {
        }//ctor 1
        public Users(IUser[] users):base(users)
        {
        }//ctor 2
        public Users(List<IUser> users) : base(users)
        {
        }//ctor 2
        public bool UsernameExists(string username)
        {
            int i = base._collection.FindIndex(user => user.UserName == username);
            return i >= 0;
        }//UsernameExists

        public bool AlreadyExist(IUser user)
        {
            IUser _user = base._collection.FirstOrDefault(u => u.UserID == user.UserID);
            if (_user is null)
                return false;
            return true;
        }//AlreadyExist
        public bool LogInUser(string username, string password, out IUser _logged_in_user)
        {
            _logged_in_user = base._collection.FirstOrDefault(ur => ur.UserName == username);
            if (_logged_in_user is null)
                return false;
            if (!_logged_in_user.SignIn(username, password))
                return false;
            return true;
        }//LogInUser
        public IUser GetUser(string userid)
        {
            return base._collection.FirstOrDefault<IUser>(user => user.UserID == userid);
        }//GetUser
    }//class
}//namespace