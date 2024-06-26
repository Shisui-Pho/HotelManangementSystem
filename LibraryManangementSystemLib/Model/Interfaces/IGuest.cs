﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGuest : IUser, IHotelModel<IGuest>
    {
         //To Add Guest Card
        IContactDetails ContactDetails { get; }
        IUSerAccount Account { get; }
        event delOnPropertyChanged GuestPropertyChangedEvent;
        event delBalanceChanged BalanceChangedEvent;
        void SetEmailAddress(string email);
        void SetCellNumber(string _cellnumber);
        void SetEmergencyNumber(string _emergency);
        void SetContactDetails(IContactDetails details);
        string ToGuestCSVFormat();
    }//IGuest
}//namespace
