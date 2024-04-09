using HotelManangementSystemLibrary;
using HotelManangementSystemLibrary.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
namespace HotelManangementSystemUI.Input_Forms
{
    public partial class CdlgCustomRooms : Form
    {
        public IRoom Room { get; private set; } = null;
        public CdlgCustomRooms(IRoom _toModify = null)
        {
            InitializeComponent();
            if(_toModify != null)
            {
                //Fill in controls

            }//end if
        }//ctor 01

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IRoom room = (radSingle.Checked) ? RoomFactory.CreateRoom(TypeOfRoom.SingleRoom, txtRoomNumber.Text)
                                             : RoomFactory.CreateRoom(TypeOfRoom.SharingRoom, txtRoomNumber.Text);
            //Add other stuff here

            //Set the room property
            Room = room;
        }//btnAdd_Click

        private void radDouble_Click(object sender, EventArgs e)
        {
            KryptonRadioButton clicked = (KryptonRadioButton)sender;

            if (clicked == radSingle)
            {
                picRoom.Image = Properties.Resources.single;
                lblStandardPrice.Text = Standard.SingleRoomPrice.ToString("C2");
            }
            else
            {
                picRoom.Image = Properties.Resources._double;
                lblStandardPrice.Text = Standard.DoubleRoomPrice.ToString("C2");
            }
                
        }//radDouble_Click
    }//class
}//namespace
