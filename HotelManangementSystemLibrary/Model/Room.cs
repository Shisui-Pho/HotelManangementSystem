using System;
namespace HotelManangementSystemLibrary
{
    public delegate void delOnPriceChanged(IRoom room);
    internal abstract class Room : IRoom
    {
        //Data members        
        protected static decimal _doubleRoomStandardValue = 400m;
        protected static decimal _singleRoomStandardValue = 550m;
        protected static decimal _entertainments = 50m;

        public event delOnPriceChanged OnPriceChangedEvent;

        //Properties
        public string RoomNumber { get; private set; }

        public bool HasTV { get; protected set; } = false;

        public bool IsSingleRoom { get; protected set; }

        public decimal Price { get; set; }

        public string TelephoneNumber { get; private set; }

        public bool IsRoomUnderMaintenance { get; private set; } = false;

        public RoomFeatures RoomFeatures { get; }

        public Room(string _roomNumber)
        {
            RoomNumber = _roomNumber;
            RoomFeatures = new RoomFeatures();
            RoomFeatures.OnFeaturesModified += RoomFeatures_OnFeaturesModified;
        }//ctor

        private void RoomFeatures_OnFeaturesModified(IFeature feature, bool isAdded)
        {
            if (isAdded)
                Price += feature.Price;
            else
                Price -= feature.Price;

            OnPriceChangedEvent?.Invoke(this);
        }//RoomFeatures_OnFeaturesModified

        public void ChangeRoomNumber(string _newRoomNumber)
        {
            RoomNumber = _newRoomNumber;
        }//ChangeRoomNumber

        public void AddTV()
        {
            if (HasTV)
                return;
            HasTV = true;
            Price += _entertainments;
        }//AddTV
        public void RemoveTV()
        {
            if (!HasTV)
                return;
            HasTV = false;
            Price -= _entertainments;
        }//RemoveTV

        public void SetPrice(decimal amount)
        {
            if (IsSingleRoom)
            {
                if (amount < _singleRoomStandardValue)
                    Price = _singleRoomStandardValue;
            }
            else
            {
                if (amount < _doubleRoomStandardValue)
                    Price = _doubleRoomStandardValue;
            }
        }//SetPrice
        public void UpdateTelephoneNumber(string _number)
        {
            if (Service.IsCellphoneNumberCorrect(_number))
                TelephoneNumber = _number;
        }//UpdateTelephoneNumber
        public override string ToString()
        {
            return String.Format($"{IsSingleRoom};{RoomNumber};{Price.ToString("0.00")};{HasTV};{IsRoomUnderMaintenance}");
        }//ToString

        public string ToCSVFormat()
        {
            string sAmount = this.Price.ToString("0.00");
            if (sAmount.IndexOf(',') >= 0)
                sAmount = sAmount.Replace(',', '.');
            return String.Format($"{this.IsSingleRoom},{this.RoomNumber},{sAmount},{this.HasTV}");
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
        }//HideUnhideRoom
        public bool Equals(IRoom other)
        {
            return this.RoomNumber == other.RoomNumber;
        }
    }//class
}//namespace
