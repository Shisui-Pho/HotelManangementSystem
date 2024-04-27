using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelManangementSystemLibrary;
using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary.Factory;
using UIServiceLibrary;
using UIServiceLibrary.Extensions;

namespace HotelManangementControlLibrary.Dashboard
{
    public partial class BookingsControl : UserControl
    {
        //Will be injected on a method
        private IRoomBookings bookings;

        //private datamember
        //-This will hold the copy of the room bookings per room
        private List<IRoomBooking> lstBookingsPerRoom;
        private IRooms _bookedRooms;

        private delOnBookingCancelled funcBookingCancelled;
        public BookingsControl(delOnBookingCancelled cancelled)
        {
            InitializeComponent();
            this.funcBookingCancelled = cancelled;
            //SetUpControls();
        }//ctor 01
        public BookingsControl()
        {
            InitializeComponent();
        }//ctor 01
        public void AddBookings(IRoomBookings bookings)
        {
            this.bookings = bookings;
            SetUpControls();
        }//AddBookings
        private void SetUpControls()
        {
            //Display members
            lstbxRoomsBooked.DisplayMember = "RoomNumber";

            //Adding items
            cmboRoomStatus.Items.Clear();
            cmboRoomStatus.Items.AddRange(new string[] { "All", "Hidden", "Unhidden" });
            cmboTypeOfRooms.Items.Clear();
            cmboTypeOfRooms.Items.AddRange(new string[] { "All", "Single", "Double" });

            //Add items
            RefreshList();
            //Set default selections
            radSelection.Checked = true;
            cmboTypeOfRooms.SelectedIndex = cmboRoomStatus.SelectedIndex = 0;
        }//SetUpControls
        private void RefreshList(int index = 0)
        {
            lstbxRoomsBooked.Items.Clear();
            _bookedRooms = bookings.GetBookedRooms();
            foreach (IRoom room in _bookedRooms)
                lstbxRoomsBooked.Items.Add(room);
            if (lstbxRoomsBooked.Items.Count > 0)
                lstbxRoomsBooked.SelectedIndex = index;
        }//RefreshList
        private void cmboTypeOfRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }//cmboTypeOfRooms_SelectedIndexChanged

        private void cmboRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }//cmboRoomStatus_SelectedIndexChanged
        private void ApplyFilter()
        {
            //For filtering room type
            if (cmboTypeOfRooms.SelectedIndex == 0)
                Filter.FilterTypesOfRooms<IRoom>(lstbxRoomsBooked,_bookedRooms );//All the rooms
            if (cmboTypeOfRooms.SelectedIndex == 1)
                Filter.FilterTypesOfRooms<ISingleRoom>(lstbxRoomsBooked,_bookedRooms);//Only single rooms
            if (cmboTypeOfRooms.SelectedIndex == 2)
                Filter.FilterTypesOfRooms<IDoubleRoom>(lstbxRoomsBooked,_bookedRooms);//Only double rooms

            //For filtering room status
            if (cmboRoomStatus.SelectedIndex == 0)
                Filter.FilterStatusOfRooms(lstbxRoomsBooked,null);
            if (cmboRoomStatus.SelectedIndex == 1)
                Filter.FilterStatusOfRooms(lstbxRoomsBooked, true);
            if (cmboRoomStatus.SelectedIndex == 2)
                Filter.FilterStatusOfRooms(lstbxRoomsBooked, false);

            //Select the first item
            if (lstbxRoomsBooked.Items.Count > 0)
                lstbxRoomsBooked.SelectedIndex = 0;
            if (lstbxBookingDates.Items.Count > 0)
                lstbxBookingDates.SelectedIndex = 0;
        }//ApplyStatusFilter
        private void lstbxRoomBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstbxRoomsBooked.SelectedIndex;
            if (i < 0)
                return;
            IRoom room = (IRoom)lstbxRoomsBooked.Items[i];

            //A copy of the bookings per room
            lstBookingsPerRoom = bookings.FindBookings(room).ToList();
            lstbxBookingDates.Items.Clear();

            //Add the items in the list
            //-NOTE : The items in the list will be linked to the items in the copy
            foreach (IRoomBooking item in lstBookingsPerRoom)
                lstbxBookingDates.Items.Add(item.DateBookedFor.ToString("dd MMMM yyyy"));
        }//lstbxRoomBooking_SelectedIndexChanged          

        private void lstbxBookingDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOTE 
            //-Using the items in the copy and items in the list as parallel arrays
            int index = lstbxBookingDates.SelectedIndex;
            if (index < 0)
                return;

            //Get the Booking details from the copy list
            IRoomBooking booking = lstBookingsPerRoom[index];

            //Display on the form
            lblRoomNumber.Text = booking.Room.RoomNumber;
            lblGuestName.Text = booking.Guest.Name;
            lblGuestSurname.Text = booking.Guest.Surname;
            lblTypeOfRoom.Text = (booking.Room.IsSingleRoom) ? "Single room" : "Double room";
            lblBookedDate.Text = booking.DateBookedFor.ToString("dd MMMM yyyy");
            lblFee.Text = booking.BookingFee.AmoutToPay.ToString("C2");
            int days = booking.NumberOfDaysToStay;
            lblDuration.Text = (days > 1) ? days.ToString() + " days" : days.ToString() + " day";
        }//lstbxBookingDates_SelectedIndexChanged

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            int index = lstbxBookingDates.SelectedIndex;
            if(index < 0)
            {
                Messages.ShowErrorMessage("Please select the room and date to cancel");
                return;
            }//end if

            //Remove the booking
            //-Business rules will be applied where the method is defined
            //-Note that the copy and the lstbxDates acts as parrallel arrays
            if (funcBookingCancelled(lstBookingsPerRoom[index]))
            {
                //Remove the items from the list by refresshing it
                if (index > 0)
                    RefreshList(index - 1);
                else
                    Refresh();
            }//end if
        }//btnCancelBooking_Click

        private void radSelection_CheckedChanged(object sender, EventArgs e)
        {
            grpbxSelection.Enabled = radSelection.Checked;
            grpSearching.Enabled = radSearching.Checked;
        }//radSelection_CheckedChanged

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }//btnSearch_Click
    }//class
}//namespace
