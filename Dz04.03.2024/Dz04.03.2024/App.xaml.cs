using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dz04._03._2024 {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            try {
                base.OnStartup(e);
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ua-UA");
                using (var context = new Context()) {
                    var main = new MainVM(context.authors, context.books);
                    MainWindow view = new MainWindow();
                    MainWindow = view;
                    view.DataContext = main;
                    view.Show();
                }
            }
            catch (DbException ex) { MessageBox.Show("Ошибка подключения к бд: " + ex.Message); }
            catch (Exception ex) { MessageBox.Show("Ошибка приложения: " + ex.Message); }
        }
    }
}