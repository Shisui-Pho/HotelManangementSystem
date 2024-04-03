using System;
using System.Windows.Forms;
using HotelManangementControlLibrary.Input_Forms;
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
            booking.Guest.Account.BalanceChanged += Account_BalanceChanged;
        }//AddBookingToProfile

        private void Account_BalanceChanged(decimal newBalance, decimal newAmountOwing)
        {
            lblBalance.Text = newBalance.ToString("C2");
            lblDeptAmount.Text = newAmountOwing.ToString("C2");
        }//Account_BalanceChanged

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
            lblBookingCost.Text = booking.BookingFee.BookingCost.ToString("C2");
            lblAmountPaidd.Text = booking.BookingFee.AmountPaid.ToString("C2");
            lblRoomType.Text = (booking.Room is ISingleRoom) ? "Single Room" : "Double Room";
            picRoom.Image = (booking.Room.IsSingleRoom) ? Properties.Resources.single : Properties.Resources._double;
            if (booking.BookingFee.AmoutToPay <= 0)
                btnPayForBooking.Visible = false;
            else
                btnPayForBooking.Visible = true;
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

        private void btnDeposit_Click(object sender, EventArgs e)
        {

        }//btnDeposit_Click

        private void btnPayForBooking_Click(object sender, EventArgs e)
        {
            int index = lstbxBookings.SelectedIndex;
            if(index < 0)
            {
                Messages.ShowErrorMessage("Please select the booking on the list on the left");
                return;
            }
            IRoomBooking booking = (IRoomBooking)lstbxBookings.Items[index];
            CdlgPayBooking makepayment = new CdlgPayBooking(booking);
            if(makepayment.ShowDialog()== DialogResult.OK)
            {
                //lstbxBookings.Refresh();
                lstbxBookings.Items.RemoveAt(index);
                lstbxBookings.Items.Insert(index, booking);
                lstbxBookings.SelectedIndex = index;
            }//
        }//btnPayForBooking_Click
    }//class
}//namespace