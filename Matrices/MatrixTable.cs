using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Matrices
{
    public class MatrixTable : Dictionary<string, Matrix>
    {
        public Matrix AddMatrix(string name, int rows, int cols)
        {
            if (this.ContainsKey(name) && this[name] != null && this[name] is Matrix && this[name].Rows == rows && this[name].Cols == cols)
                this[name].Clear();
            else
                this[name] = new Matrix(rows, cols);

            return this[name];
        }
        public string[] GetOrderedNetworkIds()
        {
            string[] orderedNetIds = new string[this.Count];
            foreach (KeyValuePair <string, Matrix> kvp in this)
            {
                orderedNetIds[kvp.Value.NetworkId] = kvp.Key;
            }
            return orderedNetIds;
        }

    }
}
