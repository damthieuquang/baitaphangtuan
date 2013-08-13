using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TienXyLyDuLieu
{
    public partial class Form_XuLyDuLieu : Form
    {
        private List<List<string>> data = new List<List<string>>();
        public Form_XuLyDuLieu()
        {
            InitializeComponent();
        }

        private void chiaTheoChiềuRộngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chọnDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.csv)|*.csv|(*.arff)|*.arff";
            

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileName;
                StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    List<string> chuoi = new List<string>();
                    string[] line = reader.ReadLine().Split(',');
                    for (int i = 0; i < line.Length;i++ )
                    {
                        chuoi.Add(line[i]);
                    }
                    data.Add(chuoi);
                }
                reader.Close();
            }
            for (int i = 1; i < data.LongCount() ; i++)
            {
                List<string> str = data.ElementAt(i);
                dataGridView1.Rows.Add(str.ElementAt(0), str.ElementAt(1), str.ElementAt(2), str.ElementAt(3), str.ElementAt(4), str.ElementAt(5), str.ElementAt(6), str.ElementAt(7), str.ElementAt(8), str.ElementAt(9), str.ElementAt(10), str.ElementAt(11));
            }
        }
    }
}
