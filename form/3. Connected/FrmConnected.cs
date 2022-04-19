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
            tabControl1.SelectedIndex = 4;

            //把list view改成 Details
            this.listView1.View = View.Details;

            LoadCountryinCombobox();
            CreadListview();

            this.pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += PictureBox1_DragEnter;
            pictureBox1.DragDrop += PictureBox1_DragDrop;
            //------------------------------
            flowLayoutPanel1.AllowDrop = true;
            flowLayoutPanel1.DragEnter += FlowLayoutPanel1_DragEnter;
            flowLayoutPanel1.DragDrop += FlowLayoutPanel1_DragDrop;


        }

        private void FlowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            for(int i = 0; i < file.Length; i++)
            {
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(file[i]);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Width = 120;
                pic.Height =120;


                flowLayoutPanel1.Controls.Add(pic);
            }
        }

        private void FlowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect=DragDropEffects.Copy;
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {

            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            pictureBox1.Image = Image.FromFile(file[0]);
        }


        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {

            e.Effect = DragDropEffects.Copy;       
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
                    DataTable dt = dataReader.GetSchemaTable();
                    dataGridView1.DataSource = dt;



                    //把資料裡面有幾個Row提出來
                    for (int i = 0; i < dt.Rows.Count; i++)
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
                    comboBox1.SelectedItem = "USA";
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
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = U;
                    //command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value=P;
                    //分解示如下
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@PassWord";
                    p1.SqlDbType = SqlDbType.NVarChar;
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
                    command.CommandText = "InsertMember";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    conn.Open();


                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = U;
                    command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value = P;

                    //-----------------------------------------------------------
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@RETURN_VALUE";
                    p1.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(p1);
                    command.ExecuteNonQuery();

                    MessageBox.Show("+Meber sucessfull   MBID=" + p1.Value);
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

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string U = textBox1.Text;
                    string P = textBox2.Text;
                    P = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(P, "sha1");
                    SqlCommand command = new SqlCommand(); //("select distinct Country from Customers", conn);
                    command.CommandText = $"Insert into MyMember (UserName,Password) Values (@UserName,@Password)";


                    //前面的部分是使用Para...add...的方式 把  U的變數帶入 @"UserName"裡
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = U;
                    //command.Parameters.Add("PassWord", SqlDbType.NVarChar, 40).Value=P;
                    //分解示如下
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@PassWord";
                    p1.SqlDbType = SqlDbType.NVarChar;
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

        private void button12_Click(object sender, EventArgs e)
        {
            this.myMemberTableAdapter1.Insert("XXX", "xxx");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    conn.Open();

                    comm.CommandText = "Select sum(UnitPrice) from Products";
                    listBox2.Items.Add($"UnitPrice Min=  {comm.ExecuteScalar():c2}");

                    comm.CommandText = "Select Max(UnitPrice) from Products";
                    listBox2.Items.Add($"UnitPrice Min=  {comm.ExecuteScalar():c2}");

                    comm.CommandText = "Select Min(UnitPrice) from Products";
                    listBox2.Items.Add($"UnitPrice Min=  {comm.ExecuteScalar():c2}");

                    comm.CommandText = "Select Avg(UnitPrice) from Products";
                    listBox2.Items.Add($"UnitPrice Avg=  {comm.ExecuteScalar():c2}");

                    comm.CommandText = "Select Count(*) from Products";
                    listBox2.Items.Add($"UnitPrice Count=  {comm.ExecuteScalar()}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "Select * from Categories;Select * from Products";
                    comm.Connection = conn;
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["CategoryName"]);
                    }
                    //-------------------------


                    reader.NextResult();
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader["ProductName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string Sqlcommand = " CREATE TABLE[dbo].[MyImageTable](   " +
                "[ImageID][int] IDENTITY(1, 1) NOT NULL,   " +
                "[Description] [text] NULL,	" +
                "[Image] [image] NULL, " +
                "CONSTRAINT[PK_MyImageTable] " +
                "PRIMARY KEY CLUSTERED(   [ImageID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = Sqlcommand;
                    comm.Connection = conn;
                    conn.Open();

                    comm.ExecuteNonQuery();
                    MessageBox.Show("Cread MyImage Successful");

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.jpg)|*.jpg";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image =Image.FromFile (this.openFileDialog1.FileName);
            }
           
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {               
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"Insert into MyImageTable (Description,Image) Values (@Desc,@Image)";

                    //----------------------------------
                    //下列陣列裡放圖片2進制 圖片的資料型態是陣列
                    byte[] bytes;//= { 1, 3 };

                    //先做一個放資料的記憶體
                    System.IO.MemoryStream da = new System.IO.MemoryStream();

                    //把Picturebox裡的圖片存在剛建立的記憶體裡(記憶體,指定資料型態)
                    pictureBox1.Image.Save(da, System.Drawing.Imaging.ImageFormat.Jpeg);

                   //剛剛建立的陣列從記憶體裡getbuffer出來
                    bytes = da.GetBuffer();
                    //--------------------------------------

                    command.Parameters.Add("@Desc", SqlDbType.Text).Value = this.textBox4.Text;
                     
                    command.Parameters.Add("@Image", SqlDbType.Image).Value = bytes;  //bytes變數為圖片2進制格式為陣列
                 

                    command.Connection = conn;
                    conn.Open();

                    command.ExecuteNonQuery();
                    MessageBox.Show("+ Image sucessfull");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "Select * from MyImageTable";
                    comm.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();

                    listBox3.Items.Clear();
                    listBox4.Items.Clear();
                    while (reader.Read())
                    {
                        listBox3.Items.Add(reader["Description"]);

                        //lbx4來放ID方便查詢圖片
                        listBox4.Items.Add(reader["ImageID"]);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //做一個int 變數來做下列方式查詢      //lB4 Item裡面是放 IMG ID所以後面索引是要取LB3的索引
            int image = (int)listBox4.Items[listBox3.SelectedIndex];
            ShowImage(image);
        }

        private void ShowImage(int image)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"Select*from MyImageTable where ImageID={image}";
                    command.Connection = conn;

                    conn.Open();

                    SqlDataReader reader= command.ExecuteReader();
                    reader.Read();
                   // if (reader.HasRows)
                   // {

                        byte[] bytes = (byte[])reader["Image"];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                        pictureBox2.Image = Image.FromStream(ms);
                      
                   // }
                  //  else
                 //   {
                 //     pictureBox2.Image = pictureBox2.ErrorImage;                      
                 //   }              
                }
            }
            catch
            {
                pictureBox2.Image = pictureBox2.ErrorImage;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "Select * from MyImageTable";
                    comm.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();

                    listBox5.Items.Clear();
                    while (reader.Read())
                    {
                        MyImage myImage = new MyImage();
                        myImage.Description = reader["Description"].ToString();
                        myImage.Image = (int)reader["ImageID"] ;
                        listBox5.Items.Add(myImage);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //宣告另一個類別來放參數
            //lisB5裡面add類別
            //int 變數裡放 lis5裡的selecteditem 要轉成 Myimage型別的
            int image = ((MyImage)listBox5.SelectedItem).Image;
            ShowImage(image);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand();

                    command.Connection = conn;
                    conn.Open();
                    command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (100,'xxx')";
                    command.ExecuteNonQuery();

                    //command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (100,'asdasd')";
                    //command.ExecuteNonQuery();

                    command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (101,'yyy')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("+ Region sucessfull");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            SqlTransaction txn = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    // SqlConnection conn = new SqlConnection();

                    SqlCommand command = new SqlCommand();

                    command.Connection = conn;
                    conn.Open();

                    txn = conn.BeginTransaction();
                    command.Transaction = txn;

                    command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (100,'xxx')";
                    command.ExecuteNonQuery();

                    //command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (100,'asdasd')";
                    //command.ExecuteNonQuery();

                    command.CommandText = $"Insert into Region (RegionID,RegionDescription) Values (101,'yyy')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("+ Region sucessfull");


                    txn.Commit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txn.Rollback();
            }
        }
    }
    class MyImage
    {
       internal string Description;
       internal int Image;

    }
        

}
