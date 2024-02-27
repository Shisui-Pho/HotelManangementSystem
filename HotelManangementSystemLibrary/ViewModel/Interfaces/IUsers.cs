using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IUsers : ICollectionHotel<IUser>
    {
        bool AlreadyExist(IUser user);
    }//IUsers
}//namespace
