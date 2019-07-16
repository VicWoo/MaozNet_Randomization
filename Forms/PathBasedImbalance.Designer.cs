using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NetworkGUI.Forms
{
    public partial class  PathBasedImbalance :Form
    {
        //Class to process random iterations and input file
        public partial class support
        {
            public double[,] p_r;
            public double[,] n_r;
            public double[,] imb_r;



            public support(int itr_limit)
            {

                this.p_r = new double[itr_limit + 1, 3];
                this.n_r = new double[itr_limit + 1, 3];
                this.imb_r = new double[itr_limit + 1, 3];
            }

            public static int[,] rand_matrix(int size, int target)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int idx_col;
                int idx_row;
                int[,] output = new int[size, size];


                while (target > 0)
                {
                    idx_col = rnd.Next(size);
                    idx_row = rnd.Next(size);
                    if (idx_row == idx_col) continue;
                    if (output[idx_row, idx_col] > 0) continue;
                    else
                    {

                        output[idx_row, idx_col] = Math.Min(target, rnd.Next(target + 1));
                        target = target - output[idx_row, idx_col];
                    }
                }
                return output;

            }

            public static int[,] rand_matrix_roof(int size, int target, int roof)
            {

                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int idx_col;
                int idx_row;
                int[,] output = new int[size, size];
                int yet_to_fill;
                int gap;
                int k = 1;

                while (target > 0)
                {
                    idx_col = rnd.Next(size);
                    idx_row = rnd.Next(size);
                    if (idx_row == idx_col) continue;
                    if (output[idx_row, idx_col] > 0) continue;
                    else
                    {
                        yet_to_fill = size * size - k;
                        gap = target - yet_to_fill;
                        //Console.WriteLine("{0}-->{1}",gap, roof+1);
                        if (gap > 0 && gap < roof + 1)
                        {
                            //Min check may not be required
                            output[idx_row, idx_col] = rnd.Next(gap, roof + 1);
                        }
                        else
                        {
                            output[idx_row, idx_col] = rnd.Next(roof + 1);
                        }
                        target = target - output[idx_row, idx_col];
                        k++;


                    }
                }
                return output;
            }


            public void iterate_rand(int n, int order, int size, int pos_target, int neg_target, int pos_max, int neg_max)
            {

                string NetID;
                Console.WriteLine("size: {2} pos: {0} neg: {1}", pos_target, neg_target, size);

                Random rnd = new Random(Guid.NewGuid().GetHashCode());


                //ITERATING n (100) TIMES
                for (int i = 0; i < n; i++)
                {

                    int[,] x_plus = rand_matrix_roof(size, pos_target, pos_max);
                    int[,] x_minus = rand_matrix_roof(size, neg_target, neg_max);

                    NetID = rnd.Next(10000).ToString();

                     PathBasedImbalance g = new  PathBasedImbalance(NetID, x_plus, x_minus);

                    g.infoTable(this, order);

                }

            }

            public static void printMatrix(int[,] matrix)
            {
                int size = matrix.GetLength(0);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write("{0}", matrix[i, j]);
                    }

                    Console.Write("\n");
                }
            }



            
        }

        //To Display DataGridView
public class Bind_DataGridView_Using_DataTable :Form
{
    
    private DataGridView songsDataGridView = new DataGridView();


    public Bind_DataGridView_Using_DataTable()
    {
        this.Load += new EventHandler(Form1_Load);
    }

    private void Form1_Load(System.Object sender, System.EventArgs e)
    {
        SetupDataGridView();
        PopulateDataGridView();
    }

    private void songsDataGridView_CellFormatting(object sender,
        System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
    {
        if (e != null)
        {
            if (this.songsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
            {
                if (e.Value != null)
                {
                    try
                    {
                        e.Value = DateTime.Parse(e.Value.ToString())
                            .ToLongDateString();
                        e.FormattingApplied = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                    }
                }
            }
        }
    }



    private void SetupDataGridView()
    {
        this.Controls.Add(songsDataGridView);

        songsDataGridView.ColumnCount = 5;

        songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
        songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        songsDataGridView.ColumnHeadersDefaultCellStyle.Font =
            new Font(songsDataGridView.Font, FontStyle.Bold);

        songsDataGridView.Name = "songsDataGridView";
        songsDataGridView.Location = new Point(8, 8);
        songsDataGridView.Size = new Size(500, 250);
        songsDataGridView.AutoSizeRowsMode =
            DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
        songsDataGridView.ColumnHeadersBorderStyle =
            DataGridViewHeaderBorderStyle.Single;
        songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        songsDataGridView.GridColor = Color.Black;
        songsDataGridView.RowHeadersVisible = false;

        songsDataGridView.Columns[0].Name = "Release Date";
        songsDataGridView.Columns[1].Name = "Track";
        songsDataGridView.Columns[2].Name = "Title";
        songsDataGridView.Columns[3].Name = "Artist";
        songsDataGridView.Columns[4].Name = "Album";
        songsDataGridView.Columns[4].DefaultCellStyle.Font =
            new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

        songsDataGridView.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        songsDataGridView.MultiSelect = false;
        songsDataGridView.Dock = DockStyle.Fill;

        songsDataGridView.CellFormatting += new
            DataGridViewCellFormattingEventHandler(
            songsDataGridView_CellFormatting);
    }

    private void PopulateDataGridView()
    {

        string[] row0 = { "11/22/1968", "29", "Revolution 9", 
            "Beatles", "The Beatles [White Album]" };
        string[] row1 = { "1960", "6", "Fools Rush In", 
            "Frank Sinatra", "Nice 'N' Easy" };
        string[] row2 = { "11/11/1971", "1", "One of These Days", 
            "Pink Floyd", "Meddle" };
        string[] row3 = { "1988", "7", "Where Is My Mind?", 
            "Pixies", "Surfer Rosa" };
        string[] row4 = { "5/1981", "9", "Can't Find My Mind", 
            "Cramps", "Psychedelic Jungle" };
        string[] row5 = { "6/10/2003", "13", 
            "Scatterbrain. (As Dead As Leaves.)", 
            "Radiohead", "Hail to the Thief" };
        string[] row6 = { "6/30/1992", "3", "Dress", "P J Harvey", "Dry" };

        songsDataGridView.Rows.Add(row0);
        songsDataGridView.Rows.Add(row1);
        songsDataGridView.Rows.Add(row2);
        songsDataGridView.Rows.Add(row3);
        songsDataGridView.Rows.Add(row4);
        songsDataGridView.Rows.Add(row5);
        songsDataGridView.Rows.Add(row6);

        songsDataGridView.Columns[0].DisplayIndex = 3;
        songsDataGridView.Columns[1].DisplayIndex = 4;
        songsDataGridView.Columns[2].DisplayIndex = 0;
        songsDataGridView.Columns[3].DisplayIndex = 1;
        songsDataGridView.Columns[4].DisplayIndex = 2;
    }
}
     }
}
