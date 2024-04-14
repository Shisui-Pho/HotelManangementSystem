using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRooms : IGeneralCollection<IRoom>
    {
        IRoom this[string roomnumber] { get;set; }
        IRoom FindRoom(string roomNumber);
        List<T> GetRoomFilter<T>();
    }//IRooms
}//namespace
