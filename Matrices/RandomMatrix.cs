using System;
using System.Collections.Generic;
using System.Text;
using RandomUtilities;
using System.Windows.Forms;

namespace Network.Matrices
{
    public sealed class RandomMatrix
    {
        private RandomMatrix() { }

        public static Matrix LoadNonSymmetric(int n, bool range, double pmin, double pmax)
        {
            if (!range)
            {
                Matrix m = LoadNonSymmetric(n, n);

                for (int i = 0; i < m.Rows; ++i)
                    m[i, i] = 0.0;

                return m;
            }
            else
            {

                double probno = RNG.RandomFloat(pmin, pmax);
                double newprob = probno;
                int counter = 0;
                Matrix m = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) m[i, j] = 0;
                 //       else if (((counter + 1) / (double)(n - 1)) > newprob) m[i, j] = 0;
                        else
                        {
                            m[i, j] = RNG.RandomBinary(newprob);
                            if (m[i, j] == 1) counter++;
                        }
                    } 
                    newprob = probno + newprob - (counter / (double)(n - 1));
                    counter = 0;
                }

 
                Algorithms.Iota(m.ColLabels, 1);
                Algorithms.Iota(m.RowLabels, 1);
            return m;
            }

        }

        public static Matrix LoadNonSymmetric(int rows, int cols)
        {
            Matrix m = new Matrix(rows, cols);

            Algorithms.Fill<double>(m, RNG.RandomBinary);
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            return m;
        }

        public static Matrix LoadValuedNonSymmetric(int n, double vmin, double vmax, bool datatype, bool zerodiagonalized, bool range, double pmin, double pmax)
        {

            if (!range)
            {
                Matrix m = LoadValuedNonSymmetric(n, n, vmin, vmax, datatype, zerodiagonalized);

                return m;
            }
            else
            {

                double probno = RNG.RandomFloat(pmin, pmax);
                double newprob = probno;
                int counter = 0;
                Matrix m = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j && zerodiagonalized) m[i, j] = 0;
                        //       else if (((counter + 1) / (double)(n - 1)) > newprob) m[i, j] = 0;
                        else
                        {
                            m[i, j] = RNG.RandomBinary(newprob);
                            if (m[i, j] == 1)
                            {
                                m[i, j] = datatype ? RNG.RandomInt(vmin, vmax) : RNG.RandomFloat(vmin, vmax);
                                counter++;
                            }
                        }
                    }
                    newprob = probno + newprob - (counter / (double)(zerodiagonalized?(n - 1):n));
                    counter = 0;
                }

                Algorithms.Iota(m.ColLabels, 1);
                Algorithms.Iota(m.RowLabels, 1);
                return m;
            }


        }

        public static Matrix LoadValuedNonSymmetric(int rows, int cols, double vmin, double vmax, bool datatype, bool zerodiagonalized)
        {
            Matrix m = new Matrix(rows, cols);
            if(datatype)
                Algorithms.Fill<double>(m, RNG.RandomInt, vmin, vmax);
            else
                Algorithms.Fill<double>(m, RNG.RandomFloat, vmin, vmax);

            if (zerodiagonalized)
            {
                for (int i = 0; i < m.Rows; ++i)
                    m[i, i] = 0.0;
            }
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            return m;
        }

        public static Matrix LoadWithProbabilisticRange(int rows, int cols, double min, double max)
        {
            Matrix m = new Matrix(rows, cols);
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            for (int i = 0; i < rows; ++i)
                for (int j = i + 1; j < cols; ++j)
                    m[i, j] = m[j, i] = RNG.RandomBinary(RNG.RandomFloat(min, max));

            return m;
        }

        public static Matrix LoadSymmetric(int n, bool range, double pmin, double pmax)
        {
            if (!range)
            {
                Matrix m = LoadNonSymmetric(n, range, pmin, pmax);

                for (int i = 0; i < m.Rows; ++i)
                    for (int j = i + 1; j < m.Rows; ++j)
                        m[j, i] = m[i, j];

                return m;
            }
            else
            {

                double probno = RNG.RandomFloat(pmin, pmax);
                double newprob = probno;
                int counter = 0;
                Matrix m = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) m[i, j] = 0;
                        else if (i > j)
                        {
                            m[i, j] = m[j, i];
                            if (m[i, j] == 1)
                                counter++;
                        }
                    //    else if (((counter + 1) / (double)(n - 1)) > newprob) m[i, j] = 0;
                        else
                        {
                            m[i, j] = RNG.RandomBinary(newprob);
                            if (m[i, j] == 1) counter++;
                        }
                    }
                    newprob = probno + newprob - (counter / (double)(n - 1));
                    counter = 0;
                }


                Algorithms.Iota(m.ColLabels, 1);
                Algorithms.Iota(m.RowLabels, 1); 

                return m;
            }
        }
        public static Matrix LoadBlank(int N)
        {
            Matrix m = new Matrix(N);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    m[i, j] = 0;
                }
            }
            return m;
        }

        public static Matrix LoadValuedSymmetric(int n, double vmin, double vmax, bool datatype, bool zerodiagonalized, bool range, double pmin, double pmax)
        {


            if (!range)
            {
                Matrix m = LoadValuedNonSymmetric(n, vmin, vmax, datatype, zerodiagonalized, range, pmin, pmax);

                for (int i = 0; i < m.Rows; ++i)
                    for (int j = i + 1; j < m.Rows; ++j)
                        m[j, i] = m[i, j];

                return m;
            }
            else
            {

                double probno = RNG.RandomFloat(pmin, pmax);
                double newprob = probno;
                int counter = 0;
                Matrix m = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j && zerodiagonalized) m[i, j] = 0;
                        else if (i > j)
                        {
                            m[i, j] = m[j, i];
                            if (m[i, j] != 0)
                                counter++;
                        }
                        //       else if (((counter + 1) / (double)(n - 1)) > newprob) m[i, j] = 0;
                        else
                        {
                            m[i, j] = RNG.RandomBinary(newprob);
                            if (m[i, j] == 1)
                            {
                                m[i, j] = datatype ? RNG.RandomInt(vmin, vmax) : RNG.RandomFloat(vmin, vmax);
                                counter++;
                            }
                        }
                    }
                    newprob = probno + newprob - (counter / (double)(zerodiagonalized ? (n - 1) : n));
                    counter = 0;
                }

                //int hi = 0;
                //for (int i = 0; i < n; i++)
                //    for (int j = 0; j < n; j++)
                //        if (m[i, j] != 0) hi++;

                //MessageBox.Show(hi / (n * (double)(zerodiagonalized ? (n - 1) : n)) + "");

                Algorithms.Iota(m.ColLabels, 1);
                Algorithms.Iota(m.RowLabels, 1);
                return m;
            }

        }

        public static Matrix LoadDiagonal(int n, bool normalize)
        {
            Matrix m = new Matrix(n);
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            double sum = 0.0;
            for (int i = 0; i < m.Rows; ++i)
            {
                m[i, i] = RNG.RandomFloat();
                sum += m[i, i];
            }

            if (normalize)
                for (int i = 0; i < m.Rows; ++i)
                    m[i, i] /= sum;

            return m;
        }

        public static Vector LoadVector(int n)
        {
            Vector v = new Vector(n);
            Algorithms.Iota(v.Labels, 1);
            Algorithms.Fill<double>(v, RNG.RandomBinary);

            return v;
        }

        public static Vector LoadRealUnitVector(int n)
        {
            Vector v = new Vector(n);
            Algorithms.Iota(v.Labels, 1);
            Algorithms.Fill<double>(v, RNG.RandomFloat);
            v.Normalize();

            return v;
        }


        // Yushan
        // Generate Unsigned Global Randomization
        public static Matrix GenerateGlobal(bool directed, bool sign, bool selfties, int nodes, int edges, int vmax)
        {
            Matrix m = new Matrix(nodes);
            m.Clear();
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);
            Console.WriteLine("Edges: " + edges.ToString());
            int draw;
            int _row;
            int _col;
            List<int> pool = new List<int>();

            int count = 0;
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    pool.Add(count++);
                }
            }
            if(!selfties)
            {
                for (int i = 0; i < nodes; i++)
                {
                    pool.Remove(i * nodes + i);
                }
            }

            if (directed)
            {
                while (edges > 0)
                {
                    if (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;

                        m[_row, _col] = (int)RNG.RandomInt(1, vmax); // Inclusive upper bound
                        edges--;
                        pool.Remove(draw);
                    }
                    else
                    {
                        throw new Exception("Falied to find a valid randomization of edges.");
                    }
                }

            }
            else
            {
                if (!selfties)
                {
                    if (edges % 2 != 0)
                        throw new Exception("Input postive edges and negative edges are not both even!");
                }

                while (edges > 0)
                {
                    bool found = false;
                    while (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (selfties)
                        {
                            if (_row != _col && pool.Count > 1)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                m[_col, _row] = m[_row, _col];
                                edges--;
                                edges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else if (_row == _col)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                edges--;
                                pool.Remove(draw);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }
                        else
                        {
                            if (pool.Count > 1)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                m[_col, _row] = m[_row, _col];
                                edges--;
                                edges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }

                    }
                    if (found == false)
                    {
                        throw new Exception("Failed to find a valid randomization of edges.");
                    }
                }
            }
            return m;
        }

        // Generate Signed Global Randomization
        public static Matrix GenerateGlobal(bool directed, bool sign, bool selfties, int nodes, int posedges, int negedges, int vmin, int vmax)
        {
            Matrix m = new Matrix(nodes);
            m.Clear();
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);
            // Console.WriteLine("Posedges: " + posedges.ToString() + " Negedges: " + negedges.ToString);
            int draw;
            int _row;
            int _col;
            List<int> pool = new List<int>();

            int count = 0;
            for (int i = 0; i < nodes; i++)
            {
                for (int j = 0; j < nodes; j++)
                {
                    pool.Add(count++);
                }
            }
            if (!selfties)
            {
                for (int i = 0; i < nodes; i ++)
                {
                    pool.Remove(i * nodes + i);
                }
            }
            if (directed)
            {
                while (posedges > 0)
                {
                    bool found = false;
                    while (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;

                        if (selfties)
                        {
                            m[_row, _col] = (int)RNG.RandomInt(1, vmax); // Inclusive upper bound
                            posedges--;
                            pool.Remove(draw);
                            found = true;
                            break;
                        }
                        else
                        {
                            if (_row != _col)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax); // Inclusive upper bound
                                posedges--;
                                pool.Remove(draw);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                            }
                        }
                    }
                    if (found == false)
                    {
                        throw new Exception("Falied to find a valid randomization of positive edges.");
                    }
                }


                while (negedges > 0)
                {
                    bool found = true;
                    while (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (selfties)
                        {
                            m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin)); // Inclusive lower bound
                            negedges--;
                            pool.Remove(draw);
                            found = true;
                            break;
                        }
                        else
                        {
                            if (_row != _col)
                            {
                                m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin)); // Inclusive lower bound
                                negedges--;
                                pool.Remove(draw);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                            }
                        }
                    }
                    if (found == false)
                    {
                        throw new Exception("Failed to find a valid randomization of negative edges.");
                    }
                }

            }
        
            else
            {
                if (!selfties)
                {
                    if ((posedges % 2 != 0) || (negedges % 2 != 0))
                        throw new Exception("Input postive edges and negative edges are not both even!");
                }

                while (posedges > 0)
                {
                    bool found = false;
                    while (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (selfties)
                        {
                            if (_row != _col && pool.Count > 1)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                m[_col, _row] = m[_row, _col];
                                posedges--;
                                posedges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else if (_row == _col)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                posedges--;
                                pool.Remove(draw);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }
                        else
                        {
                            if (pool.Count > 1)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, vmax);
                                m[_col, _row] = m[_row, _col];
                                posedges--;
                                posedges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }

                    }
                    if (found == false)
                    {
                        throw new Exception("Failed to find a valid randomization of positive edges.");
                    }
                }


                while (negedges > 0)
                {
                    bool found = false;
                    while (pool.Count > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (selfties)
                        {
                            if (_row != _col && pool.Count > 1)
                            {
                                m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin));
                                m[_col, _row] = m[_row, _col];
                                negedges--;
                                negedges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else if (_row == _col)
                            {
                                m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin));
                                negedges--;
                                pool.Remove(draw);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }
                        else
                        {
                            if (_row != _col && pool.Count > 1)
                            {
                                m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin));
                                m[_col, _row] = m[_row, _col];
                                negedges--;
                                negedges--;
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                                found = true;
                                break;
                            }
                            else
                            {
                                pool.Remove(draw);
                                pool.Remove(_col * nodes + _row);
                            }
                        }
                    }
                    if (found == false)
                    {
                        throw new Exception("Failed to find a valid randomization of negative edges.");
                    }
                }
            }

            return m;
        }

        // Load Global Randomization
        public static Dictionary<string, List<Matrix>> LoadGlobalRandom (int n, bool directed, bool sign, bool selfties, List<Dictionary<string, int>> networkSpec)
        {
            Dictionary<string, List<Matrix>> mRandTable = new Dictionary<string, List<Matrix>>();
            int numNet = networkSpec.Count;
            string net_id;
            int nodes;
            int edges;
            int pos_edges;
            int neg_edges;
            int vmin;
            int vmax;

            int i, j;

            if (sign)
            {
                for (i = 0; i < numNet; i++)
                {
                    net_id = networkSpec[i]["Network ID"].ToString();
                    nodes = networkSpec[i]["Nodes"];
                    pos_edges = networkSpec[i]["Pos. Edges"];
                    neg_edges = networkSpec[i]["Neg. Edges"];
                    vmin = networkSpec[i]["Min Value"];
                    vmax = networkSpec[i]["Max Value"];
                    // Console.WriteLine("SelfTies:" + selfties.ToString());
                    List<Matrix> mlist = new List<Matrix>();

                    for (j = 0; j < n; j++)
                    {
                        mlist.Add(GenerateGlobal(directed, sign, selfties, nodes, pos_edges, neg_edges, vmin, vmax));
                    }

                    mRandTable.Add(net_id, mlist);

                }
            }
            else
            {
                for (i = 0; i < numNet; i++)
                {
                    net_id = networkSpec[i]["Network ID"].ToString();
                    nodes = networkSpec[i]["Nodes"];
                    edges = networkSpec[i]["Edges"];
                    vmin = networkSpec[i]["Min Value"];
                    vmax = networkSpec[i]["Max Value"];
                    // Console.WriteLine("net_id: " + net_id + " " + "nodes: " + nodes.ToString());
                    // Matrix temp = GenerateGlobal(directed, sign, selfties, nodes, edges, vmax);
                    // Console.WriteLine("Matrix R: " + temp.Rows.ToString() + " " + "Matrix C: " +temp.Cols.ToString());

                    List<Matrix> mlist = new List<Matrix>();

                    for (j = 0; j < n; j++)
                    {
                        mlist.Add(GenerateGlobal(directed, sign, selfties, nodes, edges, vmax));
                    }

                    mRandTable.Add(net_id, mlist);
                   
                }
            }


            return mRandTable;
 

        }

        // Generate Directed Configuration Models
        public static Matrix GenerateDirectedConfigModel (bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes = modelSpec.Rows;
            int i, j;
            Vector vmin, vmax;
            Vector deg, pos_deg, neg_deg;
            Matrix m = new Matrix(nodes);
            // m.Clear();
            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);


            if (sign)
            {
                pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                deg = pos_deg + neg_deg;

                for (i = 0; i < nodes; i++)
                {

                    List<int> pool = new List<int>();

                    // List<int> edge = null;
                    // List<int> nonedges = null;
                    for (int k = 0; k < nodes; k++)
                    {
                        if (!selfties && k == i)
                        {
                            continue;
                        }
                        else
                        {
                            pool.Add(k);
                        }
                    }
                    // Configure the positive edges
                    for (int k = 0; k < pos_deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        m[i, _col] = (int)RNG.RandomInt(1, vmax[i]);
                        pos_deg[_col]--;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }

                    // Configure the negative edges
                    for (int k = 0; k < neg_deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        m[i, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin[i]));
                        neg_deg[_col]--;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }
                }

            }

            else
            {

                deg = modelSpec.GetColVector(modelSpec.ColLabels["Degree"]);

                for (i = 0; i < nodes; i++)
                {

                    List<int> pool = new List<int>();

                    // List<int> edge = null;
                    // List<int> nonedges = null;
                    for (int k = 0; k < nodes; k++)
                    {
                        if (!selfties && k == i)
                        {
                            continue;
                        }
                        else
                        {
                            pool.Add(k);
                        }
                    }
                    // Configure the positive edges
                    for (int k = 0; k < deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        m[i, _col] = (int)RNG.RandomInt(1, vmax[i]);
                        deg[_col]--;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }
                }
            }
          return m;
        }

        // Generate Undirected Configuration Models (Version 1.0)
        public static Matrix GenerateUndirectedConfigModel(bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes = modelSpec.Rows;
            int i;
            // const int UPPER_BOUND = 200;
            Vector vmin, vmax;
            Vector deg, pos_deg, neg_deg;

            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);

            Matrix m = new Matrix(nodes);
 
            if (sign)
            {
                pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                deg = pos_deg + neg_deg;
                if (selfties)
                {
                    for (i = 0; i < nodes; i++)
                    {
                        List<int> pool = new List<int>();
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i; k < nodes; k++)
                        {
                            pool.Add(k);
                        }
                        // Configure the positive edges
                        if (pool.Count >= pos_deg[i])
                        {
                            for (int k = 0; k < pos_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int _col;
                                    int vtemp;
                                    _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(vmax[i], vmax[_col]) > 0 && pos_deg[_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[i], vmax[_col]));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        pos_deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + pos_deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input positive degrees");
                        }

                        // Configure the negative edges
                        if (pool.Count >= neg_deg[i])
                        {
                            for (int k = 0; k < neg_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int _col;
                                    int vtemp;
                                    _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(Math.Abs(vmin[i]), Math.Abs(vmin[_col])) > 0 && neg_deg[_col] > 0)
                                    {
                                        vtemp = (-1) * (int)RNG.RandomInt(1, Math.Min(Math.Abs(vmin[i]), Math.Abs(vmin[_col])));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        neg_deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + neg_deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input negative degrees");
                        }
                    }
                }
                else
                {
                    for (i = 0; i < (nodes - 1); i++)
                    {
                        List<int> pool = new List<int>();
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i + 1; k < nodes; k++)
                        {
                            pool.Add(k);
                        }

                        // Configure the positive edges
                        if (pool.Count >= pos_deg[i])
                        {
                            for (int k = 0; k < pos_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int _col;
                                    int vtemp;
                                    _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(vmax[i], vmax[_col]) > 0 && pos_deg[_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[i], vmax[_col]));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        pos_deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input positive degrees");
                        }

                        // Configure the negative edges
                        if (pool.Count >= neg_deg[i])
                        {
                            for (int k = 0; k < neg_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int _col;
                                    int vtemp;
                                    _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(Math.Abs(vmin[i]), Math.Abs(vmin[_col])) > 0 && neg_deg[_col] > 0)
                                    {
                                        vtemp = (-1) * (int)RNG.RandomInt(1, Math.Min(Math.Abs(vmin[i]), Math.Abs(vmin[_col])));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        neg_deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input negative degrees");
                        }
                    }
                }
            }
            else
            {
                deg = modelSpec.GetColVector(0);
                if (selfties)
                {

                    for (i = 0; i < nodes; i++)
                    {
                        List<int> pool = new List<int>();
                        
                       // Console.WriteLine("Row: " + i.ToString() + " Degree: " + deg[i].ToString());
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i; k < nodes; k++)
                        {
                            pool.Add(k);
                        }

                        // Console.WriteLine(pool.Count.ToString());
                        // Configure the edges
                        if (pool.Count >= deg[i])
                        {
                            for (int k = 0; k < deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    // Console.WriteLine(loop.ToString());
                                    int _col;
                                    int vtemp;
                                    _col = pool[RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(vmax[i], vmax[_col]) > 0 && deg[_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[i], vmax[_col]));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        // Console.WriteLine(deg[_col].ToString());
                                        deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }

                                // Console.WriteLine(loop.ToString());
                                if (found == false)
                                {
                                    // Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());
                                    
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            // Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input degrees");
                        }
                    }
                }
                else
                {
                    for (i = 0; i < (nodes - 1); i++)
                    {
                        List<int> pool = new List<int>();
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i + 1; k < nodes; k++)
                        {
                            pool.Add(k);
                        }

                        // Configure the edges
                        if (pool.Count >= deg[i])
                        {
                            for (int k = 0; k < deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int _col;
                                    int vtemp;
                                    _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    if (Math.Min(vmax[i], vmax[_col]) > 0 && deg[_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[i], vmax[_col]));
                                        m[_col, i] = m[i, _col] = vtemp;
                                        deg[_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input degrees");
                        }                     
                    }
                }
            }

            return m;
        }

        // Generate Undirected Configuration Models (Version 1.1)
        public static Matrix GenerateUndirectedConfigModel_Revised(bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes = modelSpec.Rows;
            int i;
            // const int UPPER_BOUND = 200;
            Vector vmin, vmax;
            Vector deg, pos_deg, neg_deg;

            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);

            Matrix m = new Matrix(nodes);

            if (sign)
            {
                pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                deg = pos_deg + neg_deg;

                // Sort pos_deg in descending order
                List<int> pos_deg_list = new List<int>();
                for (int k = 0; k < nodes; k++)
                {
                    pos_deg_list.Add((int)pos_deg[k]);
                }
                List<int> sorted_pos_deg_list = new List<int>(pos_deg_list);
                List<int> sorted_pos_index = new List<int>();
                sorted_pos_deg_list.Sort();
                sorted_pos_deg_list.Reverse(); // Sort the array of degrees in descending order
                for (int k = 0; k < nodes; k++)
                {
                    int temp = pos_deg_list.FindIndex(y => y == sorted_pos_deg_list[k]); // The original row index
                    sorted_pos_index.Add(temp);
                    pos_deg_list[temp] = -1;
                }
                Vector orig_pos_deg = new Vector(pos_deg);
                pos_deg.Clear();
                pos_deg = new Vector(sorted_pos_deg_list.ToArray());

                // Sort neg_deg in descending order
                List<int> neg_deg_list = new List<int>();
                for (int k = 0; k < nodes; k++)
                {
                    neg_deg_list.Add((int)neg_deg[k]);
                }
                List<int> sorted_neg_deg_list = new List<int>(neg_deg_list);
                List<int> sorted_neg_index = new List<int>();
                sorted_neg_deg_list.Sort();
                sorted_neg_deg_list.Reverse(); // Sort the array of degrees in descending order
                for (int k = 0; k < nodes; k++)
                {
                    int temp = neg_deg_list.FindIndex(y => y == sorted_neg_deg_list[k]); // The original row index
                    sorted_neg_index.Add(temp);
                    neg_deg_list[temp] = -1;
                }
                Vector orig_neg_deg = new Vector(neg_deg);
                neg_deg.Clear();
                neg_deg = new Vector(sorted_neg_deg_list.ToArray());


                if (selfties)
                {
                    for (i = 0; i < nodes; i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_pos_index[i];
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i; k < nodes; k++)
                        {
                            pool.Add(sorted_pos_index[k]);
                        }
                        // Configure the positive edges
                        if (pool.Count >= pos_deg[i])
                        {
                            for (int k = 0; k < pos_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int vtemp;
                                    int _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_pos_index.FindIndex(x => x == _col);
                                    if (Math.Min(vmax[_row], vmax[_col]) > 0 && pos_deg[temp_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        pos_deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + pos_deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input positive degrees");
                        }
                    }
                    for (i = 0; i < nodes; i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_neg_index[i];
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i; k < nodes; k++)
                        {
                            if (m[_row, k] == 0)
                            {
                                pool.Add(sorted_neg_index[k]);
                            }
                        }
                        // Configure the negative edges
                        if (pool.Count >= neg_deg[i])
                        {
                            for (int k = 0; k < neg_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int vtemp;
                                    int _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_neg_index.FindIndex(x => x == _col);
                                    if (Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])) > 0 && neg_deg[temp_col] > 0)
                                    {
                                        vtemp = (-1) * (int)RNG.RandomInt(1, Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        neg_deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + neg_deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input negative degrees");
                        }
                    }
                }
                else
                {
                    if (pos_deg.sum() % 2 != 0)
                    {
                        throw new Exception("The sum of the positive degrees is not even!");
                    }
                    if (neg_deg.sum() % 2 != 0)
                    {
                        throw new Exception("The sum of the negative degrees is not even!");
                    }
                    for (i = 0; i < (nodes - 1); i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_pos_deg_list[i];
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i + 1; k < nodes; k++)
                        {
                            pool.Add(sorted_pos_deg_list[k]);
                        }

                        // Configure the positive edges
                        if (pool.Count >= pos_deg[i])
                        {
                            for (int k = 0; k < pos_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int vtemp;
                                    int _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_pos_deg_list.FindIndex(x => x == _col);
                                    if (Math.Min(vmax[_row], vmax[_col]) > 0 && pos_deg[_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        pos_deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input positive degrees");
                        }
                    }
                    for (i = 0; i < (nodes - 1); i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_neg_index[i];
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i + 1; k < nodes; k++)
                        {
                            if (m[_row, k] == 0)
                            {
                                pool.Add(sorted_neg_index[k]);
                            }
                        }
                        // Configure the negative edges
                        if (pool.Count >= neg_deg[i])
                        {
                            for (int k = 0; k < neg_deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int vtemp;
                                    int _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_neg_index.FindIndex(x => x == _col);
                                    if (Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])) > 0 && neg_deg[temp_col] > 0)
                                    {
                                        vtemp = (-1) * (int)RNG.RandomInt(1, Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        neg_deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input negative degrees");
                        }
                    }
                }
            }
            else
            {
                deg = modelSpec.GetColVector(0);
                List<int> deg_list = new List<int>();
                for (int k = 0; k < nodes; k++)
                {
                    deg_list.Add((int)deg[k]);
                }
                List<int> sorted_deg_list = new List<int>(deg_list);
                List<int> sorted_index = new List<int>();
                sorted_deg_list.Sort();
                sorted_deg_list.Reverse(); // Sort the array of degrees in descending order
                for (int k = 0; k < nodes; k++)
                {
                    int temp = deg_list.FindIndex(y => y == sorted_deg_list[k]); // The original row index
                    sorted_index.Add(temp);
                    deg_list[temp] = -1;
                }
                Vector orig_deg = new Vector(deg);
                deg.Clear();
                deg = new Vector(sorted_deg_list.ToArray());

                // Matrix temp_m = new Matrix(nodes);
                if (selfties)
                {

                    for (i = 0; i < nodes; i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_index[i];
                        // Console.WriteLine("Row: " + i.ToString() + " Degree: " + deg[i].ToString());
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = 0; k < nodes; k++)
                        {
                            pool.Add(k);
                        }
                        for (int k = 0; k < i; k++)
                        {
                            pool.Remove(sorted_index[k]);
                        }
                        // Console.WriteLine(pool.Count.ToString());
                        // Configure the edges
                        if (pool.Count >= deg[i])
                        {
                            for (int k = 0; k < deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    // Console.WriteLine(loop.ToString());
                                    int vtemp;
                                    int _col = pool[RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_index.FindIndex(x => x == _col);
                                    if (Math.Min(vmax[_row], vmax[_col]) > 0 && deg[temp_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        // Console.WriteLine(deg[_col].ToString());
                                        deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }

                                // Console.WriteLine(loop.ToString());
                                if (found == false)
                                {
                                    // Console.WriteLine("Row: " + i.ToString() + " " + "Loop: " + loop.ToString());

                                    throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            // Console.WriteLine("pool.Count: " + pool.Count.ToString() + " " + "row: " + i.ToString() + " deg[row]: " + deg[i].ToString());
                            throw new Exception("Failed to generate a valid configuration with the input degrees");
                        }
                    }
                }
                else
                {
                    if (deg.sum() % 2 != 0)
                    {
                        throw new Exception("The sum of degrees is not even!");
                    }
                    for (i = 0; i < (nodes - 1); i++)
                    {
                        List<int> pool = new List<int>();
                        int _row = sorted_index[i];
                        // List<int> edge = null;
                        // List<int> nonedges = null;
                        for (int k = i+1; k < nodes; k++)
                        {
                            pool.Add(sorted_index[k]);
                        }

                        // Configure the edges
                        if (pool.Count >= deg[i])
                        {
                            for (int k = 0; k < deg[i]; k++)
                            {
                                int loop = 0;
                                bool found = false;
                                while (pool.Count > 0)
                                {
                                    int vtemp;
                                    int _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                                    int temp_col = sorted_index.FindIndex(x => x == _col);
                                    if (Math.Min(vmax[_row], vmax[_col]) > 0 && deg[temp_col] > 0)
                                    {
                                        vtemp = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                        m[_col, _row] = m[_row, _col] = vtemp;
                                        deg[temp_col]--;
                                        // edge.Add(_col);
                                        pool.Remove(_col);
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        // nonedges.Add(_col);
                                        pool.Remove(_col);
                                    }
                                    loop++;
                                }
                                if (found == false)
                                {
                                    //throw new Exception("Failed to find a valied configuration within the limit of loops.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate a valid configuration with the input degrees");
                        }
                    }
                }
            }

            return m;
        }

        // Load Configuration Model 
        public static Dictionary<string, List<Matrix>> LoadConfigModel (int n, bool directed, bool sign, bool selfties, MatrixTable networkSpec_data)
        {
            Dictionary<string, List<Matrix>> mRandTable = new Dictionary<string, List<Matrix>>();
            foreach (KeyValuePair<string, Matrix> kvp in networkSpec_data)
            {
                string net_ID = kvp.Key;
                Matrix modelSpec = kvp.Value;               
                mRandTable.Add(net_ID, new List<Matrix>());
                for (int i = 0; i < n; i++)
                {   if (directed)
                    {
                        mRandTable[net_ID].Add(GenerateDirectedConfigModel(sign, selfties, modelSpec));
                    }
                    else
                    {
                        mRandTable[net_ID].Add(GenerateUndirectedConfigModel_Revised(sign, selfties, modelSpec));
                    }
                }
            }

            return mRandTable;
        }
        //
    }
}
