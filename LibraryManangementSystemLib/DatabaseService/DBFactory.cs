namespace HotelManangementSystemLibrary.DatabaseService
{
    internal static class DBFactory
    {
        public static IGuests CreateAndLoadDBGuests(string connectionString, IUsers users)
        {
            return new DBGuests(connectionString,users);
        }
        public static IUsers CreateAndLoadDBUsers(string connectionString)
        {
            return new DBUsers(connectionString);
        }
        public static IRoomBookings CreateAndLoadDBRoomBookings(string connectionString, IGuests guests,IRooms rooms)
        {
            return new DBBookings(connectionString, guests,rooms);
        }
        public static IRooms CreateAndLoadDBRooms(string connectionString)
        {
            return new DBRooms(connectionString);
        }
    }//class
}//namespace
