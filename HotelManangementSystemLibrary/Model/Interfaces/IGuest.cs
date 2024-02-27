using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGuest : IUser
    {
         //To Add Guest Card
         ContactDetails ContactDetails { get; set; }
        void SetEmailAddress(string email);
        void SetCellNumber(string _cellnumber);
        void SetEmergencyNumber(string _emergency);
        void SetContactDetails(ContactDetails details);
    }//IGuest
}//namespace
