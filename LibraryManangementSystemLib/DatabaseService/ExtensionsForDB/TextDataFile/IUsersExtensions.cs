using HotelManangementSystemLibrary.Factory;
using System;
using System.IO;
using System.Text;

namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    internal static class IUsersExtensions
    {
        private static readonly string file = "users.csv";

        public static void SaveUsers(this IUsers users)
        {
            if (users is null)
                return;
            StringBuilder bl = new StringBuilder();
            foreach (IUser user in users)
                bl.AppendLine(user.ToCSVFormat());
            File.WriteAllText(file, bl.ToString());
        }//SaveUsers

        public static IUsers LoadUsers(this IUsers users)
        {
            users = UsersFactory.CreateUsers();
            string[] records = Service.CheckFilesExistAndLoadTextData(file);
            if(records.Length <= 0)
            {
                //throw new FileNotFoundException($"The file {file} was not found");
                return users;
            }
            foreach (string record in records)
            {
                string[] fields = record.Split(',');

                TypeOfUser type = (TypeOfUser)Enum.Parse(typeof(TypeOfUser), fields[0]);
                DateTime dt = DateTime.ParseExact(fields[6],"dd/MM/yyyy",null);
                IUser user = UsersFactory.CreateUser(type, fields[4], fields[5],dt , fields[1]);
                user.SetUsername(fields[2]);
                user.SetPassword(fields[3]);
                users.Add(user);
            }//end foreach
            return users;
        }//LoadUsers
    }//class
}//namespace
