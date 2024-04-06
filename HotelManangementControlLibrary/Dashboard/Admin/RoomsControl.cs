using HotelManangementControlLibrary.Service;
using HotelManangementSystemLibrary;
using System;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
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
            lstbxRooms.DisplayMember = "RoomNumber";
            RefreshListBoxAndComboBoxes();
            SubScribeToEvents();
        }//ctor 01
        private void SubScribeToEvents()
        {
            foreach (IRoom item in _rooms)
            {
                item.OnPriceChangedEvent += Item_OnPriceChangedEvent;
            }
        }

        private void Item_OnPriceChangedEvent(IRoom room)
        {
            lblRoomPrice.Text = room.Price.ToString("C2");
        }//Item_OnPriceChangedEvent

        private void RefreshListBoxAndComboBoxes()
        {
            cmboRoomStatus.Items.Clear();
            cmboRoomStatus.Items.AddRange(new string[] { "All", "Hidden", "Unhidden" });
            cmboTypeOfRooms.Items.Clear();
            cmboTypeOfRooms.Items.AddRange(new string[] { "All","Single","Double"});

            cmboTypeOfRooms.SelectedIndex = 0;
            cmboRoomStatus.SelectedIndex = 0;
        }//RefreshListBox
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
            //_rooms.Remove(oldRoom);
            //_rooms.Add(modifiedRoom);
            _rooms.Update(oldRoom, modifiedRoom);
            //Refresh
            lstbxRooms.Items.Remove(oldRoom);
            lstbxRooms.Items.Insert(index,modifiedRoom);
            lstbxRooms.SelectedIndex = index;
        }//btnModifyRoom_Click

        private void btnHideRoom_Click(object sender, EventArgs e)
        {
            //Perform somethings here
            int i = lstbxRooms.SelectedIndex;
            if(i < 0)
            {
                Messages.ShowErrorMessage("Please select a room to hide");
                return;
            }//end if
            IRoom room = (IRoom)lstbxRooms.Items[i];
            //if (Messages.AskYesOrNot($"You are about to {btnHideRoom.Text} {room.RoomNumber}.\nDou you wish to continue with this operation.", "Hide room")
            //     == DialogResult.No)
            //    return;
            _rooms[room.RoomNumber].MaintenanceSwitch();
            ApplyFilter();
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
            ApplyFilter();
        }//btnRemoveRoom_Click
        private void cmboTypeOfRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }//cmboTypeOfRooms_SelectedIndexChanged

        private void cmboRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }//cmboRoomStatus_SelectedIndexChanged
        private void lstbxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRoom room = (IRoom)lstbxRooms.SelectedItem;
            if (room == null)
                return;
            picRoom.Image = room.IsRoomUnderMaintenance ? Properties.Resources.single : Properties.Resources._double;
            //Perfom some operations
            lblIsMaintenance.Text = room.IsRoomUnderMaintenance.ToString();
            lblRoomNumber.Text = room.RoomNumber;
            lblRoomPrice.Text = room.Price.ToString("C2");
            lstbxFeatures.Items.Clear();
            foreach (IFeature item in room.RoomFeatures.GetRoomFeatures())
            {
                lstbxFeatures.Items.Add(item);
            }
        }//lstbxRooms_SelectedIndexChanged
        #region Filter
        private void ApplyFilter()
        {
            //For filtering room type
            if (cmboTypeOfRooms.SelectedIndex == 0)
                FilterTypesOfRooms<IRoom>();//All the rooms
            if (cmboTypeOfRooms.SelectedIndex == 1)
                FilterTypesOfRooms<ISingleRoom>();//Only single rooms
            if (cmboTypeOfRooms.SelectedIndex == 2)
                FilterTypesOfRooms<IDoubleRoom>();//Only double rooms

            //For filtering room status
            if (cmboRoomStatus.SelectedIndex == 0)
                FilterStatusOfRooms(null);
            if (cmboRoomStatus.SelectedIndex == 1)
                FilterStatusOfRooms(true);
            if (cmboRoomStatus.SelectedIndex == 2)
                FilterStatusOfRooms(false);
        }//ApplyStatusFilter
        private void FilterTypesOfRooms<T>() where T : IHotelModel<T>
        {
            lstbxRooms.Items.Clear();
            foreach (var item in _rooms)
            {
                if (item is T)
                    lstbxRooms.Items.Add(item);
            }//end foreach
        }//FilterTypesOfRooms
        private void FilterStatusOfRooms(bool? hidden)
        {
            if (hidden == null)
                return;
            ListBox lstBox = new ListBox();
            foreach (var item in lstbxRooms.Items)
            {
                if (hidden == ((IRoom)item).IsRoomUnderMaintenance)
                    lstBox.Items.Add((IRoom)item);
            }//end foreach

            lstbxRooms.Items.Clear();
            lstbxRooms.Items.AddRange(lstBox.Items);
        }//FilterStatusOfRooms
        #endregion Filter

        private void lstbxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFeature feature = (IFeature)lstbxFeatures.SelectedItem;
            if (feature is null)
                return;
            lblFeatureName.Text = feature.FeatureName;
            lblFeaturePrice.Text = feature.Price.ToString("C2");
            txtFeatureDescription.Text = feature.Description;
        }//lstbxFeatures_SelectedIndexChanged

        private void btnAddFeature_Click(object sender, EventArgs e)
        {
            //A feature will be added
        }//btnAddFeature_Click

        private void btnRemoveFeature_Click(object sender, EventArgs e)
        {
            //A feature will be removed
            IFeature f = (IFeature)lstbxFeatures.SelectedItem;
            if (f is null)
                return;

            string featureID = f.FeatureID;
            IRoom room = (IRoom)lstbxRooms.SelectedItem;
            room.RoomFeatures.RemoveFeature(featureID);
        }//btnRemoveFeature_Click
    }//class
}//namespace
