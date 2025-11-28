namespace Project_Virus
{
    partial class Form3
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
            this.textlogin = new System.Windows.Forms.TextBox();
            this.buttonlog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonback = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textlogin
            // 
            this.textlogin.Location = new System.Drawing.Point(300, 207);
            this.textlogin.Name = "textlogin";
            this.textlogin.Size = new System.Drawing.Size(185, 22);
            this.textlogin.TabIndex = 0;
            // 
            // buttonlog
            // 
            this.buttonlog.Location = new System.Drawing.Point(508, 203);
            this.buttonlog.Name = "buttonlog";
            this.buttonlog.Size = new System.Drawing.Size(95, 30);
            this.buttonlog.TabIndex = 1;
            this.buttonlog.Text = "Login";
            this.buttonlog.UseVisualStyleBackColor = true;
            this.buttonlog.Click += new System.EventHandler(this.buttonlog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(350, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "LOGIN";
            // 
            // buttonback
            // 
            this.buttonback.Location = new System.Drawing.Point(12, 12);
            this.buttonback.Name = "buttonback";
            this.buttonback.Size = new System.Drawing.Size(95, 30);
            this.buttonback.TabIndex = 3;
            this.buttonback.Text = "Back";
            this.buttonback.UseVisualStyleBackColor = true;
            this.buttonback.Visible = false;
            this.buttonback.Click += new System.EventHandler(this.buttonback_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonback);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonlog);
            this.Controls.Add(this.textlogin);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textlogin;
        private System.Windows.Forms.Button buttonlog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonback;
    }
}