#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace NetworkGUI
{
    partial class YearRangeForm : Form
    {
        public YearRangeForm()
        {
            InitializeComponent();
        }

        private void YearRangeForm_Load(object sender, EventArgs e)
        {
            this.SetMode(false);
        }

        public void SetMode(bool big)
        {
            if (big)
            {
                this.Height = 120;
            }
            else
            {
                this.Height = 120;
            }
        }

        public bool Cohesion
        {
            get { return cohesionCheckBox.Checked; }
        }

        // Yushan
        public string from
        {
            get
            {
                return fromText.Text;
            }
            set
            {
                fromText.Text = value;
            }
        }

        // Yushan
        public string to
        {
            get
            {
                return toText.Text;
            }
            set
            {
                toText.Text = value;
            }
        }

        // Yushan
        private void goButton_Click(object sender, EventArgs e)
        {
            //int fromYear, toYear;
            //try
            //{
            //    fromYear = int.Parse(fromText.Text);
            //    toYear = int.Parse(toText.Text);
            //    if (toYear < fromYear)
            //    {
            //        MessageBox.Show("The start year must be less than or equal to the end year!", "Error!");
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("The years entered are invalid!", "Error!");
            //    return;
            //}

            if (toText.Text == "")
            {
                MessageBox.Show("Please enter the start", "Error!");
                return;
            }
            else if (toText.Text == "")
            {
                MessageBox.Show("Please enter the end", "Error!");
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void cohesionCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}