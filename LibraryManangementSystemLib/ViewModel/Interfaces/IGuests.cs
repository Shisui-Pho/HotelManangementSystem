using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGuests : IGeneralCollection<IGuest>
    {
        IGuest CurrentGuest { get;}
        IGuest FindGuest(string guestId);
        IGuest FindGuest(IUser user);
    }//interface
}//namespace
