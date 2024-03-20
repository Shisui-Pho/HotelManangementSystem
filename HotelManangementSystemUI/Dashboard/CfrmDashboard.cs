using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ComponentFactory.Krypton.Toolkit;
using HotelManangementControlLibrary.Dashboard;
using HotelManangementControlLibrary.Dashboard.Admin;
using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.DatabaseService;
using HotelManangementSystemUI.Input_Forms;
using HotelManangementControlLibrary.Dashboard.Guest;

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
        private readonly GuestProfileControl _guestProfileControl;

        //To modify
        private readonly BookingsControl _adminBookingsControl;
        private readonly GuestsControl _adminGuestControl;
        private readonly HotelStatisticsControl _adminHotelStatics;
        private readonly HistoricalBookingsControl _adminOldBookings;


        public CfrmDashboard(IDatabaseService database, IUser _logged_in_user)
        {
            InitializeComponent();
            this.database = database;
            _guestRoomBookingsControl = new RoomBookingControl(database, null,null);//use ctor 01
            _adminRoomsControl = new RoomsControl(database.Rooms, ModifyRoom, NewRoom);
            _guestProfileControl = new GuestProfileControl(database.Guests.FindGuest(_logged_in_user));
            this._logged_in_user = _logged_in_user;

            //Setting custom events
            _guestProfileControl.PasswordChanged += _guestProfileControl_PasswordChanged;
        }//ctor 01
         //Testing
        public CfrmDashboard()
        {
            InitializeComponent();
            //_guestRoomBookingsControl = new RoomBookingControl();
            //_adminRoomsControl = new RoomsControl(ModifyRoom, NewRoom);
            database = FactoryTest.CreateDatabase();
            _guestRoomBookingsControl = new RoomBookingControl(database, null, null);//use ctor 01
            _adminRoomsControl = new RoomsControl(database.Rooms, ModifyRoom, NewRoom);
            _guestProfileControl = new GuestProfileControl();

            //To modify
            _adminGuestControl = new GuestsControl(database.Guests);
            _adminHotelStatics = new HotelStatisticsControl();
            _adminBookingsControl = new BookingsControl(database.Bookings);
            _adminOldBookings = new HistoricalBookingsControl();
            LinkUnlinkedButtons();
        }//ctir testing
        private void LinkUnlinkedButtons()
        {
            btnStaff.Parent = plnAdminPanel;
            btnStatistics.Parent = plnAdminPanel;
        }//LinkUnlinkedButtons
        private void _guestProfileControl_PasswordChanged(string newpassword)
        {
            //Set the password
            _logged_in_user.SetPassword(newpassword);
        }//_guestProfileControl_PasswordChanged
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
            //Add the controls in the container
            plnContainer.Controls.Add(_guestRoomBookingsControl);
            plnContainer.Controls.Add(_adminRoomsControl);
            plnContainer.Controls.Add(_guestProfileControl);
            plnContainer.Controls.Add(_adminBookingsControl);
            plnContainer.Controls.Add(_adminGuestControl);
            plnContainer.Controls.Add(_adminHotelStatics);
            plnContainer.Controls.Add(_adminOldBookings);

            //Make the room booking container visible
           
            _guestRoomBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = true;
            _guestRoomBookingsControl.BackColor = System.Drawing.Color.Transparent;
        }//AddControls

        private IRoom NewRoom()
        {
            CdlgCustomRooms newroom = new CdlgCustomRooms();
            if(newroom.ShowDialog() == DialogResult.OK)
            {
                database.Rooms.Add(newroom.Room);
                return newroom.Room;
            }
            return default;
        }//NewRoom
        private IRoom ModifyRoom(IRoom oldroom)
        {
            CdlgCustomRooms newroom = new CdlgCustomRooms(oldroom);
            if (newroom.ShowDialog() == DialogResult.Cancel)
                return newroom.Room;

            //Update the database
            database.Rooms.Remove(oldroom);
            database.Rooms.Add(newroom.Room);

            //Check if the room has previous bookings
            IRoomBooking[] bookings = database.Bookings.HasBookings(oldroom);
            if (bookings.Length <= 0)
                return newroom.Room;

            //Ask the admin if the want to keep the bookings or not
            if (MessageBox.Show($"Room {oldroom.RoomNumber} still has bookings, do you wish to move bookings to {newroom.Room.RoomNumber} ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.No)
                return newroom.Room;
            //Book the room for the specified dates
            IRoom room = newroom.Room;
            DoBookings(room, bookings);
            return room;
            //return default;
        }//ModifyRoom
        private void DoBookings(IRoom room,IRoomBooking[] bookings)
        {
            foreach (IRoomBooking booking in bookings)
            {
                IRoomBooking b = booking;
                b.ChangeRoom(room);
                database.Bookings.Add(booking);
            }//end foreach
        }//DoBookings

        private void CfrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Unsubscribe to events
            _guestProfileControl.PasswordChanged -= _guestProfileControl_PasswordChanged;
        }//CfrmDashboard_FormClosing


        #region Clicks event handlers
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            plnAdminPanel.Visible = !plnAdminPanel.Visible;
        }//btnSignOut_Click

        private void btnManangeRooms_Click(object sender, EventArgs e)
        {
            _adminRoomsControl.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = false;
            _adminRoomsControl.Visible = true;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeRooms_Click

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            _guestRoomBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = true;
            _adminRoomsControl.Visible = false;
            _guestProfileControl.Visible = false;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = false;
            _adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnBookRoom_Click
        private void btnManangeGuests_Click(object sender, EventArgs e)
        {
            _adminGuestControl.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = false;
            _adminRoomsControl.Visible = false;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = true;
            _adminBookingsControl.Visible = false;
            _adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeGuests_Click

        private void btnManangeOldBookings_Click(object sender, EventArgs e)
        {
            _adminOldBookings.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = false;
            _adminRoomsControl.Visible = false;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = false;
            _adminOldBookings.Visible = true;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeOldBookings_Click
        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            _guestProfileControl.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = true;
            _adminRoomsControl.Visible = false;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = false;
            _adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnViewProfile_Click
        private void btnManangeBookings_Click(object sender, EventArgs e)
        {
            _adminBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = false;
            _adminRoomsControl.Visible = false;

            _adminHotelStatics.Visible = false;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = true;
            _adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeBookings_Click
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            _adminHotelStatics.BringToFront();
            _guestRoomBookingsControl.Visible = false;
            _guestProfileControl.Visible = false;
            _adminRoomsControl.Visible = false;

            _adminHotelStatics.Visible = true;
            _adminGuestControl.Visible = false;
            _adminBookingsControl.Visible = false;
            _adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnStatistics_Click
        private void TrackButton(string buttonName)
        {
           
        }//TrackButton
        private void RevertColors()
        {

        }
        #endregion Clicks event handlers
    }//class
}//namespace
