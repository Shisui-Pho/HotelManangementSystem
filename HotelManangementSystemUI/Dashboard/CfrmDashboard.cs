using HotelManangementSystemLibrary.DatabaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManangementSystemUI.Dashboard
{
    public partial class CfrmDashboard : Form
    {
        //Database datamember
        private readonly IDatabaseService database;
        public CfrmDashboard(IDatabaseService database)
        {
            InitializeComponent();
            this.database = database;
        }//ctor 01
        private async Task LoadAllBookings()
        {
            //This will load all the other Properties
           await Task.Run(() => database.LoadBookings());
            //Also need to load warehouse data for admin purposes
        }//LoadGuest

        private async void CfrmDashboard_Shown(object sender, EventArgs e)
        {
            //Load Data after form has been loaded
            await LoadAllBookings();
            //Do some set ups
        }//CfrmDashboard_Shown
    }//class
}//namespace
