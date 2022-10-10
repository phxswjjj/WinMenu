namespace WinMenu.UI
{
    partial class FrmLogin
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
            this.btnUser1 = new System.Windows.Forms.Button();
            this.btnUser2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUser1
            // 
            this.btnUser1.Location = new System.Drawing.Point(38, 31);
            this.btnUser1.Name = "btnUser1";
            this.btnUser1.Size = new System.Drawing.Size(91, 47);
            this.btnUser1.TabIndex = 0;
            this.btnUser1.Text = "User1";
            this.btnUser1.UseVisualStyleBackColor = true;
            this.btnUser1.Click += new System.EventHandler(this.btnUser1_Click);
            // 
            // btnUser2
            // 
            this.btnUser2.Location = new System.Drawing.Point(179, 31);
            this.btnUser2.Name = "btnUser2";
            this.btnUser2.Size = new System.Drawing.Size(91, 47);
            this.btnUser2.TabIndex = 0;
            this.btnUser2.Text = "User2";
            this.btnUser2.UseVisualStyleBackColor = true;
            this.btnUser2.Click += new System.EventHandler(this.btnUser2_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 107);
            this.Controls.Add(this.btnUser2);
            this.Controls.Add(this.btnUser1);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmLogin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUser1;
        private System.Windows.Forms.Button btnUser2;
    }
}