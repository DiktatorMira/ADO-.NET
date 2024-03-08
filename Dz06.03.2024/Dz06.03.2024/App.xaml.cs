using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Windows;

namespace Dz06._03._2024 {
    public partial class App : Application {
        private void OnStartup(object sender, StartupEventArgs e) {
            try {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ua-UA");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ua-UA");
                using (var context = new Context()) {
                    MainWindow view = new MainWindow();
                    var viewmodel = new MainVM(context.Positions, context.Employees);
                    view.DataContext = viewmodel;
                    view.Show();
                }
            }
            catch (DbException ex) {
                MessageBox.Show("Ошибка подключения к бд: " + ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка: " + ex.Message);
                Environment.Exit(2);
            }
        }
    }
}
