﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class RoomService : IRoomService
    {
        //data member
        private List<Ticket> _tickets;
        public string ServiceID { get; private set; }

        public IRoom Room { get; private set; }

        public IServicePersonel Personel { get; private set; }

        public DateTime DateAndTime { get; private set; }

        public Ticket? this[int index]
        {
            get
            {
                if (index >= _tickets.Count)
                    return null;
                return _tickets[index];
            }//
        }//end indexer of tickets

        public RoomService(IRoom room, IServicePersonel personel)
        {
            _tickets = new List<Ticket>();
            Room = room;
            Personel = personel;
        }//ctor 01

        public void AddTicket(string _description)
        {
            Ticket ticket = new Ticket(_tickets.Count + 1, _description,this.Personel.UserID);
            _tickets.Add(ticket);
        }//SetDescription

        public IEnumerator<Ticket> GetEnumerator()
        {
            foreach (Ticket ticket in _tickets)
            {
                yield return ticket;
            }//end foreach
        }//GetEnumerator
        public void ChangeServicePersonel(IServicePersonel personel)
        {
            this.Personel = personel;
        }//ChangeServicePersonel
    }//clas
}//namespcae
