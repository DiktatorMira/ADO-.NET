using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Text;
using System.Net;
using System;

namespace FirstTask {
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
                    Console.WriteLine("0) Выход");
                    Console.WriteLine("1) Отобразить всех покупателей");
                    Console.WriteLine("2) Отобразить emial всех покупателей");
                    Console.WriteLine("3) Отобразить список разделов");
                    Console.WriteLine("4) Отобразить список акционных товаров");
                    Console.WriteLine("5) Отобразить все города");
                    Console.WriteLine("6) Отобразить все страны");
                    Console.WriteLine("7) Отобразить всех покупателей из конкретного города");
                    Console.WriteLine("8) Отобразить всех покупателей из конкретной страны");
                    Console.WriteLine("10) Вставить информацию о новом покупателе");
                    Console.WriteLine("11) Вставить новую страну");
                    Console.WriteLine("12) Вставить новый город");
                    Console.WriteLine("13) Вставить информацию о новом разделе");
                    Console.WriteLine("14) Вставить информацию о новом акционном товаре");
                    Console.WriteLine("15) Обновить информацию о покупателе");
                    Console.WriteLine("16) Обновить информацию о стране");
                    Console.WriteLine("17) Обновить информацию о городе");
                    Console.WriteLine("18) Обновить информацию о разделе");
                    Console.WriteLine("19) Обновить информацию о акционном товаре");
                    Console.WriteLine("20) Удалить покупателя");
                    Console.WriteLine("21) Удалить страну");
                    Console.WriteLine("22) Удалить город");
                    Console.WriteLine("23) Удалить раздел");
                    Console.WriteLine("24) Удалить акционный товар");
                    Console.WriteLine("25) Отобразить города конкретной страны");
                    Console.WriteLine("26) Отобразить разделы конкретного покупателя");
                    Console.WriteLine("27) Отобразить акционные товары конкретного раздела");
                    Console.Write("\nВведите значение: ");
                    string res = Console.ReadLine()!.ToString();
                    using (IDbConnection db = new SqlConnection(connection)) {
                        Console.Clear();
                        switch (res) {
                            case "0":
                                return;
                            case "1":
                                ShowAllBuyers(db);
                                break;
                            case "2":
                                ShowEmailBuyers(db);
                                break;
                            case "3":
                                ShowChapterList(db);
                                break;
                            case "4":
                                ShowPromotionProducts(db);
                                break;
                            case "5":
                                ShowCities(db);
                                break;
                            case "6":
                                ShowCountries(db);
                                break;
                            case "7":
                                ShowBuyersByCity(db);
                                break;
                            case "8":
                                ShowBuyersByCountry(db);
                                break;
                            case "9":
                                ShowPromotionsByCountry(db);
                                break;
                            case "10":
                                AddBuyer(db);
                                break;
                            case "11":
                                AddCountry(db);
                                break;
                            case "12":
                                AddCity(db);
                                break;
                            case "13":
                                AddChapter(db);
                                break;
                            case "14":
                                AddProduct(db);
                                break;
                            case "15":
                                UpdateBuyer(db);
                                break;
                            case "16":
                                UpdateCountry(db);
                                break;
                            case "17":
                                UpdateCity(db);
                                break;
                            case "18":
                                UpdateChapter(db);
                                break;
                            case "19":
                                UpdateProduct(db);
                                break;
                            case "20":
                                DeleteBuyer(db);
                                break;
                            case "21":
                                DeleteCountry(db);
                                break;
                            case "22":
                                DeleteCity(db);
                                break;
                            case "23":
                                DeleteBuyer(db);
                                break;
                            case "24":
                                DeleteChapter(db);
                                break;
                            case "25":
                                ShowCountryCities(db);
                                break;
                            case "26":
                                ShowBuyerChapters(db);
                                break;
                            case "27":
                                ShowChapterProducts(db);
                                break;
                            default:
                                DeleteProduct(db);
                                continue;
                        }
                        Console.Write("\nНажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                    }
                }
            }
            catch (FormatException fex) { Console.WriteLine(fex.Message); }
        }
        static void ShowAllBuyers(IDbConnection db)  {
            List<Buyer> buyers = (List<Buyer>)db.Query<Buyer>("SELECT * FROM Buyers");
            int iter = 0;
            foreach (var buyer in buyers) Console.WriteLine($"Покупатель #{++iter}: {buyer.FullName}, Email: {buyer.Email}");
        }
        static void ShowEmailBuyers(IDbConnection db) {
            List<Buyer> emails = db.Query<Buyer>("SELECT Email FROM Buyer").ToList();
            int iter = 0;
            foreach (var email in emails) Console.WriteLine($"Email покупателя #{++iter}: {email}");
        }
        static void ShowChapterList(IDbConnection db) {
            List<Chapter> chapters = db.Query<Chapter>("SELECT * FROM Chapters").ToList();
            int iter = 0;
            foreach (var chapter in chapters) Console.WriteLine($"Раздел #{++iter}: {chapter.Title}");
        }
        static void ShowPromotionProducts(IDbConnection db) {
            List<Promotional> promotions = db.Query<Promotional>("SELECT * FROM Promotionals").ToList();
            int iter = 0;
            foreach (var promotion in promotions) {
                Console.WriteLine($"Акционный товар #{++iter}:");
                Console.WriteLine($"Начальная дата: {promotion.StartDate}");
                Console.WriteLine($"Конечная дата: {promotion.EndDate}");
            }
        }
        static void ShowCities(IDbConnection db) {
            List<City> cities = db.Query<City>("SELECT * FROM Cities").ToList();
            int iter = 0;
            foreach (var city in cities) Console.WriteLine($"Город #{++iter}: {city.Title}");
        }
        static void ShowCountries(IDbConnection db) {
            List<City> countries = db.Query<City>("SELECT * FROM Countries").ToList();
            int iter = 0;
            foreach (var country in countries) Console.WriteLine($"Страна #{++iter}: {country.Title}");
        }
        static void ShowBuyersByCity(IDbConnection db) {
            Console.Write("Введите название города: ");
            string cityName = Console.ReadLine()!.ToString();
            List<Buyer> buyers = db.Query<Buyer>($"SELECT * FROM Buyers WHERE CityId IN (SELECT Id FROM Cities WHERE Title = '{cityName}')").ToList();
            if (buyers.Count > 0) {
                int iter = 0;
                foreach (var buyer in buyers) Console.WriteLine($"Покупатель #{++iter}: {buyer.FullName}");
            }
            else Console.WriteLine($"В базе данных нет покупателей из города {cityName}.");
        }
        static void ShowBuyersByCountry(IDbConnection db) {
            Console.Write("Введите название страны: ");
            string countryName = Console.ReadLine()!.ToString();
            List<Buyer> buyers = db.Query<Buyer>($"SELECT * FROM Buyers WHERE CountryId IN (SELECT Id FROM Countries WHERE Title = '{countryName}')").ToList();
            if (buyers.Count > 0) {
                int iter = 0;
                foreach (var buyer in buyers) Console.WriteLine($"Покупатель #{++iter}: {buyer.FullName}");
            }
            else Console.WriteLine($"В базе данных нет покупателей из страны {countryName}.");
        }
        static void ShowPromotionsByCountry(IDbConnection db) {
            Console.WriteLine("Введите название страны:");
            string countryName = Console.ReadLine()!.ToString();
            List<Promotional> promotions = db.Query<Promotional>($"SELECT * FROM Promotionals WHERE CountryId IN (SELECT Id FROM Countries WHERE Title = '{countryName}')").ToList();
            if (promotions.Count > 0) {
                int iter = 0;
                foreach (var promotion in promotions) {
                    Console.WriteLine($"Акция #{++iter}:");
                    Console.WriteLine($"Начальная дата: {promotion.StartDate}");
                    Console.WriteLine($"Конечная дата: {promotion.EndDate}");
                }
            }
            else Console.WriteLine($"В базе данных нет акций для страны {countryName}.");
        }
        static void AddBuyer(IDbConnection db) {
            Console.WriteLine("Введите информацию о новом покупателе:");
            Console.Write("Имя и фамилия: ");
            string fullName = Console.ReadLine() ?? "";
            Console.Write("Дата рождения (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate)) {
                Console.WriteLine("Некорректный формат даты. Операция отменена.");
                return;
            }
            Console.Write("Пол (М/Ж): ");
            string gender = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("ID страны: ");
            if (!int.TryParse(Console.ReadLine(), out int countryId)) {
                Console.WriteLine("Некорректный формат ID страны. Операция отменена.");
                return;
            }
            Console.Write("ID города: ");
            if (!int.TryParse(Console.ReadLine(), out int cityId)) {
                Console.WriteLine("Некорректный формат ID города. Операция отменена.");
                return;
            }
            Buyer buyer = new Buyer {
                CountryId = countryId,
                CityId = cityId,
                FullName = fullName,
                Birth = birthDate,
                Male = gender,
                Email = email
            };
            var sql = "INSERT INTO Buyers VALUES (@CountryId, @CityId, @FullName, @Birth, @Male, @Email)";
            var affectedRows = db.Execute(sql, buyer);
            if (affectedRows > 0) Console.WriteLine("Информация о покупателе успешно добавлена!");
            else Console.WriteLine("Ошибка при добавлении информации о покупателе!");
        }
        static void AddCountry(IDbConnection db) {
            Console.Write("Название страны: ");
            string title = Console.ReadLine() ?? "";
            Country country = new Country { Title = title };
            var sql = "INSERT INTO Countries VALUES (@Title)";
            var affectedRows = db.Execute(sql, country);
            if (affectedRows > 0) Console.WriteLine("Информация о стране успешно добавлена!");
            else Console.WriteLine("Ошибка при добавлении информации о стране!");
        }
        static void AddCity(IDbConnection db) {
            Console.Write("Название города: ");
            string title = Console.ReadLine() ?? "";
            City city = new City { Title = title };
            var sql = "INSERT INTO Cities VALUES (@Title)";
            var affectedRows = db.Execute(sql, city);
            if (affectedRows > 0) Console.WriteLine("Информация о городе успешно добавлена!");
            else Console.WriteLine("Ошибка при добавлении информации о городе!");
        }
        static void AddChapter(IDbConnection db) {
            Console.Write("Название раздела: ");
            string title = Console.ReadLine() ?? "";
            Chapter chapter = new Chapter { Title = title };
            var sql = "INSERT INTO Chapters VALUES (@Title)";
            var affectedRows = db.Execute(sql, chapter);
            if (affectedRows > 0) Console.WriteLine("Информация о разделе успешно добавлена!");
            else Console.WriteLine("Ошибка при добавлении информации о разделе!");
        }
        static void AddProduct(IDbConnection db) {
            Console.Write("ID акции: ");
            if (!int.TryParse(Console.ReadLine(), out int promotionalId)) {
                Console.WriteLine("Некорректный формат ID акции. Операция отменена.");
                return;
            }
            Console.Write("Название товара: ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Описание товара: ");
            string desc = Console.ReadLine() ?? "";
            Console.Write("Цена товара: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.WriteLine("Некорректный формат цены. Операция отменена.");
                return;
            }
            Product product = new Product {
                PromotionalId = promotionalId,
                Title = title,
                Description = desc,
                Price = price
            };
            var sql = "INSERT INTO Products VALUES (@PromotionalId, @Title, @Description, @Price)";
            var affectedRows = db.Execute(sql, product);
            if (affectedRows > 0) Console.WriteLine("Информация о продукте успешно добавлена!");
            else Console.WriteLine("Ошибка при добавлении информации о продукте!");
        }
        static void UpdateBuyer(IDbConnection db) {
            Console.Write("Введите ID покупателя, которого хотите обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int buyerId)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            Console.Write("Имя и фамилия: ");
            string fullName = Console.ReadLine() ?? "";
            Console.Write("Дата рождения (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate)) {
                Console.WriteLine("Некорректный формат даты. Операция отменена.");
                return;
            }
            Console.Write("Пол (М/Ж): ");
            string gender = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("ID страны: ");
            if (!int.TryParse(Console.ReadLine(), out int countryId)) {
                Console.WriteLine("Некорректный формат ID страны. Операция отменена.");
                return;
            }
            Console.Write("ID города: ");
            if (!int.TryParse(Console.ReadLine(), out int cityId)) {
                Console.WriteLine("Некорректный формат ID города. Операция отменена.");
                return;
            }
            Buyer buyer = new Buyer {
                Id = buyerId,
                CountryId = countryId,
                CityId = cityId,
                FullName = fullName,
                Birth = birthDate,
                Male = gender,
                Email = email
            };
            var sql = "UPDATE Buyers SET CountryId = @CountryId, CityId = @CityId, FullName = @FullName, Birth = @Birth, Male = @Male, Email = @Email WHERE Id = @Id";
            var affectedRows = db.Execute(sql, buyer);
            if (affectedRows > 0) Console.WriteLine("Информация о покупателе успешно обновлена!");
            else Console.WriteLine("Ошибка при обновлении информации о покупателе!");
        }
        static void UpdateCountry(IDbConnection db) {
            Console.Write("Введите ID страны, которую хотите обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            Console.Write("Название: ");
            string title = Console.ReadLine() ?? "";
            Country country = new Country {
                Id = id,
                Title = title
            };
            var sql = "UPDATE Countries SET Title = @Title WHERE Id = @Id";
            var affectedRows = db.Execute(sql, country);
            if (affectedRows > 0) Console.WriteLine("Информация о стране успешно обновлена!");
            else Console.WriteLine("Ошибка при обновлении информации о стране!");
        }
        static void UpdateCity(IDbConnection db) {
            Console.Write("Введите ID города, который хотите обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            Console.Write("Название: ");
            string title = Console.ReadLine() ?? "";
            City city = new City {
                Id = id,
                Title = title
            };
            var sql = "UPDATE Cities SET Title = @Title WHERE Id = @Id";
            var affectedRows = db.Execute(sql, city);
            if (affectedRows > 0) Console.WriteLine("Информация о городе успешно обновлена!");
            else Console.WriteLine("Ошибка при обновлении информации о городе!");
        }
        static void UpdateChapter(IDbConnection db) {
            Console.Write("Введите ID раздела, который хотите обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            Console.Write("Название: ");
            string title = Console.ReadLine() ?? "";
            Chapter chapter = new Chapter {
                Id = id,
                Title = title
            };
            var sql = "UPDATE Chapters SET Title = @Title WHERE Id = @Id";
            var affectedRows = db.Execute(sql, chapter);
            if (affectedRows > 0) Console.WriteLine("Информация о разделе успешно обновлена!");
            else Console.WriteLine("Ошибка при обновлении информации о разделе!");
        }
        static void UpdateProduct(IDbConnection db) {
            Console.Write("Введите ID продукта, который хотите обновить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            Console.Write("ID акции: ");
            if (!int.TryParse(Console.ReadLine(), out int promotionalId)) {
                Console.WriteLine("Некорректный формат ID акции. Операция отменена.");
                return;
            }
            Console.Write("Название товара: ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Описание товара: ");
            string desc = Console.ReadLine() ?? "";
            Console.Write("Цена товара: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) {
                Console.WriteLine("Некорректный формат цены. Операция отменена.");
                return;
            }
            Product product = new Product {
                Id = id,
                PromotionalId = promotionalId,
                Title = title,
                Description = desc,
                Price = price
            };
            var sql = "UPDATE Products SET PromotionalId = @PromotionalId, Title = @Title, Description = @Description, Price = @Price WHERE Id = @Id";
            var affectedRows = db.Execute(sql, product);
            if (affectedRows > 0) Console.WriteLine("Информация о продукте успешно обновлена!");
            else Console.WriteLine("Ошибка при обновлении информации о продукте!");
        }
        static void DeleteBuyer(IDbConnection db) {
            Console.Write("Введите ID покупателя, которого хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            var sql = "DELETE FROM Buyers WHERE Id = @id";
            var affectedRows = db.Execute(sql, new { id });
            if (affectedRows > 0) Console.WriteLine("Покупатель успешно удален!");
            else Console.WriteLine("Ошибка при удалении покупателя!");
        }
        static void DeleteCountry(IDbConnection db) {
            Console.Write("Введите ID страны, которую хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            var sql = "DELETE FROM Countries WHERE Id = @id";
            var affectedRows = db.Execute(sql, new { id });
            if (affectedRows > 0) Console.WriteLine("Страна успешно удалена!");
            else Console.WriteLine("Ошибка при удалении страны!");
        }
        static void DeleteCity(IDbConnection db) {
            Console.Write("Введите ID города, который хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            var sql = "DELETE FROM Cities WHERE Id = @id";
            var affectedRows = db.Execute(sql, new { id });
            if (affectedRows > 0) Console.WriteLine("Город успешно удален!");
            else Console.WriteLine("Ошибка при удалении города!");
        }
        static void DeleteChapter(IDbConnection db) {
            Console.Write("Введите ID раздела, который хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int id))  {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            var sql = "DELETE FROM Chapters WHERE Id = @id";
            var affectedRows = db.Execute(sql, new { id });
            if (affectedRows > 0) Console.WriteLine("Раздел успешно удален!");
            else Console.WriteLine("Ошибка при удалении раздела!");
        }
        static void DeleteProduct(IDbConnection db) {
            Console.Write("Введите ID продукта, который хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) {
                Console.WriteLine("Некорректный формат ID. Операция отменена.");
                return;
            }
            var sql = "DELETE FROM Products WHERE Id = @id";
            var affectedRows = db.Execute(sql, new { id });
            if (affectedRows > 0) Console.WriteLine("Продукт успешно удален!");
            else Console.WriteLine("Ошибка при удалении продукта!");
        }
        static void ShowCountryCities(IDbConnection db) {
            Console.WriteLine("Введите название страны:");
            string countryName = Console.ReadLine()!.ToString();
            int countryId = db.Query<int>(@"SELECT Id FROM Countries WHERE Title = @countryName", new { countryName }).FirstOrDefault();
            var cities = db.Query<City>(@"SELECT * FROM City WHERE CountryId = @countryId", new { countryId } );
            if (cities.Count() > 0) {
                foreach (var city in cities) Console.WriteLine(city.Title);
            }
            else Console.WriteLine($"В стране {countryName} нет городов.");
        }
        static void ShowBuyerChapters(IDbConnection db) {
            Console.Write("Введите имя покупателя: ");
            string buyerName = Console.ReadLine()!.ToString();
            int buyerId = db.Query<int>(@"SELECT Id FROM Buyers WHERE FullName = @buyerName", new { buyerName } ).FirstOrDefault();
            var chapters = db.Query<Chapter>(@"SELECT * FROM Chapter WHERE BuyerId = @buyerId", new { buyerId } );
            if (chapters.Count() > 0) {
                foreach (var chapter in chapters) Console.WriteLine(chapter.Title);
            }
            else Console.WriteLine($"У покупателя с именем {buyerName} нет разделов.");
        }
        static void ShowChapterProducts(IDbConnection db) {
            Console.Write("Введите название раздела: ");
            string chapterName = Console.ReadLine()!.ToString();
            int chapterId = db.Query<int>(@"SELECT Id FROM Chapters WHERE Title = @chapterName", new { chapterName }).FirstOrDefault();
            var products = db.Query<Product>(@"SELECT * FROM Products WHERE PromotionalId IN (SELECT Id FROM Promotionals WHERE ChapterId = @chapterId AND StartDate <= GETDATE() AND EndDate >= GETDATE())", new { chapterId });
            if (products.Count() > 0) {
                foreach (var product in products) {
                    Console.WriteLine($"**Товар:** {product.Title}");
                    Console.WriteLine($"**Описание:** {product.Description}");
                    Console.WriteLine($"**Цена:** {product.Price}");
                    Console.WriteLine();
                }
            }
            else Console.WriteLine($"В разделе {chapterName} нет акционных товаров.");
        }
    }
}