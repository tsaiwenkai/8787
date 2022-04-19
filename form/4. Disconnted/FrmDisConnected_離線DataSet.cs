using form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmDisConnected_離線DataSet : Form
    {
        public FrmDisConnected_離線DataSet()
        {
            InitializeComponent();
            categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            productsTableAdapter1.Fill(nwDataSet1.Products);
            dataGridView7.DataSource = nwDataSet1.Products;
            
        }

        private void Button30_Click(object sender, EventArgs e)
        {

            //dataGridView1.DataSource = nwDataSet1.Categories;
            dataGridView1.DataSource = nwDataSet1;
            dataGridView1.DataMember = "Categories";
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            dataGridView7.AllowUserToAddRows = false;


        }

        private void Button28_Click(object sender, EventArgs e)
        {
            dataGridView7.Rows[1].Frozen = true;
            dataGridView7.Columns[1].Frozen = true;
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.dataGridView7.CurrentCell.Value.ToString());
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.dataGridView7.CurrentRow.Cells[1].Value.ToString());
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) 
            {
                int ProductID = (int)this.dataGridView7.CurrentRow.Cells["productID"].Value;
                //MessageBox.Show("ProductID= " + ProductID);
                FrmProductDetail f = new FrmProductDetail();
                f.Text = ProductID.ToString();
                f.ProuductID = ProductID;
                f.Show();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(nwDataSet1.Products);
            dataGridView8.DataSource = nwDataSet1.Products;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataColumn totel = new DataColumn("TotelPrice", typeof(decimal));
            totel.Expression = "[UnitPrice]*[UnitsInStock]";

            nwDataSet1.Products.Columns.Add(totel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmProductCRUD f = new FrmProductCRUD();
            f.Show();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            
        }
    }
}
