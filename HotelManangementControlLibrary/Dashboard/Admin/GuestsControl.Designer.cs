﻿namespace HotelManangementControlLibrary.Dashboard.Admin
{
    partial class GuestsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonLabel18 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // kryptonLabel18
            // 
            this.kryptonLabel18.AutoSize = false;
            this.kryptonLabel18.Location = new System.Drawing.Point(153, 139);
            this.kryptonLabel18.Name = "kryptonLabel18";
            this.kryptonLabel18.Size = new System.Drawing.Size(648, 503);
            this.kryptonLabel18.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel18.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel18.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.kryptonLabel18.TabIndex = 20;
            this.kryptonLabel18.Values.Text = "Guest Control";
            // 
            // GuestsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonLabel18);
            this.Name = "GuestsControl";
            this.Size = new System.Drawing.Size(955, 780);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel18;
    }
}