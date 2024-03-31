using System;
using HotelManangementSystemLibrary;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
namespace HotelManangementSystemUI.Input_Forms
{
    public partial class CdlgCancelBookingGuest : Form
    {
        public bool IsBookingCancelled { get; private set; }
        public CancellationReason Reason { get; private set; }
        public string Other { get; private set; }
        public CdlgCancelBookingGuest(IRoomBooking booking,IUser _logged_in_user)
        {
            InitializeComponent();
            //Set up cotrols
            lblRoomNumber.Text = booking.Room.RoomNumber + ((booking.Room.IsSingleRoom) ? "(Single room)" : "(Double room)");
            lblAmountPaid.Text = booking.BookingFee.AmountPaid.ToString("C2");
            lblBookingPrice.Text = booking.BookingFee.AmoutToPay.ToString("C2");
            lblCancellingAmount.Text = "R0,00";
            lblDuration.Text = booking.NumberOfDaysToStay.ToString();
            lblTimeSpent.Text = booking.DaysStayed.ToString();
            IsBookingCancelled = false;
            btnConfirm.Enabled = false;
            cmboCancellationReason.Items.Clear();
            if(_logged_in_user is IAdministrator)
            {
                cmboCancellationReason.Items.AddRange(new string[]
                {
                    "Ended",
                    "Missed",
                    "Evicted"
                });
            }//
            cmboCancellationReason.Items.AddRange(new string[]
            {
                "Cannot Make It",
                "Requirements Not Met",
                "Rescheduled",
                "Other"
            });//
        }//ctor 01

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CancellationReason reason = (CancellationReason)Enum.Parse(typeof(CancellationReason),
                                        String.Join("_", cmboCancellationReason.Text.Split(new char[] { ' ' }
                                        , StringSplitOptions.RemoveEmptyEntries)));
            if (reason == CancellationReason.Other || reason == CancellationReason.Requirements_Not_Met)
                Other = rtxtOther.Text;
            Reason = reason;
            IsBookingCancelled = true;            
        }//btnConfirm_Click

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsBookingCancelled = false;
        }//

        private void cmboCancellationReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmboCancellationReason.Text == "Other" || cmboCancellationReason.Text == "Requirements Not Met")
            {
                rtxtOther.Visible = true;
                btnConfirm.Enabled = false;
            }
            else
            {
                rtxtOther.Visible = false;
                btnConfirm.Enabled = true;
                rtxtOther.Text = "Please specify";
            }
        }//cmboCancellationReason_SelectedIndexChanged

        private void rtxtOther_TextChanged(object sender, EventArgs e)
        {
            if (rtxtOther.Text.Length > 5 && rtxtOther.Text != "Please specify")
                btnConfirm.Enabled = true;
        }//rtxtOther_TextChanged

        private void rtxtOther_Click(object sender, EventArgs e)
        {
            if (rtxtOther.Text == "Please specify")
                rtxtOther.Text = "";
        }//rtxtOther_Click
    }//class
}//namespace
