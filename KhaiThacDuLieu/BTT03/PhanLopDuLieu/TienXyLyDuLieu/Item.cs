using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhanLop
{
    public class Item
    {
        private int _index;

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        private string _attributeTOP;

        public string AttributeTOP
        {
            get { return _attributeTOP; }
            set { _attributeTOP = value; }
        }
        public void Init(Item x)
        {
            this.Index = x.Index;
            this.Distance = x.Distance;
            this.AttributeTOP = x.AttributeTOP;
        }
    }
}
