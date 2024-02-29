using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class Users : IUsers
    {
        private readonly List<IUser> _users;
        public int Count => _users.Count;

        public Users()
        {
            _users = new List<IUser>();
        }//ctor 1
        public Users(IUser[] users)
        {
            _users = new List<IUser>();
            _users.AddRange(users);
        }//ctor 2
        public Users(List<IUser> users)
        {
            _users = users;
        }//ctor 2
        public void Add(IUser user)
            => _users.Add(user);

        public bool AlreadyExist(IUser user)
        {
            IUser _user = _users.FirstOrDefault(u => u.UserID == user.UserID);
            if (_user is null)
                return false;
            return true;
        }//AlreadyExist

        public void Remove(IUser user)
            => _users.Remove(user);

        public void Update(IUser old, IUser _new)
        {
            int i = _users.FindIndex(us => us.UserID == old.UserID);
            if (i < 0)
                throw new ArgumentException("User not found");
            _users[i] = _new;
        }//Update
        public IEnumerator<IUser> GetEnumerator()
        {
            foreach (IUser user in _users)
                yield return user;
        }//GetEnumerator
        public IUser GetUser(string userid)
        {
            return _users.FirstOrDefault<IUser>(user => user.UserID == userid);
        }//GetUser
    }//class
}//namespace