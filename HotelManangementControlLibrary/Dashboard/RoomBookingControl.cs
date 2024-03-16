﻿using HotelManangementSystemLibrary.DatabaseService;
using System;
using System.Windows.Forms;
using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;
using ComponentFactory.Krypton.Toolkit;
namespace HotelManangementControlLibrary.Dashboard
{
    public partial class RoomBookingControl : UserControl
    {
        private delBookRoom bookRoom;
        private delCancelBooking cancelBooking;
        private readonly IDatabaseService database;
        public RoomBookingControl(IDatabaseService database, delBookRoom bookroom_method, delCancelBooking cancelroom_method)
        {
            InitializeComponent();
            this.database = database;
            bookRoom = bookroom_method;
            cancelBooking = cancelroom_method;
            //Set the display members
            lstRooms.DisplayMember = "RoomNumber";
            RefreshLists();
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
            lstRooms.Items.RemoveAt(i);
        }//btnBookRoom_Click
        private void RefreshLists(bool isFirstTime = true)
        {
            if (isFirstTime)
            {
                //Start with the rooms first
                lstRooms.Items.Clear();
                foreach (IRoom room in database.Rooms)
                {
                    if(!room.IsRoomHidden)
                        lstRooms.Items.Add(room);
                }//end foreach
            }//end if
            else
            {
                //Filter rooms booked
                foreach (var room in lstRooms.Items)
                {
                    if (database.Bookings.IsRoomBooked((IRoom)room, dtNotBookedOn.Value))
                        lstRooms.Items.Remove((IRoom)room);
                }//end for each
            }//end if
            if (lstRooms.Items.Count > 0)
                lstRooms.SelectedIndex = 0;
        }//RefreshLists
        private void lstRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRoom room = (IRoom)lstRooms.SelectedItem;
            lblRoomNumber.Text = room.RoomNumber;
            lblTypeOfRoom.Text = (room.IsSingleRoom) ? TypeOfRoom.SingleRoom.ToString() : TypeOfRoom.SharingRoom.ToString();
            lblRoomPrice.Text = room.Price.ToString("C2");
            if (room is ISingleRoom)
                picRoom.ImageLocation = @"images/single.jpg";
            else
                picRoom.ImageLocation = @"images/double.jpeg";
        }//lstRooms_SelectedIndexChanged

        private void radDoubleRoom_Click(object sender, EventArgs e)
        {
            KryptonRadioButton radSelected = (KryptonRadioButton)sender;
            if (radSelected == radAllRooms)
                RefreshLists();
            if (radSelected == radSingleRooms)
                FilterRooms<ISingleRoom>();
            if (radSelected == radDoubleRoom)
                FilterRooms<IDoubleRoom>();
        }//radDoubleRoom_Click
        private void FilterRooms<T>()
            where T : IRoom
        {
            lstRooms.Items.Clear();
            foreach (T room in database.Rooms.GetRoomFilter<T>())
            {
                if(!room.IsRoomHidden)
                    lstRooms.Items.Add(room);
            }//end foreach
        }//RefreshLists

        private void dtNotBookedOn_ValueChanged(object sender, EventArgs e)
        {
            RefreshLists(false);
        }//dtNotBookedOn_ValueChanged
    }//class
}//namespcae
