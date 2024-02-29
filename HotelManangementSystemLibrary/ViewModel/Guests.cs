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
            => _guests.Add(guest);

        public void Remove(IGuest guest)
            => _guests.Remove(guest);

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
    }//class
}//namespace
