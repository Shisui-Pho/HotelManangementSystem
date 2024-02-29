using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary.Factory
{
    public static class RoomFactory
    {
        public static IRoom CreateRoom(TypeOfRoom type,string _roomNumber)
        {
            switch (type)
            {
                case TypeOfRoom.SingleRoom:
                    return new SingleRoom(_roomNumber);

                case TypeOfRoom.SharingRoom:
                    return new DoubleRoom(_roomNumber);
                default:
                    throw new NotImplementedException();
            }//end switch
        }//CreateRoom 

        public static IRooms CreateRooms() => new Rooms();
        public static IRooms CreateRooms(IRoom[] rooms) => new Rooms(rooms);
        public static IRooms CreateRooms(List<IRoom> rooms) => new Rooms(rooms);
    }//class
}//namespace
