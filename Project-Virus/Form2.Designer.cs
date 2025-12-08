namespace Project_Virus
{
    partial class Form2
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
        /// Required method for Designer support — do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxT = new System.Windows.Forms.TextBox();
            this.textBoxProcent = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBoxVirus = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonlog = new System.Windows.Forms.Button();
            this.textlogin = new System.Windows.Forms.TextBox();
            this.buttonstart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Inapoi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(801, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Salveaza";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(753, 820);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(931, -1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(10, 10);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxT
            // 
            this.textBoxT.Location = new System.Drawing.Point(801, 205);
            this.textBoxT.Name = "textBoxT";
            this.textBoxT.Size = new System.Drawing.Size(102, 20);
            this.textBoxT.TabIndex = 4;
            // 
            // textBoxProcent
            // 
            this.textBoxProcent.Location = new System.Drawing.Point(827, 274);
            this.textBoxProcent.Name = "textBoxProcent";
            this.textBoxProcent.Size = new System.Drawing.Size(102, 20);
            this.textBoxProcent.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(800, 114);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(96, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // comboBoxVirus
            // 
            this.comboBoxVirus.FormattingEnabled = true;
            this.comboBoxVirus.Location = new System.Drawing.Point(802, 159);
            this.comboBoxVirus.Name = "comboBoxVirus";
            this.comboBoxVirus.Size = new System.Drawing.Size(101, 21);
            this.comboBoxVirus.TabIndex = 7;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Red;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(801, 518);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 98);
            this.button4.TabIndex = 8;
            this.button4.Text = "START\r\n";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(382, 311);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "LOGIN";
            this.label1.Visible = false;
            // 
            // buttonlog
            // 
            this.buttonlog.Location = new System.Drawing.Point(501, 351);
            this.buttonlog.Margin = new System.Windows.Forms.Padding(2);
            this.buttonlog.Name = "buttonlog";
            this.buttonlog.Size = new System.Drawing.Size(71, 24);
            this.buttonlog.TabIndex = 11;
            this.buttonlog.Text = "Login";
            this.buttonlog.UseVisualStyleBackColor = true;
            this.buttonlog.Visible = false;
            this.buttonlog.Click += new System.EventHandler(this.buttonlog_Click);
            // 
            // textlogin
            // 
            this.textlogin.Location = new System.Drawing.Point(345, 354);
            this.textlogin.Margin = new System.Windows.Forms.Padding(2);
            this.textlogin.Name = "textlogin";
            this.textlogin.PasswordChar = '*';
            this.textlogin.Size = new System.Drawing.Size(140, 20);
            this.textlogin.TabIndex = 10;
            this.textlogin.Visible = false;
            // 
            // buttonstart
            // 
            this.buttonstart.BackColor = System.Drawing.Color.RosyBrown;
            this.buttonstart.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonstart.Location = new System.Drawing.Point(800, 311);
            this.buttonstart.Name = "buttonstart";
            this.buttonstart.Size = new System.Drawing.Size(102, 40);
            this.buttonstart.TabIndex = 13;
            this.buttonstart.Text = "Simulare virus";
            this.buttonstart.UseVisualStyleBackColor = false;
            this.buttonstart.Click += new System.EventHandler(this.buttonstart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(798, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "TARA TINTA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(798, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "VIRUSUL FOLOSIT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(799, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Timpul simulari";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(767, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Procent Populatie infectata";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(941, 824);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonstart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonlog);
            this.Controls.Add(this.textlogin);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxVirus);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBoxProcent);
            this.Controls.Add(this.textBoxT);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulare Virus";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxT;
        private System.Windows.Forms.TextBox textBoxProcent;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBoxVirus;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonlog;
        private System.Windows.Forms.TextBox textlogin;
        private System.Windows.Forms.Button buttonstart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}