using HotelManangementControlLibrary.Dashboard;
using HotelManangementControlLibrary.Dashboard.Admin;
using HotelManangementSystemLibrary;
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

namespace HotelManangementSystemUI.Dashboard
{
    public partial class CfrmDashboard : Form
    {
        //Database datamember
        private readonly IDatabaseService database;
        private readonly IUser _logged_in_user;

        //Controls
        private readonly RoomBookingControl _guestRoomBookingsControl;
        private readonly RoomsControl _adminRoomsControl;
        public CfrmDashboard(IDatabaseService database, IUser _logged_in_user)
        {
            InitializeComponent();
            this.database = database;
            _guestRoomBookingsControl = new RoomBookingControl(database, null,null);//use ctor 01
            this._logged_in_user = _logged_in_user;
        }//ctor 01
        public CfrmDashboard()
        {
            InitializeComponent();
            _guestRoomBookingsControl = new RoomBookingControl();
            _adminRoomsControl = new RoomsControl();
        }
        private async Task LoadAllBookings()
        {
            //This will load all the other Properties
           await Task.Run(() => database.LoadBookings());

            if (_logged_in_user.UserType == TypeOfUser.Admin)
                //Also need to load warehouse data for admin purposes
                plnAdminPanel.Visible = true;
            if (_logged_in_user.UserType == TypeOfUser.Guest)
                plnGuestPanel.Visible = true;
        }//LoadGuest

        private /*async*/ void CfrmDashboard_Shown(object sender, EventArgs e)
        {
            //Load Data after form has been loaded
            // await LoadAllBookings();
            //Do some set ups
            AddControls();
        }//CfrmDashboard_Shown
        private void AddControls()
        {
            plnContainer.Controls.Add(_guestRoomBookingsControl);
            plnContainer.Controls.Add(_adminRoomsControl);
            _guestRoomBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = true;
            _adminRoomsControl.Visible = false;
        }//AddControls

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            plnAdminPanel.Visible = !plnAdminPanel.Visible;
        }//btnSignOut_Click

        private void btnManangeRooms_Click(object sender, EventArgs e)
        {
            _adminRoomsControl.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _adminRoomsControl.Visible = true;
        }//btnManangeRooms_Click

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            _guestRoomBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = true;
            _adminRoomsControl.Visible = false;
        }//btnBookRoom_Click
    }//class
}//namespace
