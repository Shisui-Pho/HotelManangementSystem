using System;

namespace HotelManangementSystemLibrary
{
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
