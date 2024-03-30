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

        //Controls
        private readonly RoomBookingControl _guestRoomBookingsControl;
        private readonly RoomsControl _adminRoomsControl;
        private readonly GuestProfileControl _guestProfileControl;

        //To modify
        private readonly BookingsControl _adminBookingsControl;
        private readonly GuestsControl _adminGuestControl;
        private readonly HotelStatisticsControl _adminHotelStatics;
        //private readonly HistoricalBookingsControl _adminOldBookings;
        #endregion Datamembers

        #region Constructors
        public CfrmDashboard(IDatabaseService database, IUser _logged_in_user)
        {
            InitializeComponent();
            this.database = database;
            this._logged_in_user = _logged_in_user;
            if (_logged_in_user is IAdministrator)
            {
                _adminRoomsControl = new RoomsControl(database.Rooms, ModifyRoom, NewRoom);
            }
            else if(_logged_in_user is IGuest)
            {
                _guestProfileControl = new GuestProfileControl(database.Guests.FindGuest(_logged_in_user));
                _adminGuestControl = new GuestsControl(database.Guests);
                _adminHotelStatics = new HotelStatisticsControl();
                _adminBookingsControl = new BookingsControl(database.Bookings, BookingCancelledFromAdminBookingsControl);
                //_adminOldBookings = new HistoricalBookingsControl();
            }

            _guestRoomBookingsControl = new RoomBookingControl(database, BookRoom);//use ctor 01
            
            
            
            database.Bookings.RemovedBooking += Bookings_ItemRemovedEvent;
            //Setting custom events
           
        }//ctor 01
        //Testing
        public CfrmDashboard()
        {
            InitializeComponent();
            database = FactoryTest.CreateDatabase();
            this._logged_in_user = database.Guests[0];
            _guestRoomBookingsControl = new RoomBookingControl(database, BookRoom);//use ctor 01
            _adminRoomsControl = new RoomsControl(database.Rooms, ModifyRoom, NewRoom);
            _guestProfileControl = new GuestProfileControl(database.Guests[0],BookingCancelledFromGuestProfile);

            //To modify
            
            LinkUnlinkedButtons();
        }//ctir testing

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
                foreach (var item in database.Bookings.GetBookingsOf(_logged_in_user.UserID))
                    _guestProfileControl.AddBookingToProfile(item);
                //- Register to the Password change event from the GuestControl
                _guestProfileControl.PasswordChanged += _guestProfileControl_PasswordChanged;
            }               

            //Subscribe to the Booking Removed event handler
            database.Bookings.RemovedBooking += Bookings_ItemRemovedEvent;
        }//PrepareControls
        private void AddControls()
        {
            //Add the controls in the container
            if(_logged_in_user is IGuest)
            {
                plnContainer.Controls.Add(_guestProfileControl);
                _guestProfileControl.Visible = false;
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
            }
            plnContainer.Controls.Add(_guestRoomBookingsControl);

           
            
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
        private bool BookRoom(IRoom room,DateTime date, int numberOfDays = 1)
        {
            //Create the booking
            //- Need to apply some bussiness ruls here...
            //-TO DO
            try
            {
                var booking = BookingsFactory.CreateBooking(database.Guests[0], room, date, numberOfDays);
                database.Bookings.Add(booking);
                _guestProfileControl.AddBookingToProfile(booking);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //For now
        }//BookRoom
        private bool BookingCancelledFromGuestProfile(IRoomBooking roomBooking)
        {
            //Apply some bussiness rules here
            //TO DO


            //-Cancel the booking
            database.Bookings.CancelBooking(roomBooking, BookingState.Canceld);

            //For now
            return true;
        }//BookingCancelledFromGuestProfile
        private bool BookingCancelledFromAdminBookingsControl(IRoomBooking roomBooking)
        {
            //Apply some bussiness rules here
            //-Also need to get the cancellation reason
            //TO DO


            //-Cancel the booking
            database.Bookings.CancelBooking(roomBooking, BookingState.Canceld);

            //For now
            return true;
        }//BookingCancelledFromGuestProfile
        private void Bookings_ItemRemovedEvent(object sender, HotelEventArgs args)
        {
            IRoomBooking roombooking = (IRoomBooking)sender;
            IOldBooking old = WareHouseFactory.PrepareMove(roombooking, (BookingState)Enum.Parse(typeof(BookingState), args.Description));
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
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = false;
            //_adminRoomsControl.Visible = true;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeRooms_Click

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            _guestRoomBookingsControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            //_guestRoomBookingsControl.Visible = true;
            //_adminRoomsControl.Visible = false;
            //_guestProfileControl.Visible = false;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = false;
            //_adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnBookRoom_Click
        private void btnManangeGuests_Click(object sender, EventArgs e)
        {
            _adminGuestControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = false;
            //_adminRoomsControl.Visible = false;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = true;
            //_adminBookingsControl.Visible = false;
            //_adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeGuests_Click

        private void btnManangeOldBookings_Click(object sender, EventArgs e)
        {
            
            //_adminOldBookings.BringToFront();
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = false;
            //_adminRoomsControl.Visible = false;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = false;
            //_adminOldBookings.Visible = true;
            //TrackButton(((KryptonButton)sender).Name);
        }//btnManangeOldBookings_Click
        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            _guestProfileControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = true;
            //_adminRoomsControl.Visible = false;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = false;
            //_adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnViewProfile_Click
        private void btnManangeBookings_Click(object sender, EventArgs e)
        {
            _adminBookingsControl.BringToFront();

            HandleButtonClicked(((KryptonButton)sender).Text);
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = false;
            //_adminRoomsControl.Visible = false;

            //_adminHotelStatics.Visible = false;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = true;
            //_adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnManangeBookings_Click
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            _adminHotelStatics.BringToFront();
            HandleButtonClicked(((KryptonButton)sender).Text);
            //_guestRoomBookingsControl.Visible = false;
            //_guestProfileControl.Visible = false;
            //_adminRoomsControl.Visible = false;

            //_adminHotelStatics.Visible = true;
            //_adminGuestControl.Visible = false;
            //_adminBookingsControl.Visible = false;
            //_adminOldBookings.Visible = false;
            TrackButton(((KryptonButton)sender).Name);
        }//btnStatistics_Click
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
            }
            _guestRoomBookingsControl.Visible = text == btnBookRoom.Text;
        }//HandleButtonClicked
        private void CfrmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Unsubscribe to events
            _guestProfileControl.PasswordChanged -= _guestProfileControl_PasswordChanged;
            
            {
                database.SaveBookings();
                database.SaveGuets();
                database.SaveRooms();
                database.SaveUsers();
            }
        }//CfrmDashboard_FormClosing

        #endregion Form event handlers and Button clicks
    }//class
}//namespace
