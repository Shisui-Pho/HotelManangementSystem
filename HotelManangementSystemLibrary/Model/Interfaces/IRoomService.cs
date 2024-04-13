﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoomService : IHotelModel<IRoomService>
    {
        string ServiceID { get; }
        IRoom Room { get; }
        IServicePersonel Personel { get; }
        DateTime DateAndTime { get; }
        Ticket? this[int index] { get; }
        void AddTicket(string _description);
        IEnumerator<Ticket> GetEnumerator();
        void ChangeServicePersonel(IServicePersonel personel);
    }//interface
}//namespace
