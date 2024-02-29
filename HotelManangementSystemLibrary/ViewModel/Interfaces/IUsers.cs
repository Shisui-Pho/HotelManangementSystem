using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IUsers : ICollectionHotel<IUser>
    {
        bool AlreadyExist(IUser user);
        IUser GetUser(string userid);
    }//IUsers
}//namespace
