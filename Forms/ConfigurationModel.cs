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
    public partial class ConfigModelForm : Form
    {
        public ConfigModelForm()
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

       

        public MatrixTable loadFromInputFile(string inputFile, bool sign, bool selfTies)
        {

            string filename = inputFile;
            Dictionary<int, Dictionary<string, List<int>>> networkSpec = new Dictionary<int, Dictionary<string, List<int>>>();
            // Unsigned should have 5 cols, while signed 6 cols
            var reader = new StreamReader(filename);
            var headers = reader.ReadLine().Split(',');
            int num_items = headers.Length;


            int net_ID;
            int net_count = 0;
            int node_index;
            int[] nodes = null;
            int degree;
            int pos_degree;
            int neg_degree;
            int min_val;
            int max_val;
            int i = 0;
            if (sign == true && num_items == 6)
            {
                int tempNetID = -1;
                int tempNode = -1;
                while (!reader.EndOfStream)
                {
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[0], out net_ID) && int.TryParse(items[1], out node_index) && int.TryParse(items[2], out pos_degree) && int.TryParse(items[3], out neg_degree) && int.TryParse(items[4], out min_val) && int.TryParse(items[5], out max_val))
                    {
                        if ((net_ID > 0) && (node_index >= 0) && (((pos_degree > 0) && (max_val > 0)) || ((pos_degree == 0) && (max_val == 0))) && (((neg_degree > 0) && (min_val < 0)) || ((neg_degree == 0) && (min_val == 0))))
                        {
                            // Identify a new network ID and add it to the list 
                            if ((net_ID != tempNetID) && (node_index == 1))
                            {
                                net_count++;
                                tempNetID = net_ID;
                                tempNode = node_index;
                                networkSpec.Add(net_ID, new Dictionary<string, List<int>>());
                                networkSpec[net_ID].Add("Pos. Degree", new List<int>() { pos_degree });
                                networkSpec[net_ID].Add("Neg. Degree", new List<int>() { neg_degree });
                                networkSpec[net_ID].Add("Min", new List<int>() { min_val });
                                networkSpec[net_ID].Add("Max", new List<int>() { max_val });
                            }
                            else if ((net_ID == tempNetID) && (node_index == (tempNode + 1)))
                            {
                                networkSpec[tempNetID]["Pos. Degree"].Add(pos_degree);
                                networkSpec[tempNetID]["Neg. Degree"].Add(neg_degree);
                                networkSpec[tempNetID]["Min"].Add(min_val);
                                networkSpec[tempNetID]["Max"].Add(max_val);
                                tempNode = node_index;
                            }
                            else
                            {
                                throw new Exception("Inconsistent Network ID or Node Index!");
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
                nodes = new int[NumNetID];
                int counter = 0;
                foreach (KeyValuePair<int, Dictionary<string, List<int>>> kvp in networkSpec)
                {
                    int temp_node = 0;
                    // int edges = 0;
                    nodes[counter] = kvp.Value["Pos. Degree"].Count;
                    temp_node = nodes[counter++];
                    // Constraints on degrees: Each node cannot have a degree larger than the total number of nodes
                    // degree (pos_degree + neg_degree) [i] < Sum(degree [i])

                    if (selfTies)
                    {
                        for (i = 0; i < temp_node; i++)
                        {
                            if (Math.Ceiling((double)kvp.Value["Pos. Degree"][i] / (kvp.Value["Max"][i] == 0 ? 1 : kvp.Value["Max"][i])) + Math.Ceiling((double)kvp.Value["Neg. Degree"][i] / (kvp.Value["Min"][i] == 0 ? 1 : Math.Abs(kvp.Value["Min"][i]))) > temp_node)
                            {
                                throw new Exception("Degree of the node in Network is out of range!");
                            }

                            //else
                            //{
                            //    edges += kvp.Value["Pos. Degree"][i] + kvp.Value["Neg. Degree"][i];
                            //}
                        }
                    }
                    // For unselftied case, the total degree can only be as many as (nodes[i] - 1).
                    else
                    {
                        for (i = 0; i < temp_node; i++)
                        {
                            if (Math.Ceiling((double)kvp.Value["Pos. Degree"][i]/(kvp.Value["Max"][i]==0?1: kvp.Value["Max"][i]))  + Math.Ceiling((double)kvp.Value["Neg. Degree"][i]/ (kvp.Value["Min"][i] == 0 ? 1 : Math.Abs(kvp.Value["Min"][i]))) > (temp_node - 1))
                            {
                                throw new Exception("Degree of the node in Network is out of range!");
                            }
                            //else
                            //{
                            //    edges += kvp.Value["Pos. Degree"][i] + kvp.Value["Neg. Degree"][i];
                            //}
                        }
                    }

                    //// Constraint the total number of edges according to the given type of networks
                    //if (!(((selfTies == false) && (edges <= (temp_node * temp_node - temp_node))) ^ ((selfTies == true) && (edges <= temp_node * temp_node))))
                    //{
                    //    throw new Exception("The number of edges is out of range!");
                    //}

                }
            }
            else if (sign == false && num_items == 5)
            {
                int tempNetID = -1;
                int tempNode = -1;
                while (!reader.EndOfStream)
                {
                    var items = reader.ReadLine().Split(',');
                    if (int.TryParse(items[0], out net_ID) && int.TryParse(items[1], out node_index) && int.TryParse(items[2], out degree) && int.TryParse(items[3], out min_val) && int.TryParse(items[4], out max_val))
                    {
                        if ((net_ID > 0) && (node_index > 0) && (((degree > 0) && (max_val > 0)) || ((degree == 0) && (max_val == 0))))
                        {
                            // Console.WriteLine("net_ID: " + net_ID.ToString() + " " + "node_index: " + node_index.ToString());
                            // Identify a new network ID and add it to the list 
                            if ((net_ID != tempNetID) && (node_index == 1))
                            {
                                net_count++;
                                tempNetID = net_ID;
                                tempNode = node_index;
                                networkSpec.Add(net_ID, new Dictionary<string, List<int>>());
                                networkSpec[net_ID].Add("Degree", new List<int>() { degree });
                                networkSpec[net_ID].Add("Min", new List<int>() { min_val });
                                networkSpec[net_ID].Add("Max", new List<int>() { max_val });
                            }
                            else if ((net_ID == tempNetID) && (node_index == (tempNode + 1)))
                            {
                                networkSpec[tempNetID]["Degree"].Add(degree);
                                networkSpec[tempNetID]["Min"].Add(min_val);
                                networkSpec[tempNetID]["Max"].Add(max_val);
                                tempNode = node_index;
                            }
                            else
                            {
                                throw new Exception("Inconsistent Network ID or Node Index!");
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
                nodes = new int[NumNetID];
                int counter = 0;
                foreach (KeyValuePair<int, Dictionary<string, List<int>>> kvp in networkSpec)
                {
                    int temp_node = 0;                   
                    nodes[counter] = kvp.Value["Degree"].Count;
                    temp_node = nodes[counter++];
                    // Constraints on degrees: Each node cannot have a degree larger than the total number of nodes
                    // degree (pos_degree + neg_degree) [i] < Sum(degree [i])
                    if (selfTies)
                    {
                        for (i = 0; i < temp_node; i++)
                        {
                            if (kvp.Value["Degree"][i] > (kvp.Value["Max"][i] * temp_node))
                            {
                                throw new Exception("Degree of the node in Network is out of range!");
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i < temp_node; i++)
                        {
                            if (kvp.Value["Degree"][i] > (kvp.Value["Max"][i] * (temp_node - 1)))
                            {
                                throw new Exception("Degree of the node in Network is out of range!");
                            }
                        }
                    }
                }
            }
            else
            {
                throw new IOException("Incorrect input format!");
            }
            reader.Close();

            // Create a Matrix Table to store the input data
            MatrixTable networkSpec_data = new MatrixTable();
            i = 0;
            foreach(KeyValuePair<int, Dictionary<string, List<int>>> kvp in networkSpec)
            {
                string s = kvp.Key.ToString();
                int cols = kvp.Value.Count;
                networkSpec_data.AddMatrix(s, nodes[i++], cols);
                networkSpec_data[s].Name = s;
                networkSpec_data[s].NetworkId = int.Parse(s);
                Algorithms.Iota(networkSpec_data[s].RowLabels, 1);
                if(sign)
                {
                    string[] colLabels = { "Pos. Degree", "Neg. Degree", "Min", "Max" };
                    networkSpec_data[s].ColLabels.SetLabels(colLabels);
                    int j = 0;
                    foreach (KeyValuePair<string, List<int>> sub_kvp in kvp.Value)
                    {
                        int k = 0;
                        foreach (int x in sub_kvp.Value)
                        {
                            networkSpec_data[s][k++, j] = x;
                        }
                        j++;
                    }
                }
                else
                {
                    string[] colLabels = { "Degree", "Min", "Max" };
                    networkSpec_data[s].ColLabels.SetLabels(colLabels);
                    int j = 0;
                    foreach (KeyValuePair<string, List<int>> sub_kvp in kvp.Value)
                    {
                        int k = 0;
                        foreach (int x in sub_kvp.Value)
                        {
                            networkSpec_data[s][k++, j] = x;
                            //if (sub_kvp.Key.Equals("Degree"))
                            //{
                            //    Console.WriteLine("Degree: " + x.ToString());
                            //}
                        }
                        j++;
                    }
                }

                // Console.WriteLine("Network ID: " + networkSpec_data[s].Name + " Row: " + networkSpec_data[s].RowLabels);


            }

            return networkSpec_data;
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
            int numRand;
            try
            {
                int.TryParse(numTextBox.Text, out numRand);
            }
            catch (Exception)
            {
                MessageBox.Show("The number of networks must be an integer!", "Error!");
                return;
            }
            if (numRand < 1)
            {
                MessageBox.Show("The number of networks must be positive!", "Error!");
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

        private void ConfigModel_Load(object sender, EventArgs e)
        {

        }
    }
}
