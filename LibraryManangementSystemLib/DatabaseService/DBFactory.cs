namespace HotelManangementSystemLibrary.DatabaseService
{
    internal static class DBFactory
    {
        public static IGuests CreateAndLoadDBGuests(string connectionString, IUsers users)
        {
            DBGuests gg = new DBGuests(connectionString, users);
            gg.LoadData();
            return gg;
        }
        public static IUsers CreateAndLoadDBUsers(string connectionString)
        {
            DBUsers us = new DBUsers(connectionString);
            us.LoadData();
            return us;
        }
        public static IRoomBookings CreateAndLoadDBRoomBookings(string connectionString, IGuests guests,IRooms rooms)
        {
            DBBookings bo = new DBBookings(connectionString, guests, rooms);
            bo.LoadData();
            return bo;
        }
        public static IRooms CreateAndLoadDBRooms(string connectionString)
        {
            DBRooms ro = new DBRooms(connectionString);
            ro.LoadData();
            return ro;
        }
    }//class
}//namespace
