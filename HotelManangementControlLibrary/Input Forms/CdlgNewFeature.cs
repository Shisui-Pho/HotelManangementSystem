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

namespace HotelManangementControlLibrary.Input_Forms
{
    public partial class CdlgNewFeature : Form
    {
        public Feature Feature { get; private set; }
        public CdlgNewFeature()
        {
            InitializeComponent();
            btnAddFeature.Visible = false;
        }

        private void txtFeatureName_TextChanged(object sender, EventArgs e)
        {
            if (txtDescript.Text.Length < 2 || txtFeatureName.Text.Length < 2 || nudFeaturePrice.Value < 10)
                btnAddFeature.Visible = false;
            else
                btnAddFeature.Visible = true;
        }//txtFeatureName_TextChanged

        private void btnAddFeature_Click(object sender, EventArgs e)
        {
            Feature = new Feature(txtFeatureName.Text, txtDescript.Text, nudFeaturePrice.Value);
        }//btnAddFeature_Click
    }//class
}//namespace
