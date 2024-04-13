using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class RoomService : IRoomService
    {
        //data member
        private List<Ticket> _tickets;

        public event delOnPropertyChanged PropertyChangedEvent;

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
            //PropertyChangedEvent?.Invoke(this.ServiceID, "TicketID", ticket.TicketID.ToString());
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
            PropertyChangedEvent?.Invoke(this.ServiceID, "PersonelID", this.Personel.UserID.ToString());
        }//ChangeServicePersonel

        public string ToCSVFormat()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IRoomService other)
        {
            throw new NotImplementedException();
        }
    }//clas
}//namespcae
