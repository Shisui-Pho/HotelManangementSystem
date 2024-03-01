using System;
using System.Drawing;
using System.Windows.Forms;
namespace HotelManangementControlLibrary.Login_SignUp
{
    public partial class SignInControl : UserControl
    {
        private readonly ChangeControl dChange;
        public SignInControl(ChangeControl dChange)
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
    }//class
}//namepscae
