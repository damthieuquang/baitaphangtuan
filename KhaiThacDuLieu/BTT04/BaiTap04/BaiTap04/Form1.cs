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


                String[] title = null;
                String temp = null;
                do
                {
                    str = reader.ReadLine();
                    //Neu dong do ton tai khoang trang va @
                    if (str.Contains('@') && str.Contains(' '))
                    {
                        //Thuc hien cat chuoi
                        title = str.Split(' ');
                        //Dua tieu de vao dataset
                        ds.Tables["data"].Columns.Add(title[1]);

                        temp = title[2];
                    }
                } while (str.Length == 0 || str.Contains(' '));

                String[] distinct = temp.Split(",".ToCharArray());

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
                //Lay so lop cua du lieu
                k = distinct.Length;                
            }
        }
        /************************************************************************/
        /* Tinh khoang cach giua 2 diem du lieu                                                                     */
        /************************************************************************/
        private double DistanceMinkowski(DataRow dr1, DataRow dr2, int q)
        {
            double result = 0;
            
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
            //Lay gia tri ngau nhien
            List<Cluster> listCluster = new List<Cluster>();
            for (int i = 0; i < k; i++)
            {
                Cluster dr = new Cluster();

                dr.Point = ds.Tables["data"].Rows[i];

                listCluster.Add(dr);
            }
            do
            {
                //Duyet het tung dong trong du lieu
                for(int j=0;j<ds.Tables["data"].Rows.Count; j++)
                {
                    DataRow datarow = ds.Tables["data"].Rows[j];
                    //Tinh khoang cach                    
                    int min = 0;// DistanceMinkowski(listCluster[0].Point, datarow, 2);
                    for (int i = 0; i < listCluster.Count; i++)
                    {
                        double rs = DistanceMinkowski(listCluster[i].Point, datarow, 2);
                        if (rs < DistanceMinkowski(listCluster[min].Point, datarow, 2))
                            min = i;
                    }
                    for (int m = 0; m < ds.Tables["data"].Columns.Count;m++ )
                    {
                        
                        //listCluster[min].Mean[m]= datarow[m];
                        
                        MessageBox.Show(datarow[0].ToString()+" "+ datarow[1].ToString());
                    }
                    listCluster[min].Flag = new List<int>();
                    listCluster[min].Flag.Add(j);
                       
                    //foreach (Cluster dtr in listCluster)
                    //{
                    //    double rs = DistanceMinkowski(dtr.Point, datarow, 2);
                    //    result.Add(rs);                        
                    //}
                    //dua diem gan tam thu k ve cum do
                }
                //Tinh trung binh moi cho cac cum
            }while(true);            

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
            
            Cluster();
        }
    }
}
