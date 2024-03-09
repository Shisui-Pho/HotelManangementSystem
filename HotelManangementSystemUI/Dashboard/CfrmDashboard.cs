using System;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public CfrmDashboard(IDatabaseService database, IUser _logged_in_user)
        {
            InitializeComponent();
            this.database = database;
            _guestRoomBookingsControl = new RoomBookingControl(database, null,null);//use ctor 01
            _adminRoomsControl = new RoomsControl(database.Rooms, ModifyRoom, NewRoom);
            this._logged_in_user = _logged_in_user;
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
        private void btnManangeBookings_Click(object sender, EventArgs e)
        {

        
        }//btnManangeBookings_Click

        private void btnManangeGuests_Click(object sender, EventArgs e)
        {

        }//btnManangeGuests_Click

        private void btnManangeOldBookings_Click(object sender, EventArgs e)
        {

        }//btnManangeOldBookings_Click
    }//class

}//namespace
