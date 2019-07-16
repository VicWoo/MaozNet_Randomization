using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkGUI.Forms
{
    public partial class MatMultForm : Form
    {

        public bool isValid = false;

        public int InitialYear
        {
            get { return int.Parse(textBox1.Text); }
            set { textBox1.Text = value.ToString();}
        }
        
        public int FinalYear
        {
            get { return int.Parse(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }

        public string File1
        {
            get { return textBox3.Text; }
        }

        public string File2
        {
            get { return textBox4.Text; }
        }

        public string SaveFile
        {
            get { return textBox5.Text; }
        }

        public MatMultForm()
        {
            InitializeComponent();
        }

        private void YearInitial_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (!int.TryParse(textBox1.Text, out n))
            {
                return;
            }
        }

        private void YearFinal_TextChanged(object sender, EventArgs e) 
        {
            int n;
            if (!int.TryParse(textBox2.Text, out n))
            {
                return;
            }
        }

  

        private void button1_Click (object sender, EventArgs e)
        {

            //validating user input
            try
            {
                int.Parse(textBox1.Text);
                int.Parse(textBox2.Text);
            }

            catch (Exception)
            {
                MessageBox.Show("Enter integers only!", "Error!");
                return;
            }

            if (InitialYear > FinalYear)
            {
                MessageBox.Show("Range invalid", "Error!");
                return;
            }

            if (File1 == "")
            {
                MessageBox.Show("File1 cannot be blank", "Error!");
                return;
            }

            if (File2 == "")
            {
                MessageBox.Show("File2 cannot be blank", "Error!");
                return;
            }

            isValid = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult file = ofd.ShowDialog();
            if (file == DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult file = ofd.ShowDialog();
            if (file == DialogResult.OK)
            {
                textBox4.Text = ofd.FileName;
            }
        }

      
    }
}
