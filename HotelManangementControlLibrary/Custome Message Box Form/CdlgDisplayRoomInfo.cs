using HotelManangementSystemLibrary;
using System.Windows.Forms;

namespace HotelManangementControlLibrary.Custome_Message_Box_Form
{
    public partial class CdlgDisplayRoomInfo : Form
    {
        public CdlgDisplayRoomInfo(IRoom room)
        {
            InitializeComponent();
            lblRoomNumber.Text = room.RoomNumber;
            lblPrice.Text = room.Price.ToString("C2");
            lblTypeOfRoom.Text = room.IsSingleRoom ? "Single room" : "Double room";
            picRoom.Image = room.IsSingleRoom ? Properties.Resources.single : Properties.Resources._double;
        }//ctor 01
    }//class
}//namespace
