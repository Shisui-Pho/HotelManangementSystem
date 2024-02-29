using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRooms : ICollectionHotel<IRoom>
    {
        IRoom FindRoom(string roomNumber);
    }//IRooms
}//namespace
