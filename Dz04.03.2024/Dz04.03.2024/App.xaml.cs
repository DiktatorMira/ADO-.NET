using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dz04._03._2024 {
    public partial class App : Application {
        private void OnStartup(object sender, StartupEventArgs e) {
            try {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ua-UA");
                using (var context = new AuthorsAndBooksContext()) {
                    MainWindow view = new MainWindow();
                    view.DataContext = new MainVM(context.Authors, context.Books); ;
                    view.Show();
                }
            }
            catch (DbException ex) { MessageBox.Show("Ошибка подключения к бд: " + ex.Message); }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}