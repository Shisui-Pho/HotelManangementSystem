using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoom : IHotelModel
    {
        string RoomNumber { get; }
        bool HasTV { get; }
        bool IsSingleRoom { get; }
        decimal Price { get; set; }
        string TelephoneNumber { get; }
        bool IsRoomHidden { get; }
        void ChangeRoomNumber(string _newRoomNumber);
        void AddTV();
        void RemoveTV();
        void UpdateTelephoneNumber(string _number);
        void HideUnhideRoom();
    }//IRoom
    public interface ISingleRoom : IRoom
    { 
    }//ISingleRoom
    public interface IDoubleRoom : IRoom
    {

    }//IDoubleRoom
}//interface
