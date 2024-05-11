using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IRoomService : IHotelModel<IRoomService>
    {
        string ServiceID { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        IRoom Room { get; }
        IServicePersonel Personel { get; }
        IEnumerable<Ticket> Tickets { get; }
        IEnumerable<ServiceLog> ServiveLogs { get; }
        Ticket this[int index] { get; }
        void ChangeServicePersonel(IServicePersonel personel);
        void LogServiceActivity(string activity);
        void AddTicket(string _description);
        void UpdateTicket(int ticketId, string description);
        void ResolveTicket(int ticketNumber);
        void StartService();
        void EndService();
    }//interface
}//namespace
