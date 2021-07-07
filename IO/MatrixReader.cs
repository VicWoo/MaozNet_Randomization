using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Network.Matrices;
using System.Threading;
using System.Windows.Forms;

namespace Network.IO
{
    public sealed class MatrixReader
    {
        private MatrixReader() { }

        public static Matrix ReadMatrixFromFile(string filename)
        {
            return ReadMatrixFromFile(filename, 0, 0);
        }
        public static Matrix ReadMatrixFromFile(string filename, int networkId)
        {
            return ReadMatrixFromFile(filename, networkId, 0);
        }

        public static Matrix ReadMatrixFromFile(string filename, int networkId, int dyadicVariable)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            BufferedFileReader reader = BufferedFileTable.GetFile(filename);
            lock (reader)
            {
                switch (reader.FileType)
                {
                    case BufferedFileReader.Type.Matrix:
                        return ReadMatrixFromMatrixFile(reader, networkId);
                    case BufferedFileReader.Type.Dyadic:
                        return ReadMatrixFromDyadicFile(reader, networkId, dyadicVariable);
                    case BufferedFileReader.Type.Vector:
                        return ReadMatrixFromVectorFile(reader, networkId);
                    default:
                        throw new FileLoadException("Invalid file type.");
                }
            }
        }

        // Yushan
        // Read all matrixes into a matrix list
        public static List<Matrix> ReadMatrixListFromFile(string filename)
        {
            return ReadMatrixListFromFile(filename, 0);
        }
        public static List<Matrix> ReadMatrixListFromFile(string filename, int dyadicVariable)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            BufferedFileReader reader = BufferedFileTable.GetFile(filename);
            int numIds = reader.CountNetworkIds();
            List<Matrix> mlist = new List<Matrix>();
            Matrix temp;
            lock (reader)
            {
                switch (reader.FileType)
                {
                    case BufferedFileReader.Type.Matrix:
                        {
                            for (int i = 0; i < numIds; i++)
                            {
                                temp = ReadMatrixFromMatrixFile(reader, i);
                                mlist.Add(temp);
                            }
                            return mlist;
                        }
                    case BufferedFileReader.Type.Dyadic:
                        {
                            for (int i = 0; i < numIds; i++)
                            {
                                temp = ReadMatrixFromDyadicFile(reader, i, dyadicVariable);
                                mlist.Add(temp);
                            }
                            return mlist;
                        }
                    case BufferedFileReader.Type.Vector:
                        {
                            for (int i = 0; i < numIds; i++)
                            {
                                temp = ReadMatrixFromVectorFile(reader, i);
                                mlist.Add(temp);
                            }
                            return mlist;
                        }
                    default:
                        throw new FileLoadException("Invalid file type.");
                }
            }
        }

        public static Vector ReadVectorFromFile(string filename)
        {
            return ReadVectorFromFile(filename, -1);
        }
        public static Vector ReadVectorFromFile(string filename, int networkId)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            BufferedFileReader reader = BufferedFileTable.GetFile(filename);
            lock (reader)
            {
                switch (reader.FileType)
                {
                    case BufferedFileReader.Type.Vector:
                        return ReadVectorFromVectorFile(reader, networkId);
                    default:
                        throw new FileLoadException("Invalid file type.");
                }
            }
        }


        public static Matrix ReadAttributeVector(string filename, int netID) //working here
        {
            return createTransposeMatrix(filename, netID);
        }

        public static Matrix createTransposeMatrix(string filename, int netID) // working here - working for integers and random nodes
        {
            Dictionary <int, double> d = new Dictionary<int,double>();

            BufferedFileReader reader = BufferedFileTable.GetFile(filename);
            reader.GoToLine(0);

            while (!reader.EndOfStream) //reading file
            {
                string line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                string[] line_parts = line.Split(',');

                if (Int64.Parse(line_parts[0]) == netID)
                {
                    d.Add(ExtractNode(line_parts[1]), ExtractDouble(line_parts[2])); // need to work here for string case
                }
            }

            Matrix m = new Matrix(d.Count, d.Count);  //transpose matrix
            m.NetworkId = netID;
            m.NetworkIdStr = reader.GetNetworkRealId(netID);


            //calculation
            for (int i = 0; i < d.Count; i++)
            {
                for (int j = 0; j < d.Count; j++)
                {
                    m[i,j] = d[i + 1] * d[j + 1]; 
                }
            }
           
           return m;
        }
        
        private static Matrix ReadMatrixFromVectorFile(BufferedFileReader reader, int networkId)
        {
            networkId = reader.JumpToNetworkId(networkId, true);

            Matrix m = new Matrix(reader.CountLines(networkId));
            m.NetworkId = networkId;
            m.NetworkIdStr = reader.GetNetworkRealId(networkId);
            for (int i = 0; i < m.Rows; ++i)
            {
                string s = reader.ReadLine();
                string[] parts = s.Split(',');
                m[i, i] = ExtractDouble(parts[parts.Length - 1]);
                m.RowLabels[i] = m.ColLabels[i] = parts[parts.Length - 2];
            }

            return m;
        }

        private static Vector ReadVectorFromVectorFile(BufferedFileReader reader, int networkId)
        {
            networkId = reader.JumpToNetworkId(networkId, true);

            Vector v = new Vector(reader.CountLines(networkId));
            v.NetworkId = networkId;
            v.NetworkIdStr = reader.GetNetworkRealId(networkId);
            for (int i = 0; i < v.Size; ++i)
            {
                string s = reader.ReadLine();
                string[] parts = s.Split(',');
                v[i] = ExtractDouble(parts[parts.Length - 1]);
                v.Labels[i] = parts[parts.Length - 2];
            }

            return v;
        }

        private static Matrix ReadMatrixFromDyadicFile(BufferedFileReader reader, int networkId, int dyadicVariable)
        {
            networkId = reader.JumpToNetworkId(networkId, true);
            // Yushan
            List<string> rowLabels = new List<string>();
            List<string> colLabels = new List<string>();
            List<double> flatMatrix = new List<double>();
        
            //Dictionary<string, int> labels = reader.GetDyadicLabels(networkId);
            //int rows = labels.Count;

            //Matrix matrix = new Matrix(rows, rows);
            
            //matrix.RowLabels.SetLabels(labels.Keys);
            //matrix.ColLabels.SetLabels(labels.Keys);

            int totalLines = reader.CountLines(networkId);
            //Console.WriteLine("Total Lines: {0}", totalLines);
            for (int i = 0; i < totalLines; ++i)
            {
                string s = reader.ReadLine();

                string[] parts = s.Split(',');

                if (parts.Length < 3 + dyadicVariable)
                    throw new FileLoadException("Missing value for line: " + s);
                
                // Yushan
                if (!rowLabels.Contains(parts[1]))
                    rowLabels.Add(parts[1]);
                if (!colLabels.Contains(parts[2]))
                    colLabels.Add(parts[2]);
                flatMatrix.Add(ExtractDouble(parts[3 + dyadicVariable]));
                //matrix[labels[parts[1]], labels[parts[2]]] = ExtractDouble(parts[3 + dyadicVariable]);
            }
            /*
            if (networkId < 1000)
                networkId = int.Parse("1" + networkId);
            else
                networkId = int.Parse("2" + networkId);
            */
            int rows = rowLabels.Count;
            int cols = colLabels.Count;
            Matrix matrix = new Matrix(rows, cols);
            matrix.NetworkId = networkId;
            matrix.NetworkIdStr = reader.GetNetworkRealId(networkId);
            matrix.RowLabels.SetLabels(rowLabels);
            matrix.ColLabels.SetLabels(colLabels);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = flatMatrix[i * cols + j];
                }
            }
            
            return matrix;
        }

        public static List<Matrix> ReadMatrixFromMultipleDyadicFile(string filename, int networkId)
        {
            BufferedFileReader reader = BufferedFileTable.GetFile(filename);
            lock (reader)
            {
                networkId = reader.JumpToNetworkId(networkId, true);

                //Dictionary<string, int> labels = reader.GetDyadicLabels(networkId);
                //int rows = labels.Count;

                // initialize the matrices
                List<Matrix> multipleMatrices = new List<Matrix>();
                List<string> rowLabels = new List<string>();
                List<string> colLabels = new List<string>();
                //string[] topLabels = reader.TopLine.Split(',');
                //string[] labels = new string[reader.CountVarsInDyadicFile()];
                int varCnt = reader.CountVarsInDyadicFile();
                List<double> flatMatrixList = new List<double>();
                //for (int var = 0; var < reader.CountVarsInDyadicFile(); var++)
                //{
                //    Matrix matrix = new Matrix(rows, rows);
                //    matrix.NetworkId = networkId;
                //    matrix.Name = topLabels[var + 3];
                //    matrix.RowLabels.SetLabels(labels.Keys);
                //    matrix.ColLabels.SetLabels(labels.Keys);

                //    multipleMatrices.Add(matrix);
                //}

                int totalLines = reader.CountLines(networkId);

                for (int i = 0; i < totalLines; ++i)
                {
                    string s = reader.ReadLine();
                    string[] parts = s.Split(',');
                    if (!rowLabels.Contains(parts[1]))
                        rowLabels.Add(parts[1]);
                    if (!colLabels.Contains(parts[2]))
                        colLabels.Add(parts[2]);
                    //if (parts.Length < 3 + reader.CountVarsInDyadicFile())
                      //  throw new FileLoadException("Missing value for line: " + s);

                    for (int var = 0; var < varCnt; var++)
                    {
                        //multipleMatrices[var][labels[parts[1]], labels[parts[2]]] = ExtractDouble(parts[3 + var]);
                        flatMatrixList.Add(ExtractDouble(parts[3 + var]));
                    }
                }

                int rows = rowLabels.Count;
                int cols = colLabels.Count;
                for (int var = 0; var < varCnt; var++)
                {
                    Matrix matrix = new Matrix(rows, cols);
                    matrix.RowLabels.SetLabels(rowLabels);
                    matrix.ColLabels.SetLabels(colLabels);
                    matrix.NetworkId = networkId;
                    matrix.NetworkIdStr = reader.GetNetworkRealId(networkId);
                    multipleMatrices.Add(matrix);
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        for (int var = 0; var < varCnt; var++)
                        {
                            multipleMatrices[var][i, j] = flatMatrixList[i * cols * varCnt + j * varCnt + var];
                        }
                    }
                }
                /*
                if (networkId < 1000)
                    networkId = int.Parse("1" + networkId);
                else
                    networkId = int.Parse("2" + networkId);
                */


                return multipleMatrices;
            }
        }


        private static Matrix ReadMatrixFromMatrixFile(BufferedFileReader reader, int networkId)
        {
            networkId = reader.JumpToNetworkId(networkId, true);
            if (networkId == -1)
                throw new Exception("Network ID does not exist!");
            reader.ReadLine(); // Skip first line with the network id

            string[] colLabels = reader.ReadLine().Split(',');
            //Yushan
            List<string> rowLabels = new List<string>();
            //string[] colLabels = tempLabels.Split(',');
            int rows = reader.CountLines(networkId) - 2; // Subtract off header columns
            //int rows = reader.CountLinesAlt(tempIdStr) - 2;
            int cols = colLabels.Length - 1;

            Matrix matrix = new Matrix(rows, cols);
            matrix.NetworkId = networkId;
            matrix.NetworkIdStr = reader.GetNetworkRealId(networkId);
            matrix.ColLabels.SetLabels(colLabels);

            for (int row = 0; row < rows; ++row)
            {
                string temp = reader.ReadLine();
                if (temp == null)
                    break;
                //string[] parts = reader.ReadLine().Split(',');
                string[] parts = temp.Split(',');

                if (parts.Length > cols + 1) // one extra for header
                    throw new FileLoadException("Matrix file has too many entries for network id: " + matrix.NetworkIdStr + ", row " + parts[0]);

                if (parts.Length == 0)
                    throw new FileLoadException("Matrix file has no entries for network id: " + matrix.NetworkIdStr);

                //matrix.RowLabels[row] = parts[0];
                rowLabels.Add(parts[0]);

                for (int i = 1; i < parts.Length; ++i)
                    matrix[row, i - 1] = ExtractDouble(parts[i]);
            }
            matrix.RowLabels.SetLabels(rowLabels);
            reader.closeStream();
            reader.Dispose(); /* Change made 12/3/2010 - PM */
            
            return matrix; 
        }




        private static double ExtractDouble(string s)
        {
            double tmp;

            if (!double.TryParse(s, out tmp))
                throw new FileLoadException("Expecting floating point value: " + s);

            return tmp;
        }
        
        private static int ExtractNode(string s) //working
        {
            int node = 0;
            string number = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                    number += s[i];
            }

            node = Int32.Parse(number);

            return node;
        }
    }
}
