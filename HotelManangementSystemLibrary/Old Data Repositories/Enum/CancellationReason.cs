using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public enum CancellationReason
    {
        Default = 0,
        None,
        Ended,
        Missed,
        Evicted,
        Cannot_Make_It,
        Requirements_Not_Met,
        Other
    }
}
