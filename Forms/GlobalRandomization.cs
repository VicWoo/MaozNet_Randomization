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

namespace NetworkGUI
{
    public partial class GlobalRandomForm : Form
    {
        public GlobalRandomForm()
        {
            InitializeComponent();
        }

        public bool Sign
        {
            get { return g1RadioButton2.Checked; }
        }

        public int NumRandNet
        {
            get { return int.Parse(numTextBox.Text); }
            set { numTextBox.Text = value.ToString(); }
        }

        public bool SelfTies
        {
            get { return g2RadioButton1.Checked; }
        }

        public string InputFile
        {
            get { return inputBox.Text; }
            set { inputBox.Text = value; }
        }

        public int NumNetID
        {
            get;
            set;
        }

        public List<Dictionary<string, int>> loadFromInputFile(string inputFile, bool sign, bool selfTies)
        {
          
            string filename = inputFile;
            List<Dictionary<string, int>> networkSpec = new List<Dictionary<string, int>>();
            // Unsigned should have 5 cols, while signed 6 cols
            var reader = new StreamReader(filename);
            var headers = reader.ReadLine().Split(',');
            int num_items = headers.Length;

            
            int net_ID;
            int nodes;
            int edges;
            int pos_edges;
            int neg_edges;
            int min_val;
            int max_val;
            int i = 0;
            if (sign == true && num_items == 6)
            {
                while(!reader.EndOfStream)
                {                    
                    networkSpec.Add(new Dictionary<string, int>());
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[0], out net_ID) && int.TryParse(items[1], out nodes) && int.TryParse(items[2], out pos_edges) && int.TryParse(items[3], out neg_edges) && int.TryParse(items[4], out min_val) && int.TryParse(items[5], out max_val))
                    {
                        if ((net_ID > 0) && (nodes > 0) && (((pos_edges > 0) && (max_val > 0)) || ((pos_edges == 0) && (max_val == 0))) && (((neg_edges > 0) && (min_val < 0)) || ((neg_edges == 0) && (min_val == 0))))
                        {
                            if ((selfTies == false) && ((Math.Ceiling((double)pos_edges/((max_val == 0)? 1:max_val)) + Math.Ceiling((double)neg_edges / ((min_val == 0) ? 1 : Math.Abs(min_val)))) <= (nodes * nodes - nodes)))
                            {
                                networkSpec[i].Add("Network ID", net_ID);
                                networkSpec[i].Add("Nodes", nodes);
                                networkSpec[i].Add("Pos. Edges", pos_edges);
                                networkSpec[i].Add("Neg. Edges", neg_edges);
                                networkSpec[i].Add("Min Value", min_val);
                                networkSpec[i].Add("Max Value", max_val);
                                i++;
                            }
                            else if ((selfTies == true) && (((Math.Ceiling((double)pos_edges / ((max_val == 0) ? 1 : max_val)) + Math.Ceiling((double)neg_edges / ((min_val == 0) ? 1 : Math.Abs(min_val)))) <= nodes * nodes)))
                            {
                                networkSpec[i].Add("Network ID", net_ID);
                                networkSpec[i].Add("Nodes", nodes);
                                networkSpec[i].Add("Pos. Edges", pos_edges);
                                networkSpec[i].Add("Neg. Edges", neg_edges);
                                networkSpec[i].Add("Min Value", min_val);
                                networkSpec[i].Add("Max Value", max_val);
                                i++;
                            }
                            else
                            {
                                throw new IOException("Incorrect input format!");
                            }
                        }
                        else
                        {
                            throw new IOException("Incorrect input format!");
                        }
                    }

                }
            }
            else if (sign == false && num_items == 5)
            {
                while(!reader.EndOfStream)
                {
                    networkSpec.Add(new Dictionary<string, int>());
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[0], out net_ID) && int.TryParse(items[1], out nodes) && int.TryParse(items[2], out edges) && int.TryParse(items[3], out min_val) && int.TryParse(items[4], out max_val))
                    {
                        if ((net_ID > 0) && (nodes > 0) && (((edges > 0) && (max_val > 0)) || ((edges == 0) && (max_val == 0))))
                        {
                            if ((selfTies == false) && (Math.Ceiling((double)edges/(max_val == 0 ? 1 : max_val)) <= (nodes * nodes - nodes)))
                            {
                                networkSpec[i].Add("Network ID", net_ID);
                                networkSpec[i].Add("Nodes", nodes);
                                networkSpec[i].Add("Edges", edges);
                                networkSpec[i].Add("Min Value", min_val);
                                networkSpec[i].Add("Max Value", max_val);
                                i++;
                            }
                            else if ((selfTies == true) && (Math.Ceiling((double)edges / (max_val == 0 ? 1 : max_val)) <= nodes * nodes))
                            {
                                networkSpec[i].Add("Network ID", net_ID);
                                networkSpec[i].Add("Nodes", nodes);
                                networkSpec[i].Add("Edges", edges);
                                networkSpec[i].Add("Min Value", min_val);
                                networkSpec[i].Add("Max Value", max_val);
                                i++;
                            }
                            else
                            {
                                throw new IOException("Incorrect input format!");
                            }
                        }
                        else
                        {
                            throw new IOException("Incorrect input format!");
                        }
                    }
                    else
                    {
                        throw new IOException("Incorrect input format!");
                    }
                }
            }
            else
            {
                throw new IOException("Incorrect input format!");
            }
            reader.Close();
            NumNetID = i;
            return networkSpec;
        }


        private void GlobalRandomForm_Load(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void numTextBox_TextChanged(object sender, EventArgs e)
        {
            int n;
            genButton.Enabled = true;
            if (!int.TryParse(numTextBox.Text, out n))
            {
                genButton.Enabled = false;
                return;
            }
        }

        private void g1RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void g1RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void g2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void g2RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            genButton.Enabled = false; 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputBox.Text = openFileDialog1.FileName;
                genButton.Enabled = true;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            genButton.Enabled = inputBox.Enabled = true;
        }

        private void genButton_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(numTextBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("The number of networks must be an integer!", "Error!");
                return;
            }   
            if (inputBox.Text == "")
            {
                MessageBox.Show("Invalid Input Filename!", "Error!");
                return;
            }
            this.Close();
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
