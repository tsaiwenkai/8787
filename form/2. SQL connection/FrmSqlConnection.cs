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
using System.Configuration;
using form.Properties;
using System.Threading;

namespace Starter

{
    public partial class FrmSqlConnection : Form
    {
        public FrmSqlConnection()
        {

            InitializeComponent();
            tabControl1.SelectedIndex = 1;
            tabPage1.BackColor = Settings.Default.MyColor;
            //----------------------------------------------------------------
            string[] city = { "Taipei", "Tokyo", "Sydeny" };
            for (int i = 0; i < city.Length; i++)
            {
               
                LinkLabel x = new LinkLabel();
                x.Text = city[i];
                
                x.Left = 5;
                x.Top = 30*i;
                x.Tag = i;

                x.Click += X_Click;
                panel1.Controls.Add(x);
            }
            
        }

        private void X_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(  ((LinkLabel)sender).Text +"-" + ((LinkLabel)sender).Tag);
            //                                    //sender 因為是OB,轉型前面+()
            LinkLabel x = sender as LinkLabel;
            //如果有轉成功X就!=null 反之失敗就=Null
            if (x != null)
            {
                MessageBox.Show(x.Text);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string con = "Data Source=.;Initial Catalog=Northwind;Integrated Security=True";
            //自己建立連接
            try
            {
                using (SqlConnection conn = new SqlConnection(con))// <=con等於上面宣告的字串
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string con = "Data Source=.;Initial Catalog=Northwind;User ID =sa;Password=sa";
            try
            {
                using (SqlConnection conn = new SqlConnection(con))        //語法糖 using(......) { Open.........}<=自動關連接
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}";

                        listBox1.Items.Add(s);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string con = ConfigurationManager.ConnectionStrings["form.Properties.Settings.NorthwindConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string con = Settings.Default.MyNorthwinf;
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.MyColor = tabPage1.BackColor = colorDialog1.Color;
                Settings.Default.Save();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Student\Source\Repos\tsaiwenkai\-\form\Database1.mdf;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(con))// <=con等於上面宣告的字串
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from MyTable", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["UserName"]} - {dataReader["PassWord"]}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(con))// <=con等於上面宣告的字串
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from MyTable", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["UserName"]} - {dataReader["PassWord"]}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            builder.AttachDBFilename=Application.StartupPath+@"\Database1.mdf";
            builder.IntegratedSecurity = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from MyTable", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["UserName"]} - {dataReader["PassWord"]}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string con = Settings.Default.NorthwindConnectionString;
            //自己建立連接
            try
            {
                using (SqlConnection conn = new SqlConnection(con))// <=con等於上面宣告的字串
                {
                    conn.StateChange += Conn_StateChange1;
                    conn.Open();
                    SqlCommand command = new SqlCommand("select*from products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}";
                        listBox2.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Conn_StateChange1(object sender, StateChangeEventArgs e)
        {
            //toolStripStatusLabel1.Text = e.CurrentState.ToString();
           statusStrip1.Items[0].Text= e.CurrentState.ToString();
            statusStrip1.Items[1].Text = DateTime.Now.ToLongTimeString();
            Application.DoEvents();
            Thread.Sleep(700);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Connection.StateChange += Conn_StateChange1;
            MessageBox.Show(productsTableAdapter1.Connection.ConnectionString);
            productsTableAdapter1.Fill(nwDataSet1.Products);
            dataGridView1.DataSource = nwDataSet1.Products;
        }


    }
}
