namespace HotelManangementSystemLibrary
{
    internal class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber, bool hasTv = false) : base(_roomNumber)
        {
            HasTV = hasTv;
            IsSingleRoom = false;

            Price = _doubleRoomStandardValue;
        }//ctor

        public bool Equals(IDoubleRoom other)
        {
            return base.Equals(other);
        }
    }//DoubleRoom
}//namespace
