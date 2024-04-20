using System;
namespace HotelManangementSystemLibrary
{
    internal class ServicePersonel : User, IServicePersonel
    {
        public ServiceRole Role { get; private set; }
        public ServicePersonel(string _name, string _surname, DateTime _dob, ServiceRole role = ServiceRole.Default) 
            : base(_name, _surname, _dob)
        {
            Role = role;
        }//ctor default
        public void UpdateRole(IUser admin_user, ServiceRole newrole)
        {
            if (!(admin_user is IAdministrator))
                throw new UnauthorizedAccessException("Can only be perfomed by administrator");
            if(((IAdministrator)admin_user).Rights != AccessRights.Universal)
                throw new UnauthorizedAccessException("Not enough access rights");
            Role = newrole;
        }//UpdateRole
    }//class
}//namespace
