using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HotelManangementSystemLibrary.Factory;

namespace HotelManangementSystemLibrary.Utilities.Extensions
{
    internal static class IBookingsExtensions
    {
        private static readonly string file = "bookings.csv";
        public static void SaveBookings(this IRoomBookings bookings)
        {
            if (bookings is null)
                return;
            StringBuilder bl = new StringBuilder();
            foreach (IRoomBooking booking in bookings)
                bl.AppendLine(booking.ToCSVFormat());
            File.WriteAllText(file, bl.ToString());
        }//SaveBooking

        public static IRoomBookings LoadBookings(this IRoomBookings bookings,IGuests guests, IRooms rooms)
        {
            //Somehow this is executed twice due to async call
            if (bookings != null)
                return bookings;
            bookings = BookingsFactory.CreateBookings();

            string[] records = Service.CheckFilesExistAndLoadTextData(file);
            if(records.Length <= 0)
            {
                return bookings;
                //throw new FileNotFoundException($"The file {file} was not found");
            }

            foreach (string record in records)
            {
                string[] fields = record.Split(new char[] { ',','\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
                IRoom room = rooms.FindRoom(fields[2]);
                IGuest guest = guests.FindGuest(fields[1]);

                DateTime date = DateTime.ParseExact(fields[3], "dd/MM/yyyy", null);
                decimal mAmountToPay = Service.GetValueOfMoney(fields[5]);
                decimal mAmountPaid = Service.GetValueOfMoney(fields[6]);
                decimal mCost = Service.GetValueOfMoney(fields[7]);


                IBookingFees fees = new BookingFees(date, mCost, mAmountToPay, mAmountPaid);

                IRoomBooking booking = BookingsFactory.CreateBookingWithFees(guest, room, date, fees,int.Parse(fields[4]));

                bookings.AddExisting(booking);
            }//end foreach

            return bookings;
        }//LoadBookings
        public static void AddExisting(this IRoomBookings bookings, IRoomBooking book)
        {
            bookings.Add(book);
        }
    }//class
}//namespace
