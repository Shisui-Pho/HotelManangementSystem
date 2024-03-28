using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class ContactDetails : IContactDetails
    {
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }

        //More info need to be added
        public string EmergencyNumber { get; set; }
        public ContactDetails(string _email, string _cell, string _emergency)
        {
            EmailAddress = _email;
            CellphoneNumber = _cell;
            EmergencyNumber = _emergency;
        }//ctor
        public ContactDetails() { }
    }//class
}//namespace
