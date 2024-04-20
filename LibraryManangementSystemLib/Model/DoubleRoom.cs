namespace HotelManangementSystemLibrary
{
    internal class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber) : base(_roomNumber)
        {
            IsSingleRoom = false;

            Price = Standard.DoubleRoomPrice;
        }//ctor

        public bool Equals(IDoubleRoom other)
        {
            return base.Equals(other);
        }
    }//DoubleRoom
}//namespace
