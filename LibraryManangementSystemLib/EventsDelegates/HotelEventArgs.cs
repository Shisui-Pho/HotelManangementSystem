using System;
namespace HotelManangementSystemLibrary
{
    public class HotelEventArgs : EventArgs
    {
        public bool IsHandled { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public HotelEventArgs(string name, string description)
        {
            Name = name;
            Description = description;
        }//HotelEventArgs
    }//HotelEventArgs
}//namespace