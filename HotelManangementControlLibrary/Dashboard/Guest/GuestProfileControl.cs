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
        //event
        public event delOnUpdatePassword PasswordChanged;
        public GuestProfileControl(IGuest guest)
        {
            InitializeComponent();
            _guest = guest;
            //Set up controls
            SetControls();
            guest.Account.OnTransactionEvent += Account_OnTransactionEvent;
            lstTransactions.Items.Clear();
            lblAmountToPay.Text = guest.Account.AmountOwing.ToString("C2");
            lblAvailableAmount.Text = guest.Account.CurrentBalance.ToString("C2");
            guest.Account.BalanceChanged += Account_BalanceChanged;
        }//ctor main

        private void Account_BalanceChanged(decimal newBalance, decimal newAmountOwing)
        {
            lblAmountToPay.Text = newAmountOwing.ToString("C2");
            lblAvailableAmount.Text = newBalance.ToString("C2");
        }//Account_BalanceChanged

        private void Account_OnTransactionEvent(TransactionArgs transaction)
        {
            //Need to add this to the list of transactions
            lstTransactions.Items.Add(transaction);
        }//Account_OnTransactionEvent

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

        }//SetControls
        //This will be called form the main form
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

        private void btnDeposite_Click(object sender, EventArgs e)
        {
            _guest.Account.DepositAmount(2000);
        }//btnDeposite_Click

        private void btnWithdraw_Click(object sender, EventArgs e)
        {

        }//btnWithdraw_Click

        private void btnPayDept_Click(object sender, EventArgs e)
        {
            _guest.Account.PayForBooking(100);
        }//btnPayDept_Click
    }//class
}//namespace
