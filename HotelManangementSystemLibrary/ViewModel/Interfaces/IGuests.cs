using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManangementSystemLibrary
{
    public interface IGuests : ICollectionHotel<IGuest>
    {
        IGuest FindGuest(string guestId);
    }//interface
}//namespace
