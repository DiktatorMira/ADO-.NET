using System.Data;
using System.Data.SqlClient;

namespace SecondTask {
    public partial class Form1 : Form {
        SqlConnection connect;
        SqlCommand command;
        public Form1() {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        private async void btn_connect_Click(object sender, EventArgs e) {
            connect = new SqlConnection(@"Initial Catalog=Storage;
            Data Source=DESKTOP-D5SHCUS\MSSQLSERVER2022;Integrated Security=SSPI");
            command = new SqlCommand();
            command.Connection = connect;
            try {
                await connect.OpenAsync();
                btn_execute.Enabled = true;
                MessageBox.Show("Подключение к БД прошло успешно!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка подключения к БД: " + ex.Message); }
        }
        private async void btn_disconnect_Click(object sender, EventArgs e) {
            ListBox.Items.Clear();
            btn_execute.Enabled = false;
            await command.DisposeAsync();
            await connect.CloseAsync();
            MessageBox.Show("Отключено от БД.");
        }
        private async void ExecuteCommand() {
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ListBox.DataSource = null;
            ListBox.Items.Clear();
            while (await reader.ReadAsync()) {
                for (int i = 0; i < reader.FieldCount; i++) {
                    ListBox.Items.Add(reader[i].ToString() + "  ");
                }
            }
            await reader.CloseAsync();
        }
        private async void btn_execute_Click(object sender, EventArgs e) {
            //command.CommandText = "Select * from Products";
            //command.CommandText = "Select type from Products";
            //command.CommandText = "Select * from Providers";
            //command.CommandText = "Select Max(quantity) as max_quan from Delivery";
            //command.CommandText = "Select Min(quantity) as min_quan from Delivery";
            //command.CommandText = "Select Min(cost_price) as min_price from Products";
            //command.CommandText = "Select Max(cost_price) as max_price from Products";
            //command.CommandText = "Select * from Products where type = 'Food'";
            //command.CommandText = "SELECT P.* FROM Products P INNER JOIN Delivery D ON P.id = D.product_id WHERE D.provider_id IS NOT NULL";
            //command.CommandText = "SELECT P.* FROM Products P INNER JOIN Delivery D ON P.id = D.product_id WHERE D.delivery_date = (SELECT MIN(delivery_date) FROM Delivery)";
            //command.CommandText = "SELECT P.type, AVG(D.quantity) AS avg_quantity FROM Products P INNER JOIN Delivery D ON P.id = D.product_id GROUP BY P.type";
            ExecuteCommand();
        }
    }
}
