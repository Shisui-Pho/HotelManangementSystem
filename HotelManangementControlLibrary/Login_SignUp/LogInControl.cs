using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
namespace HotelManangementControlLibrary.Login_SignUp
{
    public partial class LogInControl : UserControl
    {
        private readonly ChangeControl dChange;
        public LogInControl(ChangeControl dChange)
        {
            InitializeComponent();
            this.dChange = dChange;
        }//ctor 01
        private void lblSignIn_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }//lblSignIn_MouseHover

        private void lblSignIn_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Blue;
        }//lblSignIn_MouseLeave

        private void lblSignIn_Click(object sender, EventArgs e)
        {
            //Switch the controls in the main form
            dChange();
        }//lblSignIn_Click

        private void btnLogIn_Click(object sender, EventArgs e)
        {

        }//btnLogIn_Click
    }//class
}//namespace
