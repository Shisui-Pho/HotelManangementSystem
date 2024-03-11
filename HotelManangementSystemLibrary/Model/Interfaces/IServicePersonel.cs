namespace HotelManangementSystemLibrary
{
    public interface IServicePersonel : IUser
    {
        ServiceRole Role { get; }
        void UpdateRole(IUser admin_user, ServiceRole newrole);
    }//interface
}//class
