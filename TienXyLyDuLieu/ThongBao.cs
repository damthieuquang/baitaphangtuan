using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TienXyLyDuLieu
{
    public partial class Form_ThongBao : Form
    {
        public List<ClassBin> bins = new List<ClassBin>();
        public int soGio;
        public int thuocTinh;
        public string tenThuocTinh;
        public bool res;

        public Form_ThongBao()
        {
            InitializeComponent();
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
            this.Text = this.tenThuocTinh;
            for (int i = 0; i < soGio; i++)
            {
                dataGridView_ThongBao.Rows.Add(i + 1, bins[i].Min, bins[i].Max, string.Format("{0:#,0.####}",bins[i].Sum / bins[i].Count),bins[i].Count);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            res = true;
            Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            res = false;
            Close();
        }
    }
}
