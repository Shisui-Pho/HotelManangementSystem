using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelManangementSystemLibrary
{
    internal class Rooms : GeneralCollection<IRoom>, IRooms
    {
        public IRoom this[string roomnumber]
        {
            get => base._collection.FirstOrDefault(r => r.RoomNumber == roomnumber);
            set
            {
                IRoom rr = base._collection.FirstOrDefault(r => r.RoomNumber == roomnumber);
                if (rr != null)
                    rr = value;
            }
        }//end indexer
        #region Contructors
        public Rooms() : base()
        {
        }//ctor 01
        public Rooms(List<IRoom> rooms) : base(rooms)
        {
        }//ctor 02
        public Rooms(IRoom[] rooms) : base(rooms)
        {
        }//ctor 03
        #endregion
        public IRoom FindRoom(string roomNumber)
        {
            return base._collection.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }//FindRoom

        public List<T> GetRoomFilter<T>()
        {
            return base._collection.Where(r => r is T)
                .Select(room => (T)room)
                .ToList();
        }//GetRoomFilter
    }//Rooms
}//namespace
