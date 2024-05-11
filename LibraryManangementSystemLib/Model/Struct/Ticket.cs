using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public class Ticket
    {
        private string userid;
        public int TicketID { get; private set; }
        public string AssignedPersonelID => userid;
        public string Description { get; private set; }
        public bool IsResolved { get; private set; }
        public Ticket(int id, string description,string userid)
        {
            TicketID = id;
            Description = description;
            this.userid = userid;
            IsResolved = false;
        }//ctor 01
        internal void UpdateTicket(string description)
        {
            Description = description;
        }//UpdateTicket
        internal void Resolve()
        {
            IsResolved = true;
        }//Resolve
        public override string ToString()
        {
            return String.Format($"{TicketID.ToString().PadRight(10)} {Description}");
        }//ToString
    }//clas
}//namespace
