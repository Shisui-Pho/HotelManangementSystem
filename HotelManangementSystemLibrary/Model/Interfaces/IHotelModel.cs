using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IHotelModel : IComparable
    {
        string ToCSVFormat();
    }//class
}//namespace
