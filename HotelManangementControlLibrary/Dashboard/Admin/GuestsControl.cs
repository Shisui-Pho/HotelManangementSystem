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
using HotelManangementControlLibrary.Utils;

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
            radAll.Checked = true;
        }//ctor 1

        private void DisplayGuests()
        {
            lstbxGuests.Items.Clear();
            foreach (IGuest guest in guests)
                lstbxGuests.Items.Add(guest);
        }//DisplayGuests

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchUserID.Enabled)
            {
                if (txtSearchUserID.Text.Length < 1)
                    return;
                lstbxGuests.Items.Clear();
                IGuest guest = guests.FindGuest(txtSearchUserID.Text);
                if(guest is null)
                {
                    lstbxGuests.Items.Add("No one found");
                    return;
                }//end if
                lstbxGuests.Items.Add(guest);
                return;
            }//end if
            if (txtSearchName.Text.Length > 1 && txtSearchSurname.Text.Length > 1)
                Search(true);
            else
                Search(false);
        }//btnSearch_Click
        private void Search(bool isBoth)
        {
            if (isBoth)
            {
                lstbxGuests.Items.Clear();
                foreach (IGuest guest in guests.GetGuests(txtSearchName.Text,txtSearchSurname.Text))
                {
                    lstbxGuests.Items.Add(guest);
                }
                if (lstbxGuests.Items.Count <= 0)
                    lstbxGuests.Items.Add("No one found");
                return;
            }//end if
        }//Search
        private void lstbxGuests_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbxGuests.SelectedIndex;
            if (index < 0)
                return;
            string userId = lstbxGuests.Items[index].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            
            IGuest guest = guests.FindGuest(userId);
            //Display info
            txtName.Text = guest.Name;
            txtSurname.Text = guest.Surname;
            txtCellphoneNumber.Text = guest.ContactDetails.CellphoneNumber;
            txtEmailAddress.Text = guest.ContactDetails.EmailAddress;
            txtEmergencyNumber.Text = guest.ContactDetails.EmergencyNumber;
            lblDOB.Text = guest.Age.ToString();
            lblDOB.Text = guest.DOB.ToString("dd MMMM yyyy");

        }//lstbxGuests_SelectedIndexChanged

        private void txtSearchName_Enter(object sender, EventArgs e)
        {
            //Need to think of something
        }//txtSearchName_Enter

        private void txtSearchName_MouseHover(object sender, EventArgs e)
        {
            txtSearchName.Enabled = txtSearchSurname.Enabled = true;
            txtSearchUserID.Enabled = false;
        }//txtSearchName_MouseHover

        private void txtSearchUserID_MouseHover(object sender, EventArgs e)
        {
            txtSearchName.Enabled = txtSearchSurname.Enabled = false;
            txtSearchUserID.Enabled = true;
        }//txtSearchUserID_MouseHover

        private void radEnaleSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked)
                plnSearch.Enabled = false;
            else
                plnSearch.Enabled = true;
        }//radEnaleSearch_CheckedChanged

        private void radEnaleSearch_Click(object sender, EventArgs e)
        {
            RadioButton radSellected = (RadioButton)sender;
            if (radAll == radSellected)
            {
                radEnaleSearch.Checked = false;
            }
                
            else if (radSellected == radEnaleSearch)
            {
                radAll.Checked = false;
            }
        }//radEnaleSearch_Click
    }//class
}//namespace