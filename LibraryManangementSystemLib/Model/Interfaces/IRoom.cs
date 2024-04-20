using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoom : IHotelModel<IRoom>
    {
        string RoomNumber { get; }
        bool IsSingleRoom { get; }
        decimal Price { get; set; }
        string TelephoneNumber { get; }
        bool IsRoomUnderMaintenance { get; }
        IRoomFeatures RoomFeatures { get; }
        event delOnPriceChanged OnPriceChangedEvent;
        IRoomBookedDate BookedDates { get; }
        void ChangeRoomNumber(string _newRoomNumber);
        void UpdateTelephoneNumber(string _number);
        /// <summary>
        /// This will switch the maintenance status of the room,
        ///     if it was false, it will be turned true, otheriwse false.
        /// </summary>
        void MaintenanceSwitch();
    }//IRoom
    public interface ISingleRoom : IRoom, IHotelModel<ISingleRoom>
    { 
    }//ISingleRoom
    public interface IDoubleRoom : IRoom, IHotelModel<IDoubleRoom>
    {

    }//IDoubleRoom
}//interface
