using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManangementControlLibrary.Dashboard.Admin
{
    public partial class RoomsControl : UserControl
    {
        //Functions
        private readonly delModifyRoom ModifyRoomFunc;
        private readonly delAddNewRoom AddNewRoomFunc;

        //datamember
        private readonly IRooms _rooms;
        public RoomsControl(delModifyRoom mod, delAddNewRoom add)
        {
            InitializeComponent();
            ModifyRoomFunc = mod;
            AddNewRoomFunc = add;
        }//ctor 01
        public RoomsControl(IRooms rooms, delModifyRoom mod, delAddNewRoom add)
        {
            InitializeComponent();
            this._rooms = rooms;
            ModifyRoomFunc = mod;
            AddNewRoomFunc = add;
        }//ctor 01
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            //Spawn a form here
            IRoom room = AddNewRoomFunc();
            if (room is null)
                return;
            _rooms.Add(room);
            //Refresh list
        }//btnAddRoom_Click

        private void btnModifyRoom_Click(object sender, EventArgs e)
        {
            int index = lstbxRooms.SelectedIndex;
            if (index < 0)
            {
                Messages.ShowErrorMessage("Please selected a room.", "SELECTION ERROR");
                return;
            }//end if

            IRoom oldRoom = (IRoom)lstbxRooms.Items[index];
            IRoom modifiedRoom = ModifyRoomFunc(oldRoom);
            if (modifiedRoom is null)
                return;

            //Modify the room
            _rooms.Remove(oldRoom);
            _rooms.Add(modifiedRoom);

            //Refresh
            lstbxRooms.Items.Remove(oldRoom);
            lstbxRooms.Items.Insert(index,modifiedRoom);
            lstbxRooms.SelectedIndex = index;
        }//btnModifyRoom_Click

        private void btnHideRoom_Click(object sender, EventArgs e)
        {
            //Perform somethings here
        }//btnHideRoom_Click

        private void btnRemoveRoom_Click(object sender, EventArgs e)
        {
            int index = lstbxRooms.SelectedIndex;
            if (index < 0)
            {
                Messages.ShowErrorMessage("Please selected a room.", "SELECTION ERROR");
                return;
            }//end if
            IRoom room = (IRoom)lstbxRooms.Items[index];
            if (Messages.Warning($"Are your sure you want to remove {room.RoomNumber} ?", "Confirmation")
                == DialogResult.No)
                return;
            _rooms.Remove(room);
            //Refresh
            lstbxRooms.Items.RemoveAt(index);
            lstbxRooms.SelectedIndex = 0;
        }//btnRemoveRoom_Click
    }//class
}//namespace
