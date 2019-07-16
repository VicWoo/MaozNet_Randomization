using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NetworkGUI.Forms
{
    public partial class MatMultProg : Form
    {
        int start, end;
        string file1, file2, curDirectory, saveName;
        bool hasBigNetworks = false;

        public MatMultProg(int s, int e, string f1, string f2, string curD, string sN) //constructor
        {
            InitializeComponent();
            Shown += new EventHandler(Form1_Shown);

            //load variables
            start = s;
            end = e;
            file1 = f1;
            file2 = f2;
            curDirectory = curD;
            saveName = sN;

            //loading number of iterations
            progressBar1.Maximum = numTimes(s, e);


            //*WARNING*
            //do no edit below
            //--------------------------------------------------------------------------------------------------------------------
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

      


        // On worker thread so do our thing!
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            var sr = new StreamReader(file1);
            var sr2 = new StreamReader(file2);
            int j = 0;
            int k = 0;

            //clearing first line
            sr.ReadLine();
            sr2.ReadLine();

           
           

            // Your background task goes here
            for (int i = 0; i < progressBar1.Maximum; i++)
            {
                if (!hasBigNetworks)
                {
                    k = i;
                }

                int curYear = start + j + k;

               // using (System.IO.StreamWriter file = File.AppendText(curDirectory + "matrixMult" + ".csv"))
               // {
                   // file.Write(curYear);
                  //  file.WriteLine();
              //  }



                readYear(curYear, end, sr, sr2, curDirectory);


                // Report progress to 'UI' thread
                backgroundWorker1.ReportProgress(i);

                // determine which year type is being used 1889001 vs 1885
                // note for readibility I have editted the spacing - Alvin
                if (hasBigNetworks)
                {
                   if (k == 99) {j += 1000; k = 0;}
                   else
                        k++;
                }

            }//end of forloop

            sr.Close();
            sr2.Close();
        }

      

        private void readYear(int start, int end, StreamReader f1, StreamReader f2, string curDirectory)
        {
            int maxlines = -1;
            int count = 0;

            //Matrix labels
            List<string> tags = new List<string>();
            List<string> f1_mat = new List<string>();
           // List<string> f2_mat = new List<string>();

            //Matrix


            //mapping tags to matrix
            Dictionary<string, int> tagMap = new Dictionary<string, int>();
            Dictionary<int, string> tagMapInv = new Dictionary<int, string>();

            //first reading first file
            while (!f1.EndOfStream && maxlines != count)
            {
                string line = f1.ReadLine();
                var split = line.Split(',');

                //dydiac formate year,statea,stateb,val

                int curYear = int.Parse(split[0]);
                string tag = split[2]; 


                //finding correct year
                if (start != curYear)
                {
                    continue;
                }

                //adding matrx labels and determining number of entries
                if (!tags.Contains(tag))
                {
                    tags.Add(tag); 
                }

                else
                {
                    maxlines = tags.Count * tags.Count;
                }

                f1_mat.Add(line);
                count++;
            }

            //adding map for matrix building
            for (int i = 0; i < tags.Count; i++)
            {
                tagMap.Add(tags[i], i);
                tagMapInv.Add(i, tags[i]);
            }

            double[][] f1Mat = createMatrix(tags.Count);
            double[][] f2Mat = createMatrix(tags.Count);
            double[][] result = createMatrix(tags.Count);

            //creating f1matrix
            for (int i = 0; i < f1_mat.Count; i++)
            {
                //year,statea,stateb,val
                var elements = f1_mat[i].Split(',');
                int row = tagMap[elements[1]];
                int col = tagMap[elements[2]];
                double edge = double.Parse(elements[3]);
                f1Mat[row][col] = edge;
            }


            count = 0;
            while (!f2.EndOfStream && maxlines != count)
            {

                if (f2.EndOfStream)
                    break;

                string line = f2.ReadLine();
                var split = line.Split(',');

                //dydiac formate year,statea,stateb,val

                int curYear = int.Parse(split[0]);

                //finding correct year
                if (start != curYear)
                {
                    continue;
                }


                //assuming correct year is found
                int row = tagMap[split[1]];
                int col = tagMap[split[2]];
                double edge = double.Parse(split[3]);
                f2Mat[row][col] = edge;

                //f2_mat.Add(line);
                count++;
            }

            //assuming 2 matrics are loaded
            //making result matrix

            for (int i = 0; i < tags.Count; i++)
            {
                for (int j = 0; j < tags.Count; j++)
                {
                    result[i][j] = 0;
                    for (int k = 0; k < tags.Count; k++)
                        result[i][j] += f1Mat[i][k] * f2Mat[k][j];
                }
            }

            //sample file
            using (StreamWriter sw = File.AppendText(curDirectory + saveName +".csv"))
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    for (int j = 0; j < tags.Count; j++)
                    {
                        double val = result[i][j];
                        string row = tagMapInv[i];
                        string col = tagMapInv[j];
                        string line = start.ToString() + ',' + row + ',' + col + ',' + val.ToString();
                        sw.WriteLine(line);
                    }
                }
            }

        }

        double[][] createMatrix(int numNodes)
        {
            double[][] newMat = new double[numNodes][];
            for (int i = 0; i < numNodes; i++)
                newMat[i] = new double[numNodes];
            return newMat;
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void MatMultProg_Load(object sender, EventArgs e)
        {

        }

        int numTimes(int start, int end)
        {
            int Iterations = 0;

            //ex        1845001
            if (start > 999999) //at least 7 digits
            {
                hasBigNetworks = true;
                int s = start;
                int e = end;

                //gettings first 4 digits
                s /= 1000;
                e /= 1000;

                if (s == e) //if same year
                {
                    if (start == end)
                        Iterations = 1;
                    else
                        Iterations = (end % 1000) - (start % 1000) + 1;
                }

                else
                {
                    if (e - s == 1)
                        Iterations = (101 - (start % 1000)) + (end % 1000);
                    else
                        Iterations = ((e - s - 1) * 100) + (101 - (start % 1000)) + (end % 1000);
                }// end of else
            }// end of if

            //ex 1845
            else
            {
                Iterations = end - start + 1;
            }

            return Iterations;
        }


        // Back on the 'UI' thread so we can update the progress bar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBar1.Value = e.ProgressPercentage;
        }

         void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            Close();
        }
    }
}
