namespace ATCSDL
{
    partial class Cart
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
            this.fLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.billsBtn = new System.Windows.Forms.Button();
            this.buyBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fLPanel
            // 
            this.fLPanel.AutoScroll = true;
            this.fLPanel.Location = new System.Drawing.Point(263, 180);
            this.fLPanel.Name = "fLPanel";
            this.fLPanel.Size = new System.Drawing.Size(659, 439);
            this.fLPanel.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(640, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 46);
            this.label3.TabIndex = 56;
            this.label3.Text = "Giỏ hàng";
            // 
            // billsBtn
            // 
            this.billsBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billsBtn.Location = new System.Drawing.Point(987, 354);
            this.billsBtn.Name = "billsBtn";
            this.billsBtn.Size = new System.Drawing.Size(195, 58);
            this.billsBtn.TabIndex = 58;
            this.billsBtn.Text = "Các đơn hàng";
            this.billsBtn.UseVisualStyleBackColor = true;
            // 
            // buyBtn
            // 
            this.buyBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buyBtn.Location = new System.Drawing.Point(987, 265);
            this.buyBtn.Name = "buyBtn";
            this.buyBtn.Size = new System.Drawing.Size(195, 58);
            this.buyBtn.TabIndex = 57;
            this.buyBtn.Text = "Thanh toán";
            this.buyBtn.UseVisualStyleBackColor = true;
            this.buyBtn.Click += new System.EventHandler(this.buyBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(987, 446);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(195, 58);
            this.backBtn.TabIndex = 60;
            this.backBtn.Text = "Quay lại";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // Cart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 654);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.billsBtn);
            this.Controls.Add(this.buyBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fLPanel);
            this.Name = "Cart";
            this.Text = "Cart";
            this.Load += new System.EventHandler(this.Cart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button billsBtn;
        private System.Windows.Forms.Button buyBtn;
        private System.Windows.Forms.Button backBtn;
    }
}