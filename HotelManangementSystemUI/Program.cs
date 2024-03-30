using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManangementSystemUI.Dashboard;
using HotelManangementSystemUI.Login_SignUp;
namespace HotelManangementSystemUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CfrmDashboard());
            Application.Run(new Login_Sign_Up());
        }
    }
}
