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

namespace HotelManangementControlLibrary.Dashboard.Admin
{
    public partial class GuestsControl : UserControl
    {
        private readonly IGuests guests;
        //private IGuests guestCopy;
        public GuestsControl(IGuests guests)
        {
            InitializeComponent();
            this.guests = guests;
            DisplayGuests();
        }//ctor 1

        private void DisplayGuests()
        {
            lstbxGuests.Items.Clear();
            foreach (IGuest guest in guests)
                lstbxGuests.Items.Add(guest);
        }//DisplayGuests

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }//btnSearch_Click
    }//class
}//namespace