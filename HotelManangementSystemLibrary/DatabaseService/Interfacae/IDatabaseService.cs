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
        IGuests Guests { get; }
        void SaveBookings();
        void SaveRooms();
        void SaveUsers();
        void SaveGuets();
        IRoomBookings LoadBookings();
        IRooms LoadRooms();
        IUsers LoadUsers();
        IGuests LoadGuests();
    }//interface
}//namespace