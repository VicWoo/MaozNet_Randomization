using System;
using System.Collections.Generic;
using System.Text;
using Network.Matrices;
using System.IO; 

namespace Network.IO
{
    public sealed class MatrixWriter
    {
        private MatrixWriter() { }

        public static void WriteMatrixToMatrixFile(Matrix m, string filename)
        {
            WriteMatrixToMatrixFile(m, filename, false, true, true);
        }
        public static void WriteMatrixToMatrixFile(Matrix m, string filename, bool overwrite)
        {
            WriteMatrixToMatrixFile(m, filename, overwrite, true, true);
        }
        public static void WriteMatrixToMatrixFile(Matrix m, string filename,
            bool overwrite, bool writeNetworkId, bool writeColLabels)
        {
            using (StreamWriter writer = new StreamWriter(filename, !overwrite))
            {
                if (writeNetworkId)
                    //writer.WriteLine(m.NetworkId);
                    // Yushan
                    writer.WriteLine(m.NetworkIdStr);

                if (writeColLabels)
                {
                    // writer.Write(',');
                    // writer.WriteLine(m.ColLabels.ToString());
                    for (int j = 0; j < m.Cols; ++j)
                    {
                        if (String.Equals(m.ColLabels[j], "Network ID")) { }
                        else
                        {
                            writer.Write(',');
                            writer.Write(m.ColLabels[j]);
                        }
                    }
                    writer.WriteLine();
                }

                for (int i = 0; i < m.Rows; ++i)
                {
                    writer.Write(m.RowLabels[i]);
                    for (int j = 0; j < m.Cols; ++j)
                    {
                        if (String.Equals(m.ColLabels[j], "Network ID")) ;
                        else
                        {
                            writer.Write(',');
                            writer.Write(m[i, j]);
                        }
                    }
                    writer.WriteLine();
                }
                // Yushan
                writer.Flush();
                writer.Close();
            }
        }

        public static void WriteMatrixToDyadicFile(Matrix m, string filename)
        {
            WriteMatrixToDyadicFile(m, filename, false, true, true);
        }
        public static void WriteMatrixToDyadicFile(Matrix m, string filename, bool overwrite)
        {
            WriteMatrixToDyadicFile(m, filename, overwrite, true, true);
        }
        public static void WriteMatrixToDyadicFile(Matrix m, string filename,
            bool overwrite, bool writeNetworkId, bool writeColLabels)
        {
            using (StreamWriter writer = new StreamWriter(filename, !overwrite))
            {
                for (int i = 0; i < m.Rows; ++i)
                {
                    for (int j = 0; j < m.Cols; ++j)
                    {
                        //string line = m.NetworkId + "," + m.RowLabels[i].ToString() + ","; // modified
                        string line = m.NetworkIdStr + "," + m.RowLabels[i].ToString() + ",";
                        line += m.RowLabels[j].ToString() + "," + m[i, j].ToString();
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static void WriteDyadicHeader(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine("\"Network ID\",\"Row\",\"Column\",\"value\"");
            }
        }

        public static void WriteVectorToVectorFile(Vector v, string filename)
        {
            WriteMatrixToMatrixFile(v, filename, false);
        }
        public static void WriteVectorToVectorFile(Vector v, string filename, bool overwrite)
        {
            using (StreamWriter writer = new StreamWriter(filename, !overwrite))
            {
                for (int i = 0; i < v.Size; ++i)
                {
                    //writer.WriteLine("{0},{1},{2}", v.NetworkId, v.Labels[i], v[i]);
                    writer.WriteLine("{0},{1},{2}", v.NetworkIdStr, v.Labels[i], v[i]);
                }
            }
        }
    }
}
