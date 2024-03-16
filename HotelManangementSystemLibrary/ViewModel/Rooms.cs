using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class Rooms : IRooms
    {
        private List<IRoom> _rooms;
        public int Count => _rooms.Count;
        private bool isSorted = false;
        public IRoom this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return _rooms[index];
            }
        }//end indexer
        public IRoom this[string roomnumber]
        {
            get => _rooms.FirstOrDefault(r => r.RoomNumber == roomnumber);
            set
            {
                IRoom rr = _rooms.FirstOrDefault(r => r.RoomNumber == roomnumber);
                if (rr != null)
                    rr = value;
            }
        }
        public Rooms()
        {
            _rooms = new List<IRoom>();
        }//ctor 01

        public Rooms(List<IRoom> rooms)
        {
            _rooms = rooms;
        }//ctor 02

        public Rooms(IRoom[] rooms)
        {
            _rooms = new List<IRoom>();
            _rooms.AddRange(rooms);
        }//ctor 03

        public void Add(IRoom room)
           => _rooms.Add(room);

        public void Remove(IRoom room)
            => _rooms.Remove(room);

        public void Update(IRoom old, IRoom _new)
        {
            int i = _rooms.FindIndex(rm => rm.RoomNumber == old.RoomNumber);
            if (i < 0)
                throw new ArgumentException($"The room number \"{old.RoomNumber}\" was not found");
            _rooms[i] = _new;
        }//Update
        public IEnumerator<IRoom> GetEnumerator()
        {
            foreach (IRoom room in _rooms)
            {
                yield return room;
            }
        }//GetEnumerator

        public IRoom FindRoom(string roomNumber)
        {
            return _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }//FindRoom

        public List<T> GetRoomFilter<T>()
        {
            return _rooms.Where(r => r is T)
                .Select(room => (T)room)
                .ToList();
        }//GetRoomFilter

        public void BatchSort()
        {
            if (!isSorted)
            {
                isSorted = true;
                _rooms.Sort();
            }//end if             
        }//BatchSort
    }//Rooms
}//namespace
