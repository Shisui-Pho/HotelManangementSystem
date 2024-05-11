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
    public delegate void delOnServiceLog(ServiceLogEventArgs args);
    public delegate void delOnTicketAdded(Ticket ticket,string serviceid);
    public delegate bool delUserExceptionEvent(AlertUserEvent args);
    //Event Class
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
    }//HotelEventArgs
    public class FeatureEventArgs : EventArgs
    {
        public string RoomNumber { get; set; }
    }//FeatureEventArgs
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
    }//BookingFeesChangedEventArgs
    public class ServiceLogEventArgs : EventArgs
    {
        public string RoomServiceID { get; private set; }
        public string Activity { get; private set; }
        public DateTime TimeStamp { get; private set; }
        internal ServiceLogEventArgs(string serviceid, string activity, DateTime date)
        {
            this.RoomServiceID = serviceid;
            this.Activity = activity;
            this.TimeStamp = date;
        }//ctor
    }//ServiceLogEventArgs
    public class AlertUserEvent : EventArgs
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public bool Handled { get; set; }
        public AlertUserEvent(string message, string title)
        {
            this.Message = message;
        }//ctor main
    }//AlertUserEvent
}//namespace
