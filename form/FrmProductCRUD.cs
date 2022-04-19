using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form
{
    public partial class FrmProductCRUD : Form
    {
        public FrmProductCRUD()
        {
            InitializeComponent();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.nWDataSet);

        }

        private void FrmProductCRUD_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'nWDataSet.Products' 資料表。您可以視需要進行移動或移除。
            this.productsTableAdapter.Fill(this.nWDataSet.Products);

        }

        private void Button15_Click(object sender, EventArgs e)
        {
            productsBindingSource.Position += 1;
        }
        bool x = false;
        private void Button23_Click(object sender, EventArgs e)
        {
            if (x)
            {
                productsBindingSource.Sort = "ProductID Asc";
            }
            else
            {
                productsBindingSource.Sort = "ProductID Desc";
            }
            x = !x;
        }

        private void Button22_Click(object sender, EventArgs e)
        {

            
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            productsBindingSource.Filter = "[UnitPrice]>30";
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            productsBindingSource.RemoveAt(productsBindingSource.Position);
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            Frmdialog f = new Frmdialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("ok");
                NWDataSet.ProductsRow  productRow=nWDataSet.Products.NewProductsRow();
                productRow.ProductName = f.textBox1.Text;
                productRow.Discontinued = f.checkBox1.Checked;

                nWDataSet.Products.AddProductsRow(productRow);
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }
    }
}
