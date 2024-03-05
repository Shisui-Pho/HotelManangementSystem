using HotelManangementSystemLibrary.DatabaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManangementSystemLibrary.Factory;
using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;

namespace HotelManangementControlLibrary.Dashboard
{
    public partial class RoomBookingControl : UserControl
    {
        private BookRoom bookRoom;
        private CancelBooking cancelBooking;
        private readonly IDatabaseService database;
        public RoomBookingControl(IDatabaseService database, BookRoom bookroom_method, CancelBooking cancelroom_method)
        {
            InitializeComponent();
            this.database = database;
            bookRoom = bookroom_method;
            cancelBooking = cancelroom_method;
            //Set the display members
            lstRooms.DisplayMember = "RoomNumber";
        }//ctor 01
        public RoomBookingControl()
        {
            InitializeComponent();
        }//
        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Trigered");
            return;
            cancelBooking(null);
        }//btnCancelBooking_Click
        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            //Book the room
            int i = lstbxAllBookings.SelectedIndex;
            if (i < 0)
            {
                Messages.ShowErrorMessage("Please select room", "Selection error");
                return;
            }//end if
            bookRoom((IRoom)lstRooms.Items[i]);//book the room via a delegate method

            //Refresh
            RefreshLists(false);
        }//btnBookRoom_Click
        private void RefreshLists(bool isFirstTime = true)
        {
            if (isFirstTime)
            {
                //Start with the rooms first
                lstRooms.Items.Clear();
                foreach (IRoom room in database.Rooms)
                {
                    lstRooms.Items.Add(room);
                }//end foreach
            }
        }//RefreshLists
        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }//lstRooms_SelectedIndexChanged

    }//class
}//namespcae
