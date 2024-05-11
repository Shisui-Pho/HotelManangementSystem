using HotelManangementSystemLibrary.Logging;
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
            {
                Exception("Can olny be performed by an administrator.");
                return;
            }
            if(((IAdministrator)admin_user).Rights != AccessRights.Universal)
            {
                Exception("Not enough access rights");
                return;
            }
            Role = newrole;
        }//UpdateRole
        private void Exception(string message)
        {
            var ex = new UnauthorizedAccessException(message);
            bool handled = ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Warning, TypeOfError.UserError);
            if (!handled)
                throw ex;
        }//CreateLog
        //private void Exception(string message)
        //{
        //    var ex = new ArgumentException(message);
        //    bool handled = ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Warning, TypeOfError.UserError);
        //    if (!handled)
        //        throw ex;
        //}//CreateLog
    }//class
}//namespace
