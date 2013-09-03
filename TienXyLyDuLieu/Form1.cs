﻿using System;
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
        //private List<ClassBin> bins = new List<ClassBin>();

        public Form_XuLyDuLieu()
        {
            InitializeComponent();

        }
        
        private void resetChecked()
        {

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {                
                dataGridView2.Rows[i].Cells["cl_Bin"].Value = null;
            }
        }

        private void createList(List<double> ls, int cotCanKiem)
        {
            double temp;
            for (int i = 0; i < data.Count; i++)
            {                
                double.TryParse( (data.ElementAt(i)).ElementAt(cotCanKiem) , out temp);
                ls.Add(temp );
            }
        }

        private void chiềuSâuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieuThieu() == true)
            {
                MessageBox.Show("Du lieu chua duoc lam sach. Vui long lam sach du lieu.");
                return;
            }
                        
            int N = 0;
            int flag=0;            
            List<double> arrCotCanKiem = new List<double>();

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells["cl_Bin"].Value != null)
                {
                    flag=1;
                    int.TryParse(dataGridView2.Rows[i].Cells["cl_Bin"].Value.ToString(), out N);
                    if (kiemtraNumberic(i) == true)
                    {
                        arrCotCanKiem.Clear();
                        createList(arrCotCanKiem, i);                        
                        arrCotCanKiem.Sort();
                                                                            
                        //chia gio
                        chiaTheoChieuSau(arrCotCanKiem, i, N);
                    }
                    else
                    {
                        MessageBox.Show("Thuoc tinh nay la Nominal.");
                        return;
                    }
                }              

            }
            if(flag == 0 )
            {
                MessageBox.Show("Gia tri Bins khong dung. Vui long nhap lai.");
                return;
            }

            resetChecked();
        }

        private void chiaTheoChieuSau(List<double> arrCotCanKiem, int cotCanKiem, int sogio)
        {
            int i,j, k, temp, value;
            double s = 0;
            double t = 0;
            double avg = 0;
            string item;
            List<ClassBin> bins = new List<ClassBin>();
            ClassBin x = new ClassBin();
                        
            //Khoi tao cac gio
            k = 0;
            temp = 1;
            value = (int)(arrCotCanKiem.Count / sogio + 0.5);
            
                       
            for (i = 0; i < arrCotCanKiem.Count -1 ; i++)
            {
                
                if (temp == 1)
                {                    
                    x.Min = arrCotCanKiem[i];
                    
                    x.IndexItem = new List<int>();
                }
                x.Sum += arrCotCanKiem[i];
                x.Count++;
                x.IndexItem.Add(i);
                
                if (temp == value)
                {
                    x.Max = arrCotCanKiem[i];
                    if(arrCotCanKiem[i+1] != arrCotCanKiem[i])
                    {
                        k++;
                        bins.Add(x);
                        x = new ClassBin();
                        temp = 1;
                    }
                    
                }
                else
                {
                    temp++;
                }
            }
            
            if (temp == 0)  //bin moi
            {
                s = arrCotCanKiem[arrCotCanKiem.Count - 1];
                x=new ClassBin(s,s);
                x.IndexItem.Add(i);
                x.Sum += s;
                x.Count++;
            }
            else    //bin cu, con co the chua them phantu
            {
                s = arrCotCanKiem[arrCotCanKiem.Count - 1];
                x.Max = s;
                x.IndexItem.Add(i);
                x.Sum += s;
                x.Count++;
            }
            bins.Add(x);

            

            //show bins
            Form_ThongBao frm = new Form_ThongBao();
            frm.soGio = bins.Count;
            frm.thuocTinh = cotCanKiem;
            frm.tenThuocTinh = dataGridView1.Columns[cotCanKiem].Name;//.Columns[i].Name
            frm.bins = bins;
            frm.ShowDialog();

            if (frm.res)
            {                
                //khu nhieu cho mau
                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (j = 0; j < bins.Count; j++)
                    {
                        double.TryParse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString(), out s);
                        if (s >= bins[j].Min && s <= bins[j].Max)
                        {
                            // s thuoc bins[j]
                            dataGridView1.Rows[i].Cells[cotCanKiem].Value = string.Format("{0:#,0.####}", bins[j].Sum / bins[j].Count);
                        }
                    }
                }
            }            
        }

        private void chiaTheoChiềuRộngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieuThieu() == true)
            {
                MessageBox.Show("Du lieu chua duoc lam sach. Vui long lam sach du lieu.");
                return;
            }
            int N = 0;
            int flag = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {                
                if (dataGridView2.Rows[i].Cells["cl_Bin"].Value != null)
                {
                    flag = 1;
                    int.TryParse(dataGridView2.Rows[i].Cells["cl_Bin"].Value.ToString(),out N);
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
            if(flag==0)
                {
                    MessageBox.Show("Gia tri Bins khong dung. Vui long nhap lai.");
                    
                    return;
                }
            resetChecked();
        }
        private bool KiemTraDuLieuThieu()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
			{
                for (int j = 0; j < tongThuocTinh; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "?")
                        return true;
                }
			}

            return false;
        }
        private void chiaTheoChieuRong(int cotCanKiem, int sogio)
        {
            int i, j, k;
            double item = 0;
            double avg = 0;
            double Min = timMin(cotCanKiem);
            double Max = timMax(cotCanKiem);
            double value = (Max - Min) / sogio;
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
            
            //tinh gia tri cua gio
            int vt = 0;
            for (i = 0; i < bins.Count; i++)
            {
                bins[i].IndexItem = new List<int>();
                for (j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    double.TryParse(dataGridView1.Rows[j].Cells[cotCanKiem].Value.ToString(),out item);
                    if (item >= bins[i].Min && item < bins[i].Max)
                    {
                        bins[i].IndexItem.Add(j);
                        bins[i].Sum += item;
                        bins[i].Count++;
                    }
                    if (i == bins.Count - 1 && item == Max)
                    {
                        bins[bins.Count - 1].IndexItem.Add(j);
                        bins[bins.Count - 1].Sum += item;
                        bins[bins.Count - 1].Count++;
                    }
                }
                
            }
            Form_ThongBao frm = new Form_ThongBao();
            frm.soGio = sogio;
            frm.thuocTinh = cotCanKiem;
            frm.bins = bins;
            frm.ShowDialog();

            if (frm.res)
            {
                for (i = 0; i < bins.Count; i++)
                {
                    avg = bins[i].Sum / bins[i].Count;
                    for (k = 0; k < bins[i].IndexItem.Count; k++)
                    {
                        dataGridView1.Rows[bins[i].IndexItem[k]].Cells[cotCanKiem].Value = string.Format("{0:#,0.####}", avg);
                    }
                }
            }
        }

        private void SortNumeric(int cotCanKiem)
        {
            double a = 0;
            double b = 0;
            String temp;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
                {
                    a = float.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                    b = float.Parse(dataGridView1.Rows[j].Cells[cotCanKiem].Value.ToString());
                    if (a > b)
                    {                        
                        temp = dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString();
                        dataGridView1.Rows[i].Cells[cotCanKiem].Value = dataGridView1.Rows[j].Cells[cotCanKiem].Value;
                        dataGridView1.Rows[j].Cells[cotCanKiem].Value = temp;
                    }
                }
            }
        }
        

        private void chọnDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)// Doc file
        {
            //Làm sạch để load dữ liệu từ lần thứ 2 trở đi
            dataGridView2.Rows.Clear();            
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            data.Clear();

            //đọc file
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
                dataGridView2.Rows.Add(i + 1, str.ElementAt(i));
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
                            dataGridView1.Rows[j].Cells[i].Value = string.Format("{0:#,0.####}", tb);
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
                    if (!float.TryParse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString(), out tem))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private double timMin(int cotCanKiem)
        {
            double flag = double.Parse(dataGridView1.Rows[0].Cells[cotCanKiem].Value.ToString());
            double temp = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double.TryParse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString(), out temp);
                if (temp < flag)
                {
                    
                    flag = temp;
                }
            }

            
            return flag;
        }

        private double timMax(int cotCanKiem)
        {
            double flag = double.Parse(dataGridView1.Rows[0].Cells[cotCanKiem].Value.ToString());
            double temp = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double.TryParse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString(),out temp);
                if (temp > flag)
                {
                    
                    flag = temp;
                }
            }
           
            return flag;
        }

        private void chuanHoaMinMax(int cotCanKiem)
        {
            //Chuan hoa ve [0;1]
            if(KiemTraDuLieuThieu()== true)
            {
                MessageBox.Show("Dữ liệu chưa được làm sạch. Vui lòng làm sạch dữ liệu!");
                return;
            }
            
            double v = 0;
            double kq = 0;
            double min = timMin(cotCanKiem);
            double max = timMax(cotCanKiem);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {                
                v = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                kq = ((v - min) * (1 - 0)) / (max - min) + 0;

                dataGridView1.Rows[i].Cells[cotCanKiem].Value = string.Format("{0:#,0.####}", kq);                
            }            
        }

        private void TongBinhPhuong_Dem(int cotCanKiem, ref double tong, ref int somau)
        {
            double mau = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
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
            TongBinhPhuong_Dem(cotCanKiem, ref tongbp, ref somau);
            dolechchuan = Math.Sqrt(tongbp / somau);
                        
            return dolechchuan;
        }
        private void chuanHoaZ_score(int cotCanKiem)
        {
            if (KiemTraDuLieuThieu() == true)
            {
                MessageBox.Show("Dữ liệu chưa được làm sạch. Vui lòng làm sạch dữ liệu!");
                return;
            }
            if (kiemtraNumberic(cotCanKiem) != true)
            {
                MessageBox.Show("Thuộc tính này là Nominal.");
                return;
            }

            double dolechchuan = tinhDoLechChuan(cotCanKiem);
            double mau = 0;
            double trungbinh = TrungBinh(cotCanKiem);
            double kq = 0;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {                
                mau = double.Parse(dataGridView1.Rows[i].Cells[cotCanKiem].Value.ToString());
                kq = (mau - trungbinh) / dolechchuan;

                dataGridView1.Rows[i].Cells[cotCanKiem].Value = string.Format("{0:#,0.####}", kq);
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
                sw.Close();
                MessageBox.Show("File lưu thành công!");
            }
        }

        
        private void minmaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int N = 0;//thuoc tinh nao duoc chon se co N=1
            int flag = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells["cl_Bin"].Value != null)
                {
                    flag = 1;
                    int.TryParse(dataGridView2.Rows[i].Cells["cl_Bin"].Value.ToString(), out N);
                    if(N != 1)
                    {
                        MessageBox.Show("Giá trị Bins không đúng. Vui lòng nhập lại!");
                        return;
                    }
                    if (kiemtraNumberic(i) == true)
                    {
                        //chia gio

                        chuanHoaMinMax(i);
                    }
                    else
                    {
                        MessageBox.Show("Thuộc tính này là Nominal.");
                        return;
                    }
                }

            }
            if (flag == 0)
            {
                MessageBox.Show("Nhập 'Giá trị Bins = 1' cho thuộc tính bạn muốn chuẩn hóa.");

                return;
            }
            resetChecked();
        }

        private void zscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int N = 0;//thuoc tinh nao duoc chon se co N=1
            int flag = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells["cl_Bin"].Value != null)
                {
                    flag = 1;
                    int.TryParse(dataGridView2.Rows[i].Cells["cl_Bin"].Value.ToString(), out N);
                    if (N != 1)
                    {
                        MessageBox.Show("Giá trị Bins không đúng. Vui lòng nhập lại!");
                        return;
                    }
                    if (kiemtraNumberic(i) == true)
                    {
                        //chia gio
                        chuanHoaZ_score(i);
                    }
                    else
                    {
                        MessageBox.Show("Thuộc tính này là Nominal.");
                        return;
                    }
                }

            }
            if (flag == 0)
            {
                MessageBox.Show("Nhập 'Giá trị Bins = 1' cho thuộc tính bạn muốn chuẩn hóa.");

                return;
            }
            resetChecked();            
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
