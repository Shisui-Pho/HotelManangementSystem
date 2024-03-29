﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGuest : IUser
    {
         //To Add Guest Card
        IContactDetails ContactDetails { get; set; }
        void SetEmailAddress(string email);
        void SetCellNumber(string _cellnumber);
        void SetEmergencyNumber(string _emergency);
        void SetContactDetails(IContactDetails details);
    }//IGuest
}//namespace
