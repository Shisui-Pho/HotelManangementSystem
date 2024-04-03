
namespace HotelManangementControlLibrary.Input_Forms
{
    partial class CdlgPayBooking
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
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmountToPay = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblBalance = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblMessage = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel12 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnPay = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancelBooking = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.numAmount);
            this.panel1.Controls.Add(this.lblAmountToPay);
            this.panel1.Controls.Add(this.lblBalance);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.kryptonLabel12);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.kryptonLabel9);
            this.panel1.Location = new System.Drawing.Point(25, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 235);
            this.panel1.TabIndex = 0;
            // 
            // numAmount
            // 
            this.numAmount.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numAmount.Location = new System.Drawing.Point(90, 130);
            this.numAmount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(155, 20);
            this.numAmount.TabIndex = 0;
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);
            // 
            // lblAmountToPay
            // 
            this.lblAmountToPay.Location = new System.Drawing.Point(166, 78);
            this.lblAmountToPay.Name = "lblAmountToPay";
            this.lblAmountToPay.Size = new System.Drawing.Size(60, 26);
            this.lblAmountToPay.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.lblAmountToPay.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountToPay.TabIndex = 19;
            this.lblAmountToPay.Values.Text = "R0,00";
            // 
            // lblBalance
            // 
            this.lblBalance.Location = new System.Drawing.Point(166, 20);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(60, 26);
            this.lblBalance.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.lblBalance.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.TabIndex = 19;
            this.lblBalance.Values.Text = "R0,00";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = false;
            this.lblMessage.Location = new System.Drawing.Point(21, 158);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(224, 70);
            this.lblMessage.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
            this.lblMessage.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblMessage.StateCommon.ShortText.TextV = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblMessage.TabIndex = 19;
            this.lblMessage.Values.Text = "";
            // 
            // kryptonLabel12
            // 
            this.kryptonLabel12.Location = new System.Drawing.Point(3, 125);
            this.kryptonLabel12.Name = "kryptonLabel12";
            this.kryptonLabel12.Size = new System.Drawing.Size(87, 26);
            this.kryptonLabel12.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel12.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel12.TabIndex = 19;
            this.kryptonLabel12.Values.Text = "Amount :";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 78);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(130, 26);
            this.kryptonLabel2.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel2.TabIndex = 20;
            this.kryptonLabel2.Values.Text = "Amount to pay";
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(3, 20);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(141, 26);
            this.kryptonLabel9.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel9.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel9.TabIndex = 20;
            this.kryptonLabel9.Values.Text = "Current Balance ";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(80, 5);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(192, 26);
            this.kryptonLabel7.StateCommon.ShortText.Color1 = System.Drawing.Color.RoyalBlue;
            this.kryptonLabel7.StateCommon.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel7.TabIndex = 18;
            this.kryptonLabel7.Values.Text = "Payment Confirmation";
            // 
            // btnPay
            // 
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPay.Location = new System.Drawing.Point(30, 278);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(95, 45);
            this.btnPay.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnPay.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnPay.StateCommon.Border.Rounding = 5;
            this.btnPay.StateCommon.Border.Width = 1;
            this.btnPay.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnPay.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btnPay.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnPay.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btnPay.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnPay.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnPay.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnPay.TabIndex = 1;
            this.btnPay.Values.Text = "Pay";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelBooking.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelBooking.Location = new System.Drawing.Point(203, 278);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(95, 45);
            this.btnCancelBooking.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateCommon.Border.Color2 = System.Drawing.Color.MediumAquamarine;
            this.btnCancelBooking.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCancelBooking.StateCommon.Border.Rounding = 5;
            this.btnCancelBooking.StateCommon.Border.Width = 1;
            this.btnCancelBooking.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancelBooking.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btnCancelBooking.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelBooking.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancelBooking.StatePressed.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btnCancelBooking.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(194)))), ((int)(((byte)(126)))));
            this.btnCancelBooking.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.WhiteSmoke;
            this.btnCancelBooking.StateTracking.Content.ShortText.Color2 = System.Drawing.Color.WhiteSmoke;
            this.btnCancelBooking.TabIndex = 2;
            this.btnCancelBooking.Values.Text = "Cancel";
            // 
            // CdlgPayBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 334);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnCancelBooking);
            this.Controls.Add(this.kryptonLabel7);
            this.Controls.Add(this.panel1);
            this.Name = "CdlgPayBooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPay;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancelBooking;
        private System.Windows.Forms.NumericUpDown numAmount;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAmountToPay;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblBalance;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMessage;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel12;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
    }
}