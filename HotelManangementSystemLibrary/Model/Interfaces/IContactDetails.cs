using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IContactDetails
    {
        string EmailAddress { get; set; }
        string CellphoneNumber { get; set; }
        string EmergencyNumber { get; set; }
    }//interface
}//namespace
