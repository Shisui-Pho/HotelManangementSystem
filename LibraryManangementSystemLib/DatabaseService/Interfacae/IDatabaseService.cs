using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        Task<IRoomBookings> LoadBookingsAsync(IUser user);
        IRooms LoadRooms();
        IUsers LoadUsers();
        IGuests LoadGuests();
        void LoadEntireDatabaseAsync();
    }//interface
}//namespace