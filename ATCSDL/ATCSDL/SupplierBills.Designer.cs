namespace ATCSDL
{
    partial class SupplierBills
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
            this.deliveredBtn = new System.Windows.Forms.Button();
            this.waitDeliBtn = new System.Windows.Forms.Button();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.returnBtn = new System.Windows.Forms.Button();
            this.allBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // deliveredBtn
            // 
            this.deliveredBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deliveredBtn.Location = new System.Drawing.Point(884, 350);
            this.deliveredBtn.Name = "deliveredBtn";
            this.deliveredBtn.Size = new System.Drawing.Size(195, 48);
            this.deliveredBtn.TabIndex = 188;
            this.deliveredBtn.Text = "Đã bán";
            this.deliveredBtn.UseVisualStyleBackColor = true;
            this.deliveredBtn.Click += new System.EventHandler(this.deliveredBtn_Click);
            // 
            // waitDeliBtn
            // 
            this.waitDeliBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitDeliBtn.Location = new System.Drawing.Point(884, 279);
            this.waitDeliBtn.Name = "waitDeliBtn";
            this.waitDeliBtn.Size = new System.Drawing.Size(195, 48);
            this.waitDeliBtn.TabIndex = 187;
            this.waitDeliBtn.Text = "Đang giao hàng";
            this.waitDeliBtn.UseVisualStyleBackColor = true;
            this.waitDeliBtn.Click += new System.EventHandler(this.waitDeliBtn_Click);
            // 
            // confirmBtn
            // 
            this.confirmBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBtn.Location = new System.Drawing.Point(884, 206);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(195, 48);
            this.confirmBtn.TabIndex = 186;
            this.confirmBtn.Text = "Chờ xác nhận";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(884, 485);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(195, 48);
            this.backBtn.TabIndex = 185;
            this.backBtn.Text = "Quay lại";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // returnBtn
            // 
            this.returnBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnBtn.Location = new System.Drawing.Point(884, 419);
            this.returnBtn.Name = "returnBtn";
            this.returnBtn.Size = new System.Drawing.Size(195, 48);
            this.returnBtn.TabIndex = 184;
            this.returnBtn.Text = "Trả hàng";
            this.returnBtn.UseVisualStyleBackColor = true;
            this.returnBtn.Click += new System.EventHandler(this.returnBtn_Click);
            // 
            // allBtn
            // 
            this.allBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allBtn.Location = new System.Drawing.Point(884, 131);
            this.allBtn.Name = "allBtn";
            this.allBtn.Size = new System.Drawing.Size(195, 48);
            this.allBtn.TabIndex = 183;
            this.allBtn.Text = "Tất cả";
            this.allBtn.UseVisualStyleBackColor = true;
            this.allBtn.Click += new System.EventHandler(this.allBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(362, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 46);
            this.label3.TabIndex = 182;
            this.label3.Text = "Các đơn hàng";
            // 
            // fLPanel
            // 
            this.fLPanel.AutoScroll = true;
            this.fLPanel.Location = new System.Drawing.Point(101, 131);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(727, 402);
            this.fLPanel.TabIndex = 181;
            // 
            // SupplierBills
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 553);
            this.Controls.Add(this.deliveredBtn);
            this.Controls.Add(this.waitDeliBtn);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.returnBtn);
            this.Controls.Add(this.allBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fLPanel);
            this.Name = "SupplierBills";
            this.Text = "SupplierBills";
            this.Load += new System.EventHandler(this.SupplierBills_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button deliveredBtn;
        private System.Windows.Forms.Button waitDeliBtn;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button returnBtn;
        private System.Windows.Forms.Button allBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel fLPanel;
    }
}