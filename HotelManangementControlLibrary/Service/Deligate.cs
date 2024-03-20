using HotelManangementSystemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementControlLibrary
{
    public delegate void delChangeControl();
    public delegate void delBookRoom(IRoom room);//The user will be the current user
    public delegate void delCancelBooking(IRoom room);//The user will be the current user'
    public delegate IRoom delAddNewRoom();
    public delegate IRoom delModifyRoom(IRoom room);


    //Event delegates
    public delegate void delOnUpdatePassword(string newpassword);
    public delegate void delOnUpdateContactDetails();
    public delegate void delOnBookingCancelled(IRoomBooking booking);
}//namespace
