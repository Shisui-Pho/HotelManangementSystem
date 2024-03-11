using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IServicePersonel : IUser
    {
        ServiceRole Role { get; }
        void UpdateRole(IUser admin_user, ServiceRole newrole);
    }//interface
}//class
