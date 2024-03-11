using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //event
        public event delOnUpdatePassword PasswordChanged;
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
            txtName.Text = _guest.Name;
            txtSurname.Text = _guest.Surname;
            txtUsername.Text = _guest.UserName;
            lblAge.Text = _guest.Age.ToString();
            lblDOB.Text = _guest.DOB.ToString("dd/MMMM/yyyy");
            lblGender.Text = "Male";
        }//SetControls
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
        }//btnUpdatePassword_Click

        private void btnUpadateContactDetails_Click(object sender, EventArgs e)
        {
            if(txtEmailAddress.Text.Length > 1)
            {
                if (Inputs.IsEmailCorrect(txtEmailAddress.Text))
                    _guest.SetEmailAddress(txtEmailAddress.Text);
                else
                {
                    Messages.ShowErrorMessage("Email not in the right format");
                    return;
                }//end else
            }//end if email address
            if(txtEmergencyNumber.Text.Length > 1)
            {
                if (Inputs.IsCellphoneNumberCorrect(txtEmergencyNumber.Text))
                    _guest.SetEmergencyNumber(txtEmergencyNumber.Text);
                else
                {
                    Messages.ShowErrorMessage("Emergency number is not in the correct format.");
                    return;
                }//end else
            }//end if emergency number
            if (txtCellphoneNumber.Text.Length > 1)
            {
                if (Inputs.IsCellphoneNumberCorrect(txtCellphoneNumber.Text))
                    _guest.SetEmergencyNumber(txtCellphoneNumber.Text);
                else
                {
                    Messages.ShowErrorMessage("Cellphone number is not in the correct format.");
                    return;
                }//end ese
            }//end if cellephone number
        }//btnUpadateContactDetails_Click
    }//class
}//namespace
