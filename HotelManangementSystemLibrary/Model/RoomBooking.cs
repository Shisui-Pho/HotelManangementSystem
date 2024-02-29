﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class RoomBooking : IRoomBooking
    {
        //Data member
        private decimal _amount;

        //Properties
        public IGuest Guest { get; private set; }

        public IRoom Room { get; private set; }

        public DateTime DateBookedFor { get; private set; }

        public int NumberOfDaysToStay { get; private set; }

        public decimal AmoutToPay => _amount;

        public string BookingID { get; private set; }

        public RoomBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
        {
            Guest = guest;
            Room = room;
            DateBookedFor = date;
            NumberOfDaysToStay = numberOfDays;
            _amount = Room.Price * numberOfDays;
        }//RoomBooking
        internal void SetBookingID(string _id) => BookingID = _id;
    }//class
}//namespace
