using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TienXyLyDuLieu
{
    public class ClassBin
    {
        //min
        private double _min;

        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        //max
        private double _max;

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        //list chua chi so dong trong datagrisview1 cua mau
        private List<int> _indexItem;

        public List<int> IndexItem
        {
            get { return _indexItem; }
            set { _indexItem = value; }
        }

        //sum so mau trong bin
        private double _sum;

        public double Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }

        //count so mau trong bin
        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        //phuong thuc khoi tao
        public ClassBin()
        {
            _min = 0;
            _max = 0;
            _indexItem = null;
            _sum = 0;
            _count = 0;
        }

        public ClassBin(double min, double max)
        {
            _min = min;
            _max = max;
            _indexItem = null;
            _sum = 0;
            _count = 0;
        }
    }
}
