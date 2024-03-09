using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal abstract class Room : IRoom
    {
        public string RoomNumber { get; private set; }

        public bool HasTV { get; protected set; } = false;

        public bool IsSingleRoom { get; protected set; }

        public decimal Price { get; set; }
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
            HasTV = true;
        }//AddTV
        public void RemoveTV()
        {
            HasTV = false;
        }//RemoveTV

        public void SetPrice(decimal amount)
        {
        }//SetPrice
        public override string ToString()
        {
            return String.Format($"{IsSingleRoom};{RoomNumber};{Price.ToString("0.00")};{HasTV}");
        }//ToString
    }//class
    internal class SingleRoom : Room , ISingleRoom
    {
        public SingleRoom(string _roomNumber, bool hasTv = false): base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = true;
        }//ctor
    }//Singleroom

    internal class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber, bool hasTv = false) : base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = false;
        }//ctor
    }//DoubleRoom
}//namespace
