using HotelManangementSystemLibrary;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
namespace HotelManangementControlLibrary.Service
{
    internal static class Filter
    {

        public static void FilterTypesOfRooms<T>(KryptonListBox lstbxRooms, IRooms _rooms) where T : IHotelModel<T>
        {
            lstbxRooms.Items.Clear();
            foreach (var item in _rooms)
            {
                if (item is T)
                    lstbxRooms.Items.Add(item);
            }//end foreach
        }//FilterTypesOfRooms
        public static void FilterStatusOfRooms(KryptonListBox lstbxRooms, bool? hidden)
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
    }
}
