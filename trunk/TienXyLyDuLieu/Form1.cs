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
        public List<bool> isNumeric()
        {
            List<bool> listNumeric = new List<bool>();
            float temp;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (float.TryParse(dataGridView1.Rows[1].Cells[i].Value.ToString(), out  temp))
                {
                    listNumeric.Add(true);
                }
                else
                {
                    listNumeric.Add(false);
                }
            }
            
            return listNumeric;
        }
        private void chiaTheoChiềuRộngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //nhan gia tri N-gio do nguoi dung nhap
            //sort tang dan theo ThuocTinh can chia gio
            //lay (max-min)/N de lay khoang
            //=> Tim gio:
            //[min + (max-min)*i/N,min + (max-min)*(i+1)/N) i:0->N-1;min + (max-min)*(i+1)/N < max
            //[min + (max-min)*i/N,max) i:0->N-1;min + (max-min)*(i+1)/N > max
            //Tinh gia tri trung binh cua tung thuoc tinh
            //DataGridViewColumn col = dataGridView1.SortedColumn;
            //ListSortDirection direction = ListSortDirection.Ascending;
            //dataGridView1.Sort(col, direction);
            //MessageBox.Show("hehe");
            /*DataGridViewColumn newColumn =
        dataGridView1.Columns.GetColumnCount(
        DataGridViewElementStates.Selected) == 1 ?
        dataGridView1.SelectedColumns[0] : null;

            DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not currently sorted. 
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder. 
                if (oldColumn == newColumn &&
                    dataGridView1.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            // If no column has been selected, display an error dialog  box. 
            if (newColumn == null)
            {
                MessageBox.Show("Select a single column and try again.",
                    "Error: Invalid Selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                dataGridView1.Sort(newColumn, direction);
                newColumn.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    SortOrder.Ascending : SortOrder.Descending;
            }*/
            //dataGridView1.Sort(dataGridView1.Columns["cl_residual_sugar"], ListSortDirection.Ascending);
            //DataGridViewTextBoxColumn textboxColumn = new DataGridViewTextBoxColumn();
            //textboxColumn.ValueType= typeof(string);
            //string a = "1";
            //string b = "2.3";
            //string c = "3";
            //string d = "9.1";
            //string f = "10";
            //int kq = b.CompareTo(f);
            //MessageBox.Show(kq.ToString());

            List<bool> list = isNumeric();
            for (int j = 0; j < 1; j++)
            {
                //MessageBox.Show(list[j].ToString());
                //NEU LA NUMERIC THI SORT THEO INT
                if (list[j] == false)
                {
                    //MessageBox.Show(list[j].ToString());
                    //dataGridView1.Columns[j].ValueType = typeof(float);
                    dataGridView1.Sort(dataGridView1.Columns[j], ListSortDirection.Ascending);
                    
                    /*
                     * COI NHU SORT XONG
                     */
                    //float dorong = (float)(dataGridView1.Rows[0].Cells[j].Value) - dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j].Value) / 2;
                }
                else
                {
                    //NEU LA NOMINAL THI SORT THEO STRING
                   // SortNumeric(j);
                }
                
            }
           // dataGridView1.SortCompare += new DataGridViewSortCompareEventHandler(
           //this.dataGridView1_SortCompare);
           // Controls.Add(this.dataGridView1);
            
            
            
        }

        public void SortNumeric(int x)
        {
            DataGridViewRow row = new DataGridViewRow();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[x].Value.ToString().CompareTo(dataGridView1.Rows[j].Cells[x].Value.ToString()) > 0)
                    {
                       
                        //row = dataGridView1.Rows[i];
                        //dataGridView1.Rows[i].SetValues = dataGridView1.Rows[j];
                        //dataGridView1.Rows[j] = row;
                    }
                }
            }

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

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            e.SortResult = System.String.Compare(
            e.CellValue1.ToString(), e.CellValue2.ToString());

            // If the cells are equal, sort based on the ID column. 
            if (e.SortResult == 0 && e.Column.Name != "cl_residual_sugar")
            {
                e.SortResult = System.String.Compare(
                    dataGridView1.Rows[e.RowIndex1].Cells["cl_residual_sugar"].Value.ToString(),
                    dataGridView1.Rows[e.RowIndex2].Cells["cl_residual_sugar"].Value.ToString());
            }
            e.Handled = true;
        }
    }
}
