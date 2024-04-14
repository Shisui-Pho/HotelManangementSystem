using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    //Event delegates
    public delegate void delOnRemovedEvent(object sender, HotelEventArgs args);
    public delegate void delOnUpdatedEvent(object old, object @new, HotelEventArgs args);
    public delegate void delOnAddedEvent(HotelEventArgs args);
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
}//namespace
