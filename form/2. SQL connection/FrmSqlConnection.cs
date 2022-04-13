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

namespace Starter

{
    public partial class FrmSqlConnection : Form
    {
        public FrmSqlConnection()
        {
            InitializeComponent();
            tabPage1.BackColor = Settings.Default.MyColor;
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
    }
}
