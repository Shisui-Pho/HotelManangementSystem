using System;

namespace HotelManangementSystemLibrary
{
    public interface IOldBooking : IComparable, IEquatable<IOldBooking>
    {
        IRoomBooking Booking { get; }
        CancellationReason? State { get; }
        string StateString { get; }
    }//
}//namespace
