﻿namespace HotelManangementSystemLibrary
{
    internal class SingleRoom : Room , ISingleRoom
    {
        public SingleRoom(string _roomNumber, bool hasTv = false): base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = true;
            Price = _singleRoomStandardValue;
        }//ctor
        public bool Equals(ISingleRoom other)
        {
            return base.Equals(other);
        }
    }//Singleroom
}//namespace
