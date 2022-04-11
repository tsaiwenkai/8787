using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form
{
    public partial class fromtset : Form
    {
        public fromtset()
        {
            InitializeComponent();
        }
        string name = "";
        private void btnhel_Click(object sender, EventArgs e)
        {
            name = txtname.Text  ;
            MessageBox.Show("hello,  "+name);
            
        }

        private void btnhi_Click(object sender, EventArgs e)
        {
            name = txtname.Text;
            MessageBox.Show("hi,  " + name);
        }

        private void btnus_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Black;
            label1.ForeColor = Color.White;


            label1.BorderStyle = BorderStyle.Fixed3D;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Click += button3_Click;  //本身BTN3沒有設置click事件,用以下方法來實作
            button3.Click += aaa;
            button3.Enabled = true;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hahahahahahaha");

        }

        private void   aaa(object sender, EventArgs e)
        {
            MessageBox.Show("ohhhhhhh");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SystemInformation.ComputerName);

           // SystemInformation.ComputerName = "xxx";    //set information
           //string name = SystemInformation.ComputerName;   //get information

            btnhel.Text = "唉呦?";
            btnhi.Text = "想不到吧?";



        }

        private void button4_Click(object sender, EventArgs e)
        {
            //static

            File.Copy("a.txt", "a1.txt", true);

            //================
            //instance 

            FileInfo f = new FileInfo("b.txt");
            MessageBox.Show(f.FullName + "\n" + f.Extension + "\n" + f.CreationTime);
            f.CopyTo("b1.txt", true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //demo
            // Form1.text = "hello " + txtname.Text;  //form沒有實體 要建立一個實體

            //demo2
            //Form1 f = new Form1();
            //f.Text = "hellow " + txtname.Text;  //雖然form有實體 但只會改掉虛擬的form名子


           this.Text ="hello,  " + this.txtname.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
