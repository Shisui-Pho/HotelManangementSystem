using HotelManangementSystemLibrary.Factory;
using System.Collections.Generic;
using System.Linq;
using System;
using HotelManangementSystemLibrary.Logging;

namespace HotelManangementSystemLibrary
{
    internal class Guests : GeneralCollection<IGuest>, IGuests
    {
        public IGuest CurrentGuest { get; private set; }

        public Guests() : base() { }//ctor 01
        public Guests(IGuest[] guests) : base(guests) { }//ctor 02
        public Guests(List<IGuest> guests) : base(guests) { }//ctor 03
        public IGuest FindGuest(string guestId)
        {
            CurrentGuest = base._collection.FirstOrDefault(g => g.UserID == guestId);
            return CurrentGuest;
        }//FindGuest
        private bool Exists(IGuest guest)
        {
            return base._collection.Exists(gs => guest.UserID == gs.UserID);
        }//Exists

        public IGuest FindGuest(IUser user)
        {
            if (!(user is IGuest))
            {
                var ex = new System.ArgumentException($"Cannot retrieve profile of {user.UserType.ToString()}");
                ExceptionLog.GetLogger().LogActivity(ex, ErrorServerity.Fetal, TypeOfError.DatabaseError);
                throw ex;
            }

            int i = base._collection.FindIndex(gs => gs.UserID == user.UserID);
            CurrentGuest = base._collection[i];
            //If the guest exists
            if (i >= 0)
                return CurrentGuest;
            //if guest does not exist
            //-Create a new guest profile
            IUSerAccount account = UsersFactory.CreateUserAccount();
            IContactDetails contacts = UsersFactory.CreateContactDetails();
            IGuest guest = UsersFactory.CreateGuest(user,contacts,account);
            Add(guest);
            CurrentGuest = guest;
            return CurrentGuest;
        }//FindGuest
    }//class
}//namespace
