﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IHotelModel<T> : IComparable, IEquatable<T>
    {
        string ToCSVFormat();
    }//class
}//namespace
