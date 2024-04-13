using System;
namespace HotelManangementSystemLibrary
{
    internal class RoomBooking : IRoomBooking
    {
        private static int maxDays = 10;
        private static int bookingCount = 500;

        public event delOnPropertyChanged PropertyChangedEvent;

        //Properties
        public string BookingID { get; private set; }
        public IGuest Guest { get; private set; }
        public IRoom Room { get; private set; }
        public IRoomService RoomService { get; private set; }
        public DateTime DateBookedFor { get; private set; }
        public bool IsCheckedIn { get; private set; }
        public int DaysStayed { get; set; }
        public int NumberOfDaysToStay { get; private set; }

        public IBookingFees BookingFee { get; private set; }

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
            BookingFee = new BookingFees(date, Room.Price * numberOfDays);
        }//RoomBooking
        internal void SetBookingID(string _id) => BookingID = _id;
        internal void SetBookingFees(IBookingFees fees) => this.BookingFee = fees;
        public void ChangeBookingDate(DateTime date, int numberOfDays = 1)
        {
            if (numberOfDays > maxDays)
                numberOfDays = maxDays;
            DateBookedFor = date;
            int tempDays = NumberOfDaysToStay;
            NumberOfDaysToStay = numberOfDays;
            PropertyChangedEvent?.Invoke(this.BookingID, "BookedDate", date.ToString("dd/MM/yyyy"));
            if(tempDays != NumberOfDaysToStay)
                PropertyChangedEvent?.Invoke(this.BookingID, "Duration", NumberOfDaysToStay.ToString());
        }//ChangeBooking
        public void ChangeRoom(IRoom room)
        {
            if (room is null)
                return;
            Room = room;
            PropertyChangedEvent?.Invoke(this.BookingID, "RoomNumber", Room.RoomNumber);
        }//ChangeRoom
        public void CheckIn()
        {
            if (IsCheckedIn)
                return;

            IsCheckedIn = true;
            DaysStayed++;
        }//CheckIn
        public bool AssignServicePersonel(IServicePersonel personel)
        {
            if (RoomService != null)
                return false;

            IRoomService Service = new RoomService(this.Room, personel);
            this.RoomService = Service;
            PropertyChangedEvent?.Invoke(this.BookingID, "ServiceID", Service.ServiceID);
            return true;
        }//AddRemoService
        public string ToCSVFormat()
        {
            string sAmounttoPay = Service.ToStringMoney(this.BookingFee.AmoutToPay);
            string sAmountPayed = Service.ToStringMoney(this.BookingFee.AmountPaid);
            string sBookingCost = Service.ToStringMoney(this.BookingFee.BookingCost);
            string serviceID = (RoomService == null) ? "None" : RoomService.Personel.UserID;

            return string.Format($"{this.BookingID},{this.Guest.UserID},{this.Room.RoomNumber}," +
                $"{this.DateBookedFor.ToString("dd/MM/yyyy")},{this.NumberOfDaysToStay.ToString()}," +
                $"{sAmounttoPay},{sAmountPayed},{sBookingCost}, {serviceID}");
        }//ToCSVFormat
        public override string ToString()
        {
            return DateBookedFor.ToString("dd MMMM yyyy");
        }
        public int CompareTo(object obj) =>
            this.BookingID.CompareTo(((IRoomBooking)obj).BookingID);
        public bool Equals(IRoomBooking other) =>
            this.BookingID == other.BookingID;
    }//class
}//namespace