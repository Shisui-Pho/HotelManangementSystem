namespace HotelManangementSystemLibrary
{
    internal class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber, bool hasTv = false) : base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = false;

            Price = _doubleRoomStandardValue + (hasTv ? _entertainments : 0);
        }//ctor

        public bool Equals(IDoubleRoom other)
        {
            return base.Equals(other);
        }
    }//DoubleRoom
}//namespace
