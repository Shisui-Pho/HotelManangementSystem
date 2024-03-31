using System;
using System.Windows.Forms;
using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;

namespace HotelManangementControlLibrary.Dashboard.Guest
{
    public partial class GuestBookingsControl : UserControl
    {
        private readonly delOnBookingCancelled BookingCancelled;
        public GuestBookingsControl(delOnBookingCancelled cancelled)
        {
            InitializeComponent();
            BookingCancelled = cancelled;
            //Clear the listBox
            lstbxBookings.Items.Clear();
        }//ctor 
        public void AddBookingToProfile(IRoomBooking booking)
        {
            lstbxBookings.Items.Add(booking);
        }//AddBookingToProfile
        private void lstbxBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbxBookings.SelectedIndex;
            if (index < 0)
                return;

            //Get the roombooking details
            IRoomBooking booking = (IRoomBooking)lstbxBookings.Items[index];

            lblDuration.Text = booking.NumberOfDaysToStay.ToString();
            lblRoomNumber.Text = booking.Room.RoomNumber;
            lblAmountToPay.Text = booking.BookingFee.AmoutToPay.ToString("C2");
            lblRoomType.Text = (booking.Room is ISingleRoom) ? "Single Room" : "Double Room";
        }//lstbxBookings_SelectedIndexChanged
        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            int index = lstbxBookings.SelectedIndex;
            if (index < 0)
            {
                Messages.ShowErrorMessage("No bookings selected");
                return;
            }

            //Need to apply some more business rules for booking cancelation
            //-For now I just cancel it
            IRoomBooking booking = (IRoomBooking)lstbxBookings.Items[index];

            //Cancel the booking
            bool isCancelled = BookingCancelled(booking);

            if (isCancelled)
            {
                //Remove from the listbox
                lstbxBookings.Items.RemoveAt(index);
            }//
        }//btnCancelBooking_Click

        private void btnChangeBooking_Click(object sender, EventArgs e)
        {

        }//btnChangeBooking_Click
    }//class
}//namespace