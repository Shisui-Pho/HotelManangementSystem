using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace HotelManangementSystemLibrary.DatabaseService
{
    public class AccessDatabase : IDatabaseService
    {
        private IUsers users;
        private IGuests guests;
        public IRoomBookings Bookings => throw new NotImplementedException();

        public IRooms Rooms => throw new NotImplementedException();

        public IUsers Users 
        {
            get
            {
                if (users is null)
                    return LoadUsers();
                return users;
            }//end getter
        }//Users

        public IGuests Guests 
        {
            get => guests;
        }

        public IRoomBookings LoadBookings()
        {
            throw new NotImplementedException();
        }//LoadBookings

        public IGuests LoadGuests()
        {
            throw new NotImplementedException();
        }//LoadGuests

        public IRooms LoadRooms()
        {
            throw new NotImplementedException();
        }//LoadRooms

        public IUsers LoadUsers()
        {
            throw new NotImplementedException();
        }

        public void SaveBookings()
        {
            throw new NotImplementedException();
        }//SaveBookings

        public void SaveGuets()
        {
            throw new NotImplementedException();
        }//SaveGuets

        public void SaveRooms()
        {
            throw new NotImplementedException();
        }//SaveRooms

        public void SaveUsers()
        {
            throw new NotImplementedException();
        }//SaveUser
    }//class
}//namespace