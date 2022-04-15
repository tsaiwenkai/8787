using form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmConnected : Form
    {
        public FrmConnected()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 1;

            //把list view改成 Details
            this.listView1.View = View.Details;

            LoadCountryinCombobox();
            CreadListview();
        }

        private void CreadListview()
        {
             try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Customers", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                   DataTable dt= dataReader.GetSchemaTable();
                    dataGridView1.DataSource = dt;



                    //把資料裡面有幾個Row提出來
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        //List的Columns=資料表裡的 ROW [有幾個] [第幾行]
                        listView1.Columns.Add(dt.Rows[i][0].ToString());
                    }
                    // 資料裡的Columns自動等距     
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);  
                    


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadCountryinCombobox()
        {
           //Select
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select distinct Country from Customers", conn);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader["Country"]);
                    }
                    comboBox1.SelectedItem ="USA";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Customers where Country='comboBox1.Text' ", conn);
                    command.CommandText = $" select * from Customers where Country='{comboBox1.Text}' ";
                    command.Connection = conn;

                    SqlDataReader dataReader = command.ExecuteReader();

                    listView1.Items.Clear();

                    Random r = new Random();


                    while (dataReader.Read())
                    {
                        ListViewItem Lvi = listView1.Items.Add(dataReader[0].ToString());

                        //上面設一個random變數 讓下面圖示都做一個隨機變換
                        Lvi.ImageIndex = r.Next(0, ImageList1.Images.Count);


                        //每個item都有index屬性 為資料索引
                        if (Lvi.Index % 2 == 0)
                        {
                            Lvi.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            Lvi.BackColor = Color.LightSalmon;
                        }

                        for (int i = 1; i < dataReader.FieldCount; i++)
                        {
                            //如果data table 裡為空直
                            if (dataReader.IsDBNull(i))
                            {
                                Lvi.SubItems.Add("null");
                            }
                            else
                            {
                                Lvi.SubItems.Add(dataReader[i].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("xxx");
        }

        private void lrageIcanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void detailViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    SqlCommand command = new SqlCommand(); //("select distinct Country from Customers", conn);
                    command.CommandText = $"Insert into MyMember (UserName,Password) Values ('{U}','{P}')";
                    command.Connection = conn;
                    conn.Open();

                    command.ExecuteNonQuery();
                    MessageBox.Show("+Meber sucessfull");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    conn.Open();
                    SqlCommand command = new SqlCommand();// ("select distinct Country from Customers", conn);
                    command.CommandText = $"select * from MyMember where UserName='{U}' and PassWord='{P}'";
                    command.Connection = conn;

                    SqlDataReader dataReader = command.ExecuteReader();

                    //hasrows代表有沒有這個資料
                    if (dataReader.HasRows)
                    {
                        MessageBox.Show("登入成功");
                        
                    }
                    else
                    {
                        MessageBox.Show("登入失敗");
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
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    SqlCommand command = new SqlCommand(); //("select distinct Country from Customers", conn);
                    command.CommandText = $"Insert into MyMember (UserName,Password) Values (@UserName,@Password)";


                    //前面的部分是使用Para...add...的方式 把  U的變數帶入 @"UserName"裡
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value=U;
                    //command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value=P;
                    //分解示如下
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@PassWord";
                    p1.SqlDbType= SqlDbType.NVarChar;
                    p1.Size = 40;
                    p1.Value = P;
                    command.Parameters.Add(p1);

                    command.Connection = conn;
                    conn.Open();

                    command.ExecuteNonQuery();
                    MessageBox.Show("+Meber sucessfull");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    
                    SqlCommand command = new SqlCommand();// ("select distinct Country from Customers", conn);
                    command.CommandText = $"select * from MyMember where UserName=@UserName and PassWord=@Password";
                    command.Connection = conn;

                    //前面的部分是使用Para...add...的方式 把  U的變數帶入 @"UserName"裡
                    command.Parameters.Add("UserName", SqlDbType.NVarChar, 16).Value = U;
                    command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value = P;
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();

                    //hasrows代表有沒有這個資料
                    if (dataReader.HasRows)
                    {
                        MessageBox.Show("登入成功");

                    }
                    else
                    {
                        MessageBox.Show("登入失敗");
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
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    SqlCommand command = new SqlCommand();
                    command.CommandText ="InsertMember";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    conn.Open();
 
                    
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = U;
                    command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value=P;

                    //-----------------------------------------------------------
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@RETURN_VALUE";
                    p1.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(p1);
                    command.ExecuteNonQuery();
                    
                    MessageBox.Show("+Meber sucessfull   MBID="+p1.Value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Ir";
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.Add("UserName", SqlDbType.NVarChar, 16).Value = U;
                    command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value = P;
                    conn.Open();


                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        MessageBox.Show("登入成功");

                    }
                    else
                    {
                        MessageBox.Show("登入失敗");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
