using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IHotelModel<T> : IComparable, IEquatable<T>
    {
        event delOnPropertyChanged PropertyChangedEvent;
        string ToCSVFormat();
    }//class
}//namespace
