using System;
using System.Collections.Generic;
using System.Text;

namespace Network.IO
{
    class NetworkIdTable
    {
        private Dictionary<int, int> _table;
        private int _minNetworkId, _maxNetworkId;

        public int MinNetworkId
        {
            get { return _minNetworkId; }
        }

        public int MaxNetworkId
        {
            get { return _maxNetworkId; }
        }

        public NetworkIdTable()
        {
            _table = new Dictionary<int, int>();
            _minNetworkId = FindMinKey();
            _maxNetworkId = FindMaxKey();
        }

        public int KeyCounts()
        {
            int count = _table.Count;
            return count;
        }

        public int FindMaxKey()
        {
            // int max = _minNetworkId;
            int max = -1;
            foreach (KeyValuePair<int, int> kvp in _table)
            {
                if (kvp.Value > max) max = kvp.Value;
            }
            return max;
        }

        public int FindMinKey()
        {
            // int max = _minNetworkId;
            int min = 0;
            foreach (KeyValuePair<int, int> kvp in _table)
            {
                if (kvp.Value < min) min = kvp.Value;
            }
            return min;
        }
        public bool ContainsNetworkId(int networkId)
        {
            return _table.ContainsKey(networkId);
        }

        public int GetNextNetworkId(int networkId)
        {
            if (networkId >= _maxNetworkId || !ContainsNetworkId(++networkId))
                return -1;
            if (networkId < _minNetworkId)
                return _minNetworkId;

            return networkId;
        }

        public int GetPreviousNetworkId(int networkId)
        {
            if (networkId <= _minNetworkId || !ContainsNetworkId(--networkId))
                return -1;
            if (networkId > _maxNetworkId)
                return _maxNetworkId;

            return networkId;
        }

        public int this[int networkId]
        {
            get
            {
                return _table[networkId];
            }
            set
            {
                _table[networkId] = value;

                if (networkId < _minNetworkId)
                    _minNetworkId = networkId;
                if (networkId > _maxNetworkId)
                    _maxNetworkId = networkId;
            }
        }
    }
}
