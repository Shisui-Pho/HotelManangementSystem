﻿using System;
namespace HotelManangementSystemLibrary
{
    internal abstract class Room : IRoom
    {
        public event delOnPriceChanged OnPriceChangedEvent;
        public event delOnPropertyChanged PropertyChangedEvent;
        //Properties
        public string RoomNumber { get; private set; }
        public bool IsSingleRoom { get; protected set; }

        public decimal Price { get; set; }

        public string TelephoneNumber { get; private set; }

        public bool IsRoomUnderMaintenance { get; private set; } = false;

        public IRoomFeatures RoomFeatures { get; }

        public IRoomBookedDate BookedDates { get; private set; }

        public Room(string _roomNumber, IRoomFeatures features, IRoomBookedDate bookedDates)
        {
            RoomNumber = _roomNumber;
            RoomFeatures = features;
            this.BookedDates = bookedDates;
            RoomFeatures.OnFeaturesModified += RoomFeatures_OnFeaturesModified;
        }//ctor

        private void RoomFeatures_OnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args)
        {
            args.RoomNumber = this.RoomNumber;
            if (isAdded)
                Price += feature.Price;
            else
                Price -= feature.Price;
            OnPriceChangedEvent?.Invoke(this);
        }//RoomFeatures_OnFeaturesModified

        public void ChangeRoomNumber(string _newRoomNumber)
        {
            RoomNumber = _newRoomNumber;
            PropertyChangedEvent?.Invoke(this.RoomNumber, "RoomNumber", _newRoomNumber);
        }//ChangeRoomNumber
        public void UpdateTelephoneNumber(string _number)
        {
            if (Service.IsCellphoneNumberCorrect(_number))
                TelephoneNumber = _number;
            PropertyChangedEvent.Invoke(this.RoomNumber, "TelephoneNumber", _number);
        }//UpdateTelephoneNumber
        public override string ToString()
        {
            return String.Format($"{IsSingleRoom};{RoomNumber};{Price.ToString("0.00")};{IsRoomUnderMaintenance}");
        }//ToString

        public string ToCSVFormat()
        {
            string sAmount = Service.ToStringMoney(this.Price);
            return $"{this.IsSingleRoom},{this.RoomNumber},{sAmount},{RoomFeatures.ToString()}";
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.RoomNumber.CompareTo(((IRoom)obj).RoomNumber);
        }//CompareTo

        public void MaintenanceSwitch()
        {
            if (IsRoomUnderMaintenance)
                IsRoomUnderMaintenance = false;
            else
                IsRoomUnderMaintenance = true;
            PropertyChangedEvent?.Invoke(this.RoomNumber, "IsRoomUnderMaintenance", IsRoomUnderMaintenance.ToString());
        }//HideUnhideRoom
    }//class
}//namespace
