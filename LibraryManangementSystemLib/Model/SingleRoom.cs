namespace HotelManangementSystemLibrary
{
    internal class SingleRoom : Room , ISingleRoom
    {
        public SingleRoom(string _roomNumber, IRoomFeatures features, IRoomBookedDate bookedDates)
            : base(_roomNumber, features, bookedDates)
        {
            IsSingleRoom = true;
            Price = Standard.SingleRoomPrice;
        }//ctor
        public bool Equals(ISingleRoom other)
        {
            return base.Equals(other);
        }
    }//Singleroom
}//namespace
