using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IHotelModel<T> : IComparable
    {
        event delOnPropertyChanged PropertyChangedEvent;
        string ToCSVFormat();
    }//class
}//namespace
