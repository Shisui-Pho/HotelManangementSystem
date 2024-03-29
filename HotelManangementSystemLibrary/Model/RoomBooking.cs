﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class RoomBooking : IRoomBooking
    {
        //Data member
        private decimal _amount;
        private static int maxDays = 10;
        private static int bookingCount = 500;
        //Properties
        public IGuest Guest { get; private set; }

        public IRoom Room { get; private set; }

        public DateTime DateBookedFor { get; private set; }

        public int NumberOfDaysToStay { get; private set; }

        public decimal AmoutToPay => _amount;

        public string BookingID { get; private set; }

        public int DaysStayed {get;set;}

        public decimal AmountPaid => throw new NotImplementedException();

        public bool IsCheckedIn { get; private set; }

        public RoomBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
        {
            Guest = guest;
            Room = room;
            //Make sure guest cannot book for a past date
            if (DateTime.UtcNow > date)
                throw new ArgumentException("Cannot book on this date!.");
            DateBookedFor = date;
            NumberOfDaysToStay = numberOfDays;
            _amount = Room.Price * numberOfDays;
            bookingCount += 50;
            BookingID = bookingCount.ToString();
            DaysStayed = 0;
            IsCheckedIn = false;
        }//RoomBooking

        public RoomBooking()
        {

        }//

        internal void SetBookingID(string _id) => BookingID = _id;

        public void ChangeBookingDate(DateTime date, int numberOfDays = 1)
        {
            if (numberOfDays > maxDays)
                numberOfDays = maxDays;
            DateBookedFor = date;
            NumberOfDaysToStay = numberOfDays;
        }//ChangeBooking
        public override string ToString()
        {
            return DateBookedFor.ToString("dd MMMM yyyy"); //Room.RoomNumber + "\t" + Guest.Name;
        }

        public void ChangeRoom(IRoom room)
        {
            if (room is null)
                return;
            Room = room;
        }//ChangeRoom

        public string ToCSVFormat()
        {
            return string.Format($"{BookingID};{Guest.UserID};{Room.RoomNumber};{DateBookedFor.ToString("dd/MM/yyyy")};{NumberOfDaysToStay.ToString()}");
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.BookingID.CompareTo(((IRoomBooking)obj).BookingID);
        }//CompareTo

        public void CheckIn()
        {
            if (IsCheckedIn)
                return;

            IsCheckedIn = true;
            DaysStayed++;
        }//CheckIn
    }//class
}//namespace
