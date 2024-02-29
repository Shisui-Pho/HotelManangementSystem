using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.DatabaseService
{
    public interface IDatabaseService
    {
        IRoomBookings Bookings { get; }
        IRooms Rooms { get; }
        IUsers Users { get; }
        void SaveBookings();
        void SaveRooms();
        void SaveUsers();
        IRoomBookings LoadBookings();
        IRooms LoadRooms();
        IUsers LoadUsers();
    }//interface
}//namespace