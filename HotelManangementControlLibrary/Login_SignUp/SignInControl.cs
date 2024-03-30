using System;
using System.Drawing;
using System.Windows.Forms;
using UIServiceLibrary.Evaluations;
namespace HotelManangementControlLibrary.Login_SignUp
{
    public partial class SignInControl : UserControl
    {
        //Property for checking if all the data is correct
        public bool IsSignInHandled { get; private set; } = false;
        private readonly delChangeControl dChange;
        public SignInControl(delChangeControl dChange)
        {
            InitializeComponent();
            this.dChange = dChange;
        }//ctor 01
        private void lblLogIn_Click_1(object sender, EventArgs e)
        {
            dChange();
        }//lblLogIn_Click_1

        private void lblLogIn_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }//lblLogIn_MouseHover

        private void lblLogIn_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Blue;
        }//lblLogIn_MouseLeave

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            IsSignInHandled = false;
            if(!txtName.Text.IsValidInput()
               || !txtSurname.Text.IsValidInput()
               || !txtUsername.Text.IsValidInput()
               || !txtxPassword.Text.IsValidInput()
               || !txtConfirmPassword.Text.IsValidInput())
            {
                MessageBox.Show("Please fill in all the fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsSignInHandled = true;//Handle the event here
            }//end if
            //Need to handle same usernames here
            else if(txtxPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords not matching.", "Registration error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsSignInHandled = true;
            }//end if
                
        }//btnSignIn_Click
    }//class
}//namepscae
