﻿using HotelManangementSystemLibrary.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary.DatabaseService
{
    public class TextFileDatabase : IDatabaseService
    {
        //DateMembers
        private IRoomBookings _bookings = null;
        private IRooms _rooms = null;
        private IUsers _users = null;
        private IGuests _guests = null;

        //Properties
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
        public IGuests Guests
        {
            get
            {
                if (_guests is null)
                    return LoadGuests();
                return _guests;
            }
        }//Guets

        public TextFileDatabase()
        {
        }//ctor 01
        //The Saving and Loading methods are in extension classes
        public IRoomBookings LoadBookings()
        {
            if (_rooms is null)
                LoadRooms();
            if (_guests is null)
                LoadGuests();
            if(_bookings is null)
                _bookings = _bookings.LoadBookings(_guests, _rooms);
            return _bookings;
        }//LoadBookings

        public IRooms LoadRooms()
        {
            _rooms = _rooms.LoadRooms();
            return _rooms;
        }//LoadRooms

        public IUsers LoadUsers()
        {
            if(_users is null)
                _users = _users.LoadUsers();
            return _users;
        }//LoadUsers
        public IGuests LoadGuests()
        {
            if (_users is null)
                LoadUsers();
            if(_guests is null)
                _guests = _guests.LoadGuests(_users);
            return _guests;
        }//LoadGuests
        public void SaveBookings()
        {
            _bookings.SaveBookings();
        }//SaveBookings

        public void SaveRooms()
        {
            _rooms.SaveRooms();
        }//SaveRooms

        public void SaveUsers()
        {
            _users.SaveUsers();
        }//SaveUsers

        public void SaveGuets()
        {
            _guests.SaveGuests();
        }//SaveGuets

        public async void LoadEntireDatabaseAsync()
        {
            await Task.Run(() =>
            {
                LoadUsers();
                LoadRooms();                
                LoadGuests();
                LoadBookings();
            });
        }//LoadEntireDatabaseAsync
        public Task<IRoomBookings> LoadBookingsAsync(IUser user)
        {
            throw new NotImplementedException();
        }
    }//class
}//namespace
