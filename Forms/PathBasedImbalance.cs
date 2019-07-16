using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using NetworkGUI.Forms;
using Network.Matrices;


namespace NetworkGUI.Forms
{
    public partial class 
        
        
        
        
        
         PathBasedImbalance : Form
    {


        private string NetID;
        public int[,] x_plus;
        public int[,] x_minus;
        private int size;
        private bool[] visited;
        private int[] path;
        private int[] pos;
        private int[] neg;
        private int pos_target;
        private int neg_target;
        private int pos_max;
        private int neg_max;
        public DataTable table;

        public PathBasedImbalance()
        {
        }


        public PathBasedImbalance(string NetID, int[,] x_plus, int[,] x_minus)
        {
            this.NetID = NetID;
            this.x_minus = x_minus;
            this.x_plus = x_plus;
            this.size = x_plus.GetLength(0);
            this.visited = new bool[this.size];
            this.path = new int[this.size];
            this.pos = new int[4];
            this.neg = new int[4];
            this.pos_target = matrix_sum(this.x_plus);
            this.neg_target = matrix_sum(this.x_minus);
            this.pos_max = matrix_max(this.x_plus);
            this.neg_max = matrix_max(this.x_minus);
            this.table = new DataTable();


        }

        public int matrix_sum(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int output = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    output += matrix[i, j];

                }
            }
            return output;

        }

        public int matrix_max(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int output = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //output = Math.Max(output, matrix[i,j]);
                    if (output < matrix[i, j]) output = matrix[i, j];

                }
            }
            //Console.WriteLine("{0} is the matrix max",output);
            return output;
        }



        public void path_count(int[] path, int path_index)
        {
            //To log paths
            //StreamWriter sr = new StreamWriter("log.csv", true);
            int p = x_plus[this.path[0], this.path[1]];
            int n = x_minus[this.path[0], this.path[1]];
            int p_;
            int n_;
            //Console.Write("{0}\t",this.path[0]);

            for (int i = 1; i < path_index; i++)
            {
                //Printing path
                //Console.Write("{0}\t",this.path[i]);
                if (i != path_index - 1)
                {
                    int w_pos = x_plus[this.path[i], this.path[i + 1]];
                    int w_neg = x_minus[this.path[i], this.path[i + 1]];



                    if (w_pos > 0 && w_neg > 0)
                    {
                        p_ = p * w_pos + n * w_neg;
                        n_ = n * w_pos + p * w_neg;
                    }
                    else if (w_pos > 0)
                    {
                        p_ = p * w_pos + n * w_neg;
                        n_ = n * w_pos + p * w_neg;
                    }
                    else
                    {
                        p_ = p * w_pos + n * w_neg;
                        n_ = n * w_pos + p * w_neg;
                    }
                    p = p_;
                    n = n_;


                }
            }
            //Printing path
            //Console.Write("\n");
            //sr.Close();

            //if(path_index<5){
            this.pos[path_index - 1] += p;
            this.neg[path_index - 1] += n;
            //Array.Clear(this.visited,false,this.visited.Length);
            //Console.Write("-->p: {0} n:{1}\n",this.pos[path_index-1],this.neg[path_index-1]);
            //}
        }


        public void findPaths(int s, int d, ref int path_index)
        {

            this.visited[s] = true;
            this.path[path_index] = s;
            path_index++;
            //Console.WriteLine("source: {0} destination: {1}",s, d);
            if (s == d && path_index > 1)
            {
                //Console.WriteLine("final");
                path_count(this.path, path_index);
            }
            else if (path_index < 4)
            {
                // Console.WriteLine("final");

                for (int i = 0; i < this.size; i++)
                {
                    if (this.x_minus[s, i] != 0 || this.x_plus[s, i] != 0)
                    {
                        if (!this.visited[i] || (this.visited[i] && i == d))
                        {
                            //Console.WriteLine("Going deeper from i :{1} & d :{2} at path_index:{0}",path_index,i,d);
                            findPaths(i, d, ref path_index);
                        }
                    }
                }
            }
            path_index--;
            this.visited[s] = false;


        }
        public void findPaths_org(int s, int d, ref int path_index)
        {

            visited[s] = true;
            this.path[path_index] = s;
            path_index++;
            if (s == d)
            {
                path_count(this.path, path_index);
            }
            else if (path_index < 4)
            {


                for (int i = 0; i < this.size; i++)
                {

                    if (this.x_minus[s, i] != 0 || this.x_plus[s, i] != 0)
                    {
                        if (!visited[i])
                        {
                            //Console.WriteLine("Going deeper at path_index:{0}",path_index);
                            findPaths(i, d, ref path_index);
                        }
                    }
                }
            }
            path_index--;
            visited[s] = false;


        }

        public void infoTable(Dictionary<string, int> states_hash, int order, bool Null)
        {

            int itr_print = 0;
            int itr_limit = this.size * (this.size) - 1;
            support e = new support(itr_limit);
            if (Null)
            {

                e.iterate_rand(100, order, this.size, this.pos_target, this.neg_target, this.pos_max, this.neg_max);
            }
            //Initialize table columns
            this.table.Columns.Add("networkid");
            this.table.Columns.Add("i");
            this.table.Columns.Add("j");
            this.table.Columns.Add("positive_1");
            this.table.Columns.Add("negative_1");
            this.table.Columns.Add("imbal_1");
            if (Null)
            {
                this.table.Columns.Add("p_r_1");
                this.table.Columns.Add("n_r_1");
                this.table.Columns.Add("rand_imbal_1");
            }
            if (order > 1)
            {
                this.table.Columns.Add("positive_2");
                this.table.Columns.Add("negative_2");
                this.table.Columns.Add("total_positive_2");
                this.table.Columns.Add("total_negative_2");
                this.table.Columns.Add("imbal_2");
                if (Null)
                {
                    this.table.Columns.Add("p_r_2");
                    this.table.Columns.Add("n_r_2");
                    this.table.Columns.Add("rand_imbal_2");
                }
            }
            if (order == 3)
            {
                this.table.Columns.Add("positive_3");
                this.table.Columns.Add("negative_3");
                this.table.Columns.Add("total_positive_3");
                this.table.Columns.Add("total_negative_3");
                this.table.Columns.Add("imbal_3");
                if (Null)
                {
                    this.table.Columns.Add("p_r_3");
                    this.table.Columns.Add("n_r_3");
                    this.table.Columns.Add("rand_imbal_3");
                }
            }

            /*file_writer.WriteLine("networkid\ti\tj\t"+
            "positive_1\tnegative_1\timbal_1\trand_imbal_1\t"+
            "positive_2\tnegative_2\timbal_2\trand_imbal_2\t"+
            "positive_3\tnegative_3\timbal_3\trand_imbal_3");
               */

            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {

                    DataRow row_ = this.table.NewRow();
                    //if(i != j)
                    {
                        //file_writer.Write("{0}\t{1}\t{2}\t",this.NetID, states_hash.FirstOrDefault(x => x.Value == i).Key,states_hash.FirstOrDefault(x => x.Value == j).Key);
                        row_["networkid"] = this.NetID;
                        row_["i"] = states_hash.FirstOrDefault(x => x.Value == i).Key;
                        row_["j"] = states_hash.FirstOrDefault(x => x.Value == j).Key;

                        int path_index = 0;
                        //Console.WriteLine("i = {0}\t j = {1}", i, j);
                        this.findPaths(i, j, ref path_index);

                        for (int path_length = 1; path_length < order + 1; path_length++)
                        {

                            double p = this.pos[path_length];
                            double n = this.neg[path_length];
                            row_["positive_" + path_length.ToString()] = p;
                            row_["negative_" + path_length.ToString()] = n;
                            double imb = 0;


                            if (path_length == 2)
                            {

                                row_["total_positive_2"] = int.Parse(row_["positive_1"].ToString()) + int.Parse(row_["positive_2"].ToString());
                                row_["total_negative_2"] = int.Parse(row_["negative_1"].ToString()) + int.Parse(row_["negative_2"].ToString()) + int.Parse(row_["positive_1"].ToString()) * int.Parse(row_["negative_1"].ToString());
                                p = Convert.ToDouble(row_["total_positive_2"]);
                                n = Convert.ToDouble(row_["total_negative_2"]);

                            }
                            if (path_length == 3)
                            {
                                row_["total_positive_3"] = int.Parse(row_["positive_1"].ToString()) + int.Parse(row_["positive_2"].ToString()) + int.Parse(row_["positive_3"].ToString());
                                row_["total_negative_3"] = int.Parse(row_["negative_1"].ToString()) + int.Parse(row_["negative_2"].ToString()) + int.Parse(row_["negative_3"].ToString());
                                p = Convert.ToDouble(row_["total_positive_3"]);
                                n = Convert.ToDouble(row_["total_negative_3"]);

                            }
                            if (p + n > 1)
                            {
                                imb = (2 * p * n) / ((p + n) * (p + n - 1));

                            }



                            row_["imbal_" + path_length.ToString()] = imb;
                            if (Null)
                            {
                                row_["p_r_" + path_length.ToString()] = e.p_r[itr_print, path_length - 1] / 100;
                                row_["n_r_" + path_length.ToString()] = e.n_r[itr_print, path_length - 1] / 100;
                                row_["rand_imbal_" + path_length.ToString()] = e.imb_r[itr_print, path_length - 1] / 100;
                            }

                        }
                        if (itr_print > itr_limit) itr_print = 0;
                        else itr_print++;

                        Array.Clear(this.pos, 0, this.pos.Length);
                        Array.Clear(this.neg, 0, this.neg.Length);
                        this.table.Rows.Add(row_);
                        //file_writer.Write("\n");

                    }

                }
            }
        }

    public double[,] ConvertDataTableToMatrix()
{
    double[,] matrix = new double[this.table.Rows.Count, this.table.Columns.Count];

    for(int col = 0; col < this.table.Columns.Count; col++){
    for (int row = 0; row < this.table.Rows.Count; row++)
    {
        matrix[row,col] = Convert.ToDouble(this.table.Rows[row][col]);
    }
    }
    return matrix;
}

        public void printDataTable(StreamWriter writer)
        {
            //StreamWriter writer = new StreamWriter("rough_text.csv");
            foreach (DataRow dataRow in this.table.Rows)
            {
                foreach (DataColumn item in this.table.Columns)
                {

                    writer.Write("{0:0.###},", dataRow[item]);
                }
                writer.Write("\n");
            }


        }




        public void infoTable(support e, int order)
        {
            int itr_stats_print = 0;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    //if(i != j){
                    int path_index = 0;
                    this.findPaths(i, j, ref path_index);




                    for (int path_length = 1; path_length < order + 1; path_length++)
                    {
                        double p = this.pos[path_length];
                        double n = this.neg[path_length];
                        double imb = 0;
                        if (p + n > 1)
                        {


                            if (path_length == 2)
                            {
                                p = p + this.pos[path_length - 1];
                                n = n + this.neg[path_length - 1] + this.pos[path_length - 1] * this.neg[path_length - 1];
                            }
                            if (path_length == 3)
                            {
                                p = p + this.pos[path_length - 1] + this.pos[path_length - 2];
                                n = n + this.neg[path_length - 1] + this.neg[path_length - 2];
                            }

                            imb = (2 * p * n) / ((p + n) * (p + n - 1));

                        }

                        e.p_r[itr_stats_print, path_length - 1] = e.p_r[itr_stats_print, path_length - 1] + p;
                        e.n_r[itr_stats_print, path_length - 1] = e.n_r[itr_stats_print, path_length - 1] + n;
                        e.imb_r[itr_stats_print, path_length - 1] = e.imb_r[itr_stats_print, path_length - 1] + imb;

                    }
                    if (itr_stats_print > e.p_r.GetLength(0)) itr_stats_print = 0;
                    else itr_stats_print++;

                    Array.Clear(this.pos, 0, this.pos.Length);
                    Array.Clear(this.neg, 0, this.neg.Length);

                }
            }


        }



                
        //new
        public double[,] supportScript(string inputFile, int order, bool Null, int startYear)
        {
            string file_name = inputFile.Replace(@"\\", @"\");
            
            int val; // to check if int or string
            var main_reader = new StreamReader(file_name);
            //output_writer must append --> option
           double [,] output = null;
            //finding first element
            var reader_ = new StreamReader(file_name);
            string current_year = reader_.ReadLine();
            var reader__ = reader_.ReadLine().Split(',');
            int num_cols = reader__.Length;
            current_year = reader__[0];
            // logger.WriteLine("First current_year value = {0}", current_year);
            reader_.Close();
            

            StreamReader local_reader = new StreamReader(file_name);
            //Scrolling through file
            while (!main_reader.EndOfStream)
            {
                //if(Int32.Parse(current_year)<startYear){
                
                  //var skip = main_reader.ReadLine();
                //continue;}
                //if(Int32.Parse(current_year)>endYear)break;
              
                var main_line = main_reader.ReadLine();
                //logger.WriteLine("In first while while main_reader at :{0}",main_line);
               
                var cols = main_line.Split(',');

                // /Console.WriteLine("The num_cols : {0}",num_cols);

                if (!int.TryParse(cols[0],out val)) continue;
                if(Int32.Parse(cols[0]) ==startYear){
                HashSet<string> states_set = new HashSet<string>();
                
                string NetID = cols[0];
                //Building set of distinct nodes for matrix size & indices
                //logger.WriteLine("Building HashSet for : {0}", current_year);

                while (current_year != "" && NetID == cols[0])
                {
                    if (main_reader.EndOfStream)
                    {
                        cols[0] = "end"; break;
                    }
                    states_set.Add(cols[1]);
                    states_set.Add(cols[2]);
                            
                    string line = main_reader.ReadLine();
                    //Console.WriteLine(line);


                    cols = line.Split(',');

                }//main_reader aka cols is at next year.

                //Graph to be called using current_year as NetID
                

                Dictionary<string, int> states_hash = new Dictionary<string, int>();

                //Building HashMap
                //logger.WriteLine("Building HashMap for year : {0}", current_year);
                int idx = 0;
                //Printing hashMap gathered for current year
                foreach (var element in states_set)
                {
                    states_hash.Add(element, idx);
                    //logger.WriteLine("{0}-->{1}", element, states_hash[element]);
                    idx++;
                }


                int n_nodes = states_set.Count;
                int[,] x_plus_ref = new int[n_nodes, n_nodes];
                int[,] x_minus_ref = new int[n_nodes, n_nodes];



                while (!local_reader.EndOfStream && current_year != cols[0])
                {

                    var local_line = local_reader.ReadLine();
                    //logger.WriteLine("local:{0}", local_line);
                    var row_in_year = local_line.Split(',');
                    
                    
                    if (!int.TryParse(row_in_year[0],out val)) continue;
                    current_year = row_in_year[0];
                    if (row_in_year[1] == row_in_year[2]) continue;
                    if(Int32.Parse(row_in_year[0]) ==startYear){
                    

                    for (int i = 3; i < num_cols; i++)
                    {
                       //logger.WriteLine("key_i = {0}, key_j = {1}",row_in_year[1], row_in_year[2]);
                        if (int.Parse(row_in_year[i]) > 0)
                        {
                            //logger.WriteLine("pos: {0},{1}-->{2},{3}", states_hash[row_in_year[1]], states_hash[row_in_year[2]], row_in_year[1], row_in_year[2]);
                            x_plus_ref[states_hash[row_in_year[1]], states_hash[row_in_year[2]]]++;
                        }
                        if (int.Parse(row_in_year[i]) < 0)
                        {
                            //logger.WriteLine("neg: {0},{1}-->{2},{3}", states_hash[row_in_year[1]], states_hash[row_in_year[2]], row_in_year[1], row_in_year[2]);
                            x_minus_ref[states_hash[row_in_year[1]], states_hash[row_in_year[2]]]++;
                        }
                    }
                }
                    }//current_year now is cols[0] of exit i.e next year

                PathBasedImbalance g_ref = new PathBasedImbalance(NetID, x_plus_ref, x_minus_ref);
                g_ref.infoTable(states_hash, order, Null);
                output = g_ref.ConvertDataTableToMatrix();
                
                Array.Clear(x_plus_ref, 0, x_plus_ref.Length);
                Array.Clear(x_minus_ref, 0, x_minus_ref.Length);
                states_hash.Clear();
                states_set.Clear();
            }
            }
            main_reader.Close();
            local_reader.Close();
           return output;
        }
        public double[,] displayScript(string inputFile, int order, bool Null)
        {
            string file_name = inputFile.Replace(@"\\", @"\");
            
            int val; // to check if int or string
            var main_reader = new StreamReader(file_name);
            //output_writer must append --> option
           double [,] output = null;
            //finding first element
            var reader_ = new StreamReader(file_name);
            string current_year = reader_.ReadLine();
            var reader__ = reader_.ReadLine().Split(',');
            int num_cols = reader__.Length;
            current_year = reader__[0];
           //logger.WriteLine("First current_year value = {0}", current_year);
            reader_.Close();
            
            //For display
            
            int endYear = Int32.Parse(current_year);
            StreamReader local_reader = new StreamReader(file_name);
            //Scrolling through file
            while (!main_reader.EndOfStream)
            {
                //if(Int32.Parse(current_year)<startYear)continue;
                if(Int32.Parse(current_year)>endYear)break;

                var main_line = main_reader.ReadLine();
                //logger.WriteLine("In first while while main_reader at :{0}",main_line);
                var cols = main_line.Split(',');

                // /Console.WriteLine("The num_cols : {0}",num_cols);

                if (!int.TryParse(cols[0],out val)) continue;

                HashSet<string> states_set = new HashSet<string>();

                //Building set of distinct nodes for matrix size & indices
                //logger.WriteLine("Building HashSet for : {0}", current_year);

                while (current_year != "" && current_year == cols[0])
                {
                    if (main_reader.EndOfStream)
                    {
                        cols[0] = "end"; break;
                    }
                    states_set.Add(cols[1]);
                    states_set.Add(cols[2]);

                    string line = main_reader.ReadLine();
                    //Console.WriteLine(line);


                    cols = line.Split(',');
                }//main_reader aka cols is at next year.

                //Graph to be called using current_year as NetID
                string NetID = current_year;

                Dictionary<string, int> states_hash = new Dictionary<string, int>();

                //Building HashMap
                //logger.WriteLine("Building HashMap for year : {0}", current_year);
                int idx = 0;
                //Printing hashMap gathered for current year
                foreach (var element in states_set)
                {
                    states_hash.Add(element, idx);
                    //logger.WriteLine("{0}-->{1}", element, states_hash[element]);
                    idx++;
                }


                int n_nodes = states_set.Count;
                int[,] x_plus_ref = new int[n_nodes, n_nodes];
                int[,] x_minus_ref = new int[n_nodes, n_nodes];



                while (!local_reader.EndOfStream && current_year != cols[0])
                {
                    var local_line = local_reader.ReadLine();
                    //logger.WriteLine("local:{0}", local_line);
                    var row_in_year = local_line.Split(',');
                    
                    if (!int.TryParse(row_in_year[0],out val)) continue;
                    current_year = row_in_year[0];
                    if (row_in_year[1] == row_in_year[2]) continue;

                    for (int i = 3; i < num_cols; i++)
                    {
                       //logger.WriteLine("key_i = {0}, key_j = {1}",row_in_year[1], row_in_year[2]);
                        if (int.Parse(row_in_year[i]) > 0)
                        {
                            //logger.WriteLine("pos: {0},{1}-->{2},{3}", states_hash[row_in_year[1]], states_hash[row_in_year[2]], row_in_year[1], row_in_year[2]);
                            x_plus_ref[states_hash[row_in_year[1]], states_hash[row_in_year[2]]]++;
                        }
                        if (int.Parse(row_in_year[i]) < 0)
                        {
                            //logger.WriteLine("neg: {0},{1}-->{2},{3}", states_hash[row_in_year[1]], states_hash[row_in_year[2]], row_in_year[1], row_in_year[2]);
                            x_minus_ref[states_hash[row_in_year[1]], states_hash[row_in_year[2]]]++;
                        }
                    }
                }//current_year now is cols[0] of exit i.e next year

                PathBasedImbalance g_ref = new PathBasedImbalance(NetID, x_plus_ref, x_minus_ref);
                g_ref.infoTable(states_hash, order, Null);
                output = g_ref.ConvertDataTableToMatrix();
                
                Array.Clear(x_plus_ref, 0, x_plus_ref.Length);
                Array.Clear(x_minus_ref, 0, x_minus_ref.Length);
                states_hash.Clear();
                states_set.Clear();
            }

            main_reader.Close();
            local_reader.Close();
           return output;
        }
    }

}
