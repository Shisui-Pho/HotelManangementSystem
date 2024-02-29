using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoom
    {
        string RoomNumber { get; }
        bool HasTV { get; }
        bool IsSingleRoom { get; }
        decimal Price { get; set; }
        void ChangeRoomNumber(string _newRoomNumber);
        void AddTV();
        void RemoveTV();
    }//IRoom
    public interface ISingleRoom : IRoom
    { 
    }//ISingleRoom
    public interface IDoubleRoom : IRoom
    {

    }//IDoubleRoom
}//interface
