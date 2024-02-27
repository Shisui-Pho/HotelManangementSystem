using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IAdministrator : IUser
    {
        AccessRights Rights { get; }
        void ChangeAccessRights(IAdministrator admin, AccessRights right);
    }//class
}//namespace
