using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
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
                but_execute1.Enabled = true;
                MessageBox.Show("Подключение к БД прошло успешно!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка подключения к БД: " + ex.Message); }
        }
        private async void btn_disconnect_Click(object sender, EventArgs e)
        {
            DataGrid.DataSource = null;
            DataGrid.Refresh();
            but_execute1.Enabled = but_execute2.Enabled = false;
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
                try
                {
                    switch (itemName)
                    {
                        case "menu1":
                            using (SqlCommand command = new SqlCommand("GetAllTablesData", connect)) {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            but_execute2.Enabled = true;
                            break;
                        case "menu2":
                            command.CommandText = "SELECT * FROM GetProductTypes();";
                            break;
                        case "menu3":
                            command.CommandText = "SELECT * FROM GetSalesManagers();";
                            break;
                        case "menu4":
                            command.CommandText = "SELECT * FROM GetProductsMaxQuantity();";
                            break;
                        case "menu5":
                            command.CommandText = "SELECT * FROM GetProductsMinQuantity();";
                            break;
                        case "menu6":
                            command.CommandText = "SELECT * FROM GetProductsMinCost();";
                            break;
                        case "menu7":
                            command.CommandText = "SELECT * FROM GetProductsMaxCost();";
                            break;
                        case "menu8":
                            command.CommandText = "SELECT * FROM GetProductsByType();";
                            break;
                        case "menu9":
                            command.CommandText = "SELECT * FROM GetProductsSoldByManager();";
                            break;
                        case "menu10":
                            command.CommandText = "SELECT * FROM GetProductsPurchasedByCompany();";
                            break;
                        case "menu11":
                            command.CommandText = "SELECT * FROM GetLatestSalesInfo();";
                            break;
                        case "menu12":
                            command.CommandText = "SELECT * FROM GetAverageProductAmountByType();";
                            break;
                        case "menu13":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = true;
                            text1.Text = "title";
                            text2.Text = "type";
                            text3.Text = "3";
                            text4.Text = "10,50";
                            value = 0;
                            break;
                        case "menu14":
                            btn_add.Enabled = text1.Enabled = true;
                            text1.Text = "type";
                            value = 1;
                            break;
                        case "menu15":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = true;
                            text1.Text = "name";
                            text2.Text = "surname";
                            text3.Text = "email";
                            value = 2;
                            break;
                        case "menu16":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = true;
                            text1.Text = "title";
                            text2.Text = "email";
                            value = 3;
                            break;
                        case "menu17":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = true;
                            text1.Text = "title";
                            text2.Text = "type";
                            text3.Text = "3";
                            text4.Text = "10,50";
                            value = 4;
                            break;
                        case "menu18":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = true;
                            text1.Text = "title";
                            text2.Text = "email";
                            value = 5;
                            break;
                        case "menu19":
                            btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = true;
                            text1.Text = "Name";
                            text2.Text = "Surname";
                            text3.Text = "Email";
                            value = 6;
                            break;
                        case "menu20":
                            btn_add.Enabled = text1.Enabled = true;
                            text1.Text = "type";
                            value = 7;
                            break;
                        case "menu21":
                            var selectedRow = DataGrid.SelectedRows;
                            if (selectedRow.Count == 0) {
                                MessageBox.Show("Не выбрана запись!");
                                return;
                            }
                            DialogResult res = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res != DialogResult.Yes){
                                using (SqlCommand command = new SqlCommand("DeleteProduct", connect))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = selectedRow });
                                    command.ExecuteNonQuery();
                                }
                            }
                            break;
                        case "menu22":
                            var selectedRow1 = DataGrid.SelectedRows;
                            if (selectedRow1.Count == 0)
                            {
                                MessageBox.Show("Не выбрана запись!");
                                return;
                            }
                            DialogResult res1 = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res1 != DialogResult.Yes)
                            {
                                using (SqlCommand command = new SqlCommand("DeleteManager", connect))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int) { Value = selectedRow1 });
                                    command.ExecuteNonQuery();
                                }
                            }
                            break;
                        case "menu23":
                            var selectedRow2 = DataGrid.SelectedRows;
                            if (selectedRow2.Count == 0)
                            {
                                MessageBox.Show("Не выбрана запись!");
                                return;
                            }
                            DialogResult res2 = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res2 != DialogResult.Yes)
                            {
                                using (SqlCommand command = new SqlCommand("DeleteTypeProduct", connect))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int) { Value = selectedRow2 });
                                    command.ExecuteNonQuery();
                                }
                            }
                            break;
                        case "menu24":
                            var selectedRow3 = DataGrid.SelectedRows;
                            if (selectedRow3.Count == 0)
                            {
                                MessageBox.Show("Не выбрана запись!");
                                return;
                            }
                            DialogResult res3 = MessageBox.Show("Вы действительно желаете удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res3 != DialogResult.Yes)
                            {
                                using (SqlCommand command = new SqlCommand("DeleteCompany", connect))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int) { Value = selectedRow3 });
                                    command.ExecuteNonQuery();
                                }
                            }
                            break;
                    }
                    ExecuteCommand();
                }
                catch (Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
            }
        }
        private void btn_add_Click(object sender, EventArgs e) {
            try
            {
                switch (value)
                {
                    case 0:
                        if (text1.Text != null && text2.Text != null && text3.Text != null && text4.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("AddProduct", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = text2.Text });
                                command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int) { Value = int.Parse(text3.Text) });
                                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = decimal.Parse(text4.Text, CultureInfo.InvariantCulture) });
                            }
                        }
                        else {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 1:
                        if(text1.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("InsertTypeProduct", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = text1.Text });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 2:
                        if(text1.Text != null && text2.Text != null && text3.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("AddManager", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50) { Value = text2.Text });
                                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text3.Text });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 3:
                        if (text1.Text != null && text2.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("AddCompany", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text2.Text });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 4:
                        if (text1.Text != null && text2.Text != null && text3.Text != null && text4.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("UpdateProduct", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = text2.Text });
                                command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int) { Value = int.Parse(text3.Text) });
                                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = decimal.Parse(text4.Text, CultureInfo.InvariantCulture) });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 5:
                        if(text1.Text != null && text2.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("UpdateCompanies", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text2.Text });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 6:
                        if(text1.Text != null && text2.Text != null && text3.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("UpdateManagers", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = text1.Text });
                                command.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50) { Value = text2.Text });
                                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text3.Text });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                    case 7:
                        if(text1.Text != null)
                        {
                            using (SqlCommand command = new SqlCommand("UpdateTypeProduct", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пустые поля недопустимы!");
                            return;
                        }
                        break;
                }
                btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = false;
                ExecuteCommand();
            }
            catch(Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
        }
    }
}
