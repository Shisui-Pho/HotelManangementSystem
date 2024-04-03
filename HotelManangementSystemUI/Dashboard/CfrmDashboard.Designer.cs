
namespace HotelManangementSystemUI.Dashboard
{
    partial class CfrmDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plnCommonControls = new System.Windows.Forms.Panel();
            this.plnAdminPanel = new System.Windows.Forms.Panel();
            this.btnStatistics = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnManangeOldBookings = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnStaff = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnManangeGuests = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnManangeBookings = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnManangeRooms = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnBookRoom = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.plnGuestPanel = new System.Windows.Forms.Panel();
            this.btnViewProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnViewBookings = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSignOut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.plnHeader = new System.Windows.Forms.Panel();
            this.lblNameAndSurname = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.plnContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.plnCommonControls.SuspendLayout();
            this.plnAdminPanel.SuspendLayout();
            this.plnGuestPanel.SuspendLayout();
            this.plnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(111)))), ((int)(((byte)(150)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnSignOut);
            this.panel1.Controls.Add(this.plnHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 790);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.plnCommonControls);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 251);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 477);
            this.panel2.TabIndex = 5;
            // 
            // plnCommonControls
            // 
            this.plnCommonControls.Controls.Add(this.plnAdminPanel);
            this.plnCommonControls.Controls.Add(this.btnBookRoom);
            this.plnCommonControls.Controls.Add(this.plnGuestPanel);
            this.plnCommonControls.Location = new System.Drawing.Point(1, 3);
            this.plnCommonControls.Name = "plnCommonControls";
            this.plnCommonControls.Size = new System.Drawing.Size(280, 478);
            this.plnCommonControls.TabIndex = 0;
            // 
            // plnAdminPanel
            // 
            this.plnAdminPanel.Controls.Add(this.btnStatistics);
            this.plnAdminPanel.Controls.Add(this.btnManangeOldBookings);
            this.plnAdminPanel.Controls.Add(this.btnStaff);
            this.plnAdminPanel.Controls.Add(this.btnManangeGuests);
            this.plnAdminPanel.Controls.Add(this.btnManangeBookings);
            this.plnAdminPanel.Controls.Add(this.btnManangeRooms);
            this.plnAdminPanel.Location = new System.Drawing.Point(3, 64);
            this.plnAdminPanel.Name = "plnAdminPanel";
            this.plnAdminPanel.Size = new System.Drawing.Size(280, 378);
            this.plnAdminPanel.TabIndex = 0;
            this.plnAdminPanel.Visible = false;
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(7, 315);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(249, 51);
            this.btnStatistics.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnStatistics.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnStatistics.StateCommon.Border.Rounding = 5;
            this.btnStatistics.StateCommon.Border.Width = 1;
            this.btnStatistics.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnStatistics.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnStatistics.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistics.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnStatistics.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnStatistics.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnStatistics.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnStatistics.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnStatistics.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnStatistics.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStatistics.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnStatistics.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnStatistics.TabIndex = 4;
            this.btnStatistics.Tag = "ControlButton";
            this.btnStatistics.Values.Text = "Hotel Statistics";
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // btnManangeOldBookings
            // 
            this.btnManangeOldBookings.Location = new System.Drawing.Point(7, 258);
            this.btnManangeOldBookings.Name = "btnManangeOldBookings";
            this.btnManangeOldBookings.Size = new System.Drawing.Size(249, 51);
            this.btnManangeOldBookings.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnManangeOldBookings.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnManangeOldBookings.StateCommon.Border.Rounding = 5;
            this.btnManangeOldBookings.StateCommon.Border.Width = 1;
            this.btnManangeOldBookings.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeOldBookings.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeOldBookings.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManangeOldBookings.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeOldBookings.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeOldBookings.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeOldBookings.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeOldBookings.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeOldBookings.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeOldBookings.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeOldBookings.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeOldBookings.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeOldBookings.TabIndex = 4;
            this.btnManangeOldBookings.Tag = "ControlButton";
            this.btnManangeOldBookings.Values.Text = "View old Bookings";
            this.btnManangeOldBookings.Click += new System.EventHandler(this.btnManangeOldBookings_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(7, 196);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(249, 51);
            this.btnStaff.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnStaff.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnStaff.StateCommon.Border.Rounding = 5;
            this.btnStaff.StateCommon.Border.Width = 1;
            this.btnStaff.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnStaff.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnStaff.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaff.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnStaff.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnStaff.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnStaff.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnStaff.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnStaff.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.TabIndex = 4;
            this.btnStaff.Tag = "ControlButton";
            this.btnStaff.Values.Text = "Hotel Staff";
            // 
            // btnManangeGuests
            // 
            this.btnManangeGuests.Location = new System.Drawing.Point(7, 134);
            this.btnManangeGuests.Name = "btnManangeGuests";
            this.btnManangeGuests.Size = new System.Drawing.Size(249, 51);
            this.btnManangeGuests.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnManangeGuests.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnManangeGuests.StateCommon.Border.Rounding = 5;
            this.btnManangeGuests.StateCommon.Border.Width = 1;
            this.btnManangeGuests.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeGuests.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeGuests.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManangeGuests.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeGuests.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeGuests.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeGuests.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeGuests.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeGuests.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeGuests.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeGuests.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeGuests.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeGuests.TabIndex = 4;
            this.btnManangeGuests.Tag = "ControlButton";
            this.btnManangeGuests.Values.Text = "Manange &Guests";
            this.btnManangeGuests.Click += new System.EventHandler(this.btnManangeGuests_Click);
            // 
            // btnManangeBookings
            // 
            this.btnManangeBookings.Location = new System.Drawing.Point(7, 71);
            this.btnManangeBookings.Name = "btnManangeBookings";
            this.btnManangeBookings.Size = new System.Drawing.Size(249, 51);
            this.btnManangeBookings.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnManangeBookings.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnManangeBookings.StateCommon.Border.Rounding = 5;
            this.btnManangeBookings.StateCommon.Border.Width = 1;
            this.btnManangeBookings.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeBookings.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeBookings.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManangeBookings.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeBookings.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeBookings.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeBookings.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeBookings.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeBookings.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeBookings.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeBookings.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeBookings.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeBookings.TabIndex = 4;
            this.btnManangeBookings.Tag = "ControlButton";
            this.btnManangeBookings.Values.Text = "Manange &Bookings";
            this.btnManangeBookings.Click += new System.EventHandler(this.btnManangeBookings_Click);
            // 
            // btnManangeRooms
            // 
            this.btnManangeRooms.Location = new System.Drawing.Point(7, 8);
            this.btnManangeRooms.Name = "btnManangeRooms";
            this.btnManangeRooms.Size = new System.Drawing.Size(249, 51);
            this.btnManangeRooms.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnManangeRooms.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnManangeRooms.StateCommon.Border.Rounding = 5;
            this.btnManangeRooms.StateCommon.Border.Width = 1;
            this.btnManangeRooms.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeRooms.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeRooms.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManangeRooms.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeRooms.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnManangeRooms.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnManangeRooms.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnManangeRooms.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeRooms.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeRooms.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnManangeRooms.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeRooms.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnManangeRooms.TabIndex = 4;
            this.btnManangeRooms.Tag = "ControlButton";
            this.btnManangeRooms.Values.Text = "Manange &Rooms";
            this.btnManangeRooms.Click += new System.EventHandler(this.btnManangeRooms_Click);
            // 
            // btnBookRoom
            // 
            this.btnBookRoom.Location = new System.Drawing.Point(10, 6);
            this.btnBookRoom.Name = "btnBookRoom";
            this.btnBookRoom.Size = new System.Drawing.Size(249, 51);
            this.btnBookRoom.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnBookRoom.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnBookRoom.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnBookRoom.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBookRoom.StateCommon.Border.Rounding = 5;
            this.btnBookRoom.StateCommon.Border.Width = 1;
            this.btnBookRoom.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnBookRoom.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnBookRoom.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookRoom.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnBookRoom.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnBookRoom.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnBookRoom.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnBookRoom.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnBookRoom.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnBookRoom.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnBookRoom.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnBookRoom.TabIndex = 4;
            this.btnBookRoom.Tag = "ControlButton";
            this.btnBookRoom.Values.Text = "&Booking";
            this.btnBookRoom.Click += new System.EventHandler(this.btnBookRoom_Click);
            // 
            // plnGuestPanel
            // 
            this.plnGuestPanel.Controls.Add(this.btnViewProfile);
            this.plnGuestPanel.Controls.Add(this.btnViewBookings);
            this.plnGuestPanel.Location = new System.Drawing.Point(3, 49);
            this.plnGuestPanel.Name = "plnGuestPanel";
            this.plnGuestPanel.Size = new System.Drawing.Size(259, 142);
            this.plnGuestPanel.TabIndex = 0;
            // 
            // btnViewProfile
            // 
            this.btnViewProfile.Location = new System.Drawing.Point(7, 79);
            this.btnViewProfile.Name = "btnViewProfile";
            this.btnViewProfile.Size = new System.Drawing.Size(249, 51);
            this.btnViewProfile.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnViewProfile.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnViewProfile.StateCommon.Border.Rounding = 5;
            this.btnViewProfile.StateCommon.Border.Width = 1;
            this.btnViewProfile.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnViewProfile.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnViewProfile.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewProfile.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnViewProfile.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnViewProfile.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnViewProfile.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnViewProfile.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnViewProfile.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnViewProfile.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewProfile.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnViewProfile.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnViewProfile.TabIndex = 4;
            this.btnViewProfile.Tag = "ControlButton";
            this.btnViewProfile.Values.Text = "View Profile";
            this.btnViewProfile.Click += new System.EventHandler(this.btnViewProfile_Click);
            // 
            // btnViewBookings
            // 
            this.btnViewBookings.Location = new System.Drawing.Point(7, 16);
            this.btnViewBookings.Name = "btnViewBookings";
            this.btnViewBookings.Size = new System.Drawing.Size(249, 51);
            this.btnViewBookings.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnViewBookings.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnViewBookings.StateCommon.Border.Rounding = 5;
            this.btnViewBookings.StateCommon.Border.Width = 1;
            this.btnViewBookings.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnViewBookings.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnViewBookings.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewBookings.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnViewBookings.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnViewBookings.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateNormal.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnViewBookings.StateNormal.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnViewBookings.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnViewBookings.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnViewBookings.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnViewBookings.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnViewBookings.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnViewBookings.TabIndex = 4;
            this.btnViewBookings.Tag = "ControlButton";
            this.btnViewBookings.Values.Text = "View Bookings";
            this.btnViewBookings.Click += new System.EventHandler(this.btnViewBookings_Click);
            // 
            // btnSignOut
            // 
            this.btnSignOut.Location = new System.Drawing.Point(3, 734);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(249, 51);
            this.btnSignOut.StateCommon.Back.Color1 = System.Drawing.Color.Red;
            this.btnSignOut.StateCommon.Back.Color2 = System.Drawing.Color.Red;
            this.btnSignOut.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnSignOut.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSignOut.StateCommon.Border.Rounding = 5;
            this.btnSignOut.StateCommon.Border.Width = 1;
            this.btnSignOut.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnSignOut.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnSignOut.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignOut.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnSignOut.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(146)))), ((int)(((byte)(163)))));
            this.btnSignOut.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.Red;
            this.btnSignOut.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.Red;
            this.btnSignOut.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnSignOut.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnSignOut.TabIndex = 4;
            this.btnSignOut.Values.Text = "&Sign out";
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // plnHeader
            // 
            this.plnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plnHeader.Controls.Add(this.lblNameAndSurname);
            this.plnHeader.Controls.Add(this.picUser);
            this.plnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.plnHeader.Location = new System.Drawing.Point(0, 0);
            this.plnHeader.Name = "plnHeader";
            this.plnHeader.Size = new System.Drawing.Size(280, 251);
            this.plnHeader.TabIndex = 0;
            // 
            // lblNameAndSurname
            // 
            this.lblNameAndSurname.AutoSize = false;
            this.lblNameAndSurname.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblNameAndSurname.Location = new System.Drawing.Point(2, 194);
            this.lblNameAndSurname.Name = "lblNameAndSurname";
            this.lblNameAndSurname.Size = new System.Drawing.Size(274, 39);
            this.lblNameAndSurname.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblNameAndSurname.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameAndSurname.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblNameAndSurname.StateCommon.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblNameAndSurname.TabIndex = 1;
            this.lblNameAndSurname.Values.Text = "Names and Surname";
            // 
            // picUser
            // 
            this.picUser.Image = global::HotelManangementSystemUI.Properties.Resources.generaluser;
            this.picUser.Location = new System.Drawing.Point(40, 10);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(191, 178);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUser.TabIndex = 0;
            this.picUser.TabStop = false;
            // 
            // plnContainer
            // 
            this.plnContainer.Location = new System.Drawing.Point(288, 0);
            this.plnContainer.Name = "plnContainer";
            this.plnContainer.Size = new System.Drawing.Size(955, 780);
            this.plnContainer.TabIndex = 1;
            // 
            // CfrmDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1252, 790);
            this.Controls.Add(this.plnContainer);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CfrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CfrmDashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CfrmDashboard_FormClosing);
            this.Shown += new System.EventHandler(this.CfrmDashboard_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.plnCommonControls.ResumeLayout(false);
            this.plnAdminPanel.ResumeLayout(false);
            this.plnGuestPanel.ResumeLayout(false);
            this.plnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plnHeader;
        private System.Windows.Forms.PictureBox picUser;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblNameAndSurname;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnSignOut;
        private System.Windows.Forms.Panel plnContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel plnCommonControls;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnBookRoom;
        private System.Windows.Forms.Panel plnGuestPanel;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnViewProfile;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnViewBookings;
        private System.Windows.Forms.Panel plnAdminPanel;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnStatistics;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnManangeOldBookings;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnStaff;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnManangeGuests;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnManangeBookings;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnManangeRooms;
    }
}