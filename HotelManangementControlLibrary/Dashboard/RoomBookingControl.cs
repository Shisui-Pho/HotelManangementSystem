using HotelManangementSystemLibrary.DatabaseService;
using System;
using System.Windows.Forms;
using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;
using ComponentFactory.Krypton.Toolkit;
using HotelManangementControlLibrary.Custome_Message_Box_Form;

namespace HotelManangementControlLibrary.Dashboard
{
    public partial class RoomBookingControl : UserControl
    {
        private readonly delBookRoom funcBookRoom;
        private readonly IDatabaseService database;
        private IRoom selectedRoom;
        public RoomBookingControl(IDatabaseService database, delBookRoom bookroom_method)
        {
            InitializeComponent();
            this.database = database;
            funcBookRoom = bookroom_method;
            //Set the display members
            lstRooms.DisplayMember = "RoomNumber";
            RefreshLists();
        }//ctor 01
        public RoomBookingControl()
        {
            InitializeComponent();
        }//
        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            //Book the room
            if (selectedRoom is null)
            {
                Messages.ShowErrorMessage("Please select room", "Selection error");
                return;
            }//end if
            if (funcBookRoom(selectedRoom,dtBookDate.Value,(int)numBookingLength.Value))
            {
                //Refresh
                RefreshLists();
            }//book the room via a delegate method
        }//btnBookRoom_Click
        private void RefreshLists(bool isFirstTime = true)
        {
            if (isFirstTime)
            {
                //Start with the rooms first
                lstRooms.Items.Clear();
                foreach (IRoom room in database.Rooms)
                {
                    if(!room.IsRoomUnderMaintenance)
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
            selectedRoom = (IRoom)lstRooms.SelectedItem;
            lblRoomNumber.Text = selectedRoom.RoomNumber;
            lblTypeOfRoom.Text = (selectedRoom.IsSingleRoom) ? TypeOfRoom.SingleRoom.ToString() : TypeOfRoom.SharingRoom.ToString();
            lblRoomPrice.Text = selectedRoom.Price.ToString("C2");
            if (selectedRoom is ISingleRoom)
                picRoom.Image = Properties.Resources.single;
            else
                picRoom.Image = Properties.Resources._double;
            //Add room bookings
            AddBookings(database.Bookings.HasBookings(selectedRoom));
        }//lstRooms_SelectedIndexChanged
        private void AddBookings(IRoomBooking[] bookings)
        {
            lstbxAllBookings.Items.Clear();
            if (bookings.Length <= 0)
            {
                lstbxAllBookings.Items.Add("Room has not been booked");
                return;
            }//end if

            for (int i = 0; i < bookings.Length; i++)
            {
                for (int j = 0; j < bookings[i].NumberOfDaysToStay; j++)
                {
                    lstbxAllBookings.Items.Add(bookings[i].DateBookedFor.AddDays(j).ToString("dd  MMMM  yyyy"));
                }//end outer for
            }//end for
        }//AddBooking
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
                if(!room.IsRoomUnderMaintenance)
                    lstRooms.Items.Add(room);
            }//end foreach
        }//RefreshLists

        private void dtNotBookedOn_ValueChanged(object sender, EventArgs e)
        {
            RefreshLists(false);
        }//dtNotBookedOn_ValueChanged

        private void btnMoreInfo_Click(object sender, EventArgs e)
        {
            if(selectedRoom is null)
            {
                Messages.ShowErrorMessage("Please select a room");
                return;
            }
            CdlgDisplayRoomInfo messageBox = new CdlgDisplayRoomInfo(selectedRoom);
            
            //Display the custom message box
            messageBox.ShowDialog();

        }//btnMoreInfo_Click
    }//class
}//namespcae
