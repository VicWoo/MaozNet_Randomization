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

        public struct RandomNetwork
        {
            private int nodes, edge, posEdg, negEdg, min_val, max_val;
            private string networkId;
            public string NetworkId
            {
                get
                {
                    return networkId;
                }
                set
                {
                    networkId = value;
                }
            }
            public int Edge
            {
                get
                {
                    return edge;
                }
                set
                {
                    edge = value;
                }
            }
            public int PosEdg
            {
                get
                {
                    return posEdg;
                }
                set
                {
                    posEdg = value;
                }
            }
            public int NegEdg
            {
                get
                {
                    return negEdg;
                }
                set
                {
                    negEdg = value;
                }
            }
            public int Min
            {
                get
                {
                    return min_val;
                }
                set
                {
                    min_val = value;
                }
            }
            public int Max
            {
                get
                {
                    return max_val;
                }
                set
                {
                    max_val = value;
                }
            }
            public int Nodes
            {
                get
                {
                    return nodes;
                }
                set
                {
                    nodes = value;
                }
            }
        }
        public MatrixTable loadFromInputFile(string inputFile, bool sign, bool selfTies)
        {

            string filename = inputFile;
            // List<Dictionary<string, int>> networkSpec = new List<Dictionary<string, int>>();
            Dictionary<string, RandomNetwork> networkSpec = new Dictionary<string, RandomNetwork>();
            // Unsigned should have 5 cols, while signed 6 cols
            var reader = new StreamReader(filename);
            var headers = reader.ReadLine().Split(',');
            int num_items = headers.Length;


            string net_ID;
            int nodes;
            int edges;
            int pos_edges;
            int neg_edges;
            int min_val;
            int max_val;
            if (sign == true && num_items == 6)
            {
                string temp_ID = "";
                RandomNetwork tempNetwork = new RandomNetwork();
                while (!reader.EndOfStream)
                {
                    // networkSpec.Add(new Dictionary<string, int>());
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[1], out nodes) && int.TryParse(items[2], out pos_edges) && int.TryParse(items[3], out neg_edges) && int.TryParse(items[4], out min_val) && int.TryParse(items[5], out max_val))
                    {
                        if ((nodes >= 0) && ((pos_edges >= 0) && (max_val >= 0)) && ((neg_edges >= 0) && (min_val <= 0)) && !(pos_edges > 0 && max_val == 0) && !(neg_edges > 0 && min_val == 0))
                        {
                            net_ID = items[0];
                            if (net_ID != "")
                            {
                                temp_ID = net_ID;

                                tempNetwork.NetworkId = net_ID;
                                tempNetwork.PosEdg = pos_edges;
                                tempNetwork.NegEdg = neg_edges;
                                tempNetwork.Min = min_val;
                                tempNetwork.Max = max_val;
                                tempNetwork.Nodes = nodes;

                                networkSpec.Add(net_ID, tempNetwork);

                            }
                            else if (string.Equals(net_ID, temp_ID) && temp_ID != "")
                            {

                                throw new Exception("Repetitive nodes!");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid input values!");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid input format!");
                    }
                }
                NumNetID = networkSpec.Count;
                foreach (KeyValuePair<string, RandomNetwork> kvp in networkSpec)
                {
                    // Console.WriteLine("Nodes: " + kvp.Value.Nodes.ToString());
                    if (SelfTies)
                    {
                        if ((Math.Ceiling((double)kvp.Value.PosEdg / ((kvp.Value.Max == 0) ? 1 : kvp.Value.Max)) + Math.Ceiling((double)kvp.Value.NegEdg / ((kvp.Value.Min == 0) ? 1 : Math.Abs(kvp.Value.Min)))) > (kvp.Value.Nodes * kvp.Value.Nodes - kvp.Value.Nodes))
                        {
                            throw new Exception("Edges of this network is out of range!");
                        }
                    }
                    else
                    {
                        if ((Math.Ceiling((double)kvp.Value.PosEdg / ((kvp.Value.Max == 0) ? 1 : kvp.Value.Max)) + Math.Ceiling((double)kvp.Value.NegEdg / ((kvp.Value.Min == 0) ? 1 : Math.Abs(kvp.Value.Min)))) > kvp.Value.Nodes * kvp.Value.Nodes)
                        {
                            throw new Exception("Edges of this network is out of range!");
                        }

                    }
                }
            }

            else if (sign == false && num_items == 5)
            {
                string temp_ID = "";
                RandomNetwork tempNetwork = new RandomNetwork();
                while (!reader.EndOfStream)
                {
                    // networkSpec.Add(new Dictionary<string, int>());
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[1], out nodes) && int.TryParse(items[2], out edges) && int.TryParse(items[3], out min_val) && int.TryParse(items[4], out max_val))
                    {
                        if ((nodes >= 0) && ((edges >= 0) && (max_val >= 0)) && (max_val >= min_val && min_val >= 0) && !(edges > 0 && max_val == 0))
                        {
                            net_ID = items[0];
                            if (net_ID != "")
                            {
                                temp_ID = net_ID;

                                tempNetwork.NetworkId = net_ID;
                                tempNetwork.Edge = edges;
                                tempNetwork.Min = min_val;
                                tempNetwork.Max = max_val;
                                tempNetwork.Nodes = nodes;

                                networkSpec.Add(net_ID, tempNetwork);
                            }
                            else if (string.Equals(net_ID, temp_ID) && temp_ID != "")
                            {

                                throw new Exception("Repetitive nodes!");
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid input values!");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid input format!");
                    }
                }
                NumNetID = networkSpec.Count;
                foreach (KeyValuePair<string, RandomNetwork> kvp in networkSpec)
                {
                    if (SelfTies)
                    {
                        if ((Math.Ceiling((double)kvp.Value.Edge / ((kvp.Value.Max == 0) ? 1 : kvp.Value.Max))) > (kvp.Value.Nodes * kvp.Value.Nodes - kvp.Value.Nodes))
                        {
                            throw new Exception("Edges of this network is out of range!");
                        }
                    }
                    else
                    {
                        if ((Math.Ceiling((double)kvp.Value.Edge / ((kvp.Value.Max == 0) ? 1 : kvp.Value.Max))) > kvp.Value.Nodes * kvp.Value.Nodes)
                        {
                            throw new Exception("Edges of this network is out of range!");
                        }

                    }
                }
            }
            else
            {
                throw new IOException("Incorrect input format!");
            }
            reader.Close();

            MatrixTable networkSpec_data = new MatrixTable();
            foreach(KeyValuePair<string, RandomNetwork> kvp in networkSpec)
            {
                string s = kvp.Key;
                int cols = sign ? 5 : 4;
                networkSpec_data.AddMatrix(s, 1, cols);
                networkSpec_data[s].Name = s;
                if (sign)
                {
                    string[] colLabels = { "Nodes", "Pos. Edges", "Neg. Edges", "Min", "Max" };
                    networkSpec_data[s].ColLabels.SetLabels(colLabels);

                    networkSpec_data[s][0, 0] = kvp.Value.Nodes;
                    networkSpec_data[s][0, 1] = kvp.Value.PosEdg;
                    networkSpec_data[s][0, 2] = kvp.Value.NegEdg;
                    networkSpec_data[s][0, 3] = kvp.Value.Min;
                    networkSpec_data[s][0, 4] = kvp.Value.Max;

                }
                else
                {
                    string[] colLabels = { "Nodes", "Edges", "Min", "Max" };
                    networkSpec_data[s].ColLabels.SetLabels(colLabels);
                    networkSpec_data[s][0, 0] = kvp.Value.Nodes;
                    networkSpec_data[s][0, 1] = kvp.Value.Edge;
                    networkSpec_data[s][0, 2] = kvp.Value.Min;
                    networkSpec_data[s][0, 3] = kvp.Value.Max;

                }
            }
            return networkSpec_data;
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
