﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBooking
    {
        IGuest Guest { get; }
        IRoom Room { get; }
        DateTime DateBookedFor { get; }
        int NumberOfDaysToStay { get; }
        decimal AmoutToPay { get; }
    }//IRoomBooking
}//namespace
