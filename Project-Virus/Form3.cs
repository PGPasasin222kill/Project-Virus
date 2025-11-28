using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Virus
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonlog_Click(object sender, EventArgs e)
        {
            if(textlogin.Text=="Savant")
            {
                textlogin.Hide();
                buttonlog.Hide();
                buttonback.Visible = true;
                label1.Hide();
            }
            else
            {
                MessageBox.Show("Acces interzis!");
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide(); ;
        }
    }
}
