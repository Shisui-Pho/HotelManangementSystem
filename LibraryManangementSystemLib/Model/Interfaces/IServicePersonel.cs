namespace HotelManangementSystemLibrary
{
    public interface IServicePersonel : IUser, IHotelModel<IServicePersonel>
    {
        ServiceRole Role { get; }
        void UpdateRole(IUser admin_user, ServiceRole newrole);
    }//interface
}//class
