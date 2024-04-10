using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using HotelManangementControlLibrary.Dashboard;
using HotelManangementControlLibrary.Dashboard.Admin;
using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.DatabaseService;
using HotelManangementSystemUI.Input_Forms;
using HotelManangementControlLibrary.Dashboard.Guest;
using HotelManangementSystemLibrary.Factory;
using HotelManangementSystemUI.Extensions;

namespace HotelManangementSystemUI.Dashboard
{
    public partial class CfrmDashboard : Form
    {

        #region Datamembers
        //Database datamembers
        private readonly IDatabaseService database;
        private readonly IUser _logged_in_user;
        private readonly IOldBookingRepoistory oldBookingRepoistory = WareHouseFactory.WareHouse();

        private readonly IGuest _guestProfile;
        private readonly IRooms _rooms;
        private readonly IRoomBookings _bookings;


        //Controls
        private readonly RoomBookingControl _guestRoomBookingsControl;
        private readonly RoomsControl _adminRoomsControl;
        private readonly GuestProfileControl _guestProfileControl;
        private readonly BookingsControl _adminBookingsControl;
        private readonly GuestsControl _adminGuestControl;
        private readonly HotelStatisticsControl _adminHotelStatics;
        private readonly GuestBookingsControl _guestBookingsControl;
        //private readonly HistoricalBookingsControl _adminOldBookings;
        #endregion Datamembers

        #region Constructors

            //Admin contructor
        public CfrmDashboard(IDatabaseService database, IUser _logged_in_user)
        {
            InitializeComponent();
            this.database = database;
            this._logged_in_user = _logged_in_user;
            if (_logged_in_user is IAdministrator)
            {
                _adminRoomsControl = new RoomsControl(database.Rooms, UpdatingRoomFromAdminRoomManangementControl, CreatingNewRoomFromAdminRoomManangementControl);
                _adminGuestControl = new GuestsControl(database.Guests);
                _adminHotelStatics = new HotelStatisticsControl();
                _adminBookingsControl = new BookingsControl(database.Bookings, CancelBookingDelFunction);
                //_adminOldBookings = new HistoricalBookingsControl();
            }
            _guestRoomBookingsControl = new RoomBookingControl(database.Bookings,database.Rooms, RoomBookingFromRoomBookingControl);//use ctor 01
            
            database.Bookings.RemovedBooking += Bookings_ItemRemovedEvent;
            LinkUnlinkedButtons();
        }//ctor 01

        //Guest Contructor
        public CfrmDashboard(IGuest profile,IRoomBookings bookings ,IRooms rooms)
        {
            InitializeComponent();
            this._logged_in_user = profile;
            this._rooms = rooms;
            this._bookings = bookings;
            this._guestProfile = profile;
            //For guests
            _guestProfileControl = new GuestProfileControl(profile);
            _guestBookingsControl = new GuestBookingsControl(CancelBookingDelFunction);
            _guestRoomBookingsControl = new RoomBookingControl(bookings ,rooms,RoomBookingFromRoomBookingControl);//use ctor 01
        }//
        //Testing
        public CfrmDashboard()
        {
            InitializeComponent();
            database = FactoryTest.CreateDatabase();
            this._logged_in_user = database.Guests[0];
            _guestRoomBookingsControl = new RoomBookingControl(database, RoomBookingFromRoomBookingControl);//use ctor 01
            _adminRoomsControl = new RoomsControl
                    (database.Rooms, UpdatingRoomFromAdminRoomManangementControl, CreatingNewRoomFromAdminRoomManangementControl);
            _guestProfileControl = new GuestProfileControl(database.Guests[0]);
            _guestBookingsControl = new GuestBookingsControl(CancelBookingDelFunction);
            //To modify
            
            LinkUnlinkedButtons();
        }//ctor testing

        #endregion Constructors

        #region Helper Methods
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
        private void PrepareControls()
        {
            if (_logged_in_user is IGuest)
            {
                //For the GuestProfile Control
                //-Add the current booking to the profile
                foreach (var item in _bookings.GetBookingsOf(_logged_in_user.UserID))
                    _guestBookingsControl.AddBookingToProfile(item);
                //- Register to the Password change event from the GuestControl
                _guestProfileControl.PasswordChanged += _guestProfileControl_PasswordChanged;
                _bookings.RemovedBooking += Bookings_ItemRemovedEvent;
            }
            if (_logged_in_user is IAdministrator)
                database.Bookings.RemovedBooking += Bookings_ItemRemovedEvent;
            //Subscribe to the Booking Removed event handler

        }//PrepareControls
        private void AddControls()
        {
            //Add the controls in the container
            if(_logged_in_user is IGuest)
            {
                plnContainer.Controls.Add(_guestProfileControl);
                plnContainer.Controls.Add(_guestBookingsControl);
                _guestProfileControl.Visible = false;

                picUser.Image = Properties.Resources.generaluser;
            }
            if(_logged_in_user is IAdministrator)
            {
                plnContainer.Controls.Add(_adminRoomsControl);
                plnContainer.Controls.Add(_adminBookingsControl);
                plnContainer.Controls.Add(_adminGuestControl);
                plnContainer.Controls.Add(_adminHotelStatics);
                _adminBookingsControl.Visible = false;
                _adminGuestControl.Visible = false;
                _adminHotelStatics.Visible = false;
                _adminRoomsControl.Visible = false;

                //For buttons
                _guestRoomBookingsControl.btnBookRoom.Enabled = false;

                picUser.Image = Properties.Resources.adminuser;
            }
            plnContainer.Controls.Add(_guestRoomBookingsControl);

            lblNameAndSurname.Text = _logged_in_user.Name + "  " + _logged_in_user.Surname;
            
            //plnContainer.Controls.Add(_adminOldBookings);

            //Make the room booking container visible           
            _guestRoomBookingsControl.BringToFront();
            _guestRoomBookingsControl.Visible = true;
            _guestRoomBookingsControl.BackColor = System.Drawing.Color.Transparent;

            PrepareControls();
        }//AddControls
        private void LinkUnlinkedButtons()
        {
            btnStaff.Parent = plnAdminPanel;
            btnStatistics.Parent = plnAdminPanel;
        }//LinkUnlinkedButtons

        #endregion Helper Methods

        #region Delegate Functions
        private IRoom CreatingNewRoomFromAdminRoomManangementControl()
        {
            CdlgCustomRooms newroom = new CdlgCustomRooms();
            if(newroom.ShowDialog() == DialogResult.OK)
            {
                database.Rooms.Add(newroom.Room);
                return newroom.Room;
            }
            return default;
        }//NewRoom
        private IRoom UpdatingRoomFromAdminRoomManangementControl(IRoom oldroom)
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
        private bool RoomBookingFromRoomBookingControl(IRoom room,DateTime date, int numberOfDays = 1)
        {
            //Create the booking
            //- Need to apply some bussiness ruls here...
            //-TO DO
            try
            {
                //SINCE THE ADMIN WON'T BE ABLE TO BOOK A ROOM, WE CAN ASSUME THAT WE WILL NOT NEED THE ENTIRE DATABASE OBJECT
                //var booking = BookingsFactory.CreateBooking(database.Guests[0], room, date, numberOfDays);
                CdlgConfirmUpdateBooking newBooking = new CdlgConfirmUpdateBooking
                    (_guestProfile, room, date, numberOfDays);
                if(newBooking.ShowDialog() == DialogResult.OK)
                {
                    _bookings.Add(newBooking.RoomBooking);
                    _guestBookingsControl.AddBookingToProfile(newBooking.RoomBooking);
                    return true;
                }
                return false;                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //For now
        }//BookRoom
        private bool CancelBookingDelFunction(IRoomBooking roomBooking)
        {
            //Apply some bussiness rules here
            //TO DO
            CdlgCancelBookingGuest cancel = new CdlgCancelBookingGuest(roomBooking,_logged_in_user);
            if(cancel.ShowDialog() == DialogResult.OK || cancel.IsBookingCancelled)
            {

                //-Cancel the booking
                if (cancel.Reason == CancellationReason.Other || cancel.Reason == CancellationReason.Requirements_Not_Met)
                    _bookings.CancelBooking(roomBooking, cancel.Other);
                else
                    _bookings.CancelBooking(roomBooking, cancel.Reason);
                return true;
            }//
            return false;
            
        }//BookingCancelledFromGuestProfile
        private void Bookings_ItemRemovedEvent(object sender, HotelEventArgs args)
        {
            IRoomBooking roombooking = (IRoomBooking)sender;
            IOldBooking old = null;
            try
            {
                old = WareHouseFactory.PrepareMove(roombooking, (CancellationReason)Enum.Parse(typeof(CancellationReason), args.Description));
            }
            catch
            {
                WareHouseFactory.PrepareMove(roombooking, args.Description);
            }
            oldBookingRepoistory.Add(old);
            args.IsHandled = true;
        }//Bookings_ItemRemovedEvent
        #endregion Delegate Functions

        #region Form event handlers and Button clicks
        private void _guestProfileControl_PasswordChanged(string newpassword)
        {
            //Set the password
            _logged_in_user.SetPassword(newpassword);
        }//_guestProfileControl_PasswordChanged
        private async void CfrmDashboard_Shown(object sender, EventArgs e)
        {
            //Load Data after form has been loaded
            if(_logged_in_user is IAdministrator)
                await LoadAllBookings();
            //Do some set ups
            AddControls();
        }//CfrmDashboard_Shown       
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            //plnAdminPanel.Visible = !plnAdminPanel.Visible;
            this.Close();
        }//btnSignOut_Click
                                      
        private void btnManangeRooms_Click(object sender, EventArgs e)
        {
            _adminRoomsControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeRooms_Click

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            _guestRoomBookingsControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnBookRoom_Click
        private void btnManangeGuests_Click(object sender, EventArgs e)
        {
            _adminGuestControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeGuests_Click

        private void btnManangeOldBookings_Click(object sender, EventArgs e)
        {
          
        }//btnManangeOldBookings_Click
        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            _guestProfileControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnViewProfile_Click
        private void btnManangeBookings_Click(object sender, EventArgs e)
        {
            _adminBookingsControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeBookings_Click
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            _adminHotelStatics.BringToFront();
            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnStatistics_Click
        private void btnViewBookings_Click(object sender, EventArgs e)
        {
            _guestBookingsControl.BringToFront();
            HandleButtonClicked(((KryptonButton)sender).Text);
            TrackButton(((KryptonButton)sender).Name);
        }//btnViewBookings_Click
        private void TrackButton(string buttonName)
        {
           
        }//TrackButton
        private void HandleButtonClicked(string text)
        {
            if(_logged_in_user is IAdministrator)
            {
                _adminHotelStatics.Visible = text == btnStatistics.Text;
                _adminGuestControl.Visible = text == btnManangeGuests.Text;
                _adminBookingsControl.Visible = text == btnManangeBookings.Text;
                _adminRoomsControl.Visible = text == btnManangeRooms.Text;
            }
            if(_logged_in_user is IGuest)
            {
                _guestProfileControl.Visible = text == btnViewProfile.Text;
                _guestBookingsControl.Visible = text == btnViewBookings.Text;
            }
            _guestRoomBookingsControl.Visible = text == btnBookRoom.Text;
        }//HandleButtonClicked
        private void CfrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Unsubscribe to events
            if (_logged_in_user is IGuest)
                _guestProfileControl.PasswordChanged -= _guestProfileControl_PasswordChanged;
            if(_logged_in_user is IAdministrator)
            {
                database.SaveBookings();
                database.SaveGuets();
                database.SaveRooms();
            }
        }//CfrmDashboard_FormClosing

        #endregion Form event handlers and Button clicks

    }//class
}//namespace
