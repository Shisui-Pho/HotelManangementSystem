using System;
using System.Text;
using System.IO;
using HotelManangementSystemLibrary.Factory;

namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    internal static class IRoomsExtensions
    {
        private static readonly string file ="rooms.csv";
        public static void SaveRooms(this IRooms rooms)
        {
            StringBuilder bl = new StringBuilder();
            foreach (IRoom room in rooms)
                bl.AppendLine(String.Format($"{room.IsSingleRoom};{room.RoomNumber};{room.Price.ToString("0.00")};{room.HasTV}"));
            File.WriteAllText(file, bl.ToString());
        }//SaveRooms
        public static IRooms LoadRooms(this IRooms rooms)
        {
            string[] records = Service.CheckFilesExistAndLoadTextData(file);
            if (records.Length == 0)//Should just return the empty rooms
                throw new FileNotFoundException($"The file {file} was not found");

            rooms = RoomFactory.CreateRooms();
            foreach (string record in records)
            {
                string[] fields = record.Split(';');
                TypeOfRoom _type = (bool.Parse(fields[0]) == true) ? TypeOfRoom.SingleRoom : TypeOfRoom.SharingRoom;
                IRoom room = RoomFactory.CreateRoom(_type, fields[1]);
                if (bool.Parse(fields[3]))
                    room.AddTV();
                room.Price = Service.GetValueOfMoney(fields[2]);
                rooms.Add(room);
            }//end foreach
            return rooms;
        }//LoadRooms
    }//class
}//namespace
