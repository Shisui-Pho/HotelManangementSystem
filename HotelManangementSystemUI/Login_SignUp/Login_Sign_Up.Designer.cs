
namespace HotelManangementSystemUI.Login_SignUp
{
    partial class Login_Sign_Up
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
            this.plnPictureLogo = new System.Windows.Forms.Panel();
            this.plnContainer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.plnPictureLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.plnPictureLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(387, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 543);
            this.panel1.TabIndex = 0;
            // 
            // plnPictureLogo
            // 
            this.plnPictureLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.plnPictureLogo.Controls.Add(this.pictureBox1);
            this.plnPictureLogo.Location = new System.Drawing.Point(21, 10);
            this.plnPictureLogo.Name = "plnPictureLogo";
            this.plnPictureLogo.Size = new System.Drawing.Size(282, 248);
            this.plnPictureLogo.TabIndex = 0;
            // 
            // plnContainer
            // 
            this.plnContainer.BackColor = System.Drawing.Color.Transparent;
            this.plnContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plnContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnContainer.Location = new System.Drawing.Point(0, 0);
            this.plnContainer.Name = "plnContainer";
            this.plnContainer.Size = new System.Drawing.Size(387, 543);
            this.plnContainer.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(100, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Login_Sign_Up
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(715, 543);
            this.Controls.Add(this.plnContainer);
            this.Controls.Add(this.panel1);
            this.Name = "Login_Sign_Up";
            this.Text = "Login_Sign_Up";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_Sign_Up_FormClosing);
            this.Load += new System.EventHandler(this.Login_Sign_Up_Load);
            this.Shown += new System.EventHandler(this.Login_Sign_Up_Shown);
            this.panel1.ResumeLayout(false);
            this.plnPictureLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plnPictureLogo;
        private System.Windows.Forms.Panel plnContainer;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}