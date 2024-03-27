using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System;

namespace Dz27._03._2024 {
    public static class Program {
        static string? connection;
        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;

            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connection = config.GetConnectionString("DefaultConnection");

            try {
                while (true) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("0) Выход");
                    Console.WriteLine("1) Вывести все канцтовары");
                    Console.WriteLine("2) Вывести типы канцтоваров");
                    Console.WriteLine("3) Вывести менеджеров");
                    Console.WriteLine("4) Вывести макс кол-во канцтоваров");
                    Console.WriteLine("5) Вывести мин кол-во канцтоваров");
                    Console.WriteLine("6) Вывести канцтовар с мин стоимостью");
                    Console.WriteLine("7) Вывести канцтовар с макс стоимостью");
                    Console.WriteLine("8) Вывести канцтовары заданного типа");
                    Console.WriteLine("9) Вывести канцтовары проданные конкретным менеджером");
                    Console.WriteLine("10) Вывести канцтовары купленные конкретной фирмой");
                    Console.WriteLine("11) Вывести самую недавнюю продажу");
                    Console.WriteLine("12) Вывести среднее кол-во товаров по каждому типу");
                    Console.WriteLine("13) Добавить канцтовар");
                    Console.WriteLine("14) Добавить тип канцтовара");
                    Console.WriteLine("15) Добавить менеджера");
                    Console.WriteLine("16) Добавить фирму");
                    Console.WriteLine("17) Обновить канцтовар");
                    Console.WriteLine("18) Обновить фирму");
                    Console.WriteLine("19) Обновить менеджера");
                    Console.WriteLine("20) Обновить тип канцтовара");
                    Console.WriteLine("21) Удалить канцтовар");
                    Console.WriteLine("22) Удалить менеджера");
                    Console.WriteLine("23) Удалить тип канцтовара");
                    Console.WriteLine("24) Удалить фирму");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("\nВведите значение: ");
                    string res = Console.ReadLine()!.ToString();

                    using (IDbConnection db = new SqlConnection(connection)) {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Clear();
                        switch (res) {
                            case "0":
                                Console.ResetColor();
                                return;
                            case "1":
                                ShowStationary(db);
                                break;
                            case "2":
                                ShowStationaryType(db);
                                break;
                            case "3":
                                ShowManagers(db);
                                break;
                            case "4":
                                ShowMaxStationary(db);
                                break;
                            case "5":
                                ShowMinStationary(db);
                                break;
                            case "6":
                                ShowMinCostStationary(db);
                                break;
                            case "7":
                                ShowMaxCostStationary(db);
                                break;
                            case "8":
                                ShowStationaryByType(db);
                                break;
                            case "9":
                                ShowStationaryByManager(db);
                                break;
                            case "10":
                                ShowStationaryByCompany(db);
                                break;
                            case "11":
                                ShowRecentlySale(db);
                                break;
                            case "12":
                                ShowAverageByType(db);
                                break;
                            case "13":
                                AddStationary(db);
                                break;
                            case "14":
                                AddStationaryType(db);
                                break;
                            case "15":
                                AddManager(db);
                                break;
                            case "16":
                                AddCompany(db);
                                break;
                            case "17":
                                UpdateStationary(db);
                                break;
                            case "18":
                                UpdateCompany(db);
                                break;
                            case "19":
                                UpdateManager(db);
                                break;
                            case "20":
                                UpdateStationaryType(db);
                                break;
                            case "21":
                                DeleteStationary(db);
                                break;
                            case "22":
                                DeleteManager(db);
                                break;
                            case "23":
                                DeleteStationaryType(db);
                                break;
                            case "24":
                                DeleteCompany(db);
                                break;
                            default:
                                continue;
                        }
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\nНажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                    }
                }
            } catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка: " + ex.Message); 
            }
        }
        static void ShowStationary(IDbConnection db) {
            var stationaryItems = db.Query<dynamic>("GetAllTablesData", commandType: CommandType.StoredProcedure);
            foreach (var item in stationaryItems) {
                Console.WriteLine($"Номер продажи: {item.sales_number}");
                Console.WriteLine($"Название продукта: {item.product_title}");
                Console.WriteLine($"Тип продукта: {item.type}");
                Console.WriteLine($"Количество: {item.amount}");
                Console.WriteLine($"Цена: {item.price}");
                Console.WriteLine($"Имя менеджера: {item.manager_name}");
                Console.WriteLine($"Фамилия менеджера: {item.manager_surname}");
                Console.WriteLine($"Email менеджера: {item.manager_email}");
                Console.WriteLine($"Название компании: {item.company_title}");
                Console.WriteLine($"Email компании: {item.company_email}");
                Console.WriteLine($"Дата продажи: {item.sale_date}\n");
            }
        }
        static void ShowStationaryType(IDbConnection db) {
            var productTypes = db.Query<string>("SELECT * FROM GetProductTypes()");
            foreach (var type in productTypes) Console.WriteLine($"Тип канцтовара: {type}");
        }
        static void ShowManagers(IDbConnection db) {
            var managers = db.Query<Manager>("SELECT * FROM GetSalesManagers()");
            foreach (var manager in managers) {
                Console.WriteLine($"ID менеджера: {manager.Id}");
                Console.WriteLine($"Имя менеджера: {manager.Name}");
                Console.WriteLine($"Фамилия менеджера: {manager.Surname}");
                Console.WriteLine($"Email менеджера: {manager.Email}\n");
            }
        }
        static void ShowMaxStationary(IDbConnection db) {
            var maxQuantity = db.Query<Product>("SELECT * FROM GetProductsMaxQuantity()");
            foreach (var product in maxQuantity) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}\n");
            }
        }
        static void ShowMinStationary(IDbConnection db) {
            var minQuantity = db.Query<Product>("SELECT * FROM GetProductsMinQuantity()");
            foreach (var product in minQuantity) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}\n");
            }
        }
        static void ShowMinCostStationary(IDbConnection db) {
            var minCost = db.Query<Product>("SELECT * FROM GetProductsMinCost()");
            foreach (var product in minCost) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}\n");
            }
        }
        static void ShowMaxCostStationary(IDbConnection db) {
            var maxCost = db.Query<Product>("SELECT * FROM GetProductsMaxCost()");
            foreach (var product in maxCost) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}\n");
            }
        }
        static void ShowStationaryByType(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите тип канцтоваров: ");
            string type = Console.ReadLine()!.ToString();

            var existingProductType = db.QueryFirstOrDefault<string>("SELECT TOP 1 type FROM Products WHERE type = @productType", new { type });
            if (existingProductType == null) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Тип канцтоваров {type} не существует.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var products = db.Query<Product>("SELECT * FROM GetProductsByType(@productType)", new { type });
            foreach (var product in products) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}");
                Console.WriteLine();
            }
        }
        static void ShowStationaryByManager(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID менеджера: ");
            if (!int.TryParse(Console.ReadLine(), out int managerId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID менеджера.");
                return;
            }

            var existingManager = db.QueryFirstOrDefault<int>("SELECT TOP 1 id FROM Managers WHERE id = @managerId", new { managerId });
            if (existingManager == 0) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Менеджер с ID {managerId} не найден.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var products = db.Query<Product>("SELECT * FROM GetProductsSoldByManager(@managerId)", new { managerId });
            foreach (var product in products) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}");
                Console.WriteLine();
            }
        }
        static void ShowStationaryByCompany(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID компании: ");
            if (!int.TryParse(Console.ReadLine(), out int companyId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID компании.");
                return;
            }

            var existingCompany = db.QueryFirstOrDefault<int>("SELECT TOP 1 id FROM Companies WHERE id = @companyId", new { companyId });
            if (existingCompany == 0) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Компания с ID {companyId} не найдена.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var products = db.Query<Product>("SELECT * FROM GetProductsPurchasedByCompany(@companyId)", new { companyId });
            foreach (var product in products) {
                Console.WriteLine($"ID продукта: {product.Id}");
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Тип продукта: {product.Type}");
                Console.WriteLine($"Количество: {product.Amount}");
                Console.WriteLine($"Цена: {product.Price}");
                Console.WriteLine();
            }
        }
        static void ShowRecentlySale(IDbConnection db) {
            var latestSale = db.QueryFirstOrDefault<Sale>("SELECT TOP 1 * FROM GetLatestSalesInfo() ORDER BY SaleDate DESC");
            Console.WriteLine("Информация о самой недавней продаже:");
            Console.WriteLine($"ID продукта: {latestSale?.ProductId}");
            Console.WriteLine($"Название продукта: {latestSale?.Product?.Title}");
            Console.WriteLine($"Тип продукта: {latestSale?.Product?.Type}");
            Console.WriteLine($"Количество: {latestSale?.Product?.Amount}");
            Console.WriteLine($"Цена: {latestSale?.Product?.Price}");
            Console.WriteLine($"ID продажи: {latestSale?.Id}");
            Console.WriteLine($"Номер продажи: {latestSale?.SalesNumber}");
            Console.WriteLine($"Цена продажи: {latestSale?.Price}");
            Console.WriteLine($"Дата продажи: {latestSale?.SaleDate}");
        }
        static void ShowAverageByType(IDbConnection db) {
            var averageAmounts = db.Query("SELECT * FROM GetAverageProductAmountByType()");
            foreach (var averageAmount in averageAmounts)  Console.WriteLine($"Тип: {averageAmount.ProductType}, Среднее количество: {averageAmount.AverageProductAmount}");
        }
        static void AddStationary(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите название канцтовара: ");
            string title = Console.ReadLine()!.ToString();
            Console.Write("Введите тип канцтовара: ");
            string type = Console.ReadLine()!.ToString();
            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int amount)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректное количество.");
                return;
            }
            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректная цена.");
                return;
            }

            var parameters = new { Title = title, Type = type, Amount = amount, Price = price };
            db.Execute("AddProduct", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Новый канцтовар успешно добавлен.");
        }
        static void AddStationaryType(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите новый тип канцтовара: ");
            string type = Console.ReadLine()!.ToString();

            db.Execute("InsertTypeProduct", new { Type = type }, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Новый тип канцтовара успешно добавлен.");
        }
        static void AddManager(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите имя менеджера: ");
            string name = Console.ReadLine()!.ToString();
            Console.Write("Введите фамилию менеджера: ");
            string surname = Console.ReadLine()!.ToString();
            Console.Write("Введите адрес электронной почты менеджера: ");
            string email = Console.ReadLine()!.ToString();

            var parameters = new { Name = name, Surname = surname, Email = email };
            db.Execute("AddManager", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Новый менеджер по продажам успешно добавлен.");
        }
        static void AddCompany(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите название фирмы: ");
            string title = Console.ReadLine()!.ToString();
            Console.Write("Введите адрес электронной почты фирмы: ");
            string email = Console.ReadLine()!.ToString();

            var parameters = new { Title = title, Email = email };
            db.Execute("AddCompany", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Новая фирма успешно добавлена.");
        }
        static void UpdateStationary(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID канцтовара, который необходимо обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int productId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID канцтовара.");
                return;
            }
            Console.Write("Введите новое название канцтовара: ");
            string title = Console.ReadLine()!.ToString();
            Console.Write("Введите новый тип канцтовара: ");
            string type = Console.ReadLine()!.ToString();
            Console.Write("Введите новое количество: ");
            if (!int.TryParse(Console.ReadLine(), out int amount)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректное количество.");
                return;
            }
            Console.Write("Введите новую цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректная цена.");
                return;
            }

            var parameters = new { Id = productId, Title = title, Type = type, Amount = amount, Price = price };
            db.Execute("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Информация о канцтоваре с ID {productId} успешно обновлена.");
        }
        static void UpdateCompany(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID компании, информацию о которой необходимо обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int companyId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID компании.");
                return;
            }
            Console.Write("Введите новое название компании: ");
            string title = Console.ReadLine()!.ToString();
            Console.Write("Введите новый адрес электронной почты компании: ");
            string email = Console.ReadLine()!.ToString();

            var parameters = new { Id = companyId, Title = title, Email = email };
            db.Execute("UpdateCompanies", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Информация о компании с ID {companyId} успешно обновлена.");
        }
        static void UpdateManager(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID менеджера, информацию о котором необходимо обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int managerId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID менеджера.");
                return;
            }
            Console.Write("Введите новое имя менеджера: ");
            string name = Console.ReadLine()!;
            Console.Write("Введите новую фамилию менеджера: ");
            string surname = Console.ReadLine()!;
            Console.Write("Введите новый адрес электронной почты менеджера: ");
            string email = Console.ReadLine()!;

            var parameters = new { Id = managerId, Name = name, Surname = surname, Email = email };
            db.Execute("UpdateManagers", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Информация о менеджере с ID {managerId} успешно обновлена.");
        }
        static void UpdateStationaryType(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите новый тип канцтовара: ");
            string type = Console.ReadLine()!;

            db.Execute("UpdateTypeProduct", new { Type = type }, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Тип канцтовара успешно обновлен.");
        }
        static void DeleteStationary(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID канцтовара, который необходимо удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int productId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID канцтовара.");
                return;
            }
            Console.Write("Вы уверены, что хотите удалить этот канцтовар? (y/n): ");

            if (Console.ReadLine()!.ToLower() != "y") return;
            db.Execute("DeleteProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Канцтовар с ID {productId} успешно удален.");
        }
        static void DeleteManager(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID менеджера, которого необходимо удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int managerId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID менеджера.");
                return;
            }
            Console.Write("Вы уверены, что хотите удалить этого менеджера? (y/n): ");
            if (Console.ReadLine()!.ToLower() != "y") return;

            db.Execute("DeleteManager", new { ManagerId = managerId }, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Менеджер с ID {managerId} успешно удален.");
        }
        static void DeleteStationaryType(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите тип канцтовара, который необходимо удалить: ");
            string type = Console.ReadLine()!;
            Console.Write("Вы уверены, что хотите удалить этот тип канцтовара? (y/n): ");
            if (Console.ReadLine()!.ToLower() != "y") return;

            var parameters = new { Type = type };
            db.Execute("DeleteTypeProduct", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Тип канцтовара {type} успешно удален.");
        }
        static void DeleteCompany(IDbConnection db) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Введите ID компании, которую необходимо удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int companyId)) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Некорректный ID компании.");
                return;
            }
            Console.Write("Вы уверены, что хотите удалить эту компанию? (y/n): ");
            if (Console.ReadLine()!.ToLower() != "y") return;

            var parameters = new { CompanyId = companyId };
            db.Execute("DeleteCompany", parameters, commandType: CommandType.StoredProcedure);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Компания с ID {companyId} успешно удалена.");
        }
    }
}