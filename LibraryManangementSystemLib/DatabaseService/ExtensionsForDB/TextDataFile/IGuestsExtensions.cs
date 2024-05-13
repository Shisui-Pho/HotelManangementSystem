using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HotelManangementSystemLibrary.Factory;

namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    internal static class IGuestsExtensions
    {
        private static readonly string file = "guests.csv";

        public static void SaveGuests(this IGuests guests)
        {
            if (guests is null)
                return;
            StringBuilder bl = new StringBuilder();
            foreach (IGuest guest in guests)
            {
                bl.AppendLine(guest.ToGuestCSVFormat());
            }
            File.WriteAllText(file, bl.ToString());
        }//SaveGuests

        public static IGuests LoadGuests(this IGuests guests, IUsers users)
        {
            guests = UsersFactory.CreateGuests();
            string[] records = Service.CheckFilesExistAndLoadTextData(file);
            if (records.Length <= 0)
            {
                return guests;
                //throw new FileNotFoundException($"The file {file} was not found");
            }

            foreach (string record in records)
            {
                string[] fields = record.Split(',');
                IUser user = users.GetUser(fields[0]);

                decimal mBalance = Service.GetValueOfMoney(fields[4]);
                decimal mDept = Service.GetValueOfMoney(fields[5]);
                IUSerAccount account = UsersFactory.CreateUserAccount(mBalance, mDept, "");
                IContactDetails det = UsersFactory.CreateContactDetails(fields[2], fields[1], fields[3]);
                IGuest guest = UsersFactory.CreateGuest(user,det,account);
                guests.Add(guest);
            }//end foreach
            return guests;
        }//LoadGuests
    }//class
}//namespace
