using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementSystemLibrary
{
    public interface IRoomBookedDate : IEnumerable<DateTime>
    {
        bool HasBookings { get; }
        bool AddBookingDate(DateTime date, int duration);
        bool ChangeBookingDate(DateTime olddate, int oldduration, DateTime newdate, int newduaration);
        bool RemoveBookings(DateTime date, int duration);
    }
}
