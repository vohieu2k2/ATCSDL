namespace ATCSDL
{
    partial class HomeSupplier
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
            this.label3 = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.mesBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.fLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.billsBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.categoryCb = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(512, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 46);
            this.label3.TabIndex = 45;
            this.label3.Text = "Trang chủ";
            // 
            // addBtn
            // 
            this.addBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(1068, 197);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(195, 58);
            this.addBtn.TabIndex = 46;
            this.addBtn.Text = "Thêm sản phẩm";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // mesBtn
            // 
            this.mesBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesBtn.Location = new System.Drawing.Point(1068, 379);
            this.mesBtn.Name = "mesBtn";
            this.mesBtn.Size = new System.Drawing.Size(195, 58);
            this.mesBtn.TabIndex = 47;
            this.mesBtn.Text = "Tin nhắn";
            this.mesBtn.UseVisualStyleBackColor = true;
            this.mesBtn.Click += new System.EventHandler(this.mesBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(1068, 469);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(195, 58);
            this.backBtn.TabIndex = 50;
            this.backBtn.Text = "Đăng xuất";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // fLPanel
            // 
            this.fLPanel.AutoScroll = true;
            this.fLPanel.Location = new System.Drawing.Point(53, 156);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(976, 444);
            this.fLPanel.TabIndex = 55;
            // 
            // billsBtn
            // 
            this.billsBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billsBtn.Location = new System.Drawing.Point(1068, 291);
            this.billsBtn.Name = "billsBtn";
            this.billsBtn.Size = new System.Drawing.Size(195, 58);
            this.billsBtn.TabIndex = 56;
            this.billsBtn.Text = "Các đơn hàng";
            this.billsBtn.UseVisualStyleBackColor = true;
            this.billsBtn.Click += new System.EventHandler(this.billsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 33);
            this.label1.TabIndex = 84;
            this.label1.Text = "Danh mục";
            // 
            // categoryCb
            // 
            this.categoryCb.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryCb.FormattingEnabled = true;
            this.categoryCb.Location = new System.Drawing.Point(181, 93);
            this.categoryCb.Name = "categoryCb";
            this.categoryCb.Size = new System.Drawing.Size(212, 41);
            this.categoryCb.TabIndex = 85;
            this.categoryCb.SelectedValueChanged += new System.EventHandler(this.categoryCb_SelectedValueChanged);
            // 
            // HomeSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 647);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoryCb);
            this.Controls.Add(this.billsBtn);
            this.Controls.Add(this.fLPanel);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.mesBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label3);
            this.Name = "HomeSupplier";
            this.Text = "HomeSupplier";
            this.Load += new System.EventHandler(this.HomeSupplier_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button mesBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.FlowLayoutPanel fLPanel;
        private System.Windows.Forms.Button billsBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox categoryCb;
    }
}