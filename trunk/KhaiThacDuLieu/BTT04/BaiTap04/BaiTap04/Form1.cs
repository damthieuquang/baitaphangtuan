using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BaiTap04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Dung de chua du lieu doc tu file
        private static DataSet ds = new DataSet();
        //Mac dinh k se bang 2
        private static int k = 2;

        /************************************************************************/
        /* Doc file va luu vao dataset                                                                     */
        /************************************************************************/
        private void btn_Open_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.arff)|*.arff";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileName;
                StreamReader reader = new StreamReader(path);
                //Khai bao table trong dataset 
                ds.Tables.Add("data");

                //Doc dong dau tien
                string str = reader.ReadLine();

                do
                {
                    str = reader.ReadLine();
                } while (str.IndexOf('@') != 0);//Kiem tra xem dong do co chua ki tu @

                do
                {
                    str = reader.ReadLine();
                    //Neu dong do ton tai khoang trang va @
                    if (str.Contains('@') && str.Contains(' '))
                    {
                        //Thuc hien cat chuoi
                        String[] title = str.Split(' ');
                        //Dua tieu de vao dataset
                        ds.Tables["data"].Columns.Add(title[1]);
                    }
                } while (str.Length == 0 || str.Contains(' '));

                //Doc den het file
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    if (str.Contains(','))
                    {
                        String[] items = str.Split(",".ToCharArray());
                        ds.Tables["data"].Rows.Add(items);
                    }
                }
                reader.Close();
                //DataRow dr = ds.Tables["data"].Rows[0];

                //textBox_ClusterOuput.Text = dr[0].ToString();
                //dataGridView1.DataSource = ds.Tables["data"].DefaultView;
            }
        }
        /************************************************************************/
        /* Tinh khoang cach giua 2 diem du lieu                                                                     */
        /************************************************************************/
        private double DistanceMinkowski(DataRow dr1, DataRow dr2, int q)
        {
            double result = -1;
            //Lay so cot
            int numcol = ds.Tables["data"].Columns.Count;

            for (int i = 0; i < numcol; i++)
            {
                double out1, out2;

                //Chuyen thanh kieu double
                out1 = double.Parse(dr1[i].ToString());
                out2 = double.Parse(dr2[i].ToString());

                result += Math.Pow(out1 - out2, q);//Truong hop q = 1 la Manhattan, q = 2 la Euclide, q > 2 Minkowski
            }
            //Lay can bat q
            result = Math.Pow(result, 1 / (q * 1.0));

            return result;
        }

        /************************************************************************/
        /* Kiem tra du lieu co ton tai hay khong                                                                     */
        /************************************************************************/
        private bool CheckData()
        {
            bool result = true;

            if (ds.Tables["data"] == null)
                result = false;

            return result;
        }
        /************************************************************************/
        /* Phan lop du lieu                                                                     */
        /************************************************************************/
        private void Cluster()
        {
            //Lay so cum
            k = int.Parse(txt_NumK.ToString());

            //Lay gia tri ngau nhien
            DataRow k1, k2, k3;
            k1 = ds.Tables["data"].Rows[0];
            k2 = ds.Tables["data"].Rows[1];
            k3 = ds.Tables["data"].Rows[2];

            //Duyet het tung dong trong du lieu
            foreach (DataRow datarow in ds.Tables["data"].Rows)
            {
                double rs1, rs2, rs3;
                //Tinh khoang cach
                rs1 = DistanceMinkowski(k1, datarow, 2);
                rs2 = DistanceMinkowski(k2, datarow, 2);
                rs3 = DistanceMinkowski(k3, datarow, 2);

                
            }
        }

        /************************************************************************/
        /* Thuc hien tinh K-Means                                                                     */
        /************************************************************************/
        private void button_Start_Click(object sender, EventArgs e)
        {
            //Kiem tra du lieu
            if (!CheckData())
            {
                MessageBox.Show("Chưa nhập dữ liệu!");
                return;
            }
            
        }
    }
}
