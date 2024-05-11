using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class RoomService : IRoomService
    {
        //data member
        private List<Ticket> _tickets;
        private List<ServiceLog> _servicelogs;

        //Events
        public event delOnPropertyChanged PropertyChangedEvent;
        public event delOnServiceLog OnServiceLogging;
        public event delOnTicketAdded OnTicketAdded;

        public string ServiceID { get; private set; }

        public IRoom Room { get; private set; }

        public IServicePersonel Personel { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public IEnumerable<Ticket> Tickets => _tickets;

        public IEnumerable<ServiceLog> ServiveLogs => _servicelogs;
        public RoomService(IRoom room, IServicePersonel personel)
        {
            _tickets = new List<Ticket>();
            _servicelogs = new List<ServiceLog>();
            Room = room;
            Personel = personel;

            //Log the activity
            LogServiceActivity($"Room service created for {personel.UserID}");
        }//ctor 01
        public void ChangeServicePersonel(IServicePersonel personel)
        {
            string oldPersonel = this.Personel.UserID;
            this.Personel = personel;
            //Enable the property changed event 
            PropertyChangedEvent?.Invoke(this.ServiceID, "PersonelID", this.Personel.UserID.ToString());

            //Log the activity
            LogServiceActivity($"Service Personel changed from {oldPersonel} to {personel.UserID}");
        }//ChangeServicePersone
        public void AddTicket(string _description)
        {
            //Create the ticket
            Ticket ticket = new Ticket(_tickets.Count + 1, _description, this.Personel.UserID);//, this.ServiceID);

            //Add it to the list of tickests
            _tickets.Add(ticket);

            //Push the ticket to the database
            OnTicketAdded?.Invoke(ticket,this.ServiceID);

            //Log it to the service log
            LogServiceActivity($"New ticket added: { _description}");
        }//SetDescription
        public void ResolveTicket(int ticketNumber)
        {
            Ticket ticket = _tickets.Find(t => t.TicketID == ticketNumber);
            if(ticket != null)
            {
                //Resolve the ticket
                ticket.Resolve();

                //Raise the property changed event handler
                PropertyChangedEvent?.Invoke(ticket.TicketID.ToString(), "Resolved", "True");

                //Log this to the service log
                LogServiceActivity($"Ticket {ticket.TicketID} resolved");
            }//end if
        }//ResolveTicket
        public void UpdateTicket(int ticketId, string description)
        {
            //Find the ticket
            Ticket _ticket = _tickets.Find(t => ticketId == t.TicketID);

            //Check if the ticket exists
            if(_ticket != null)
            {
                //Update the tick desciption
                _ticket.UpdateTicket(description);

                //Log it in the service log 
                LogServiceActivity($"Ticket {ticketId} updated: {description}");

                //Invoke the property changed event
                PropertyChangedEvent?.Invoke(ticketId.ToString(), "Description", description);
            }//namespace
        }//UpdateTicket
        public void StartService()
        {
            //End the service
            StartTime = DateTime.Now;

            //Envoke the property changed event to push to the database
            PropertyChangedEvent?.Invoke(this.ServiceID, "StartTime", StartTime.ToString());

            //Log the activety to the service log
            LogServiceActivity($"Service started at {StartTime}");
        }//StartService
        public void EndService()
        {
            EndTime = DateTime.Now;

            //Invoke the property changed to push to the database
            PropertyChangedEvent?.Invoke(this.ServiceID, "EndTime", EndTime.ToString());

            //Log to activity
            LogServiceActivity($"Service eneded at {EndTime}");
        }//EndService
        public void LogServiceActivity(string activity)
        {
            //Create a new service log
            ServiceLog log = new ServiceLog(activity, DateTime.Now);

            //Add it to the internal list
            _servicelogs.Add(log);

            //Invoke the service logged event
            OnServiceLogging?.Invoke(new ServiceLogEventArgs(this.ServiceID, activity, log.Timestamp));
        }//LogServiceActivity
        public string ToCSVFormat()
        {
            throw new NotImplementedException();
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.CompareTo((RoomService)obj);
        }//CompareTo
    }//clas
}//namespcae
