using System;

namespace HotelManangementSystemLibrary
{
    internal static class IDGenerator
    {
        public static string GenerateID<T>(IGeneralCollection<T> collection)
            where T : IHotelModel
        { 
            T elem1 = collection[0];
            if (elem1 is IAdministrator)
                return UserId((IUsers)collection);
            if (elem1 is IRoomBooking)
                return RoomBookingId((IRoomBookings)collection);
            return "";
        }//
        private static string UserId(IUsers users)
        {
            return default;
        }//AdministartorId
        private static string RoomBookingId(IRoomBookings bookings)
        {
            return default;
        }//RoomBookingId
        public static string GenerateUserId(IUsers users, IUser user)
        {
            string id = "";
            if (user is IAdministrator)
                id += String.Format("AD-");
            if (user is IServicePersonel)
                id += String.Format("SP-");
            if (users is IGuest)
                id += String.Format("GS-");
            int count = users.Count;
            string last = user.UserID.Substring(user.UserID.Length - 2);//Get last two digist

            Random rnd = new Random();
            int val = int.Parse(last);
            string ev = (val + rnd.Next(10, 100)).ToString("00000").Substring(0,5);//Take the first 5 digits
            id += DateTime.Now.Year.ToString("0000")
               + DateTime.Now.Month.ToString("00")
               + DateTime.Now.Day.ToString("00")
               + ev;
            return id;
        }//GenerateUser
        public static string GenerateRoomBookingID(IRoomBookings bookings)
        {
            Random rnd = new Random();
            return (bookings.Count * rnd.Next(10, 100)).ToString("0000000").Substring(0, 5);
        }//GenerateRoomBookingID
    }//class
}//namespace
