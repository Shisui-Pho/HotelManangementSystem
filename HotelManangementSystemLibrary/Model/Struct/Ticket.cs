using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public struct Ticket
    {
        public int TicketID { get; private set; }
        public string Description { get; private set; }
        public Ticket(int id, string description)
        {
            TicketID = id;
            Description = description;
        }//ctor 01
    }//clas
}//namespace
