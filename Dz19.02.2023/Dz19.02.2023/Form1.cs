using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dz19._02._2023
{
    public partial class Form1 : Form
    {
        DataSet dataset = new DataSet();
        SqlConnection connection;
        SqlDataAdapter adapter1, adapter2, adapter3;
        SqlCommandBuilder build1, build2, build3;
        public Form1()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("settings.json");
            var config = builder.Build();
            string? connectstr = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectstr);
        }
        private void but_create_Click(object sender, EventArgs e)
        {
            try
            {
                adapter1 = new SqlDataAdapter("select * from Products", connection);
                build1 = new SqlCommandBuilder(adapter1);
                MessageBox.Show(build1.GetUpdateCommand().CommandText);
                MessageBox.Show(build1.GetInsertCommand().CommandText);
                MessageBox.Show(build1.GetDeleteCommand().CommandText);
                DataTable products = dataset.Tables.Add("Products");
                products.Columns.Add("id", typeof(int));
                products.Columns.Add("title", typeof(string));
                products.Columns.Add("type", typeof(string));
                products.Columns.Add("price", typeof(int));
                products.Constraints.Add("PK_Products", products.Columns["id"], true);
                products.Columns["id"].AutoIncrement = true;
                products.Columns["id"].AllowDBNull = false;
                products.Columns["title"].AllowDBNull = false;
                products.Columns["type"].AllowDBNull = false;
                products.Columns["price"].AllowDBNull = false;
                adapter1.Fill(dataset, "Products");
                DataGrid1.DataSource = products;

                adapter2 = new SqlDataAdapter("select * from Providers", connection);
                build2 = new SqlCommandBuilder(adapter2);
                DataTable providers = dataset.Tables.Add("Providers");
                providers.Columns.Add("id", typeof(int));
                providers.Columns.Add("title", typeof(string));
                providers.Constraints.Add("PK_Provider", providers.Columns["id"], true);
                providers.Columns["id"].AutoIncrement = true;
                providers.Columns["id"].AllowDBNull = false;
                providers.Columns["title"].AllowDBNull = false;
                adapter2.Fill(dataset, "Providers");
                DataGrid2.DataSource = providers;

                adapter3 = new SqlDataAdapter("select * from Delivery", connection);
                build3 = new SqlCommandBuilder(adapter3);
                DataTable delivery = dataset.Tables.Add("Delivery");
                delivery.Columns.Add("id", typeof(int));
                delivery.Columns.Add("product_id", typeof(int));
                delivery.Columns.Add("provider_id", typeof(int));
                delivery.Constraints.Add("PK_Delivery", delivery.Columns["id"], true);
                delivery.Columns["id"].AutoIncrement = true;
                delivery.Columns["id"].AllowDBNull = false;
                delivery.Columns["product_id"].AllowDBNull = false;
                delivery.Columns["provider_id"].AllowDBNull = false;
                ForeignKeyConstraint FK_Products =
                    new ForeignKeyConstraint("FK_Products", products.Columns["id"], delivery.Columns["product_id"]);
                FK_Products.DeleteRule = Rule.Cascade;
                FK_Products.UpdateRule = Rule.Cascade;
                ForeignKeyConstraint FK_Providers =
                    new ForeignKeyConstraint("FK_Providers", providers.Columns["id"], delivery.Columns["provider_id"]);
                FK_Providers.DeleteRule = Rule.Cascade;
                FK_Providers.UpdateRule = Rule.Cascade;
                delivery.Constraints.Add(FK_Products);
                delivery.Constraints.Add(FK_Providers);
                delivery.Columns.Add("quantity", typeof(int));
                delivery.Columns["quantity"].AllowDBNull = false;
                delivery.Columns.Add("delivery_date", typeof(DateTime));
                delivery.Columns["delivery_date"].AllowDBNull = false;
                adapter3.Fill(dataset, "Delivery");
                DataGrid3.DataSource = dataset.Tables["Delivery"];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ProductsTitle = { "Steak", "Butter", "Battery" };
                string[] ProductsType = { "Meal", "Food", "Electrician" };
                int[] PdoductsPrice = { 1, 2, 3 };
                for (int i = 0; i < 3; i++)
                {
                    DataRow row = dataset.Tables["Products"].NewRow();
                    row["title"] = ProductsTitle[i];
                    row["type"] = ProductsType[i];
                    row["price"] = PdoductsPrice[i];
                    dataset.Tables["Products"].Rows.Add(row);
                }
                DataGrid1.Refresh();
                adapter1.Update(dataset, "Products");

                string[] ProvidersTitle = { "Metalind", "Ultracraft", "Moneymake" };
                for (int i = 0; i < 3; i++)
                {
                    DataRow row = dataset.Tables["Providers"].NewRow();
                    row["title"] = ProvidersTitle[i];
                    dataset.Tables["Providers"].Rows.Add(row);
                }
                DataGrid2.Refresh();
                adapter2.Update(dataset, "Providers");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow1 = DataGrid1.SelectedRows;
                if (selectedRow1.Count == 0)
                {
                    MessageBox.Show("Не выбрана запись!");
                    return;
                }
                foreach (DataGridViewRow row in selectedRow1)
                {
                    int Id = (int)row.Cells[0].Value;
                    dataset.Tables["Products"].Rows[Id].Delete();
                }
                DataGrid1.Refresh();
                adapter1.Update(dataset, "customers");
                DataGrid3.Refresh();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow1 = DataGrid1.SelectedRows;
                if (selectedRow1.Count == 0)
                {
                    MessageBox.Show("Не выбрана запись!");
                    return;
                }
                DataRow[] ar;
                foreach (DataGridViewRow row in selectedRow1)
                {
                    int Id = (int)row.Cells[0].Value;
                    ar = dataset.Tables["Providers"].Select("id = " + Id);
                    ar[0]["title"] = "newname";
                }
                DataGrid2.Refresh();
                adapter2.Update(dataset, "Providers");
            }
            catch (Exception ex){MessageBox.Show(ex.Message);}
        }
    }
}
