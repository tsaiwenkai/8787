using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form._1._Overview
{
    public partial class Frmtool : Form
    {
        public Frmtool()
        {
            InitializeComponent();
        }

        private void categoriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.categoriesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.nWDataSet);

        }

        private void Frmtool_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'nWDataSet.Categories' 資料表。您可以視需要進行移動或移除。
            this.categoriesTableAdapter.Fill(this.nWDataSet.Categories);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result=openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                picturePictureBox.Image = Image.FromFile(openFileDialog1.FileName);
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DialogResult color=colorDialog1.ShowDialog();
            //if (color == DialogResult.OK)
            //{
            //    this.BackColor = colorDialog1.Color;
            //}
            //else 
            //{
            //    return;
            //}


            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;
            }
        }
    }
}
