using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


//doc file
	
	//m,n,U={};k
	//Luu Tru: strust hay matran?=>list
	//TinhKhoangCach => kiemtra Nominal
	//chon k mau gan U nhat
	//TinhTanSuat
	//chon TanSuat Max

	//Thong so can biet: k KhoangCach Min, thuoc tinh quyetdinh tuong ung



namespace PhanLop
{
    public partial class Form_XuLyDuLieu : Form
    {
        private List<List<string>> data = new List<List<string>>();
        private List<string> U = new List<string>();
        private int tongThuocTinh = 0;
        
        private List<Item> listKhoangCach = new List<Item>();

        public Form_XuLyDuLieu()
        {
            InitializeComponent();

        }               
        
        private void chọnDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)// Doc file
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.csv)|*.csv";
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileName;
                StreamReader reader = new StreamReader(path);
                string[] line=null;
                while (!reader.EndOfStream)
                {
                    List<string> chuoi = new List<string>();
                    line = reader.ReadLine().Split(',');
                    for (int i = 0; i < line.Length; i++)
                    {
                        chuoi.Add(line[i]);
                    }
                    data.Add(chuoi);
                }

                //lay thong tin Mau can phan lop (U)
                for (int i = 0; i < line.Length; i++)
                {
                    U.Add(line[i]);
                }
                data.RemoveAt(data.Count - 1);

                //show Mau U
                ShowU(U); 

                reader.Close();
                
                DuLieu();//Xuat du lieu
                ThongTin();
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
            //List<string> str = data.ElementAt(0);

            if (KiemTraLoaiDuLieu())
            {
                //ton tai thuoc tinh Nominal
                for (int i = 0; i < data.Count; i++)
                {
                    MessageBox.Show("Tồn tại dữ liệu kiểu Nominal, vui lòng thực hiện tiền xử lý dữ liệu.");
                    return;
                }
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    KhoangCachEuclide(i, U);
                    listKhoangCach.ElementAt(listKhoangCach.Count - 1).AttributeTOP = (data.ElementAt(i)).ElementAt(data.ElementAt(i).Count - 1);
                    //MessageBox.Show(listKhoangCach.ElementAt(listKhoangCach.Count - 1).AttributeTOP);
                    dataGridView2.Rows.Add(i + 1, "D" + (i + 1), string.Format("{0:#,0.####}", listKhoangCach.ElementAt(listKhoangCach.Count-1).Distance));
                }
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

        private void ShowU(List<string> U)
        {
            string str = "U = {" + U.ElementAt(0);
            for (int i = 1; i < U.Count; i++)
            {
                str += " , " + U.ElementAt(i);
            }
            str += " }";
            label1.Text = str;
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

        private bool KiemTraLoaiDuLieu()//true neu ton tai Nominal
        {
            for (int i = 0; i < tongThuocTinh-1; i++)//tach ham do tranh chap dau "?"
            {
                if (kiemtraNumberic(i) ==false)
                {
                    return true;
                }
            }
            return false;
        }

        private void KhoangCachEuclide(int vt, List<string> U)
        {
            List<string> arr = data.ElementAt(vt);
            Item d = new Item();
            double a,b;
            double sum = 0;

            d.Index = vt;
            d.Distance = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                double.TryParse(arr.ElementAt(i), out a);
                double.TryParse(U.ElementAt(i), out b);
                sum += Math.Pow(a - b, 2);
            }
            d.Distance = Math.Sqrt(sum);

            listKhoangCach.Add(d);
        }

        
        private void Sort()
        {
            Item temp = new Item();
            for (int i = 0; i < listKhoangCach.Count-1; i++)
            {
                for (int j = i + 1; j < listKhoangCach.Count; j++)
                {
                    if (listKhoangCach[i].Distance > listKhoangCach[j].Distance)
                    {
                        temp.Init(listKhoangCach[i]);
                        listKhoangCach[i].Init(listKhoangCach[j]);
                        listKhoangCach[j].Init(temp);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //nhan so k-nearist neighbor
            int K =0;
            int.TryParse(textBox1.Text,out K);

            //sap tang khoang cach
            Sort();
            dataGridView2.Rows.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                dataGridView2.Rows.Add(i + 1, "D" + (listKhoangCach.ElementAt(i).Index +1), string.Format("{0:#,0.####}", listKhoangCach.ElementAt(i).Distance));
                
            }

            //lay k-nearist neighbor
            string thuoctinh = TanXuatMax(K);
            

            //gan thuoc tinh phan lop cho mau U
            U.RemoveAt(U.Count - 1);
            U.Add(thuoctinh);
            ShowU(U);

        }

        private int TanXuat(string value, int K)
        {
            int count = 0;
            for (int i = 0; i < K; i++)
            {
                if (listKhoangCach.ElementAt(i).AttributeTOP.CompareTo(value) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        private string TanXuatMax(int K)
        {
            int max = 0;
            string str = null;
            for (int i = 0; i < K; i++)
            {
                int temp = TanXuat(listKhoangCach.ElementAt(i).AttributeTOP, K);
                if (temp > max)
                {
                    max = temp;
                    str = listKhoangCach.ElementAt(i).AttributeTOP;
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

               
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
