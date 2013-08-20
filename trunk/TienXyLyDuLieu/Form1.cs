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
        private void resetChecked()
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Cells["cl_checked"].Value = false;
                dataGridView2.Rows[i].Cells["cl_Bin"].Value = "";
            }
        }

        private void chiaTheoChiềuRộngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dataGridView_ChoSanPham.Rows[i].Cells[clCheck.Index].Value.ToString() == "True"
            //nhan gia tri N-gio do nguoi dung nhap           
            //lay (max-min)/N de lay dorong cua khoang
            //=> Tim gio:
            //[min + (max-min)*i/N,min + (max-min)*(i+1)/N) i:0->N-1;min + (max-min)*(i+1)/N < max
            //[min + (max-min)*i/N,max) i:0->N-1;min + (max-min)*(i+1)/N > max
            //Tinh gia tri trung binh cua tung thuoc tinh
            //bins: [min,max),count, gtriTB

            //Class bins{min, max,thanhphan,gtriTB}
            //list bins

            int N = 0;//N la so gio nguoi dung NHAP
            for (int i = 0; i < dataGridView2.RowCount; i++)
            { 
                //khong tinh cot cuoi cung
                
                if (dataGridView2.Rows[i].Cells[cl_checked.Index].Value.ToString() == "True")
                {
                    if (dataGridView2.Rows[i].Cells["cl_Bin"].Value == null)
                    {
                        MessageBox.Show("Vui long chon thuoc tinh va dien so gio ban muon chia o khung ben trai.\n Sau do bam chon kieu chia o thanh menu");
                        return;
                    }
                    else
                    {
                        N = int.Parse(dataGridView2.Rows[i].Cells["cl_Bin"].Value.ToString());
                        if (kiemtraNumberic(i) == true)
                        {
                            //chia gio

                            chiaTheoChieuRong(i, N);
                        }
                        else
                        {
                            MessageBox.Show("Thuoc tinh nay la Nominal.");
                            return;
                        }
                    }
                }
            }
            resetChecked();
           
            //xuat list bins ra form2
            //int Index = dataGridView1.CurrentCell.OwningColumn.Index;
            
            
            
            
        }
        private void chiaTheoChieuRong(int cotCanKiem, int sogio)
        {
            int i,j,k;
            double item = 0;
            double avg= 0;
            double Min = timMin(cotCanKiem);
            double Max = timMax(cotCanKiem);
            double value = (Max - Min)/sogio;
            List<ClassBin> bins = new List<ClassBin>();
            ClassBin temp;
            //Khoi tao cac gio
            for (i = 0; i < sogio - 1; i++)
            {
                temp = new ClassBin(Min + i * value, Min + (i + 1) * value);
                bins.Add(temp); 
            }
            temp = new ClassBin(Min + (sogio - 1) * value, Max);
            bins.Add(temp);
            //MessageBox.Show("bins= " + bins.Count.ToString());
            //tinh gia tri cua gio
            int vt = 0;
            for (i = 0; i < bins.Count; i++)
            {
                bins[i].IndexItem = new List<int>();
                for (j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    item = double.Parse(dataGridView1.Rows[j].Cells[cotCanKiem].Value.ToString());
                    if (item >= bins[i].Min && item < bins[i].Max)
                    {
                        bins[i].IndexItem.Add(j);
                        bins[i].Sum += item;
                        bins[i].Count++;
                    }
                    if (i ==bins.Count-1 && item == Max)
                    {
                        bins[bins.Count - 1].IndexItem.Add(j);
                        bins[bins.Count - 1].Sum += item;
                        bins[bins.Count - 1].Count ++;
                    }
                }
                avg = bins[i].Sum/bins[i].Count;
                for (k = 0; k <  bins[i].IndexItem.Count; k++)
                {
                    dataGridView1.Rows[bins[i].IndexItem[k]].Cells[cotCanKiem].Value = avg;
                }
                //vt = bins[i].IndexItem.Count;
                MessageBox.Show("min:" + bins[i].Min.ToString() + "\nMax: " + bins[i].Max.ToString() + "\nCount: " + bins[i].Count.ToString() + "\navg: " + avg.ToString());
            }
        }

        //private void SortNumeric(int cotCanKiem)
        //{
        //    double a = 0;
        //    double b = 0;
        //    DataGridViewRow temp = new DataGridViewRow();

        //    //dataGridView1.Columns[cotCanKiem].ValueType = typeof(float);
        //    //dataGridView1.Sort(dataGridView1.Columns[cotCanKiem], ListSortDirection.Ascending);
        //    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
        //        {
        //            a = float.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
        //            b = float.Parse(dataGridView1.Rows[j].Cells[cotCanKiem].Value.ToString());
        //            if (a > b)
        //            {
        //                MessageBox.Show(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString() + " " + dataGridView1.Rows[j].Cells[cotCanKiem].Value.ToString());
        //                //temp =dataGridView1.Rows[i];
        //                //dataGridView1.Rows[i].SetValues(dataGridView1.Rows[j]);
        //                //dataGridView1.Rows[j].SetValues(temp);


        //            }
        //        }
        //    }
        //}

        //public void SortNominal(int x)
        //{
        //    DataGridViewRow row = new DataGridViewRow();
        //    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
        //        {
        //            if (dataGridView1.Rows[i].Cells[x].Value.ToString().CompareTo(dataGridView1.Rows[j].Cells[x].Value.ToString()) > 0)
        //            {
                       
        //                //row = dataGridView1.Rows[i];
        //                //dataGridView1.Rows[i].SetValues = dataGridView1.Rows[j];
        //                //dataGridView1.Rows[j] = row;
        //            }
        //        }
        //    }

        //}

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
            MessageBox.Show(dataGridView2.Rows.Count.ToString());
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

        private double timMin(int cotCanKiem)
        {
            double flag = double.Parse(dataGridView1.Rows[0].Cells[cotCanKiem].Value.ToString());
            double temp = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString()) < flag)
                {
                    flag = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());

                }
            }

            MessageBox.Show("min: " + flag.ToString());
            return flag;
        }

        private double timMax(int cotCanKiem)
        {
            double flag = double.Parse(dataGridView1.Rows[0].Cells[cotCanKiem].Value.ToString());
            double temp = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString()) > flag)
                {
                    flag = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());

                }
            }
            MessageBox.Show("max: " + flag.ToString());
            return flag;
        }

        private void chuanHoaMinMax(int cotCanKiem)
        {
            //Chuan hoa ve [0;1]
            //ktra truoc: neu la numeric voi vao ham nay
            //tao datagridview3 duoi datagridview1
            if (kiemtraNumberic(cotCanKiem) != true)
            {
                MessageBox.Show("Thuoc tinh nay la Nominal.");
                return;
            }
            double v = 0;
            double kq = 0;
            double min = timMin(cotCanKiem);
            double max = timMax(cotCanKiem);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString() == "?")
                {
                    MessageBox.Show("Du lieu chua duoc lam sach. Vui long lam sach du lieu.");
                    return;
                }
                v = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                kq = (v - min) * (1 - 0) / (max - min) + 0;
                //ghi vao datagridview1
                //MessageBox.Show(kq.ToString());
                dataGridView1.Rows[i].Cells[cotCanKiem].Value = kq;
                //MessageBox.Show(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
            }
           // MessageBox.Show("minmax: " + (float.Parse(dataGridView1.Rows[2].Cells[cotCanKiem].Value.ToString())).ToString()+" "+ min.ToString() +" "+ (max-min).ToString());
        }

        private void TongBinhPhuong_Dem(int cotCanKiem,ref double tong,ref int somau)
        {
            double mau = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString() == "?")
                {
                    MessageBox.Show("Du lieu chua duoc lam sach. Vui long lam sach du lieu.");
                    return;
                }
                mau = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                tong += mau * mau;
                somau++;
                
            }
        }

        private double tinhDoLechChuan(int cotCanKiem)
        {
            double tongbp = 0;
            int somau = 0;
            double dolechchuan = 0;
            TongBinhPhuong_Dem(cotCanKiem,ref tongbp,ref somau);
            dolechchuan = Math.Sqrt(tongbp / somau);

            MessageBox.Show(tongbp.ToString());
            MessageBox.Show(somau.ToString());
            MessageBox.Show(dolechchuan.ToString());
            return dolechchuan;
        }
        private void chuanHoaZ_score(int cotCanKiem)
        {
            //tinh Tong binh phuong nhung mau != "?"
            //tinh tong nhung mau != "?"
            //=> phuong sai
            //=> do lech chuan = can(phuongsai)
            //=> chuan hoa Zscore
            if (kiemtraNumberic(cotCanKiem) != true)
            {
                MessageBox.Show("Thuoc tinh nay la Nominal.");
                return;
            }

            double dolechchuan = tinhDoLechChuan(cotCanKiem);
            double mau=0;
            double trungbinh = TrungBinh(cotCanKiem);
            double kq = 0;
            //MessageBox.Show(((double.Parse(dataGridView1.Rows[0].Cells[cotCanKiem].Value.ToString()) - TrungBinh(cotCanKiem))/dolechchuan).ToString());

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString() == "?")
                {
                    MessageBox.Show("Du lieu chua duoc lam sach. Vui long lam sach du lieu.");
                    return;
                }
                mau = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                kq = (mau - trungbinh) / dolechchuan;
                //ghi vao datagridview1                    
                dataGridView1.Rows[i].Cells[cotCanKiem].Value = kq;
                
            }
            
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
                    sw.Write(tem);
                    if (j != tongThuocTinh - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.WriteLine();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {                    
                    for (int j = 0; j < tongThuocTinh; j++)
                    {
                        tem = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        sw.Write(tem);
                        if (j != tongThuocTinh - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
                MessageBox.Show("File lưu thành công!");
            }
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 0)
            {
                if (double.Parse(e.CellValue1.ToString()) > double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = 1;
                }
                else if (double.Parse(e.CellValue1.ToString()) > double.Parse(e.CellValue2.ToString()))
                {
                    e.SortResult = -1;
                }
                else
                {
                    e.SortResult = 0;
                }
                e.Handled = true;
            }
            //e.SortResult = System.String.Compare(
            //e.CellValue1.ToString(), e.CellValue2.ToString());

            //// If the cells are equal, sort based on the ID column. 
            //if (e.SortResult == 0 && e.Column.Name != "cl_residual_sugar")
            //{
            //    e.SortResult = System.String.Compare(
            //        dataGridView1.Rows[e.RowIndex1].Cells["cl_residual_sugar"].Value.ToString(),
            //        dataGridView1.Rows[e.RowIndex2].Cells["cl_residual_sugar"].Value.ToString());
            //}
            //e.Handled = true;
        }

        private void minmaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Index = dataGridView1.CurrentCell.OwningColumn.Index;
            MessageBox.Show(Index.ToString());
            //tru cot cuoi cung ko chuan hoa
            chuanHoaMinMax(Index);
        }

        private void zscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Index = dataGridView1.CurrentCell.OwningColumn.Index;
            //MessageBox.Show(Index.ToString());
            //tru cot cuoi cung ko chuan hoa
            chuanHoaZ_score(Index);
        }

        
    }
}
