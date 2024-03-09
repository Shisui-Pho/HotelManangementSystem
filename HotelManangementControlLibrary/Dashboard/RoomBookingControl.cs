using HotelManangementSystemLibrary.DatabaseService;
using System;
using System.Windows.Forms;
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
            ListRooms(database.Rooms);
        }//ctor 01
        private void ListRooms(IRooms rooms)
        {
            lstRooms.Items.Clear();
            foreach (IRoom room in rooms)
            {
                lstRooms.Items.Add(room);
            }//end foreach
            //Selct the first room
            if (rooms.Count > 0)
                lstRooms.SelectedIndex = 0;
        }//FillList
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
            IRoom room = (IRoom)lstRooms.SelectedItem;
            lblRoomNumber.Text = room.RoomNumber;
            lblTypeOfRoom.Text = (room.IsSingleRoom) ? TypeOfRoom.SingleRoom.ToString() : TypeOfRoom.SharingRoom.ToString();
            lblRoomPrice.Text = ((decimal)500.6m).ToString("C");
            if (room is ISingleRoom)
                picRoom.ImageLocation = @"images/single.jpg";
            else
                picRoom.ImageLocation = @"images/double.jpeg";
        }//lstRooms_SelectedIndexChanged
    }//class
}//namespcae
