using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FirstTask {
    public partial class Form1 : Form
    {
        SqlConnection connect;
        SqlCommand command;
        public Form1()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        private async void but_connect_Click(object sender, EventArgs e)
        {
            connect = new SqlConnection(@"Initial Catalog=VegetablesAndFruits;
            Data Source=DESKTOP-D5SHCUS\MSSQLSERVER2022;Integrated Security=SSPI");
            command = new SqlCommand();
            command.Connection = connect;
            try
            {
                await connect.OpenAsync();
                but_execute.Enabled = true;
                MessageBox.Show("Подключение к БД прошло успешно!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка подключения к БД: " + ex.Message); }
        }
        private async void but_disconnect_Click(object sender, EventArgs e) {
            ListBox.Items.Clear();
            but_execute.Enabled = false;
            await command.DisposeAsync();
            await connect.CloseAsync();
            MessageBox.Show("Отключено от БД.");
        }
        private async void but_execute_Click(object sender, EventArgs e) {
            //command.CommandText = "Select * from Products";
            //command.CommandText = "Select kind from Products";
            //command.CommandText = "Select color from Products";
            //command.CommandText = "Select Max(calories) as maxValue from Products";
            //command.CommandText = "Select Min(calories) as minValue from Products";
            //command.CommandText = "Select Avg(calories) as avgValue from Products";
            //command.CommandText = "Select * from Products where kind = \'Vegetables\'";
            //command.CommandText = "Select * from Products where kind = \'Fruits\'";
            //command.CommandText = "Select * from Products where color = \'Orane\'";
            //command.CommandText = "Select * from Products where calories < 130";
            //command.CommandText = "Select * from Products where calories > 130";
            //command.CommandText = "Select * from Products where calories > 70 && calories < 130";
            //command.CommandText = "Select * from Products where color = \'Red\' || color = \'Yellow\'";
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ListBox.DataSource = null;
            ListBox.Items.Clear();
            while (await reader.ReadAsync()) {
                string res = "";
                for (int i = 0; i < reader.FieldCount; i++) res += reader[i].ToString() + "  ";
                ListBox.Items.Add(res);
            }
            await reader.CloseAsync();
        }
    }
}
