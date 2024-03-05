using HotelManangementSystemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementControlLibrary
{
    public delegate void ChangeControl();
    public delegate void BookRoom(IRoom room);//The user will be the current user
    public delegate void CancelBooking(IRoom room);//The user will be the current user
}//namespace
