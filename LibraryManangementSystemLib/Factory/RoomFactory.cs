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
                    return new SingleRoom(_roomNumber, CreateRoomFeatures(), CreateRoomBookedDates());

                case TypeOfRoom.SharingRoom:
                    return new DoubleRoom(_roomNumber, CreateRoomFeatures(),CreateRoomBookedDates());
                default:
                    throw new NotImplementedException();
            }//end switch
        }//CreateRoom 
        internal static IRoom CreateRoom(TypeOfRoom type, string _roomNumber, IRoomFeatures fet, IRoomBookedDate dates)
        {
            switch (type)
            {
                case TypeOfRoom.SingleRoom:
                    return new SingleRoom(_roomNumber, fet, dates);

                case TypeOfRoom.SharingRoom:
                    return new DoubleRoom(_roomNumber, fet, dates);
                default:
                    throw new NotImplementedException();
            }//end switch
        }//CreateRoom
        public static IRoomFeatures CreateRoomFeatures()
            => new RoomFeatures();
        public static IRoomBookedDate CreateRoomBookedDates()
            => new RoomBookedDates();
        public static IRooms CreateRooms() => new Rooms();
        public static IRooms CreateRooms(IRoom[] rooms) => new Rooms(rooms);
        public static IRooms CreateRooms(List<IRoom> rooms) => new Rooms(rooms);
    }//class
}//namespace
