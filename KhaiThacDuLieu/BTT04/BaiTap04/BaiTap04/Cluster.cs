using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BaiTap04
{
    public class Cluster
    {
        private DataRow _point;

        public DataRow Point
        {
            get { return _point; }
            set { _point = value; }
        }
        private List<int> _flag;

        public List<int> Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private DataRow _mean;

        public DataRow Mean
        {
            get { return _mean; }
            set { _mean = value; }
        }

        
    }
}
