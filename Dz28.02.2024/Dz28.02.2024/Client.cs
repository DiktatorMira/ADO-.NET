using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tables;
using TablesContext;

namespace Dz28._02._2024 {
    public static class Client {
        public static async Task PrintAllInfo(Context context) {
            var allInfo = await context.countries.Include(c => c.Continent).ToListAsync();
            foreach (var country in allInfo) {
                Console.WriteLine($"ID: {country.Id}");
                Console.WriteLine($"Название: {country.Title}");
                Console.WriteLine($"Столица: {country.Capital}");
                Console.WriteLine($"Население: {country.Population}");
                Console.WriteLine($"Площадь: {country.Area}");
                Console.WriteLine($"Континент: {country.Continent?.Title}\n");
            }
            Console.WriteLine("--------------------------------------");
        }
        public static async Task AddCountry(Context context, string title, 
            string capital, long population, double area, string cont) {
            var continent = await context.сontinents.FirstOrDefaultAsync(c => c.Title == cont);
            if (continent == null){
                Console.WriteLine($"Континент с таким названием не найден в БД!");
                return;
            }
            var newCountry = new Countries {
                Title = title,
                Capital = capital,
                Population = population,
                Area = area,
                Continent = continent
            };
            context.countries.Add(newCountry);
            await context.SaveChangesAsync();
            Console.WriteLine("Страна добавлена!");
        }
        public static async Task AddCapital(Context context, string capital, string ctitle) {
            var country = await context.countries.FirstOrDefaultAsync(c => c.Title == ctitle);
            if (country == null){
                Console.WriteLine($"Страна с названием '{ctitle}' не найдена в базе данных.");
                return;
            }
            country.Capital = capital;
            await context.SaveChangesAsync();
        }
        public static async Task AddPopulation(Context context, long population, string ctitle) {
            var country = await context.countries.FirstOrDefaultAsync(c => c.Title == ctitle);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{ctitle}' не найдена в базе данных.");
                return;
            }
            country.Population = population;
            await context.SaveChangesAsync();
        }
        public static async Task UpdateCountry(Context context, string title,
            string capital, long population, double area, string cont) {
            var country = await context.countries.Include(c => c.Continent).FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            country.Title = title;
            country.Capital = capital;
            country.Population = population;
            country.Area = area;
            var continent = await context.сontinents.FirstOrDefaultAsync(c => c.Title == cont);
            if (continent == null) {
                Console.WriteLine($"Континент с названием '{cont}' не найден в базе данных.");
                return;
            }
            country.Continent = continent;
            await context.SaveChangesAsync();
        }
        public static async Task UpdateCapital(Context context, string title, string capital) {
            var country = await context.countries.FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            country.Capital = capital;
            await context.SaveChangesAsync();
        }
        public static async Task UpdatePopulation(Context context, string title, long population) {
            var country = await context.countries.FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            country.Population = population;
            await context.SaveChangesAsync();
        }
        public static async Task DeleteCountry(Context context, string title) {
            var country = await context.countries.Include(c => c.Continent).FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            context.countries.Remove(country);
            await context.SaveChangesAsync();
        }
        public static async Task DeleteCapital(Context context, string title) {
            var country = await context.countries.Include(c => c.Continent).FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            country.Capital = null;
            await context.SaveChangesAsync();
        }
        public static async Task DeletePopulation(Context context, string title) {
            var country = await context.countries.Include(c => c.Continent).FirstOrDefaultAsync(c => c.Title == title);
            if (country == null) {
                Console.WriteLine($"Страна с названием '{title}' не найдена в базе данных.");
                return;
            }
            country.Population = 0;
            await context.SaveChangesAsync();
        }
        public static async Task Main(string[] args) {
            try {
                using (var db = new Context()) {
                    await PrintAllInfo(db);
                    await AddCountry(db, "Австралия", "Канберра", 25690000, 7688000, "Австралия");
                    await AddCapital(db, "Канберра", "Австралия");
                    await AddPopulation(db, 100000000, "Австралия");
                    await UpdateCountry(db, "Аргентина", "Буэнос-Айрес", 45810000, 2780000, "Южная Америка");
                    await UpdateCapital(db, "Украина", "Буэнос-Айрес");
                    await UpdatePopulation(db, "Украина", 120000000);
                    await DeleteCountry(db, "Австралия");
                    await DeleteCapital(db, "Австралия");
                    await DeletePopulation(db, "Австралия");
                }
            }
            catch (Exception ex) {
                Console.Write("Ошибка: " + ex.Message);
                if (ex.InnerException != null) Console.WriteLine("Внутренняя ошибка: " + ex.InnerException.Message);
            }
        }
    }
}