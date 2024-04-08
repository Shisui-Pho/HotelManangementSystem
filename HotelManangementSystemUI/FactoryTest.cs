using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.DatabaseService;
using HotelManangementSystemLibrary.Factory;
using System;

namespace HotelManangementSystemUI
{
    public static class FactoryTest
    {
        public static IDatabaseService CreateDatabase()
        {
            IDatabaseService service = new TextFileDatabase();
            service.LoadBookings();
            service.LoadUsers();
            service.LoadRooms();
            service.LoadGuests();


            AddTestRooms(service);
            AddTestUsers(service);
            AddTestGuests(service);
            AddTestBookings(service);


            return service;
        }
        private static void AddTestRooms(IDatabaseService ser)
        {
            IFeatures fetures = Features.GetFeaturesInstance();
            IRoom room = RoomFactory.CreateRoom(TypeOfRoom.SingleRoom, "Room 01");
            room.AddTV();
            ser.Rooms.Add(room);
            room.RoomFeatures.AddFeature(fetures[0]);
            room.RoomFeatures.AddFeature(fetures[2]);
            room.RoomFeatures.AddFeature(fetures[3]);

            room = RoomFactory.CreateRoom(TypeOfRoom.SharingRoom, "Room 02");
            ser.Rooms.Add(room);
            room.RoomFeatures.AddFeature(fetures[0]);
            room.RoomFeatures.AddFeature(fetures[2]);
            room.RoomFeatures.AddFeature(fetures[3]);

            room = RoomFactory.CreateRoom(TypeOfRoom.SingleRoom, "Room 03");
            ser.Rooms.Add(room);
            room.RoomFeatures.AddFeature(fetures[0]);
            room.RoomFeatures.AddFeature(fetures[2]);
            room.RoomFeatures.AddFeature(fetures[3]);

            room = RoomFactory.CreateRoom(TypeOfRoom.SharingRoom, "Room 04");
            ser.Rooms.Add(room);
        }
        private static void AddTestGuests(IDatabaseService ser)
        {
            foreach (IUser item in ser.Users)
            {
                if(item.UserType == TypeOfUser.Guest)
                {
                    IGuest gs = UsersFactory.CreateGuest(item);
                    ser.Guests.Add(gs);
                }
            }
        }

        private static void AddTestUsers(IDatabaseService ser)
        {
            IUser user = UsersFactory.CreateUser(TypeOfUser.Admin, "Phiwokwakhe", "Khathwane", DateTime.Now);
            user.SetPassword("phiwo");
            user.SetUsername("phiwo");
            ser.Users.Add(user);

            user = UsersFactory.CreateUser(TypeOfUser.Guest, "Abraham", "Fischer", DateTime.Now);
            user.SetUsername("abraham");
            user.SetPassword("abr");
            ser.Users.Add(user);

            user = UsersFactory.CreateUser(TypeOfUser.Guest, "Phidophelia", "Mathebula", DateTime.Now);
            user.SetUsername("phi");
            user.SetPassword("phi");
            ser.Users.Add(user);

            user = UsersFactory.CreateUser(TypeOfUser.Guest, "Henry", "Mac'dalton", DateTime.Now);
            user.SetUsername("hen");
            user.SetPassword("abc");
            ser.Users.Add(user);


            user = UsersFactory.CreateUser(TypeOfUser.Guest, "Phiwokwakhe", "Khathwane", DateTime.Now);
            user.SetUsername("hen");
            user.SetPassword("abc");
            ser.Users.Add(user);
        }
        private static void AddTestBookings(IDatabaseService ser)
        {
            IUser us = ser.Users.GetUser("GU-8954856851");
            IGuest gs = ser.Guests.FindGuest(us);
            IRoomBooking booking = BookingsFactory.CreateBooking(gs, ser.Rooms.FindRoom("Room 01"), DateTime.Now.AddDays(5));
            ser.Bookings.Add(booking);

            us = ser.Users.GetUser("GU-8954856852");
            gs = ser.Guests.FindGuest(us);
            booking = BookingsFactory.CreateBooking(gs, ser.Rooms.FindRoom("Room 01"), DateTime.Now.AddMonths(3),2);
            ser.Bookings.Add(booking);

            us = ser.Users.GetUser("GU-8954856851");
            gs = ser.Guests.FindGuest(us);
            booking = BookingsFactory.CreateBooking(gs, ser.Rooms.FindRoom("Room 02"), DateTime.Now.AddDays(5), 3);
            ser.Bookings.Add(booking);
            ser.SaveUsers();
        }
    }
}
