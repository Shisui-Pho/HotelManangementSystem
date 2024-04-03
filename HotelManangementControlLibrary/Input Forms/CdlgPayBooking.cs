using HotelManangementSystemLibrary;
using System;
using System.Windows.Forms;

namespace HotelManangementControlLibrary.Input_Forms
{
    public partial class CdlgPayBooking : Form
    {
        private readonly IRoomBooking _booking;
        private decimal Amount;
        public CdlgPayBooking(IRoomBooking booking)
        {
            InitializeComponent();
            _booking = booking;
            lblAmountToPay.Text = booking.BookingFee.AmoutToPay.ToString("C2");
            lblBalance.Text = booking.Guest.Account.CurrentBalance.ToString("C2");
        }//ctor 01

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            Amount = numAmount.Value;
            if (Amount > _booking.Guest.Account.CurrentBalance)
            {
                lblMessage.Text = "Cannot make payment\nplease deposit amount";
                btnPay.Visible = false;
            }
            else if (Amount > _booking.BookingFee.AmoutToPay)
            {
                lblMessage.Text = "Amount exceeded booking cost";
                btnPay.Visible = false;
            }
            else
            {
                lblMessage.Text = "";
                btnPay.Visible = true;
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            //We don't need the returned values for now since fefensive programming has bee implemented
            _ = _booking.Guest.Account.PayForBooking(Amount);
            _booking.BookingFee.PayForBooking(Amount, out _);
        }//btnPay_Click
    }//class
}//namespace