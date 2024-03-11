using HotelManangementSystemLibrary.Factory;
using System.Collections.Generic;
using System.Linq;
namespace HotelManangementSystemLibrary
{
    internal class Guests : IGuests
    {
        private List<IGuest> _guests;
        public int Count => _guests.Count;
        public Guests()
        {
            _guests = new List<IGuest>();
        }

        public void Add(IGuest guest)
        {
            if (Exists(guest))
                return;
            _guests.Add(guest);
        }//Add

        public void Remove(IGuest guest)
        {
            if (Exists(guest))
                return;
            _guests.Remove(guest);
        }//Remove

        public void Update(IGuest old, IGuest _new)
        {
            int i = _guests.IndexOf(old);
            if (i < 0)
                throw new KeyNotFoundException();
            _guests[i] = _new;
        }//Update
        public IEnumerator<IGuest> GetEnumerator()
        {
            foreach (IGuest guest in _guests)
                yield return guest;
        }//GetEnumerator

        public IGuest FindGuest(string guestId)
        {
            return _guests.FirstOrDefault(g => g.UserID == guestId);
        }//FindGuest
        private bool Exists(IGuest guest)
        {
            return _guests.Exists(gs => guest.UserID == gs.UserID);
        }//Exists

        public IGuest FindGuest(IUser user)
        {
            if (!(user is IGuest))
                throw new System.ArgumentException($"Cannot retrieve profile of {user.UserType.ToString()}");
            
            int i = _guests.FindIndex(gs => gs.UserID == user.UserID);
            //If the guest exists
            if (i >= 0)
                return _guests[i];
            //if guest does not exist
            //-Create a new guest profile
            IGuest guest = UsersFactory.CreateGuest(user);
            Add(guest);
            return guest;
        }//FindGuest
    }//class
}//namespace
