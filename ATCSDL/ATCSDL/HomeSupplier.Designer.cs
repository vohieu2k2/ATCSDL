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
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(513, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 46);
            this.label3.TabIndex = 45;
            this.label3.Text = "Trang chủ";
            // 
            // addBtn
            // 
            this.addBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(1071, 172);
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
            this.mesBtn.Location = new System.Drawing.Point(1071, 354);
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
            this.backBtn.Location = new System.Drawing.Point(1071, 444);
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
            this.fLPanel.Location = new System.Drawing.Point(56, 131);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(976, 444);
            this.fLPanel.TabIndex = 55;
            // 
            // billsBtn
            // 
            this.billsBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billsBtn.Location = new System.Drawing.Point(1071, 266);
            this.billsBtn.Name = "billsBtn";
            this.billsBtn.Size = new System.Drawing.Size(195, 58);
            this.billsBtn.TabIndex = 56;
            this.billsBtn.Text = "Các đơn hàng";
            this.billsBtn.UseVisualStyleBackColor = true;
            this.billsBtn.Click += new System.EventHandler(this.billsBtn_Click);
            // 
            // HomeSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1466, 604);
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
    }
}