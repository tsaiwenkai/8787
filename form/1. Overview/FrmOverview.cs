using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace form._1._Overview
{
    public partial class FrmOverview : Form
    {
        public FrmOverview()
        {
            InitializeComponent();
            tabControl1.SelectedIndex =2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Setp1.Connection
            //Setp2.Command
            //Setp3.Reader
            //Setp4.UI Control

            SqlConnection conn = null;  //把變數宣告在外面,才能判斷最後不管有沒有expception 都能close
            try
            {
                //1.
                //SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn=new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn.Open();

                //2.  執行SQL程序 Select*From......., 後面參數為連接的資料庫
                SqlCommand command = new SqlCommand("select*from products", conn);


                //3   去執行上列執行碼
                SqlDataReader dataReader = command.ExecuteReader();

                //dataReader.Read();   //初始是0 要讓他搜尋下一列
                //dataReader.Read() 為布林直
                while (dataReader.Read()) //如果dataReader.Read()為true 則執行下列  flose 則跳出迴圈  true則有多數資料
                {

                    //  listBox1.Items.Add(dataReader["ProductName"]+dataReader["UnitPrice"].ToString());

                    //以下格式化
                    string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}"; 
                    //$"{裡面放你要搜尋的欄位},(-40)欄裡面你要給她的空間,靠左對齊," : 轉" "C貨幣" "2小數2位" "
                    listBox1.Items.Add(s);
                }

              //  MessageBox.Show(dataReader["ProductName"].ToString());  //要搜尋的欄位

           
             //  conn.Close();
              //  MessageBox.Show("successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //不管有沒有Exception都要關連結
                if (conn != null)
                    conn.Close();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Setp1.Connection
            //Setp2.data adapter
            //Setp3.dataset
            //Setp4.UI Control


            //1 第1步一樣建立連結
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");

            //2 一個是Command 一個是Adapter
            SqlDataAdapter adapter = new SqlDataAdapter("select*from products", conn);

            //3 因為Dataset需要一個參數為dataset的變數
            DataSet ds = new DataSet();

            //4 Auto =>SqlCommand =>dataReader
            adapter.Fill(ds);


            //5 UI直接Show出來 ds 為Dataset出資料  EX:ds裡面table[0]
            this.dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //test

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("select*from [dbo].[Categories]", conn);
            DataSet sd = new DataSet();
            adapter.Fill(sd);
            this.dataGridView1.DataSource = sd.Tables[0];

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("select*from products where[UnitPrice]>30 order by[UnitPrice]", conn);
            DataSet sd = new DataSet();
            adapter.Fill(sd);
            this.dataGridView1.DataSource = sd.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //productsTableAdapter1為建立的容器 fill 把後面的資料裝進容器.nwDataSet1.Products
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            //dataGridView2.DataSource為IU容器nwDataSet1.Products show出來
            this.dataGridView2.DataSource=this.nwDataSet1.Products;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            dataGridView2.DataSource = nwDataSet1.Categories;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customersTableAdapter1.Fill(nwDataSet1.Customers);
            dataGridView2.DataSource = nwDataSet1.Customers;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.FillByUnitsPrice(nwDataSet1.Products, 30);
            dataGridView2.DataSource = nwDataSet1.Products;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.InsertProducts("xxx", true);
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Update(nwDataSet1.Products);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //productsTableAdapter1.Fill(nwDataSet1.Products);
            //bindingSource1.DataSource = nwDataSet1.Products;//為ROW
            //dataGridView3.DataSource = bindingSource1;

            // load在按下之後position就=1了,所以執行下列方法label就會顯示出來
            //showP();
            //-----------------------------------------------------

            categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            bindingSource1.DataSource = nwDataSet1.Categories;
            dataGridView3.DataSource = bindingSource1;


            textBox1.DataBindings.Add("Text", bindingSource1, "CategoryName");
                                                      //Text為TB的屬性 , BS為連繫的物件  ,最後為哪個欄位
            pictureBox1.DataBindings.Add("Image", bindingSource1, "Picture", true);
            //同上最後布林值為轉換圖片的個視為true

            bindingNavigator1.BindingSource = bindingSource1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bindingSource1.Position -= 1;
            //showP();

        }




        private void button13_Click(object sender, EventArgs e)
        {
            bindingSource1.Position += 1;
            //showP();    
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bindingSource1.Position = 0;
            //showP();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bindingSource1.Position = nwDataSet1.Categories.Count-1;
            //showP();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            label2.Text = $"{bindingSource1.Position + 1}/{nwDataSet1.Categories.Count}";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Frmtool fw = new Frmtool();
            fw.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            this.categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            this.customersTableAdapter1.Fill(nwDataSet1.Customers);

            dataGridView4.DataSource = nwDataSet1.Products;
            dataGridView5.DataSource = nwDataSet1.Categories;
            dataGridView6.DataSource = nwDataSet1.Customers;
            //以上把資料庫裏面的Row秀出來
            //-----------------------------------------------------------
            //再來把Listbox把Table裡的書性秀出你要的資料

            listBox2.Items.Clear();
            //迴圈裡放TABEL有幾個TAB
            for(int i=0; i <= nwDataSet1.Tables.Count - 1; i++)
            {
                DataTable table = nwDataSet1.Tables[i];
                listBox2.Items.Add(table.TableName);


                string s = "";
                for(int column =0; column <= table.Columns.Count - 1; column++)
                {
                    //DataColumn columns = table.Columns[column];
                    //s += columns.ColumnName + "  ";
                    s += table.Columns[column]+"\t";

                }
                listBox2.Items.Add(s);
                //----------------------------------------
                string x = "";
                for(int row = 0; row <= table.Rows.Count - 1; row++)
                {
                    x = "";
                    for(int j = 0; j <= table.Columns.Count - 1; j++)
                    {

                        x += table.Rows[row][j] + "\t";
                        
                    }
                    listBox2.Items.Add(x);
                }
                
                listBox2.Items.Add("--------------------------------------------------------");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show(nwDataSet1.Products.Rows[0]["ProductName"].ToString());
                                               //伺服器           資料表         [第幾個ROW][第幾個欄位]
            MessageBox.Show(nwDataSet1.Products.Rows[0][1].ToString());
            //------------------------------------同上
            MessageBox.Show(nwDataSet1.Products[0].ProductName);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            nwDataSet1.Products.WriteXml("Products.xml", XmlWriteMode.WriteSchema);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            nwDataSet1.Products.Clear();
            nwDataSet1.Products.ReadXml("Products.xml");
            dataGridView4.DataSource = nwDataSet1.Products;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed =! splitContainer2.Panel1Collapsed;
        }
    }
}
