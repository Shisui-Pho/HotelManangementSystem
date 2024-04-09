using HotelManangementSystemLibrary;
using System;
using System.Windows.Forms;

namespace HotelManangementControlLibrary.Input_Forms
{
    public partial class CdlgAddRoomFeature : Form
    {
        private static readonly IFeatures features = Features.GetFeaturesInstance();
        public IFeature Feature { get; private set; }
        public CdlgAddRoomFeature(string roomNumber)
        {
            InitializeComponent();
            LoadFeatures();
            btnAddFeature.Visible = false;
            lblRoomNumber.Text = roomNumber;
        }//CTOR 01
        private void LoadFeatures()
        {
            foreach (IFeature item in features)
            {
                lstbxFeatures.Items.Add(item);
            }
        }//LoadFeatures

        private void lstbxFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstbxFeatures.SelectedIndex;
            if (index < 0)
                return;
            btnAddFeature.Visible = true;
            IFeature fet = (IFeature)lstbxFeatures.Items[index];
            lblName.Text = fet.FeatureName;
            lblPrice.Text = fet.Price.ToString("C2");
            txtDescript.Text = fet.Description;

            Feature = fet;
        }//lstbxFeatures_SelectedIndexChanged

        private void btnNewFeature_Click(object sender, EventArgs e)
        {
            CdlgNewFeature newfet = new CdlgNewFeature();
            if(newfet.ShowDialog() == DialogResult.OK)
            {
                features.Add(newfet.Feature);
                LoadFeatures();
                lstbxFeatures.SelectedIndex = lstbxFeatures.Items.Count - 1;//Last feature
            }//end if
        }//btnNewFeature_Click
    }//CLASS
}//NAMESPACE
