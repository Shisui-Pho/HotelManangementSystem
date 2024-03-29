using HotelManangementSystemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManangementControlLibrary
{
    //Delegate to passed as paremeters between the main form and the controls
    public delegate void delChangeControl();
    public delegate bool delBookRoom(IRoom room,DateTime date,int numberOfDays = 1);//The user will be the current user
    public delegate void delCancelBooking(IRoom room);//The user will be the current user'
    public delegate IRoom delAddNewRoom();
    public delegate IRoom delModifyRoom(IRoom room);


    //Event delegates
    public delegate void delOnUpdatePassword(string newpassword);
    public delegate void delOnUpdateContactDetails();
    public delegate bool delOnBookingCancelled(IRoomBooking booking);
}//namespace
