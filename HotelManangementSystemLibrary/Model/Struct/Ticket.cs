using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public struct Ticket
    {
        private string userid;
        public string USerID => userid;
        public int TicketID { get; private set; }
        public string Description { get; private set; }
        public Ticket(int id, string description,string userid)
        {
            TicketID = id;
            Description = description;
            this.userid = userid;
        }//ctor 01
        public override string ToString()
        {
            return String.Format($"{TicketID.ToString().PadRight(10)} {Description}");
        }//ToString
    }//clas
}//namespace
