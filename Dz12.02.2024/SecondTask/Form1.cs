using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace SecondTask {
    public partial class Form1 : Form
    {
        SqlConnection connect;
        SqlCommand command;
        string? connection;
        int value;
        public Form1()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("settings.json");
            var config = builder.Build();
            connection = config.GetConnectionString("DefaultConnection");
        }
        private async void btn_connect_Click(object sender, EventArgs e)
        {
            connect = new SqlConnection(connection);
            command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                btn_execute.Enabled = true;
                MessageBox.Show("Подключение к БД прошло успешно!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка подключения к БД: " + ex.Message); }
        }
        private async void btn_disconnect_Click(object sender, EventArgs e)
        {
            DataGrid.DataSource = null;
            DataGrid.Refresh();
            btn_execute.Enabled = false;
            await command.DisposeAsync();
            await connect.CloseAsync();
            MessageBox.Show("Отключено от БД.");
        }
        private async void ExecuteCommand()
        {
            SqlDataReader reader = await command.ExecuteReaderAsync();
            try
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid.DataSource = dt;
                await reader.CloseAsync();
            }
            catch (Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
        }
        private void menu_click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                string? itemName = menuItem.Name;
                switch (itemName)
                {
                    case "menu10":
                        command.CommandText = "SELECT Products.id AS Product_ID, " +
                        "Products.title AS ProductTitle, Products.type AS ProductType, Products.cost_price AS CostPrice," +
                        "Providers.id as Provider_ID, Providers.title AS ProviderTitle, " +
                        "Delivery.quantity AS Quantity, Delivery.delivery_date AS DeliveryDate " +
                        "FROM Products " +
                        "LEFT JOIN Delivery ON Products.id = Delivery.product_id " +
                        "LEFT JOIN Providers ON Providers.id = Delivery.provider_id;";
                        break;
                    case "menu1":
                        text1.Enabled = text2.Enabled = text3.Enabled = btn_add.Enabled = true;
                        text1.Text = "Название продукта";
                        text2.Text = "Тип продукта";
                        text3.Text = "0";
                        value = 1;
                        break;
                    case "menu2":
                        text1.Enabled = btn_add.Enabled = true;
                        text1.Text = "Название поставщика";
                        value = 2;
                        break;
                    case "menu3":
                        text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = btn_add.Enabled = true;
                        text1.Text = text2.Text = text3.Text = "0";
                        text4.Text = "01-01-1753";
                        value = 3;
                        break;
                    case "menu4":
                        command.CommandText = "UPDATE Products SET title = title, type = type, cost_price = cost_price";
                        break;
                    case "menu5":
                        command.CommandText = "UPDATE Providers SET title = title";
                        break;
                    case "menu6":
                        command.CommandText = "UPDATE Delivery SET product_id = product_id, provider_id = provider_id, quantity = quantity, delivery_date = delivery_date";
                        break;
                    case "menu7":
                        var selectedRow = DataGrid.SelectedRows;
                        if (selectedRow.Count == 0)
                        {
                            MessageBox.Show("Не выбран товар!");
                            return;
                        }
                        DialogResult res = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res != DialogResult.OK) return;
                        try
                        {
                            foreach (DataGridViewRow row in selectedRow)
                            {
                                int Id = (int)row.Cells[0].Value;
                                command.CommandText = $"DELETE FROM Products WHERE id = {Id}";
                                int n = command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
                        finally
                        {
                            ExecuteCommand();
                            MessageBox.Show("Запись удалена!");
                        }
                        break;
                    case "menu8":
                        var selectedRow1 = DataGrid.SelectedRows;
                        if (selectedRow1.Count == 0)
                        {
                            MessageBox.Show("Не выбран поставщик!");
                            return;
                        }
                        DialogResult res1 = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res1 != DialogResult.OK) return;
                        try
                        {
                            foreach (DataGridViewRow row in selectedRow1)
                            {
                                int Id = (int)row.Cells[0].Value;
                                command.CommandText = $"DELETE FROM Providers WHERE id = {Id}";
                                int n = command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
                        finally
                        {
                            ExecuteCommand();
                            MessageBox.Show("Запись удалена!");
                        }
                        break;
                    case "menu9":
                        var selectedRow2 = DataGrid.SelectedRows;
                        if (selectedRow2.Count == 0)
                        {
                            MessageBox.Show("Не выбрана доставка!");
                            return;
                        }
                        DialogResult res2 = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res2 != DialogResult.OK) return;
                        try
                        {
                            foreach (DataGridViewRow row in selectedRow2)
                            {
                                int Id = (int)row.Cells[0].Value;
                                command.CommandText = $"DELETE FROM Delivery WHERE id = {Id}";
                                int n = command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
                        finally
                        {
                            ExecuteCommand();
                            MessageBox.Show("Запись удалена!");
                        }
                        break;
                }
                ExecuteCommand();
            }
        }
        private void btn_add_Click(object sender, EventArgs e) {
            switch (value) {
                case 1:
                    command.CommandText = "INSERT INTO Products VALUES (@Title, @Type, @CostPrice)";
                    command.Parameters.AddWithValue("@Title", text1.Text);
                    command.Parameters.AddWithValue("@Type", text2.Text);
                    command.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(text3.Text));
                    break;
                case 2:
                    command.CommandText = "INSERT INTO Providers VALUES (@Title)";
                    command.Parameters.AddWithValue("@Title", text1.Text);
                    break;
                case 3:
                    command.CommandText = "INSERT INTO Delivery VALUES (@ProductID, @ProviderID, @Quantity, @DeliveryDate)";
                    command.Parameters.AddWithValue("@ProductID", Convert.ToInt32(text1.Text));
                    command.Parameters.AddWithValue("@ProviderID", Convert.ToInt32(text2.Text));
                    command.Parameters.AddWithValue("@Quantity", Convert.ToInt32(text3.Text));
                    command.Parameters.AddWithValue("@DeliveryDate", DateTime.Parse(text4.Text));
                    break;
            }
            text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = btn_add.Enabled = false;
            text1.Text = text2.Text = text3.Text = text4.Text = string.Empty;
            ExecuteCommand();
        }
    }
}
