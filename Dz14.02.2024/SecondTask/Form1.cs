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
                            command.CommandText = "SELECT " +
                            "s.id AS sale_id, p.title AS product_title, " +
                            "p.type AS product_type, s.sales_number, " +
                            "s.price, s.sale_date," +
                            "m.name AS manager_name, m.surname AS manager_surname, " +
                            "c.title AS company_title, c.email AS company_email FROM " +
                            "Sales s JOIN Products p ON s.product_id = p.id JOIN Managers " +
                            "m ON s.manager_id = m.id JOIN Companies c ON s.company_id = c.id;";
                            but_execute2.Enabled = true;
                            break;
                        case "menu2":
                            /*Те функции, что есть в бд, необходимо закометировть.*/
                            AddFunction(connect, 1);
                            command.CommandText = "SELECT * FROM GetProductTypes();";
                            break;
                        case "menu3":
                            AddFunction(connect, 2);
                            command.CommandText = "SELECT * FROM GetSalesManagers();";
                            break;
                        case "menu4":
                            AddFunction(connect, 3);
                            command.CommandText = "SELECT * FROM GetProductsMaxQuantity();";
                            break;
                        case "menu5":
                            AddFunction(connect, 4);
                            command.CommandText = "SELECT * FROM GetProductsMinQuantity();";
                            break;
                        case "menu6":
                            AddFunction(connect, 5);
                            command.CommandText = "SELECT * FROM GetProductsMinCost();";
                            break;
                        case "menu7":
                            AddFunction(connect, 6);
                            command.CommandText = "SELECT * FROM GetProductsMaxCost();";
                            break;
                        case "menu8":
                            AddFunction(connect, 7);
                            command.CommandText = "SELECT * FROM GetProductsByType();";
                            break;
                        case "menu9":
                            AddFunction(connect, 8);
                            command.CommandText = "SELECT * FROM GetProductsSoldByManager();";
                            break;
                        case "menu10":
                            AddFunction(connect, 9);
                            command.CommandText = "SELECT * FROM GetProductsPurchasedByCompany();";
                            break;
                        case "menu11":
                            AddFunction(connect, 10);
                            command.CommandText = "SELECT * FROM GetLatestSalesInfo();";
                            break;
                        case "menu12":
                            AddFunction(connect, 11);
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
                            AddFunction(connect, 16);
                            using (SqlCommand command = new SqlCommand("UpdateProduct", connect)) {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            break;
                        case "menu18":
                            AddFunction(connect, 17);
                            using (SqlCommand command = new SqlCommand("UpdateCompanies", connect)) {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            break;
                        case "menu19":
                            AddFunction(connect, 18);
                            using (SqlCommand command = new SqlCommand("UpdateManagers", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            break;
                        case "menu20":
                            AddFunction(connect, 19);
                            using (SqlCommand command = new SqlCommand("UpdateTypeProduct", connect))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.ExecuteNonQuery();
                            }
                            break;
                        case "menu21":
                            AddFunction(connect, 20);
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
                            AddFunction(connect, 21);
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
                            AddFunction(connect, 22);
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
                            AddFunction(connect, 23);
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
                        AddFunction(connect, 12);
                        using (SqlCommand command = new SqlCommand("AddProduct", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                            command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = text2.Text });
                            command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int) { Value = int.Parse(text3.Text) });
                            command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = decimal.Parse(text4.Text, CultureInfo.InvariantCulture) });
                        }
                        break;
                    case 1:
                        AddFunction(connect, 13);
                        using (SqlCommand command = new SqlCommand("InsertTypeProduct", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = text1.Text });
                        }
                        break;
                    case 2:
                        AddFunction(connect, 14);
                        using (SqlCommand command = new SqlCommand("AddManager", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = text1.Text });
                            command.Parameters.Add(new SqlParameter("@Surname", SqlDbType.NVarChar, 50) { Value = text2.Text });
                            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text3.Text });
                        }
                        break;
                    case 3:
                        AddFunction(connect, 15);
                        using (SqlCommand command = new SqlCommand("AddCompany", connect))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 50) { Value = text1.Text });
                            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = text2.Text });                        }
                        break;
                }
                btn_add.Enabled = text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = false;
                ExecuteCommand();
            }
            catch(Exception ex) { MessageBox.Show("Ошибка выполнения команды: " + ex.Message); }
        }
        private void AddFunction(SqlConnection connection, int value)
        {
            switch (value)
            {
                case 1:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductTypes()
                    RETURNS TABLE AS RETURN (
                        SELECT DISTINCT type
                        FROM Products
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 2:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetSalesManagers()
                    RETURNS TABLE AS RETURN (
                        SELECT DISTINCT
                            m.id AS ManagerId,
                            m.name AS ManagerName,
                            m.surname AS ManagerSurname,
                            m.email AS ManagerEmail
                        FROM
                            Sales s
                            INNER JOIN Managers m ON s.manager_id = m.id
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 3:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsMaxQuantity()
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        WHERE p.amount = (SELECT MAX(amount) FROM Products)
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 4:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsMinQuantity()
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        WHERE p.amount = (SELECT MIN(amount) FROM Products)
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 5:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsMinCost()
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        WHERE p.price = (SELECT MIN(price) FROM Products)
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 6:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsMaxCost()
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        WHERE p.price = (SELECT MAX(price) FROM Products)
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 7:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsByType(@productType NVARCHAR(50))
                    RETURNS TABLE AS RETURN (
                        SELECT p.type AS ProductType,
                        FROM Products p
                        WHERE p.type = @productType
                    )", connection))
                    {
                        command.Parameters.AddWithValue("@productType", "Ручки");
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 8:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsSoldByManager(@managerId INT)
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        JOIN Sales s ON p.id = s.product_id
                        WHERE s.manager_id = @managerId
                    )", connection))
                    {
                        command.Parameters.AddWithValue("@managerId", 1); // Замените 1 на фактический идентификатор менеджера
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 9:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetProductsPurchasedByCompany(@companyId INT)
                    RETURNS TABLE AS RETURN (
                        SELECT p.id AS ProductId,
                               p.title AS ProductTitle,
                               p.type AS ProductType,
                               p.amount AS ProductAmount,
                               p.price AS ProductPrice
                        FROM Products p
                        JOIN Sales s ON p.id = s.product_id
                        WHERE s.company_id = @companyId
                    )", connection))
                    {
                        command.Parameters.AddWithValue("@companyId", 1); // Замените 1 на фактический идентификатор фирмы
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 10:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetLatestSalesInfo()
                    RETURNS TABLE AS RETURN (
                        SELECT 
                            p.id AS ProductId,
                            p.title AS ProductTitle,
                            p.type AS ProductType,
                            p.amount AS ProductAmount,
                            p.price AS ProductPrice,
                            s.id AS SaleId,
                            s.sales_number AS SaleNumber,
                            s.price AS SalePrice,
                            s.sale_date AS SaleDate,
                            ROW_NUMBER() OVER (PARTITION BY p.id ORDER BY s.sale_date DESC) AS RowNum
                        FROM 
                            Products p
                            JOIN Sales s ON p.id = s.product_id
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 11:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE FUNCTION GetAverageProductAmountByType()
                    RETURNS TABLE AS RETURN (
                        SELECT 
                            p.type AS ProductType,
                            AVG(p.amount) AS AverageProductAmount
                        FROM 
                            Products p
                        GROUP BY 
                            p.type
                    )", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 12:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE AddProduct
                        @Title NVARCHAR(50),
                        @Type NVARCHAR(50),
                        @Amount INT,
                        @Price DECIMAL(10,2)
                    AS
                    BEGIN
                        INSERT INTO Products (title, type, amount, price)
                        VALUES (@Title, @Type, @Amount, @Price);
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 13:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE InsertTypeProduct
                        @Type NVARCHAR(50)
                    AS BEGIN
                        INSERT INTO Products (type)
                        VALUES (@Type);
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 14:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE AddManager
                        @Name NVARCHAR(50),
                        @Surname NVARCHAR(50),
                        @Email NVARCHAR(50)
                    AS
                    BEGIN
                        INSERT INTO Managers
                        VALUES (@Name, @Surname, @Email);
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 15:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE AddCompany
                        @Title NVARCHAR(50),
                        @Email NVARCHAR(50)
                    AS
                    BEGIN
                        INSERT INTO Companies
                        VALUES (@Title, @Email);
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 16:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE UpdateProduct
                    AS BEGIN
                         UPDATE Products SET
                             title = title, type = type,
                             amount = amount, price = price
                         WHERE id = id;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 17:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE UpdateCompanies
                    AS BEGIN
                         UPDATE Companies SET
                             title = title, email = email
                         WHERE id = id;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 18:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE UpdateManagers
                    AS BEGIN
                         UPDATE Managers SET
                             name = name, surname = surname, email = email
                         WHERE id = id;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 19:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE UpdateTypeProduct
                    AS BEGIN
                         UPDATE Products SET type = type,
                         WHERE id = id;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 20:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE DeleteProduct
                        @ProductId int
                    AS
                    BEGIN
                        DELETE FROM Products
                        WHERE id = @ProductId;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 21:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE DeleteManager
                        @ManagerId int
                    AS
                    BEGIN
                        DELETE FROM Managers
                        WHERE id = @ManagerId;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 22:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE DeleteTypeProduct
                        @Type nvarchar(50)
                    AS
                    BEGIN
                        DELETE FROM Products
                        WHERE type = @Type;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
                case 23:
                    using (SqlCommand command = new SqlCommand(@"
                    CREATE PROCEDURE DeleteCompany
                        @CompanyId int
                    AS
                    BEGIN
                        DELETE FROM Companies
                        WHERE id = @CompanyId;
                    END", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    break;
            }
        }
    }
}
