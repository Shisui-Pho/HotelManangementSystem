using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public abstract class Room : IRoom
    {
        public string RoomNumber { get; private set; }

        public bool HasTV { get; protected set; } = false;

        public bool IsSingleRoom { get; protected set; }

        public decimal Price { get; protected set; }
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
    }//class
    public class SingleRoom : Room , ISingleRoom
    {
        public SingleRoom(string _roomNumber, bool hasTv = false): base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = true;
        }//ctor
    }//Singleroom

    public class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber, bool hasTv = false) : base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = true;
        }//ctor
    }//DoubleRoom
}//namespace
