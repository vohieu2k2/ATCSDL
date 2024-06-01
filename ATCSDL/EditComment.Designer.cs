namespace ATCSDL
{
    partial class EditComment
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
            this.changeInfoBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.scoreTxt = new System.Windows.Forms.TextBox();
            this.commentTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // changeInfoBtn
            // 
            this.changeInfoBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeInfoBtn.Location = new System.Drawing.Point(390, 432);
            this.changeInfoBtn.Name = "changeInfoBtn";
            this.changeInfoBtn.Size = new System.Drawing.Size(157, 47);
            this.changeInfoBtn.TabIndex = 206;
            this.changeInfoBtn.Text = "Thay đổi";
            this.changeInfoBtn.UseVisualStyleBackColor = true;
            this.changeInfoBtn.Click += new System.EventHandler(this.changeInfoBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(590, 432);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(166, 47);
            this.backBtn.TabIndex = 205;
            this.backBtn.Text = "Quay lại";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // scoreTxt
            // 
            this.scoreTxt.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.scoreTxt.Location = new System.Drawing.Point(390, 346);
            this.scoreTxt.Multiline = true;
            this.scoreTxt.Name = "scoreTxt";
            this.scoreTxt.Size = new System.Drawing.Size(77, 37);
            this.scoreTxt.TabIndex = 204;
            this.scoreTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scoreTxt_KeyPress);
            // 
            // commentTxt
            // 
            this.commentTxt.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.commentTxt.Location = new System.Drawing.Point(390, 154);
            this.commentTxt.Multiline = true;
            this.commentTxt.Name = "commentTxt";
            this.commentTxt.Size = new System.Drawing.Size(366, 169);
            this.commentTxt.TabIndex = 203;
            this.commentTxt.TextChanged += new System.EventHandler(this.commentTxt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(153, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(231, 33);
            this.label9.TabIndex = 202;
            this.label9.Text = "Nội dung bình luận";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(157, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 33);
            this.label1.TabIndex = 201;
            this.label1.Text = "Số điểm (1 đến 10)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(241, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(469, 46);
            this.label3.TabIndex = 200;
            this.label3.Text = "Thay đổi đánh giá sản phẩm";
            // 
            // deleteBtn
            // 
            this.deleteBtn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteBtn.Location = new System.Drawing.Point(188, 432);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(157, 47);
            this.deleteBtn.TabIndex = 207;
            this.deleteBtn.Text = "Xóa";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // EditComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 544);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.changeInfoBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.scoreTxt);
            this.Controls.Add(this.commentTxt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "EditComment";
            this.Text = "EditComment";
            this.Load += new System.EventHandler(this.EditComment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button changeInfoBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.TextBox scoreTxt;
        private System.Windows.Forms.TextBox commentTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button deleteBtn;
    }
}