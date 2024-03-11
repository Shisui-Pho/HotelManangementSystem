using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.DatabaseService;
using HotelManangementSystemLibrary.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IRoom room = RoomFactory.CreateRoom(TypeOfRoom.SingleRoom, "Room 01");
            room.AddTV();
            ser.Rooms.Add(room);

            room = RoomFactory.CreateRoom(TypeOfRoom.SharingRoom, "Room 02");
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

            user = UsersFactory.CreateUser(TypeOfUser.Guest, "Hello", "World", DateTime.Now);
            user.SetUsername("hello");
            user.SetPassword("world");
            ser.Users.Add(user);
        }
        private static void AddTestBookings(IDatabaseService ser)
        {
            IUser us = ser.Users.GetUser("GU-{1253123}");
            IGuest gs = ser.Guests.FindGuest(us);
            IRoomBooking booking = BookingsFactory.CreateBooking(gs, ser.Rooms.FindRoom("Room 01"), DateTime.Now);
            ser.Bookings.Add(booking);
        }
    }
}
