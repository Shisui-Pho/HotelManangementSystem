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
            StringBuilder bl = new StringBuilder();
            foreach (IGuest guest in guests)
            {
                IContactDetails det = guest.ContactDetails;
                bl.AppendLine(String.Format($"{guest.UserID};{det.CellphoneNumber};{det.EmailAddress};{det.EmergencyNumber}"));
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
                string[] fields = record.Split(';');
                IUser user = users.GetUser(fields[0]);
                IGuest guest = UsersFactory.CreateGuest(user);
                guest.SetCellNumber(fields[1]);
                guest.SetEmailAddress(fields[2]);
                guest.SetEmergencyNumber(fields[3]);
                guests.Add(guest);
            }//end foreach
            return guests;
        }//LoadGuests
    }//class
}//namespace
