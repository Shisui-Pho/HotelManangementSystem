using System;
namespace HotelManangementSystemLibrary
{
    internal class RoomBooking : IRoomBooking
    {
        //Data member
        private decimal _amount;
        private static int maxDays = 10;
        private static int bookingCount = 500;
        public static double RefundRate = 10d;
        //Properties
        public string BookingID { get; private set; }
        public IGuest Guest { get; private set; }

        public IRoom Room { get; private set; }

        public DateTime DateBookedFor { get; private set; }

        public bool IsCheckedIn { get; private set; }
        public int DaysStayed { get; set; }
        public int NumberOfDaysToStay { get; private set; }

        public decimal BookingCost { get; private set; }
        public decimal AmoutToPay { get; private set; }
        public decimal AmountPaid { get; private set; }

        public RoomBooking(IGuest guest, IRoom room, DateTime date, int numberOfDays = 1)
        {
            Guest = guest;
            Room = room;
            //Make sure guest cannot book for a past date
            if (DateTime.UtcNow > date)
                throw new ArgumentException("Cannot book on this date!.");
            DateBookedFor = date;
            NumberOfDaysToStay = numberOfDays;
            DaysStayed = 0;
            IsCheckedIn = false;

            bookingCount += 50;
            BookingID = bookingCount.ToString();
            //for amount
            _amount = Room.Price * numberOfDays;
            AmountPaid = 0;
            AmoutToPay = _amount;
            BookingCost = _amount;
        }//RoomBooking

        public RoomBooking()
        {

        }//

        internal void SetBookingID(string _id) => BookingID = _id;

        public void ChangeBookingDate(DateTime date, int numberOfDays = 1)
        {
            if (numberOfDays > maxDays)
                numberOfDays = maxDays;
            DateBookedFor = date;
            NumberOfDaysToStay = numberOfDays;
        }//ChangeBooking
        public override string ToString()
        {
            return DateBookedFor.ToString("dd MMMM yyyy"); //Room.RoomNumber + "\t" + Guest.Name;
        }

        public void ChangeRoom(IRoom room)
        {
            if (room is null)
                return;
            Room = room;
        }//ChangeRoom

        public string ToCSVFormat()
        {
            return string.Format($"{BookingID};{Guest.UserID};{Room.RoomNumber};{DateBookedFor.ToString("dd/MM/yyyy")};{NumberOfDaysToStay.ToString()}");
        }//ToCSVFormat

        public int CompareTo(object obj)
        {
            return this.BookingID.CompareTo(((IRoomBooking)obj).BookingID);
        }//CompareTo

        public void CheckIn()
        {
            if (IsCheckedIn)
                return;

            IsCheckedIn = true;
            DaysStayed++;
        }//CheckIn
        
        public bool PayForBooking(decimal amount, out decimal change)
        {
            change = 0;
            //if the amount is less thank the acceptable 
            if (amount < 0)
                return false;
            if (amount >= AmoutToPay)
            {
                change = amount - AmoutToPay;
                AmoutToPay = 0m;
                AmountPaid = BookingCost;
            }
            else
            {
                AmoutToPay -= amount;
                AmountPaid += amount;
            }
                
            return true;
        }//PayForBooking

        public decimal GetRefundAmount()
        {
            if(DateTime.Now.AddDays(-2) <= DateBookedFor)
                return 0m;

            decimal amount = AmountPaid- GetCancellationFee();

            if (amount <= 0)
                return 0m;

            return amount;
        }//GetRefundAmount

        public decimal GetCancellationFee()
        {
            if (DateTime.Now.AddDays(-2) <= DateBookedFor)
                return BookingCost;

            return BookingCost * (decimal)(BookingCost / 100);
        }//GetCancellationFee
    }//class
}//namespace
