using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HotelManangementSystemLibrary;
using HotelManangementControlLibrary.Utils;

namespace HotelManangementControlLibrary.Dashboard.Admin
{
    public partial class GuestsControl : UserControl
    {
        private readonly IGuests guests;
        public GuestsControl(IGuests guests)
        {
            InitializeComponent();
            this.guests = guests;
            DisplayAllGuests();
            radAll.Checked = true;
        }//ctor 1

        private void DisplayAllGuests()
        {
            lstbxGuests.Items.Clear();
            foreach (IGuest guest in guests)
                lstbxGuests.Items.Add(guest);
        }//DisplayGuests

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchUserID.Enabled)
            {
                if (txtSearchUserID.Text.Length > 1)
                {
                    lstbxGuests.Items.Clear();
                    IGuest guest = guests.FindGuest(txtSearchUserID.Text);
                    if (guest is null)
                    {
                        lstbxGuests.Items.Add("No one found");
                        return;
                    }//end if
                    lstbxGuests.Items.Add(guest);
                    return;
                }
                if (txtSearchName.Text.Length > 1 && txtSearchSurname.Text.Length > 1)
                    Search(true);
                else
                    Search(false);
            }//end if
        }//btnSearch_Click
        private void Search(bool isBoth)
        {
            if (isBoth)
            {
                DisplaySearchResults(guests.GetGuests(txtSearchName.Text, txtSearchSurname.Text));
                return;
            }//end if

            if(txtSearchName.Text.Length > 1)
                DisplaySearchResults(guests.GetGuests(true, txtSearchName.Text));
            else if(txtSearchSurname.Text.Length > 1)
                DisplaySearchResults(guests.GetGuests(false, txtSearchSurname.Text));
        }//Search
        private void DisplaySearchResults(IEnumerable<IGuest> results)
        {
            lstbxGuests.Items.Clear();
            foreach (var item in results)
            {
                lstbxGuests.Items.Add(item);
            }//end foreach
            if (lstbxGuests.Items.Count <= 0)
                lstbxGuests.Items.Add("No guest found");
        }//DisplaySearchResults
        private void lstbxGuests_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbxGuests.SelectedIndex;
            if (index < 0)
                return;
            //Extract the text string
            //No guest found
            string textString = lstbxGuests.Items[index].ToString();
            if (textString == "No guest found" || textString == "No one found")
                return;
            string userId = textString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            
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
                //Add all the guests
                DisplayAllGuests();
            }//
                
            else if (radSellected == radEnaleSearch)
            {
                radAll.Checked = false;
                //Apply filtering
                btnSearch_Click(null, null);
            }
        }//radEnaleSearch_Click
    }//class
}//namespace