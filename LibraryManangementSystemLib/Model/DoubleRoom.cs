namespace HotelManangementSystemLibrary
{
    internal class DoubleRoom : Room , IDoubleRoom
    {
        public DoubleRoom(string _roomNumber, IRoomFeatures features, IRoomBookedDate bookedDates) 
            : base(_roomNumber,features,bookedDates)
        {
            IsSingleRoom = false;

            Price = Standard.DoubleRoomPrice;
        }//ctor
    }//DoubleRoom
}//namespace
