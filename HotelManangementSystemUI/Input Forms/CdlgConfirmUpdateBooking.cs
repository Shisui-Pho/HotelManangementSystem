using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.Factory;
using System;
using System.Windows.Forms;

namespace HotelManangementSystemUI.Input_Forms
{
    public partial class CdlgConfirmUpdateBooking : Form
    {
        private readonly IGuest guest;
        private readonly IRoom room;
        public IRoomBooking RoomBooking { get; private set; }

        public CdlgConfirmUpdateBooking(IGuest guest, IRoom room, DateTime dt, int days)
        {
            InitializeComponent();
            numBookingLength.Value = days;
            dtBookDate.Value = dt;
            lblRoom.Text = room.RoomNumber + "(" + ((room.IsSingleRoom) ? "Single room)" : "Double room)");
            this.guest = guest;
            this.room = room;
        }//ctor 01

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            RoomBooking = BookingsFactory.CreateBooking(guest, room, dtBookDate.Value, (int)numBookingLength.Value);
        }//btnConfirm_Click

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }//btnCancel_Click
    }//class
}//namespace
