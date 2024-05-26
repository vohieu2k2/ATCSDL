namespace ATCSDL
{
    partial class PayBill
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
            this.formPanel = new System.Windows.Forms.Panel();
            this.addCartBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.transportLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.sumLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.standDeliRadio = new System.Windows.Forms.RadioButton();
            this.fastDeliRadio = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.phoneTxt = new System.Windows.Forms.TextBox();
            this.nameCustomerTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.addressTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.paymentCb = new System.Windows.Forms.ComboBox();
            this.formPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // formPanel
            // 
            this.formPanel.AutoScroll = true;
            this.formPanel.Controls.Add(this.paymentCb);
            this.formPanel.Controls.Add(this.addCartBtn);
            this.formPanel.Controls.Add(this.backBtn);
            this.formPanel.Controls.Add(this.transportLabel);
            this.formPanel.Controls.Add(this.label9);
            this.formPanel.Controls.Add(this.sumLabel);
            this.formPanel.Controls.Add(this.label5);
            this.formPanel.Controls.Add(this.groupBox2);
            this.formPanel.Controls.Add(this.label4);
            this.formPanel.Controls.Add(this.label2);
            this.formPanel.Controls.Add(this.fLPanel);
            this.formPanel.Controls.Add(this.phoneTxt);
            this.formPanel.Controls.Add(this.nameCustomerTxt);
            this.formPanel.Controls.Add(this.label8);
            this.formPanel.Controls.Add(this.addressTxt);
            this.formPanel.Controls.Add(this.label6);
            this.formPanel.Controls.Add(this.label1);
            this.formPanel.Controls.Add(this.label3);
            this.formPanel.Location = new System.Drawing.Point(0, 1);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(1053, 544);
            this.formPanel.TabIndex = 0;
            // 
            // addCartBtn
            // 
            this.addCartBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCartBtn.Location = new System.Drawing.Point(575, 882);
            this.addCartBtn.Name = "addCartBtn";
            this.addCartBtn.Size = new System.Drawing.Size(184, 47);
            this.addCartBtn.TabIndex = 179;
            this.addCartBtn.Text = "Mua hàng";
            this.addCartBtn.UseVisualStyleBackColor = true;
            this.addCartBtn.Click += new System.EventHandler(this.addCartBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(273, 882);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(184, 47);
            this.backBtn.TabIndex = 180;
            this.backBtn.Text = "Về trang chủ";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // transportLabel
            // 
            this.transportLabel.AutoSize = true;
            this.transportLabel.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transportLabel.Location = new System.Drawing.Point(456, 774);
            this.transportLabel.Name = "transportLabel";
            this.transportLabel.Size = new System.Drawing.Size(113, 33);
            this.transportLabel.TabIndex = 178;
            this.transportLabel.Text = "0000000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(178, 774);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 33);
            this.label9.TabIndex = 177;
            this.label9.Text = "Phí vận chuyển";
            // 
            // sumLabel
            // 
            this.sumLabel.AutoSize = true;
            this.sumLabel.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sumLabel.Location = new System.Drawing.Point(456, 825);
            this.sumLabel.Name = "sumLabel";
            this.sumLabel.Size = new System.Drawing.Size(113, 33);
            this.sumLabel.TabIndex = 176;
            this.sumLabel.Text = "0000000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(178, 825);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 33);
            this.label5.TabIndex = 175;
            this.label5.Text = "Tổng tiền";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.standDeliRadio);
            this.groupBox2.Controls.Add(this.fastDeliRadio);
            this.groupBox2.Location = new System.Drawing.Point(456, 642);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 129);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            // 
            // standDeliRadio
            // 
            this.standDeliRadio.AutoSize = true;
            this.standDeliRadio.Checked = true;
            this.standDeliRadio.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.standDeliRadio.Location = new System.Drawing.Point(6, 25);
            this.standDeliRadio.Name = "standDeliRadio";
            this.standDeliRadio.Size = new System.Drawing.Size(275, 37);
            this.standDeliRadio.TabIndex = 153;
            this.standDeliRadio.TabStop = true;
            this.standDeliRadio.Text = "Giao hàng tiêu chuẩn";
            this.standDeliRadio.UseVisualStyleBackColor = true;
            this.standDeliRadio.CheckedChanged += new System.EventHandler(this.standDeliRadio_CheckedChanged);
            // 
            // fastDeliRadio
            // 
            this.fastDeliRadio.AutoSize = true;
            this.fastDeliRadio.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDeliRadio.Location = new System.Drawing.Point(6, 68);
            this.fastDeliRadio.Name = "fastDeliRadio";
            this.fastDeliRadio.Size = new System.Drawing.Size(228, 37);
            this.fastDeliRadio.TabIndex = 154;
            this.fastDeliRadio.Text = "Giao hàng nhanh";
            this.fastDeliRadio.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(178, 671);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 33);
            this.label4.TabIndex = 173;
            this.label4.Text = "Hình thức vận chuyển";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 601);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 33);
            this.label2.TabIndex = 172;
            this.label2.Text = "Hình thức thanh toán";
            // 
            // fLPanel
            // 
            this.fLPanel.AutoScroll = true;
            this.fLPanel.Location = new System.Drawing.Point(184, 335);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(640, 235);
            this.fLPanel.TabIndex = 171;
            // 
            // phoneTxt
            // 
            this.phoneTxt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneTxt.Location = new System.Drawing.Point(456, 266);
            this.phoneTxt.Multiline = true;
            this.phoneTxt.Name = "phoneTxt";
            this.phoneTxt.Size = new System.Drawing.Size(368, 45);
            this.phoneTxt.TabIndex = 170;
            // 
            // nameCustomerTxt
            // 
            this.nameCustomerTxt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameCustomerTxt.Location = new System.Drawing.Point(456, 106);
            this.nameCustomerTxt.Multiline = true;
            this.nameCustomerTxt.Name = "nameCustomerTxt";
            this.nameCustomerTxt.Size = new System.Drawing.Size(368, 45);
            this.nameCustomerTxt.TabIndex = 169;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(178, 278);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 33);
            this.label8.TabIndex = 168;
            this.label8.Text = "Số điện thoại";
            // 
            // addressTxt
            // 
            this.addressTxt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressTxt.Location = new System.Drawing.Point(456, 162);
            this.addressTxt.Multiline = true;
            this.addressTxt.Name = "addressTxt";
            this.addressTxt.Size = new System.Drawing.Size(368, 92);
            this.addressTxt.TabIndex = 167;
            this.addressTxt.Leave += new System.EventHandler(this.addressTxt_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(178, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 33);
            this.label6.TabIndex = 164;
            this.label6.Text = "Địa chỉ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(178, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 33);
            this.label1.TabIndex = 163;
            this.label1.Text = "Tên người mua";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(313, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 46);
            this.label3.TabIndex = 162;
            this.label3.Text = "Thông tin đơn hàng";
            // 
            // paymentCb
            // 
            this.paymentCb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentCb.FormattingEnabled = true;
            this.paymentCb.Location = new System.Drawing.Point(456, 595);
            this.paymentCb.Name = "paymentCb";
            this.paymentCb.Size = new System.Drawing.Size(368, 41);
            this.paymentCb.TabIndex = 181;
            // 
            // PayBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 543);
            this.Controls.Add(this.formPanel);
            this.Name = "PayBill";
            this.Text = "directBuy";
            this.Load += new System.EventHandler(this.DirectBuy_Load);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Label transportLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label sumLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton standDeliRadio;
        private System.Windows.Forms.RadioButton fastDeliRadio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel fLPanel;
        private System.Windows.Forms.TextBox phoneTxt;
        private System.Windows.Forms.TextBox nameCustomerTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox addressTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addCartBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.ComboBox paymentCb;
    }
}