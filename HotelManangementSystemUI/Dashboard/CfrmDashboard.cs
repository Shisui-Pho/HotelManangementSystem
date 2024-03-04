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
        private readonly IDatabaseService database;
        public CfrmDashboard(IDatabaseService database)
        {
            InitializeComponent();
            this.database = database;
        }//ctor 01
    }//class
}//namespace
