
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
            this.plnHeader = new System.Windows.Forms.Panel();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSignOut = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plnCommonControls = new System.Windows.Forms.Panel();
            this.plnUserSpecificControls = new System.Windows.Forms.Panel();
            this.plnContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.plnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnSignOut);
            this.panel1.Controls.Add(this.plnHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 780);
            this.panel1.TabIndex = 0;
            // 
            // plnHeader
            // 
            this.plnHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plnHeader.Controls.Add(this.kryptonLabel4);
            this.plnHeader.Controls.Add(this.picUser);
            this.plnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.plnHeader.Location = new System.Drawing.Point(0, 0);
            this.plnHeader.Name = "plnHeader";
            this.plnHeader.Size = new System.Drawing.Size(302, 295);
            this.plnHeader.TabIndex = 0;
            // 
            // picUser
            // 
            this.picUser.Image = global::HotelManangementSystemUI.Properties.Resources.generaluser;
            this.picUser.Location = new System.Drawing.Point(40, 10);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(214, 218);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUser.TabIndex = 0;
            this.picUser.TabStop = false;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(56, 246);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(175, 26);
            this.kryptonLabel4.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel4.TabIndex = 1;
            this.kryptonLabel4.Values.Text = "Names and Surname";
            // 
            // btnSignOut
            // 
            this.btnSignOut.Location = new System.Drawing.Point(7, 717);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(288, 51);
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
            this.btnSignOut.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.Red;
            this.btnSignOut.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.Red;
            this.btnSignOut.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnSignOut.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.Red;
            this.btnSignOut.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.Red;
            this.btnSignOut.TabIndex = 4;
            this.btnSignOut.Values.Text = "&Sign out";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.plnUserSpecificControls);
            this.panel2.Controls.Add(this.plnCommonControls);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 416);
            this.panel2.TabIndex = 5;
            // 
            // plnCommonControls
            // 
            this.plnCommonControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.plnCommonControls.Location = new System.Drawing.Point(0, 0);
            this.plnCommonControls.Name = "plnCommonControls";
            this.plnCommonControls.Size = new System.Drawing.Size(302, 231);
            this.plnCommonControls.TabIndex = 0;
            // 
            // plnUserSpecificControls
            // 
            this.plnUserSpecificControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnUserSpecificControls.Location = new System.Drawing.Point(0, 231);
            this.plnUserSpecificControls.Name = "plnUserSpecificControls";
            this.plnUserSpecificControls.Size = new System.Drawing.Size(302, 185);
            this.plnUserSpecificControls.TabIndex = 1;
            // 
            // plnContainer
            // 
            this.plnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnContainer.Location = new System.Drawing.Point(304, 0);
            this.plnContainer.Name = "plnContainer";
            this.plnContainer.Size = new System.Drawing.Size(918, 780);
            this.plnContainer.TabIndex = 1;
            // 
            // CfrmDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1222, 780);
            this.Controls.Add(this.plnContainer);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CfrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CfrmDashboard";
            this.Shown += new System.EventHandler(this.CfrmDashboard_Shown);
            this.panel1.ResumeLayout(false);
            this.plnHeader.ResumeLayout(false);
            this.plnHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plnHeader;
        private System.Windows.Forms.PictureBox picUser;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel plnUserSpecificControls;
        private System.Windows.Forms.Panel plnCommonControls;
        public ComponentFactory.Krypton.Toolkit.KryptonButton btnSignOut;
        private System.Windows.Forms.Panel plnContainer;
    }
}