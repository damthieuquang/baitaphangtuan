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
        private int tongThuocTinh = 0;
        public Form_XuLyDuLieu()
        {
            InitializeComponent();
        }

        private void chiaTheoChiềuRộngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chọnDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)// Doc file
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
                    for (int i = 0; i < line.Length; i++)
                    {
                        chuoi.Add(line[i]);
                    }
                    data.Add(chuoi);
                }
                reader.Close();
                ThongTin();
                DuLieu();//Xuat du lieu
            }                                             
        }

        private int soThuocTinh()//Cho biết tổng số thuộc tính của dữ liệu vào
        {
            int tong = 0;
            List<string> str = data.ElementAt(0);
            tong = str.Count;
            return tong;
        }

        private void ThongTin()// Dua thông tin tổng quan về thuộc tính và kiểu của nó (num or nom)
        {            
            dataGridView2.Visible = true;
            List<string> str = data.ElementAt(0);
            for (int i = 0; i < str.Count; i++)
            {
                dataGridView2.Rows.Add(i + 1, false, str.ElementAt(i));
            }
            
        }

        private void DuLieu()// Show dữ liệu vào datagridview
        {
            tongThuocTinh = soThuocTinh();
            dataGridView1.ColumnCount = tongThuocTinh;
            List<string> name = data.ElementAt(0);
            for (int i = 0; i < tongThuocTinh; i++)
            {
                dataGridView1.Columns[i].Name = name[i].ToString();
            }
            data.RemoveAt(0);
            dataGridView1.RowCount = data.Count;
            for (int i = 0; i < data.Count; i++)
            {
                List<string> str = data.ElementAt(i);
                for (int j = 0; j < tongThuocTinh; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = str.ElementAt(j);
                }
            }
        }

        private float TrungBinh(int CotCanTinh)// Tính giá trị trung bình của cột được chỉ định
        {
            int count = 0;
            float tong = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                float tem;
                if (float.TryParse(dataGridView1.Rows[i].Cells[CotCanTinh].Value.ToString(), out tem))
                {
                    tong += tem;
                    count++;
                }
            }

            return tong / count;
        }

        private int TanXuat(string Nominal, int CotCanDem)// Tính tuần suất
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[CotCanDem].Value.ToString() == Nominal)
                {
                    count++;
                }
            }
            return count;
        }

        private string TanXuatMax(int CotCanDem)// Tìm tần xuất lớn nhất
        {
            int max = 0;
            string str = null;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[CotCanDem].Value.ToString() != "?")
                {
                    int tem = TanXuat(dataGridView1.Rows[i].Cells[CotCanDem].Value.ToString(), CotCanDem);
                    if (tem > max)
                    {
                        max = tem;
                        str = dataGridView1.Rows[i].Cells[CotCanDem].Value.ToString();
                    }
                }
            }
            return str;
        }


        private void Form_XuLyDuLieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form_XuLyDuLieu_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;// An datagridview
        }

        private void Numberic()//Dien du lieu thieu
        {
            float tb = 0;
            
            for (int i = 0; i < tongThuocTinh; i++)//tach ham do tranh chap dau "?"
            {
                if (kiemtraNumberic(i))
                {
                    tb = TrungBinh(i);
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value.ToString() == "?")
                        {
                            dataGridView1.Rows[j].Cells[i].Value = string.Format("{0:#,0.#}", tb);
                        }
                    }                    
                }                
            }
        }

        private void Nominal_()//Dien du lieu thieu
        {
            string str = null;
            
            for (int i = 0; i < tongThuocTinh; i++)//tach ham do tranh chap dau "?"
            {
                if (!kiemtraNumberic(i))
                {
                    str = TanXuatMax(i);
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value.ToString() == "?")
                        {
                            dataGridView1.Rows[j].Cells[i].Value = str;
                        }
                    }
                }                
            }
        }

        private bool kiemtraNumberic(int cotCanKiem)// Kiểm tra có phải Numberic? ngược lại Nominal
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString() != "?")
                {
                    float tem;
                    if (float.TryParse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString(), out tem))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void thựcHiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Numberic();
            Nominal_();
            
        }

        private void lưuFileToolStripMenuItem_Click(object sender, EventArgs e)//Lưu file
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Chưa có nội dung");
                return;
            }
            SaveFileDialog sdl = new SaveFileDialog();
            sdl.Filter = "(*.csv)|*.csv|(*.arff)|*.arff";
            string tem = null;

            if (sdl.ShowDialog() == DialogResult.OK)
            {
                string path = sdl.FileName;
                StreamWriter sw = new StreamWriter(path);
                for (int j = 0; j < tongThuocTinh; j++)
                {
                    tem = dataGridView1.Columns[j].HeaderText;
                    sw.Write(tem + ",");
                }
                sw.WriteLine();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {                    
                    for (int j = 0; j < tongThuocTinh; j++)
                    {
                        tem = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        sw.Write(tem+",");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
