using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal abstract class Room : IRoom
    {
        //Data members        
        protected static decimal _doubleRoomStandardValue = 400m;
        protected static decimal _singleRoomStandardValue = 550m;
        protected static decimal _entertainments = 50m;
        //Properties
        public string RoomNumber { get; private set; }

        public bool HasTV { get; protected set; } = false;

        public bool IsSingleRoom { get; protected set; }

        public decimal Price { get; set; }

        public string TelephoneNumber { get; private set; }

        public bool IsRoomHidden { get; private set; } = false;

        public Room(string _roomNumber)
        {
            RoomNumber = _roomNumber;
        }//ctor

        public void ChangeRoomNumber(string _newRoomNumber)
        {
            RoomNumber = _newRoomNumber;
        }//ChangeRoomNumber

        public void AddTV()
        {
            if (HasTV)
                return;
            HasTV = true;
            Price += _entertainments;
        }//AddTV
        public void RemoveTV()
        {
            if (!HasTV)
                return;
            HasTV = false;
            Price -= _entertainments;
        }//RemoveTV

        public void SetPrice(decimal amount)
        {
            if (IsSingleRoom)
            {
                if (amount < _singleRoomStandardValue)
                    Price = _singleRoomStandardValue;
            }
            else
            {
                if (amount < _doubleRoomStandardValue)
                    Price = _doubleRoomStandardValue;
            }
        }//SetPrice
        public void UpdateTelephoneNumber(string _number)
        {
            if (Service.IsCellphoneNumberCorrect(_number))
                TelephoneNumber = _number;
        }//UpdateTelephoneNumber
        public override string ToString()
        {
            return String.Format($"{IsSingleRoom};{RoomNumber};{Price.ToString("0.00")};{HasTV};{IsRoomHidden}");
        }//ToString

        public string ToCSVFormat()
        {
            return String.Format($"{IsSingleRoom};{RoomNumber};{Price.ToString("0.00")};{HasTV}");
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.RoomNumber.CompareTo(((IRoom)obj).RoomNumber);
        }//CompareTo

        public void HideUnhideRoom()
        {
            IsRoomHidden = !IsRoomHidden;
        }//HideUnhideRoom

        public bool Equals(IRoom x, IRoom y)
        {
            return x.RoomNumber == y.RoomNumber;
        }

        public bool Equals(IRoom other)
        {
            return this.RoomNumber == other.RoomNumber;
        }
    }//class
}//namespace
