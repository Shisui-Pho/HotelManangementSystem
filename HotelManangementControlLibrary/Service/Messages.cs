using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManangementControlLibrary.Service
{
    public static class Messages
    {
        public static void ShowErrorMessage(string sMsg, string title = "Hotel Manangement System")
        {
            MessageBox.Show(sMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        
        public static void ShowInformationMessage(string sMsg, string title)
        {
            MessageBox.Show(sMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult AskYesOrNot(string sMsg, string title)
        {
            return MessageBox.Show(sMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Warning(string sMsg, string title)
        {
            return MessageBox.Show(sMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}
