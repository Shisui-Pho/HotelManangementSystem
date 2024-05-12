using System;

namespace HotelManangementSystemLibrary
{
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
}//namespace
