using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dz11._03._2024 {
    public partial class MainWindow : Window {
        public MainWindow() => InitializeComponent();
        private void ExecuteClick(object sender, RoutedEventArgs e){
            try {
                var clickedMenuItem = sender as MenuItem;
                using (var db = new Context()) {
                    switch (clickedMenuItem?.Header.ToString()) {
                        case "Вывести информацию о канцтоварах":
                            Info.ItemsSource = db.SalesViews.FromSqlRaw("EXEC GetProductsInfo").ToList();
                            break;
                        case "Вывести типы канцтоваров":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductTypes").ToList();
                            break;
                        case "Вывести менеджеров":
                            Info.ItemsSource = db.Managers.FromSqlRaw("EXEC GetSalesManagers").ToList();
                            break;
                        case "Вывести макс кол-во канцтоваров":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsMinQuantity").ToList();
                            break;
                        case "Вывести мин кол-во канцтоваров":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsMaxQuantity").ToList();
                            break;
                        case "Вывести канцтовары с мин ценой":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsMinCost").ToList();
                            break;
                        case "Вывести канцтовары с макс ценой":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsMaxCost").ToList();
                            break;
                        case "Вывести канцтовары заданного типа":
                            SqlParameter pType = new SqlParameter("@productType", "Рисовальные");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsByType @productType", pType).ToList();
                            break;
                        case "Вывести канцтовары проданные конкретным менеджером":
                            SqlParameter managerId = new SqlParameter("@managerId", 1);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsSoldByManager @managerId", managerId).ToList();
                            break;
                        case "Вывести канцтовары купившиеся конкретной фирмой":
                            SqlParameter companyId = new SqlParameter("@companyId", 1);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetProductsPurchasedByCompany @companyId", companyId).ToList();
                            break;
                        case "Вывести самую недавнюю продажу":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetLatestSalesInfo").ToList();
                            break;
                        case "Вывести среднее кол-во товаров по каждому типу":
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC GetAverageProductAmountByType").ToList();
                            break;
                        case "Вставить товар":
                            SqlParameter title = new SqlParameter("@Title", "Линейка"), type = new SqlParameter("@Type", "Дерево"), amount = new SqlParameter("@Amount", 265), price = new SqlParameter("@Price", 10.11);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC AddProduct @Title, @Type, @Amount,  @Price", title, type, amount, price).ToList();
                            break;
                        case "Вставить тип товара":
                            SqlParameter type1 = new SqlParameter("@Type", "Тип");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC InsertTypeProduct @Type", type1).ToList();
                            break;
                        case "Вставить менеджера":
                            SqlParameter name = new SqlParameter("@Name", "Имя"), surname = new SqlParameter("@Surname", "Фамилия"), email = new SqlParameter("@Email", "Почта");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC AddManager @Name, @Surname, @Email", name, surname, email).ToList();
                            break;
                        case "Вставить фирму":
                            SqlParameter title1 = new SqlParameter("@Title", "Название"), email1 = new SqlParameter("@Email", "Почта");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC AddCompany @Title, @Email", title1, email1).ToList();
                            break;
                        case "Обновить канцтовар":
                            SqlParameter title2 = new SqlParameter("@Title", "Название"), type2 = new SqlParameter("@Type", "Тип"), amount1 = new SqlParameter("@Amount", 3), price1 = new SqlParameter("@Price", 11.3);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC UpdateProduct @Title, @Type, @Amount, @Price", title2, type2, amount1, price1).ToList();
                            break;
                        case "Обновить фирму":
                            SqlParameter title3 = new SqlParameter("@Title", "Название"), email2 = new SqlParameter("@Email", "Почта");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC UpdateCompanies @Title, @Email", title3, email2).ToList();
                            break;
                        case "Обновить менеджера":
                            SqlParameter name1 = new SqlParameter("@Name", "Имя"), surname1 = new SqlParameter("@Surname", "Фамилия"), email3 = new SqlParameter("@Email", "Почта");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC UpdateManagers @Name, @Surname, @Email", name1, surname1, email3).ToList();
                            break;
                        case "Обновить тип канцтовара":
                            SqlParameter type3 = new SqlParameter("@Type", "Тип");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC UpdateTypeProduct @Type", type3).ToList();
                            break;
                        case "Удалить канцтовар":
                            SqlParameter id = new SqlParameter("@ProductId", 1);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC DeleteProduct @ProductId", id).ToList();
                            break;
                        case "Удалить менеджера":
                            SqlParameter id1 = new SqlParameter("@ManagerId", 1);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC DeleteManager @ManagerId", id1).ToList();
                            break;
                        case "Удалить тип канцтовара":
                            SqlParameter type4 = new SqlParameter("@Type", "Рисовальные");
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC DeleteTypeProduct @Type", type4).ToList();
                            break;
                        case "Удалить фирму":
                            SqlParameter id2 = new SqlParameter("@CompanyId", 1);
                            Info.ItemsSource = db.Products.FromSqlRaw("EXEC DeleteCompany @CompanyId", id2).ToList();
                            break;
                        default:
                            MessageBox.Show("Что за кнопку вы нажали?", "Я запутался...", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            } catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}