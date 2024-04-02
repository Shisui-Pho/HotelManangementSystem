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
            if (rooms is null)
                return;
            StringBuilder bl = new StringBuilder();
            foreach (IRoom room in rooms)
            {
                string sAmount = room.Price.ToString("0.00");
                if (sAmount.IndexOf(',') >= 0)
                    sAmount = sAmount.Replace(',', '.');
                bl.AppendLine(String.Format($"{room.IsSingleRoom},{room.RoomNumber},{sAmount},{room.HasTV}"));
            }
            File.WriteAllText(file, bl.ToString());
        }//SaveRooms
        public static IRooms LoadRooms(this IRooms rooms)
        {
            string[] records = Service.CheckFilesExistAndLoadTextData(file);
            rooms = RoomFactory.CreateRooms();
            if (records.Length == 0)//Should just return the empty rooms
            {
                return rooms;
                //throw new FileNotFoundException($"The file {file} was not found");
            }
            foreach (string record in records)
            {
                string[] fields = record.Split(',');
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
