using System;
using System.Collections.Generic;
using System.Text;
using RandomUtilities;
using System.Windows.Forms;
using System.Linq;
using NetworkGUI.Forms;
using NetworkGUI;

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
            // Console.WriteLine("Edges: " + edges.ToString());
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
                        throw new Exception("Input edges are not both even!");
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
                            if (_row != _col && pool.Count > 1 & edges > 1)
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
                            if (pool.Count > 1 & edges > 1)
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
                            if (_row != _col && pool.Count > 1 && posedges > 1)
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
                            if (pool.Count > 1 && posedges > 1)
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
                            if (_row != _col && pool.Count > 1 && negedges > 1)
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
                            if (_row != _col && pool.Count > 1 && negedges > 1)
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

        // Generate Directed Global Randomization with input edge value being the sum of random network edges
        public static Matrix GenerateDirectedGlobal(bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes;
            int vmax, vmin;
            int posedges, negedges, edges;
            vmin = (int)modelSpec.GetColVector(modelSpec.ColLabels["Min"])[0];
            vmax = (int)modelSpec.GetColVector(modelSpec.ColLabels["Max"])[0];
            nodes = (int)modelSpec[0, 0];
            
            Matrix m = new Matrix(nodes, nodes);
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            int draw;
            int _row, _col;
            List<int> pool = null;

            while (true)
            {
                m.Clear();
                pool = new List<int>();
                // Create a pool containing all available edges
                for (int i = 0; i < nodes; i++)
                {
                    for (int j = 0; j < nodes; j++)
                    {
                        if (selfties || i != j)
                        {
                            pool.Add(i * nodes + j);
                        }
                    }
                }

                if (sign)
                {
                    posedges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Pos. Edges"])[0]; ;
                    negedges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Neg. Edges"])[0]; ;
                    while (pool.Count > 0 && (posedges + negedges) > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (posedges >= negedges && posedges > 0)
                        {
                            int v; // Random value of the selected edge
                            v = (int)RNG.RandomInt(1, Math.Min(posedges, vmax));
                            m[_row, _col] = v;
                            posedges -= v;
                        }
                        else if (negedges > posedges && negedges > 0)
                        {
                            int v; //  Random value of the selected edge
                            v = (-1) * (int)RNG.RandomInt(1, Math.Min(negedges, Math.Abs(vmin)));
                            m[_row, _col] = v;
                            negedges += v;
                        }
                        pool.Remove(draw);
                    }
                    if ((posedges + negedges) == 0) return m;
                }
                else
                {
                    edges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Edges"])[0]; ;
                    while (pool.Count > 0 && edges > 0)
                    {
                        int v; //  Random value of the selected edge
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        v = (int)RNG.RandomInt(1, Math.Min(edges, vmax));
                        m[_row, _col] = v;
                        edges -= v;
                        pool.Remove(draw);
                    }
                    if (edges == 0) return m;
                }
            }
        }

        // Generate Undirected Global Randomization with input edge value being the sum of random network edges
        public static Matrix GenerateUndirectedGlobal(bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes;
            int vmin, vmax;
            vmin = (int)modelSpec.GetColVector(modelSpec.ColLabels["Min"])[0];
            vmax = (int)modelSpec.GetColVector(modelSpec.ColLabels["Max"])[0];
            nodes = (int)modelSpec.GetColVector(modelSpec.ColLabels["Nodes"])[0]; ;

            Matrix m = new Matrix(nodes, nodes, modelSpec.NetworkIdStr);
            Algorithms.Iota(m.ColLabels, 1);
            Algorithms.Iota(m.RowLabels, 1);

            int posedges, negedges, edges;
            int draw;
            int _row, _col;
            List<int> pool = null;

            while (true)
            {
                m.Clear();
                pool = new List<int>();

                // Create a pool containing all available edges
                for (int i = 0; i < nodes; i++)
                {
                    for (int j = 0; j < nodes; j++)
                    {
                        if (selfties || i != j)
                        {
                            pool.Add(i * nodes + j);
                        }
                    }
                }

                if (sign)
                {
                    posedges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Pos. Edges"])[0]; ;
                    negedges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Neg. Edges"])[0]; ;
                    
                    //Validate the input edges
                    if (!selfties)
                    {
                        if ((posedges % 2 != 0) || (negedges % 2 != 0))
                            throw new Exception("Input postive edges and negative edges are not both even!");
                    }

                    while (pool.Count > 0 && (posedges + negedges) > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (_row != _col && pool.Exists(x => x == (_col * nodes + _row)))
                        {
                            if (posedges >= negedges && posedges > 1)
                            {
                                int v; // Random value of the selected edge
                                int edg_lim = posedges / 2;
                                v = (int)RNG.RandomInt(1, Math.Min(edg_lim, vmax));
                                m[_col, _row] = m[_row, _col] = v;
                                posedges -= 2 * v;
                            }
                            else if (negedges > posedges && negedges > 1)
                            {
                                int v; // Random value of the selected edge
                                int edg_lim = negedges / 2;
                                v = (-1) * (int)RNG.RandomInt(1, Math.Min(edg_lim, Math.Abs(vmin)));
                                m[_col, _row] = m[_row, _col] = v;
                                negedges += 2 * v;
                            }
                            pool.Remove(draw);
                            pool.Remove(_col * nodes + _row);
                        }
                        else if (_row == _col)
                        {
                            if (posedges >= negedges && posedges > 0)
                            {
                                int v; // Random value of the selected edge
                                v = (int)RNG.RandomInt(1, Math.Min(posedges, vmax));
                                m[_row, _col] = v;
                                posedges -= v;
                            }
                            else if (negedges > posedges && negedges > 0)
                            {
                                int v; // Random value of the selected edge
                                v = (-1) * (int)RNG.RandomInt(1, Math.Min(negedges, Math.Abs(vmin)));
                                m[_row, _col] = v;
                                negedges += v;
                            }
                            pool.Remove(draw);
                        }
                        else
                        {
                            pool.Remove(draw);
                        }
                    }
                    if (posedges + negedges == 0)
                    {
                        return m;
                    }
                }
                else
                {
                    edges = (int)modelSpec.GetColVector(modelSpec.ColLabels["Edges"])[0]; ;

                    // Validate the input edges
                    if (!selfties)
                    {
                        if (edges % 2 != 0)
                            throw new Exception("Input edges are not both even!");
                    }

                    while (pool.Count > 0 && edges > 0)
                    {
                        draw = pool[RNG.RandomInt(pool.Count - 1)];
                        _row = draw / nodes;
                        _col = draw % nodes;
                        if (_row != _col && pool.Exists(x => x == (_col * nodes + _row)))
                        {
                            if (edges > 1)
                            {
                                int v; // Random value of the selected edge
                                int edg_lim = edges / 2;
                                v = (int)RNG.RandomInt(1, Math.Min(edg_lim, vmax));
                                m[_col, _row] = m[_row, _col] = v;
                                edges -= 2 * v;
                            }
                            pool.Remove(draw);
                            pool.Remove(_col * nodes + _row);
                        }
                        else if (_row == _col)
                        {
                            if (edges > 0)
                            {
                                int v; // Random value of the selected edge
                                v = (int)RNG.RandomInt(1, Math.Min(edges, vmax));
                                m[_row, _col] = v;
                                edges -= v;
                            }
                            pool.Remove(draw);
                        }
                        else
                        {
                            pool.Remove(draw);
                        }
                    }
                    if (edges == 0)
                    {
                        return m;
                    }
                }

            }
        }

        // Load Global Randomization
        public static Dictionary<string, List<Matrix>> LoadGlobalRandom (int n, bool directed, bool sign, bool selfties, MatrixTable networkSpec_data)
        {
            Dictionary<string, List<Matrix>> mRandTable = new Dictionary<string, List<Matrix>>();
            int numNet = networkSpec_data.Count;
            string net_id;
            //int nodes;
            //int edges;
            //int pos_edges;
            //int neg_edges;
            //int vmin;
            //int vmax;
            int i, j;

            foreach (KeyValuePair<string, Matrix> kvp in networkSpec_data)
            {
                net_id = kvp.Key;
                //nodes = networkSpec[i]["Nodes"];
                //pos_edges = networkSpec[i]["Pos. Edges"];
                //neg_edges = networkSpec[i]["Neg. Edges"];
                //vmin = networkSpec[i]["Min Value"];
                //vmax = networkSpec[i]["Max Value"];
                // Console.WriteLine("SelfTies:" + selfties.ToString());
                List<Matrix> mlist = new List<Matrix>();

                for (j = 0; j < n; j++)
                {
                    if (directed)
                        mlist.Add(GenerateDirectedGlobal(sign, selfties, kvp.Value));
                    else
                        mlist.Add(GenerateUndirectedGlobal(sign, selfties, kvp.Value));
                }

                mRandTable.Add(net_id, mlist);
            }
            //nodes = networkSpec[i]["Nodes"];
            //edges = networkSpec[i]["Edges"];
            //vmin = networkSpec[i]["Min Value"];
            //vmax = networkSpec[i]["Max Value"];
            // Console.WriteLine("net_id: " + net_id + " " + "nodes: " + nodes.ToString());
            // Matrix temp = GenerateGlobal(directed, sign, selfties, nodes, edges, vmax);
            // Console.WriteLine("Matrix R: " + temp.Rows.ToString() + " " + "Matrix C: " +temp.Cols.ToString());                                                   
            return mRandTable;
        }

        

        // Generate Directed Configuration Models
        public static Matrix GenerateDirectedConfigModel_2 (bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes = modelSpec.Rows;
            int i, j;
            Vector vmin, vmax;
            Vector deg, pos_deg, neg_deg;
            Matrix m = new Matrix(nodes, nodes);
            m.Clear();
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
                    for (j = 0; j < nodes; j++)
                    {
                        if (!selfties || i != j)
                            pool.Add(j);
                    }
                    if ((pos_deg[i] + neg_deg[i]) > pool.Count)
                        throw new Exception("The sum of positive and negative edges should not exceed the number of available ties.");
                    // Configure the positive edges
                    for (int k = 0; k < pos_deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        m[i, _col] = (int)RNG.RandomInt(1, vmax[i]);
                        // pos_deg[i]--;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }

                    // Configure the negative edges
                    for (int k = 0; k < neg_deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        m[i, _col] = (-1) * (int)RNG.RandomInt(1, Math.Abs(vmin[i]));
                        // neg_deg[_col]--;
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
                    for (j = 0; j < nodes; j++)
                    {
                        if (!selfties || i != j)
                            pool.Add(j);
                    }
                    if (deg[i] > pool.Count)
                        throw new Exception("The number of edges should not exceed that of available ties!");
                    // Configure the positive edges
                    for (int k = 0; k < deg[i]; k++)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                        m[i, _col] = (int)RNG.RandomInt(1, vmax[i]);
                        // deg[_col]--;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }
                }
            }
          return m;
        }

        // Generate Undirected Configuration Models (Version 1.0)
        public static Matrix GenerateUndirectedConfigModel_2 (bool sign, bool selfties, Matrix modelSpec)
        {
            int nodes = modelSpec.Rows;
            int i, j;
            // const int UPPER_BOUND = 200;
            Vector vmin, vmax;
            Vector orig_deg, orig_pos_deg, orig_neg_deg;
            Vector deg, pos_deg, neg_deg;
            List<List<int>> col_pool = new List<List<int>>();
            // List<List<int>> pos_col_pool = new List<List<int>>();
            // List<List<int>> neg_col_pool = new List<List<int>>();
            List<int> row_pool = new List<int>();
            // List<int> pos_row_pool = new List<int>();
            // List<int> neg_row_pool = new List<int>();
            Matrix m = new Matrix(nodes);


            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);
               

            if (sign)
            {
                orig_pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                orig_neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                if (orig_pos_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the positive degrees is not even!");
                }
                if (orig_neg_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the negative degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    pos_deg = new Vector(orig_pos_deg);
                    neg_deg = new Vector(orig_neg_deg);
                    deg = pos_deg + neg_deg;
                    m.Clear();
                    col_pool = new List<List<int>>();
                    // pos_col_pool = new List<List<int>>();
                    // neg_col_pool = new List<List<int>>();
                    row_pool = new List<int>();
                    
                    // Create row_pools and col_pools
                    for (i = 0; i < nodes; i++)
                    {
                        col_pool.Add(new List<int>());
                        // neg_col_pool.Add(new List<int>());
                        if (deg[i] > 0)
                        {
                            row_pool.Add(i);
                            //if (pos_deg[i] > 0)
                            //{
                            //    pos_row_pool.Add(i);
                            //}
                            //if (neg_deg[i] > 0)
                            //{
                            //    neg_row_pool.Add(i);
                            //}
                            for (j = 0; j < nodes; j++)
                            {
                                if (selfties || i != j)
                                    col_pool[i].Add(j);
                            }
                        }
                    }
                    int _row, _col;
                    while (row_pool.Count > 0 && deg.sum() > 0)
                    {
                        //_row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        int temp = 0;
                        int maxIndex = 0;
                        for (int k = 0; k < nodes; k++)
                        {
                            if (deg[k] > temp && row_pool.Exists(x => x == k))
                            {
                                temp = (int)deg[k];
                                maxIndex = k;
                            }
                        }
                        _row = maxIndex;
                        
                        while (col_pool[_row].Count > 0)
                        {
                            bool found = false;
                            _col = col_pool[_row][RNG.RandomInt(col_pool[_row].Count - 1)];
                            if (pos_deg[_row] >= neg_deg[_row] && pos_deg[_row] > 0)
                            {
                                if (Math.Min(vmax[_row], vmax[_col]) > 0 && pos_deg[_col] > 0)
                                {
                                    m[_row, _col] = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                    m[_col, _row] = m[_row, _col];
                                    col_pool[_row].Remove(_col);
                                    pos_deg[_row]--;
                                    deg[_row]--;

                                    if (_col != _row)
                                    {
                                        col_pool[_col].Remove(_row);
                                        pos_deg[_col]--;
                                        deg[_col]--;
                                        if (deg[_col] == 0)
                                            row_pool.Remove(_col);
                                    }
                                    found = true;
                                    break;
                                }
                            }
                            else if (neg_deg[_row] >= neg_deg[_row] && neg_deg[_row] > 0)
                            {
                                if (Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])) > 0 && neg_deg[_col] > 0)
                                {
                                    m[_row, _col] = (-1) * (int)RNG.RandomInt(1, Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])));
                                    m[_col, _row] = m[_row, _col];
                                    col_pool[_row].Remove(_col);
                                    neg_deg[_row]--;
                                    deg[_row]--;

                                    if (_col != _row)
                                    {
                                        col_pool[_col].Remove(_row);
                                        neg_deg[_col]--;
                                        deg[_col]--;
                                        if (deg[_col] == 0)
                                            row_pool.Remove(_col);
                                    }
                                    found = true;
                                    break;
                                }                               
                            }
                            if (found == false)
                            {
                                col_pool[_row].Remove(_col);
                            }
                        }
                        if (deg[_row] == 0 || col_pool[_row].Count == 0)
                        {
                            row_pool.Remove(_row);
                        }
                    }
                    if (deg.sum() == 0)
                    {
                        return m;
                    }
                    loop++;
                }
            }
            else
            {
                orig_deg = modelSpec.GetColVector(modelSpec.ColLabels["Degree"]);
                if (orig_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    deg = new Vector(orig_deg);
                    col_pool = new List<List<int>>();
                    row_pool = new List<int>();
                    m.Clear();
                    // Create row_pools and col_pools
                    for (i = 0; i < nodes; i++)
                    {
                        col_pool.Add(new List<int>());
                        // neg_col_pool.Add(new List<int>());
                        if (deg[i] > 0)
                        {
                            row_pool.Add(i);
                            for (j = 0; j < nodes; j++)
                            {
                                if (selfties || i != j)
                                    col_pool[i].Add(j);
                            }
                        }
                    }
                    int _row, _col;
                    while (row_pool.Count > 0 && deg.sum() > 0)
                    {
                        // _row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        int temp = 0;
                        int maxIndex = 0;
                        for (int k = 0; k < nodes; k++)
                        {
                            if (deg[k] > temp && row_pool.Exists(x => x == k))
                            {
                                temp = (int)deg[k];
                                maxIndex = k;
                            }
                        }
                        _row = maxIndex;
                        // bool found = false;
                        while (col_pool[_row].Count > 0)
                        {
                            _col = col_pool[_row][RNG.RandomInt(col_pool[_row].Count - 1)];

                            if (Math.Min(vmax[_row], vmax[_col]) > 0 && deg[_col] > 0)
                            {
                                m[_row, _col] = (int)RNG.RandomInt(1, Math.Min(vmax[_row], vmax[_col]));
                                m[_col, _row] = m[_row, _col];
                                col_pool[_row].Remove(_col);
                                deg[_row]--;

                                if (_col != _row)
                                {
                                    col_pool[_col].Remove(_row);
                                    deg[_col]--;
                                    if (deg[_col] == 0)
                                        row_pool.Remove(_col);
                                }
                                // found = true;
                                break;
                            }
                            else
                            {
                                col_pool[_row].Remove(_col);
                            }
                        }
                        if (deg[_row] == 0 || col_pool[_row].Count == 0)
                        {
                            row_pool.Remove(_row);
                        }
                    }
                    if (deg.sum() == 0)
                    {
                        // throw new Exception("Failed to find a valid configuration with the input degrees!");
                        return m;
                    }
                    loop++;
                }
            }
            // return m;
        }

        // Generate Directed Configuration Models
        public static Matrix GenerateDirectedConfigModel(bool sign, bool selfties, Matrix modelSpec, string netId)
        {
            int nodes = modelSpec.Rows;
            int i, j;
            Vector vmin, vmax;
            Vector deg, pos_deg, neg_deg;
            Matrix m = new Matrix(nodes, nodes, netId);
            m.Clear();
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
                    for (j = 0; j < nodes; j++)
                    {
                        if (!selfties || i != j)
                            pool.Add(j);
                    }

                    // Configure the positive edges
                    int v;
                    while(pos_deg[i] > 0)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];

                        while (true)
                        {
                            v = (int)RNG.RandomInt(1, Math.Min(pos_deg[i], vmax[i]));
                            if ((pos_deg[i] - v) < vmax[i] * (pool.Count - 1))
                                break;
                        }
                        m[i, _col] = v;
                        pos_deg[i] -= v;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }

                    // Configure the negative edges
                    while(neg_deg[i] > 0)
                    {
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                        while (true)
                        {
                            v = (-1) * (int)RNG.RandomInt(1, Math.Min(neg_deg[i], Math.Abs(vmin[i])));
                            if ((neg_deg[i] + v) < Math.Abs(vmin[i]) * (pool.Count - 1))
                                break;
                        }
                        m[i, _col] = v;
                        neg_deg[i] += v;
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
                    for (j = 0; j < nodes; j++)
                    {
                        if (!selfties || i != j)
                            pool.Add(j);
                    }
                    //if (deg[i] > pool.Count)
                    //    throw new Exception("The number of edges should not exceed that of available ties!");
                    // Configure the positive edges
                    while(deg[i] > 0 && pool.Count > 0)
                    {
                        int v;
                        int _col;
                        _col = pool[(int)RNG.RandomInt(pool.Count - 1)];
                        while (true)
                        {
                            v = (int)RNG.RandomInt(1, Math.Min(deg[i], vmax[i]));
                            if ((deg[i] - v) < vmax[i] * (pool.Count - 1))
                                break;
                        }
                        m[i, _col] = v;
                        deg[i] -= v ;
                        // edge.Add(_col);
                        pool.Remove(_col);
                    }
                }
            }
            return m;
        }

        // Generate Undirected Configuration Models (Version 1.0)
        public static Matrix GenerateUndirectedConfigModel(bool sign, bool selfties, Matrix modelSpec, string netId)
        {
            int nodes = modelSpec.Rows;
            int i, j;
            // const int UPPER_BOUND = 200;
            Vector vmin, vmax;
            Vector orig_deg, orig_pos_deg, orig_neg_deg;
            Vector deg, pos_deg, neg_deg;
            
            List<List<int>> edge_range_pos = null;
            List<List<int>> edge_range_neg = null;
            List<List<int>> edge_range = null;

            List<List<int>> col_pool = null;
            List<List<int>> pos_col_pool = null;
            List<List<int>> neg_col_pool = null;

            List<int> row_pool = null;
            // List<int> pos_row_pool = null;
            // List<int> neg_row_pool = null;
            Matrix m = new Matrix(nodes, nodes, netId);


            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);


            if (sign)
            {
                orig_pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                orig_neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                if (!selfties && orig_pos_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the positive degrees is not even!");
                }
                if (!selfties && orig_neg_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the negative degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    pos_deg = new Vector(orig_pos_deg);
                    neg_deg = new Vector(orig_neg_deg);
                    deg = pos_deg + neg_deg;
                    m.Clear();

                    edge_range_pos = new List<List<int>>();
                    edge_range_neg = new List<List<int>>();
                    pos_col_pool = new List<List<int>>();
                    neg_col_pool = new List<List<int>>();
                    row_pool = new List<int>();
                    // pos_row_pool = new List<int>();
                    // neg_row_pool = new List<int>();

                    // Create row_pools and col_pools
                    for (i = 0; i < nodes; i++)
                    {
                        pos_col_pool.Add(new List<int>());
                        neg_col_pool.Add(new List<int>());
                        edge_range_pos.Add(new List<int>());
                        edge_range_neg.Add(new List<int>());
                        if (deg[i] > 0)
                        {
                            row_pool.Add(i);
                            for (j = 0; j < nodes; j++)
                            {
                                if (pos_deg[i] * pos_deg[j] > 0 && (selfties || i != j))
                                    pos_col_pool[i].Add(j);
                                if (neg_deg[i] * neg_deg[j] > 0 && (selfties || i != j))
                                    neg_col_pool[i].Add(j);
                                edge_range_pos[i].Add((int)Math.Min(vmax[i], vmax[j]));
                                edge_range_neg[i].Add((int)Math.Min(Math.Abs(vmin[i]), Math.Abs(vmin[j])));
                            }
                        }
                    }
                    int _row, _col;
                    while (row_pool.Count > 0 && deg.sum() > 0)
                    {
                        //_row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        int temp = 0;
                        int maxIndex = -1;
                        for (int k = 0; k < nodes; k++)
                        {
                            if (deg[k] > temp && row_pool.Exists(x => x == k))
                            {
                                temp = (int)deg[k];
                                maxIndex = k;
                            }
                        }
                        if (maxIndex == -1)
                        {
                            break;
                        }
                        double p = RNG.RandomFloat();
                        //if (p < 0.9)
                        //    _row = maxIndex;
                        //else
                        //    _row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        _row = maxIndex;

                        bool found = false;

                        List<int> temp_pos_pool = new List<int>(pos_col_pool[_row]);
                        List<int> temp_neg_pool = new List<int>(neg_col_pool[_row]);
                        //if (Math.Abs(pos_deg[_row] - edge_range_pos[_row].Sum()) <= Math.Abs(neg_deg[_row] - edge_range_neg[_row].Sum()) && pos_deg[_row] > 0)
                        if (pos_deg[_row] >= neg_deg[_row])
                        {
                            while (pos_col_pool[_row].Count > 0 && pos_deg[_row] > 0 && temp_pos_pool.Count > 0)
                            {
                                int v;

                                _col = temp_pos_pool[RNG.RandomInt(temp_pos_pool.Count - 1)];


                                if (edge_range_pos[_row][_col] > 0 && pos_deg[_col] > 0)
                                {
                                    int edge_lim_1 = (_col == _row) ? (int)pos_deg[_row] : (int)Math.Min(pos_deg[_row], pos_deg[_col]); // Upper bound by edge sum
                                    int edge_lim_2 = edge_range_pos[_row][_col]; // Upper bound by maximum value

                                    int lim = (int)Math.Min(edge_lim_1, edge_lim_2);

                                    if ((pos_deg[_row] - lim) > (edge_range_pos[_row].Sum() - edge_range_pos[_row][_col]) || (pos_deg[_col] - lim) > (edge_range_pos[_col].Sum() - edge_range_pos[_col][_row]) || (pos_deg[_row] - lim - neg_deg[_row]) < (-1) * edge_range_neg[_row].Sum())
                                    {
                                        temp_pos_pool.Remove(_col);
                                        continue;
                                    }

                                    while (true)
                                    {
                                        v = (int)RNG.RandomInt(1, Math.Min(edge_lim_1, edge_lim_2));
                                        if (((pos_deg[_row] - v) <= (edge_range_pos[_row].Sum() - edge_range_pos[_row][_col])) && ((pos_deg[_col] - v) <= (edge_range_pos[_col].Sum() - edge_range_pos[_col][_row])) && ((pos_deg[_row] - v - neg_deg[_row]) >= (-1) * edge_range_neg[_row].Sum()))
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    m[_col, _row] = m[_row, _col] = v;

                                    if (_col != _row)
                                    {
                                        pos_col_pool[_col].Remove(_row);
                                        neg_col_pool[_col].Remove(_row);
                                        pos_deg[_col] -= v;
                                        deg[_col] -= v;
                                        if (deg[_col] == 0)
                                            row_pool.Remove(_col);
                                    }

                                    pos_col_pool[_row].Remove(_col);
                                    temp_pos_pool.Remove(_col);
                                    neg_col_pool[_row].Remove(_col);
                                    temp_neg_pool.Remove(_col);
                                    pos_deg[_row] -= v;
                                    deg[_row] -= v;

                                    edge_range_pos[_row][_col] = 0;
                                    edge_range_pos[_col][_row] = 0;
                                    edge_range_neg[_row][_col] = 0;
                                    edge_range_neg[_col][_row] = 0;

                                    found = true;
                                    break;
                                }
                                else
                                {
                                    pos_col_pool[_row].Remove(_col);
                                    temp_pos_pool.Remove(_col);
                                    edge_range_pos[_row][_col] = 0;
                                    edge_range_pos[_col][_row] = 0;

                                }
                            }
                            if ((pos_deg[_row] == 0 || pos_col_pool[_row].Count == 0 || found == false) && (pos_deg[_row] >= neg_deg[_row]))
                            {
                                row_pool.Remove(_row);
                            }
                        }

                        else if (pos_deg[_row] < neg_deg[_row])
                        {
                            while (neg_col_pool[_row].Count > 0 && neg_deg[_row] > 0 && temp_neg_pool.Count > 0)
                            {
                                int v;
                                _col = temp_neg_pool[RNG.RandomInt(temp_neg_pool.Count - 1)];
                                if (edge_range_neg[_row][_col] > 0 && neg_deg[_col] > 0)
                                {
                                    int edge_lim_1 = (_col == _row) ? (int)neg_deg[_row] : (int)Math.Min(neg_deg[_row], neg_deg[_col]); // Upper bound by edge sum
                                    int edge_lim_2 = edge_range_neg[_row][_col]; // Upper bound by maximum value

                                    int lim = (int)Math.Min(edge_lim_1, edge_lim_2);

                                    if ((neg_deg[_row] - lim) > (edge_range_neg[_row].Sum() - edge_range_neg[_row][_col]) || (neg_deg[_col] - lim) > (edge_range_neg[_col].Sum() - edge_range_neg[_col][_row]) || ((-1) * neg_deg[_row] + lim + pos_deg[_row]) > edge_range_pos[_row].Sum())
                                    {
                                        temp_neg_pool.Remove(_col);
                                        // if (_col != _row) neg_col_pool[_col].Remove(_row);
                                        continue;
                                    }

                                    while (true)
                                    {
                                        v = (int)RNG.RandomInt(1, lim);
                                        if (((neg_deg[_row] - v) <= (edge_range_neg[_row].Sum() - edge_range_neg[_row][_col])) && ((neg_deg[_col] - v) <= (edge_range_neg[_col].Sum() - edge_range_neg[_col][_row])) && (((-1) * neg_deg[_row] + v + pos_deg[_row]) <= edge_range_pos[_row].Sum()))
                                        {
                                            found = true;
                                            break;

                                        }
                                    }

                                    v = (-1) * v;
                                    m[_col, _row] = m[_row, _col] = v;

                                    if (_col != _row)
                                    {
                                        pos_col_pool[_col].Remove(_row);
                                        neg_col_pool[_col].Remove(_row);
                                        neg_deg[_col] += v;
                                        deg[_col] += v;
                                        if (deg[_col] == 0)
                                            row_pool.Remove(_col);
                                    }

                                    pos_col_pool[_row].Remove(_col);
                                    temp_pos_pool.Remove(_col);
                                    neg_col_pool[_row].Remove(_col);
                                    temp_neg_pool.Remove(_col);

                                    neg_deg[_row] += v;
                                    deg[_row] += v;

                                    edge_range_pos[_row][_col] = 0;
                                    edge_range_pos[_col][_row] = 0;
                                    edge_range_neg[_row][_col] = 0;
                                    edge_range_neg[_col][_row] = 0;

                                    found = true;
                                    break;
                                }
                                else
                                {
                                    neg_col_pool[_row].Remove(_col);
                                    temp_neg_pool.Remove(_col);
                                    edge_range_neg[_row][_col] = 0;
                                    edge_range_neg[_col][_row] = 0;
                                }
                            }
                            if ((neg_deg[_row] == 0 || neg_col_pool[_row].Count == 0 || found == false) && (neg_deg[_row] >= pos_deg[_row]))
                            {
                                row_pool.Remove(_row);
                            }

                        }
                    }
                    if (deg.sum() == 0)
                    {
                        // Console.WriteLine("The number of loop is " + loop.ToString());
                        return m;
                    }
                    loop++;
                }
            }
            else
            {
                orig_deg = modelSpec.GetColVector(modelSpec.ColLabels["Degree"]);
                if (!selfties && orig_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    deg = new Vector(orig_deg);
                    //if (loop == 0)
                    //{
                    //    for (int k = 0; k < nodes; k++)
                    //    {
                    //        Console.WriteLine("Expected Edges: " + deg[k].ToString());
                    //    }
                    //}
                    col_pool = new List<List<int>>();
                    row_pool = new List<int>();
                    edge_range = new List<List<int>>();
                    m.Clear();
                    // Create row_pools and col_pools
                    for (i = 0; i < nodes; i++)
                    {
                        col_pool.Add(new List<int>());
                        edge_range.Add(new List<int>());
                        // neg_col_pool.Add(new List<int>());
                        if (deg[i] > 0)
                        {
                            row_pool.Add(i);
                            for (j = 0; j < nodes; j++)
                            {
                                edge_range[i].Add((int)Math.Min(vmax[i], vmax[j]));
                                if (deg[j] > 0 && edge_range[i][j] > 0 && (selfties || i != j))
                                    col_pool[i].Add(j);
                            }
                            if (edge_range[i].Sum() < deg[i])
                                throw new Exception("The input degrees can not be reached!");
                        }
                    }
                    int _row, _col;
                    while (row_pool.Count > 0 && deg.sum() > 0)
                    {
                        // _row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        int temp = 0;
                        int maxIndex = -1;
                        for (int k = 0; k < nodes; k++)
                        {
                            if (deg[k] > temp && row_pool.Exists(x => x == k))
                            {
                                temp = (int)deg[k];
                                maxIndex = k;
                            }
                        }

                        if(maxIndex == -1)
                        {
                            break;
                        }
                        // To Do: A better distribution
                        double p = RNG.RandomFloat();
                        //if (p < 0.9)
                        //    _row = maxIndex;
                        //else
                        //    _row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        _row = maxIndex;

                        List<int> temp_pool = new List<int>(col_pool[_row]);
                        List<int> temp_edge_range = new List<int>(edge_range[_row]);
                        bool found = false;
                        while (col_pool[_row].Count > 0 && temp_pool.Count > 0)
                        {
                            // _col = col_pool[_row][RNG.RandomInt(col_pool[_row].Count - 1)];
                            _col = temp_pool[RNG.RandomInt(temp_pool.Count - 1)];

                            if (edge_range[_row][_col] > 0 && deg[_col] > 0)
                            {                               
                                int edge_lim_1 = (_col == _row) ? (int)deg[_row] : (int)Math.Min(deg[_row], deg[_col]);
                                int edge_lim_2 = edge_range[_row][_col];
                                int lim_3 = (int)Math.Min(edge_lim_1, edge_lim_2);
                                if (((deg[_row] - lim_3) > (edge_range[_row].Sum() - edge_range[_row][_col])) || ((deg[_col] - lim_3) > (edge_range[_col].Sum() - edge_range[_row][_col])))
                                {
                                    temp_pool.Remove(_col);
                                    temp_edge_range[_col] = 0;
                                    // edge_range[_row][_col] = 0;
                                    //if (_row != _col)
                                    //{
                                    //    col_pool[_col].Remove(_row);
                                    //    edge_range[_col][_row] = 0;
                                    //}
                                    continue;
                                }

                                int v = 0;
                                while (true)
                                {
                                    v = (int)RNG.RandomInt(1, lim_3);
                                    if (((deg[_row] - v) <= (edge_range[_row].Sum() - edge_range[_row][_col])) || ((deg[_col] - v) <= (edge_range[_col].Sum() - edge_range[_row][_col])))
                                    {
                                        found = true;
                                        break;
                                    }
                                }

                                // v = (int)RNG.RandomInt(1, Math.Min(edge_lim_1, edge_lim_2));
                                m[_row, _col] = v;
                                col_pool[_row].Remove(_col);
                                temp_pool.Remove(_col);
                                deg[_row] -= v;
                                edge_range[_row][_col] = 0;

                                if (_col != _row)
                                {
                                    m[_col, _row] = v;
                                    col_pool[_col].Remove(_row);
                                    deg[_col] -= v;
                                    edge_range[_col][_row] = 0;
                                    if (deg[_col] == 0 || col_pool[_col].Count == 0)
                                        row_pool.Remove(_col);

                                }
                                // found = true;
                                break;
                            }
                            else if (deg[_col] == 0)
                            {
                                col_pool[_row].Remove(_col);
                                edge_range[_row][_col] = 0;
                                edge_range[_col][_row] = 0;
                                temp_pool.Remove(_col);
                            }
                        }
                        if (deg[_row] == 0 || col_pool[_row].Count == 0)
                        {
                            row_pool.Remove(_row);
                        }
                    }
                    if (deg.sum() == 0)
                    {
                        // throw new Exception("Failed to find a valid configuration with the input degrees!");
                        // Console.WriteLine("The numer of loop is " + loop.ToString());
                        return m;
                    }
                    loop++;
                }
            }
            // return m;
        }

        // Generate Undirected Configuration Models (Version 2.0)
        public static Matrix GenerateUndirectedConfigModel_2(bool sign, bool selfties, Matrix modelSpec, string netId)

        {
            int nodes = modelSpec.Rows;
            int i, j;
            // const int UPPER_BOUND = 200;
            Vector vmin, vmax;
            Vector orig_deg, orig_pos_deg, orig_neg_deg;
            Vector deg, pos_deg, neg_deg;
            List<int> pool = new List<int>();
            // List<int> pos_row_pool = new List<int>();
            // List<int> neg_row_pool = new List<int>();
            Matrix m = new Matrix(nodes, nodes, netId);
            double[,] p = new double[nodes, nodes]; // Probability Graph --> p[i, j] = deg[i] * deg[j];

            vmin = modelSpec.GetColVector(modelSpec.ColLabels["Min"]);
            vmax = modelSpec.GetColVector(modelSpec.ColLabels["Max"]);


            if (sign)
            {
                orig_pos_deg = modelSpec.GetColVector(modelSpec.ColLabels["Pos. Degree"]);
                orig_neg_deg = modelSpec.GetColVector(modelSpec.ColLabels["Neg. Degree"]);
                if (!selfties && orig_pos_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the positive degrees is not even!");
                }
                if (!selfties && orig_neg_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the negative degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    pos_deg = new Vector(orig_pos_deg);
                    neg_deg = new Vector(orig_neg_deg);
                    deg = pos_deg + neg_deg;
                    m.Clear();

                    for (i = 0; i < nodes; i++)
                    {
                        for (j = i; j < nodes; j++)
                        {
                            if (deg[i] > 0 && deg[j] > 0 && (selfties || i != j))
                            {
                                pool.Add(i * nodes + j);
                                p[j, i] = p[i, j] = pos_deg[i] * pos_deg[j] + neg_deg[i] * neg_deg[j];
                            }
                            else
                            {
                                p[j, i] = p[i, j] = 0;
                            }
                        }
                    }
                    
                    while (pool.Count > 0 && deg.sum() > 0)
                    {
                        //_row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        double max_prob = 0;
                        int _row = -1;
                        int _col = -1;
                        int v;
                        bool found = false;
                        for (int k = 0; k < nodes; k++)
                        {
                            for (int h = k; h < nodes; h++) {
                                if (p[k, h] > max_prob && pool.Exists(x => x == (k * nodes + h)) && pool.Exists(x => x == (h * nodes + k)))
                                {
                                    max_prob = p[k, h];
                                    _row = k;
                                    _col = h;
                                }
                            }
                        }
                        if (max_prob > 0)
                        {
                            if (pos_deg[_row] * pos_deg[_col] >= neg_deg[_row] * neg_deg[_col])
                            {
                                if (Math.Min(vmax[_row], vmax[_col]) > 0)
                                {
                                    int edge_lim_1 = (_col == _row) ? (int)pos_deg[_row] : (int)Math.Min(pos_deg[_row], pos_deg[_col]); // Upper bound by edge sum
                                    int edge_lim_2 = (_col == _row) ? (int)vmax[_row] : (int)Math.Min(vmax[_row], vmax[_col]); // Upper bound by maximum value

                                    v = (int)RNG.RandomInt(1, Math.Min(edge_lim_1, edge_lim_2));
                                    m[_col, _row] = m[_row, _col] = v;

                                    if (_col != _row)
                                    {
                                        pos_deg[_col] -= v;
                                        deg[_col] -= v;
                                    }

                                    pos_deg[_row] -= v;
                                    deg[_row] -= v;

                                    found = true;

                                }
                            }
                            else
                            {
                                if (Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])) > 0 && neg_deg[_col] > 0)
                                {
                                    int edge_lim_1 = (_col == _row) ? (int)neg_deg[_row] : (int)Math.Min(neg_deg[_row], neg_deg[_col]); // Upper bound by edge sum
                                    int edge_lim_2 = (_col == _row) ? (int)Math.Abs(vmin[_row]) : (int)Math.Min(Math.Abs(vmin[_row]), Math.Abs(vmin[_col])); // Upper bound by maximum value

                                    v = (-1) * (int)RNG.RandomInt(1, Math.Min(edge_lim_1, edge_lim_2));
                                    m[_col, _row] = m[_row, _col] = v;

                                    if (_col != _row)
                                    {
                                        neg_deg[_col] += v;
                                        deg[_col] += v;
                                    }

                                    neg_deg[_row] += v;
                                    deg[_row] += v;

                                    found = true;
                                }
                            }
                            

                            p[_row, _col] = p[_col, _row] = pos_deg[_row] * pos_deg[_col] + neg_deg[_row] * neg_deg[_col];
                        }
                        if (found == false || deg[_row] * deg[_col] == 0)
                        {
                            pool.Remove(_row * nodes + _col);
                            if (_row != _col) pool.Remove(_col * nodes + _row);
                        }

                    }
                    if (deg.sum() == 0)
                    {
                        // Console.WriteLine("The number of loop is " + loop.ToString());
                        return m;
                    }
                    loop++;
                }
            }
            else
            {
                orig_deg = modelSpec.GetColVector(modelSpec.ColLabels["Degree"]);
                if (!selfties && orig_deg.sum() % 2 != 0)
                {
                    throw new Exception("The sum of the degrees is not even!");
                }
                int loop = 0;
                while (true)
                {
                    deg = new Vector(orig_deg);
                    m.Clear();
                    // Create the pool
                    for (i = 0; i < nodes; i++)
                    {
                        for (j = i; j < nodes; j++)
                        {
                            if (deg[i]*deg[j] > 0 && (selfties || i != j))
                            {
                                pool.Add(i * nodes + j);
                                p[j, i] = p[i, j] = deg[i] * deg[j];
                            }
                            else
                            {
                                p[j, i] = p[i, j] = 0;
                            }
                        }
                    }

                    while (pool.Count > 0 && deg.sum() > 0)
                    {
                        //_row = row_pool[RNG.RandomInt(row_pool.Count - 1)];
                        double max_prob = 0;
                        int _row = -1;
                        int _col = -1;
                        int v;
                        bool found = false;
                        for (int k = 0; k < nodes; k++)
                        {
                            for (int h = k; h < nodes; h++)
                            {
                                if (p[k, h] > max_prob && pool.Exists(x => x == (k * nodes + h)) && pool.Exists(x => x == (h * nodes + k)))
                                {
                                    max_prob = p[k, h];
                                    _row = k;
                                    _col = h;
                                }
                            }
                        }
                        if (max_prob > 0)
                        {
                            if (Math.Min(vmax[_row], vmax[_col]) > 0)
                            {
                                int edge_lim_1 = (_col == _row) ? (int)deg[_row] : (int)Math.Min(deg[_row], deg[_col]); // Upper bound by edge sum
                                int edge_lim_2 = (_col == _row) ? (int)vmax[_row] : (int)Math.Min(vmax[_row], vmax[_col]); // Upper bound by maximum value

                                v = (int)RNG.RandomInt(1, Math.Min(edge_lim_1, edge_lim_2));
                                m[_col, _row] = m[_row, _col] = v;

                                if (_col != _row)
                                {
                                    deg[_col] -= v;
                                }
                                deg[_row] -= v;
                                found = true;
                            }
                        }

                        if (found == false || deg[_row] * deg[_col] == 0)
                        {
                            pool.Remove(_row * nodes + _col);
                            if (_row != _col)
                                pool.Remove(_col * nodes + _row);
                        }
                        p[_row, _col] = p[_col, _row] = deg[_row] * deg[_col];
                    }                   
                    if (deg.sum() == 0)
                    {
                        // throw new Exception("Failed to find a valid configuration with the input degrees!");
                        // Console.WriteLine("The numer of loop is " + loop.ToString());
                        return m;
                    }
                    loop++;
                }
            }
            // return m;
        }

        // Load Configuration Model 
        public static Dictionary<string, List<Matrix>> LoadConfigModel (int n, bool directed, bool sign, bool selfties, MatrixTable networkSpec_data)
        {
            Dictionary<string, List<Matrix>> mRandTable = new Dictionary<string, List<Matrix>>();
            foreach (KeyValuePair<string, Matrix> kvp in networkSpec_data)
            {
                string net_ID = kvp.Key;
                Console.WriteLine("net_ID: " + net_ID);
                Matrix modelSpec = kvp.Value;               
                mRandTable.Add(net_ID, new List<Matrix>());
                for (int i = 0; i < n; i++)
                {   if (directed)
                    {
                        mRandTable[net_ID].Add(GenerateDirectedConfigModel(sign, selfties, modelSpec, net_ID));
                    }
                    else
                    {
                        mRandTable[net_ID].Add(GenerateUndirectedConfigModel(sign, selfties, modelSpec, net_ID));
                    }
                }
            }

            return mRandTable;
        }
        //
    }
}
