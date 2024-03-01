using System.Windows.Forms;
using HotelManangementControlLibrary.Login_SignUp;
namespace HotelManangementSystemUI.Login_SignUp
{
    public partial class Login_Sign_Up : Form
    {
        private readonly SignInControl _signIn;
        private readonly LogInControl _logIn;
        public Login_Sign_Up()
        {
            InitializeComponent();
            _signIn = new SignInControl(LoginLablePressed);
            _logIn = new LogInControl(SignInLablePressed);
        }//ctor main

        private void Login_Sign_Up_Shown(object sender, System.EventArgs e)
        {
            plnContainer.Controls.Add(_signIn);
            plnContainer.Controls.Add(_logIn);
            _signIn.Visible = false;
            _logIn.Visible = true;
            _logIn.BringToFront();
        }//Login_Sign_Up_Shown
        private void LoginLablePressed()
        {
            _signIn.Visible = false;
            _logIn.Visible = true;
            _logIn.BringToFront();
        }//LoginLablePressed
        private void SignInLablePressed()
        {
            _signIn.Visible = true;
            _logIn.Visible = false;
            _signIn.BringToFront();
        }//SignInLablePressed
    }//class
}//namespace
