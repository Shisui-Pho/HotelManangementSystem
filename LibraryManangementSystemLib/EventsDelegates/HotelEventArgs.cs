using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    //Event delegates
    public delegate void delOnRemovedEvent(object sender, HotelEventArgs args);
    public delegate void delOnUpdatedEvent(object old, object @new, HotelEventArgs args);
    public delegate void delOnAddedEvent(HotelEventArgs args);
    public delegate void delOnPropertyChanged(string id, string field, string newVal);
    public delegate void delOnPriceChanged(IRoom room);
    internal delegate void delOnFeatureRemoved(string roomNumber, string FeatureID);
    public delegate void delOnFeaturesModified(IFeature feature, bool isAdded, FeatureEventArgs args);
    public delegate void delOnTrasnaction(TransactionArgs transaction);
    public delegate void delOnBalanceChanged(decimal newBalance, decimal newAmountOwing);
    public delegate void delBalanceChanged(BalanceChangedEventArgs args);
    public delegate void delOnBookingFeesChanged(BookingFeesChangedEventArgs args);

    public class HotelEventArgs : EventArgs
    {
        public bool IsHandled { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public HotelEventArgs(string name,string description)
        {
            Name = name;
            Description = description;
        }//HotelEventArgs
    }//class
    public class FeatureEventArgs : EventArgs
    {
        public string RoomNumber { get; set; }
    }
    public class BalanceChangedEventArgs : EventArgs
    {
        public decimal CurrentBalance { get; private set; }
        public decimal AmountOwing { get; private set; }
        public string AccountUserID { get; private set; }
        public BalanceChangedEventArgs(decimal current,decimal amountowing, string userid)
        {
            this.CurrentBalance = current;
            this.AmountOwing = amountowing;
            this.AccountUserID = userid;
        }//ctor 01
    }//BalanceChangedEvenArgs
    public class BookingFeesChangedEventArgs : EventArgs
    {
        public decimal AmountPaid { get; private set; }
        public decimal AmountToPay { get; private set; }
        public string BookingID { get; internal set; }
        public BookingFeesChangedEventArgs(decimal paid,decimal topay,string id)
        {
            this.AmountPaid = paid;
            this.AmountToPay = topay;
            this.BookingID = id;
        }//ctor 
    }//ctor 01
}//namespace
