using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Network.Matrices;
using Network.IO;
using Network;

namespace NetworkGUI.Forms
{
    public partial class NDStatsForm : Form
    {
        public NDStatsForm()
        {
            InitializeComponent();
        }

        public double Alpha
        {
            get { return double.Parse(alpha.Text); }
            set { alpha.Text = value.ToString(); }
        }

        public int M
        {
            get { return int.Parse(m.Text); }
            set { m.Text = value.ToString(); }
        }
      

        private void alpha_TextChanged(object sender, EventArgs e)
        {
        }

        private void m_TextChanged(object sender, EventArgs e)
        {
        }


        private void genButton_Click(object sender, EventArgs e)
        {
            double _a;
            if (double.TryParse(alpha.Text, out _a))
            {
                if (_a <= 0 || _a > 1)
                {
                    MessageBox.Show("alpha must be between 0 and 1!", "Error!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("alpha is not a real number!", "Error!");
                return;
            }

            int _m;
            if (int.TryParse(m.Text, out _m))
            {
                if (_m <= 0)
                {
                    MessageBox.Show("m must be a positive integer!", "Error!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("m is not an integer!", "Error!");
                return;
            }
            this.Close();
        }

        private void NDStatsForm_Load(object sender, EventArgs e)
        {
            alpha.Text = "0.5";
            m.Text = "1";
        }
    }
}
