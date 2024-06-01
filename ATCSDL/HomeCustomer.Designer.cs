namespace ATCSDL
{
    partial class HomeCustomer
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
            this.backBtn = new System.Windows.Forms.Button();
            this.fLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.mesBtn = new System.Windows.Forms.Button();
            this.cartBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.billsBtn = new System.Windows.Forms.Button();
            this.categoryCb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(1077, 424);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(195, 58);
            this.backBtn.TabIndex = 55;
            this.backBtn.Text = "Đăng xuất";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // fLPanel
            // 
            this.fLPanel.AutoScroll = true;
            this.fLPanel.Location = new System.Drawing.Point(65, 153);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(976, 444);
            this.fLPanel.TabIndex = 54;
            // 
            // mesBtn
            // 
            this.mesBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesBtn.Location = new System.Drawing.Point(1077, 349);
            this.mesBtn.Name = "mesBtn";
            this.mesBtn.Size = new System.Drawing.Size(195, 58);
            this.mesBtn.TabIndex = 53;
            this.mesBtn.Text = "Tin nhắn";
            this.mesBtn.UseVisualStyleBackColor = true;
            this.mesBtn.Click += new System.EventHandler(this.mesBtn_Click);
            // 
            // cartBtn
            // 
            this.cartBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartBtn.Location = new System.Drawing.Point(1077, 192);
            this.cartBtn.Name = "cartBtn";
            this.cartBtn.Size = new System.Drawing.Size(195, 58);
            this.cartBtn.TabIndex = 52;
            this.cartBtn.Text = "Giỏ hàng";
            this.cartBtn.UseVisualStyleBackColor = true;
            this.cartBtn.Click += new System.EventHandler(this.cartBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(544, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 46);
            this.label3.TabIndex = 51;
            this.label3.Text = "Trang chủ";
            // 
            // billsBtn
            // 
            this.billsBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billsBtn.Location = new System.Drawing.Point(1077, 269);
            this.billsBtn.Name = "billsBtn";
            this.billsBtn.Size = new System.Drawing.Size(195, 58);
            this.billsBtn.TabIndex = 56;
            this.billsBtn.Text = "Các đơn hàng";
            this.billsBtn.UseVisualStyleBackColor = true;
            this.billsBtn.Click += new System.EventHandler(this.billsBtn_Click);
            // 
            // categoryCb
            // 
            this.categoryCb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryCb.FormattingEnabled = true;
            this.categoryCb.Location = new System.Drawing.Point(195, 97);
            this.categoryCb.Name = "categoryCb";
            this.categoryCb.Size = new System.Drawing.Size(212, 41);
            this.categoryCb.TabIndex = 83;
            this.categoryCb.SelectedValueChanged += new System.EventHandler(this.categoryCb_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh mục";
            // 
            // HomeCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 609);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoryCb);
            this.Controls.Add(this.billsBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.fLPanel);
            this.Controls.Add(this.mesBtn);
            this.Controls.Add(this.cartBtn);
            this.Controls.Add(this.label3);
            this.Name = "HomeCustomer";
            this.Text = "HomeCustomer";
            this.Load += new System.EventHandler(this.HomeCustomer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.FlowLayoutPanel fLPanel;
        private System.Windows.Forms.Button mesBtn;
        private System.Windows.Forms.Button cartBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button billsBtn;
        private System.Windows.Forms.ComboBox categoryCb;
        private System.Windows.Forms.Label label1;
    }
}