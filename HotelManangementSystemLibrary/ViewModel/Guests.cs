using HotelManangementSystemLibrary.Factory;
using System.Collections.Generic;
using System.Linq;
using System;
namespace HotelManangementSystemLibrary
{
    internal class Guests : GeneralCollection<IGuest>, IGuests
    {
        public Guests() : base() { }//ctor 01
        public Guests(IGuest[] guests) : base(guests) { }//ctor 02
        public Guests(List<IGuest> guests) : base(guests) { }//ctor 03
        public IGuest FindGuest(string guestId)
        {
            return base._collection.FirstOrDefault(g => g.UserID == guestId);
        }//FindGuest
        private bool Exists(IGuest guest)
        {
            return base._collection.Exists(gs => guest.UserID == gs.UserID);
        }//Exists

        public IGuest FindGuest(IUser user)
        {
            if (!(user is IGuest))
                throw new System.ArgumentException($"Cannot retrieve profile of {user.UserType.ToString()}");
            
            int i = base._collection.FindIndex(gs => gs.UserID == user.UserID);
            //If the guest exists
            if (i >= 0)
                return base._collection[i];
            //if guest does not exist
            //-Create a new guest profile
            IGuest guest = UsersFactory.CreateGuest(user);
            Add(guest);
            return guest;
        }//FindGuest
    }//class
}//namespace
