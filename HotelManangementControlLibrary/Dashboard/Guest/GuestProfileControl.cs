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

namespace HotelManangementControlLibrary.Dashboard.Guest
{
    public partial class GuestProfileControl : UserControl
    {
        private readonly IGuest _guest;
        public GuestProfileControl(IGuest guest)
        {
            InitializeComponent();
            _guest = guest;
            //Set up controls
        }//ctor main

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {

        }//btnUpdatePassword_Click

        private void btnUpadateContactDetails_Click(object sender, EventArgs e)
        {

        }//btnUpadateContactDetails_Click
    }//class
}//namespace
