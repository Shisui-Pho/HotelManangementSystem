using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.DatabaseService
{
    public class TextFileDatabase : IDatabaseService
    {
        //DateMembers
        private IRoomBookings _bookings = null;
        private IRooms _rooms = null;
        private IUsers _users = null;

        public IRoomBookings Bookings 
        {
            get
            {
                if (_bookings is null)
                    return LoadBookings();
                return _bookings;
            }//get
        }//Bookings
        public IRooms Rooms
        {
            get
            {
                if (_rooms is null)
                    return LoadRooms();
                return _rooms;
            }//get
        }//Bookings
        public IUsers Users
        {
            get
            {
                if (_users is null)
                    return LoadUsers();
                return _users;
            }//get
        }//Bookings
        public TextFileDatabase()
        {
        }//ctor 01
        public IRoomBookings LoadBookings()
        {
            return default;
        }//LoadBookings

        public IRooms LoadRooms()
        {
            return default;
        }//LoadRooms

        public IUsers LoadUsers()
        {
            return default;
        }//LoadUsers

        public void SaveBookings()
        {

        }//SaveBookings

        public void SaveRooms()
        {
        }//SaveRooms

        public void SaveUsers()
        {
        }//SaveUsers
    }//class
}//namespace
