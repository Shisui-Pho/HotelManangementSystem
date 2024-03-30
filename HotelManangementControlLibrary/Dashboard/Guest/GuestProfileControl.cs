using System;
using System.Windows.Forms;
using HotelManangementSystemLibrary;
using UIServiceLibrary.Evaluations;
using HotelManangementControlLibrary.Service;
namespace HotelManangementControlLibrary.Dashboard.Guest
{
    public partial class GuestProfileControl : UserControl
    {
        //Data member
        private readonly IGuest _guest;
        private readonly delOnBookingCancelled BookingCancelled;
        //event
        public event delOnUpdatePassword PasswordChanged;
        public GuestProfileControl(IGuest guest,delOnBookingCancelled cancelled)
        {
            InitializeComponent();
            _guest = guest;
            BookingCancelled = cancelled;
            //Set up controls
            SetControls();
        }//ctor main
        public GuestProfileControl(IGuest guest)
        {
            InitializeComponent();
            _guest = guest;
            //Set up controls
            SetControls();
        }//ctor main
        public GuestProfileControl() 
        {
            InitializeComponent();
        }
        private void SetControls()
        {
            //Load user information
            txtName.Text = _guest.Name;
            txtSurname.Text = _guest.Surname;
            txtUsername.Text = _guest.UserName;
            lblAge.Text = _guest.Age.ToString();
            lblDOB.Text = _guest.DOB.ToString("dd/MMMM/yyyy");
            lblGender.Text = "Male";

            //Contact details
            txtCellphoneNumber.Text = _guest.ContactDetails.CellphoneNumber;
            txtEmailAddress.Text = _guest.ContactDetails.EmailAddress;
            txtEmergencyNumber.Text = _guest.ContactDetails.EmergencyNumber;

            //Clear the listBox
            lstbxBookings.Items.Clear();
        }//SetControls

        //This will be called form the main form
        public void AddBookingToProfile(IRoomBooking booking)
        {
            lstbxBookings.Items.Add(booking);
        }//AddBookingToProfile
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        { 
            if(!txtxPassword.Text.IsPasswordAllowed(out string exeption))
            {
                //Need some work
                Messages.ShowErrorMessage(exeption);
                return;
            }//end if
            if(txtxPassword.Text != txtConfirmPassword.Text)
            {
                Messages.ShowErrorMessage("Passwords not matching!!");
                return;
            }//end if

            //Trigger event
            PasswordChanged?.Invoke(txtxPassword.Text);

            //Update the user password
            _guest.SetPassword(txtxPassword.Text);

            Messages.ShowInformationMessage("Password changed successfully", "Pasword Change");
        }//btnUpdatePassword_Click

        private void btnUpadateContactDetails_Click(object sender, EventArgs e)
        {
            if(txtEmailAddress.Text.Length > 1 && txtEmailAddress.Text != "None")
            {
                if (Inputs.IsEmailCorrect(txtEmailAddress.Text))
                    _guest.SetEmailAddress(txtEmailAddress.Text);
                else
                {
                    Messages.ShowErrorMessage("Email not in the right format");
                    return;
                }//end else
            }//end if email address
            if(txtEmergencyNumber.Text.Length > 1 && txtEmergencyNumber.Text != "None")
            {
                if (Inputs.IsCellphoneNumberCorrect(txtEmergencyNumber.Text))
                    _guest.SetEmergencyNumber(txtEmergencyNumber.Text);
                else
                {
                    Messages.ShowErrorMessage("Emergency number is not in the correct format.");
                    return;
                }//end else
            }//end if emergency number
            if (txtCellphoneNumber.Text.Length > 1 && txtCellphoneNumber.Text != "None")
            {
                if (Inputs.IsCellphoneNumberCorrect(txtCellphoneNumber.Text))
                    _guest.SetCellNumber(txtCellphoneNumber.Text);
                else
                {
                    Messages.ShowErrorMessage("Cellphone number is not in the correct format.");
                    return;
                }//end ese
            }//end if cellephone number
            Messages.ShowInformationMessage("Contact details has been updated succsessfully", "Update Complete");
        }//btnUpadateContactDetails_Click

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

        private void lstbxBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbxBookings.SelectedIndex;
            if (index < 0)
                return;

            //Get the roombooking details
            IRoomBooking booking = (IRoomBooking)lstbxBookings.Items[index];

            lblDuration.Text = booking.NumberOfDaysToStay.ToString();
            lblRoomNumber.Text = booking.Room.RoomNumber;
            lblAmountToPay.Text = booking.AmoutToPay.ToString("C2");
            lblRoomType.Text = (booking.Room is ISingleRoom) ? "Single Room" : "Double Room";
        }//lstbxBookings_SelectedIndexChanged
    }//class
}//namespace
