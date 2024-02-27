using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;

namespace Dz26._02._2024 {
    public static class Program {
        public static void PrintAllInfo(DbContextOptions<Context> options, Context context) {
            var allInfo = context.Сountries.Include(c => c.Continent).ToList();
            foreach (var country in allInfo) {
                Console.WriteLine($"ID: {country.Id}");
                Console.WriteLine($"Название: {country.Title}");
                Console.WriteLine($"Столица: {country.Capital}");
                Console.WriteLine($"Население: {country.Population}");
                Console.WriteLine($"Площадь: {country.Area}");
                Console.WriteLine($"Континент: {country.Continent?.Title}\n");
            }
        }
        public static void PrintTitles(DbContextOptions<Context> options, Context context) {
            var titles = context.Сountries.Select(c => c.Title).ToList();
            Console.WriteLine("Названия стран:");
            foreach (var title in titles) Console.WriteLine(title);
        }
        public static void PrintCapitals(DbContextOptions<Context> options, Context context) {
            var capitals = context.Сountries.Select(c => c.Capital).ToList();
            Console.WriteLine("Столицы стран:");
            foreach (var capital in capitals) Console.WriteLine(capital);
        }
        public static void PrintEuropean(DbContextOptions<Context> options, Context context) {
            var european = context.Сountries.Where(c => c.Continent != null 
            && c.Continent.Title == "Европа").ToList();
            Console.WriteLine("Названия европейских стран:");
            foreach (var country in european) Console.WriteLine(country.Title);
        }
        public static void PrintExtraCountries(DbContextOptions<Context> options, Context context) {
            var extraCountries = context.Сountries.Where(c => c.Area > 1000000).ToList();
            Console.WriteLine("Страны с площадью больше 1000000 км^2:");
            foreach (var country in extraCountries) Console.WriteLine($"Название: {country.Title}, Площадь: {country.Area}");
        }
        public static void PrintAECountries(DbContextOptions<Context> options, Context context) {
            var aeCountries = context.Сountries.Where(c => c.Title != null && c.Title.Contains("а") 
            && c.Title.Contains("е")).ToList();
            Console.WriteLine("Страны с буквами 'а' и 'е' в названии:");
            foreach (var country in aeCountries) Console.WriteLine(country.Title);
        }
        public static void PrintStartA(DbContextOptions<Context> options, Context context) {
            var countriesA = context.Сountries.Where(c => c.Title != null 
            && c.Title.StartsWith("А", StringComparison.OrdinalIgnoreCase)).ToList();
            Console.WriteLine("Страны, название которых начинается с буквы 'А':");
            foreach (var country in countriesA) Console.WriteLine(country.Title);
        }
        public static void PrintExtraArea(DbContextOptions<Context> options, Context context) {
            var extraArea = context.Сountries.Where(c => c.Area >= 500000 && c.Area <= 2000000).ToList();
            Console.WriteLine("Страны с площадью от 500000 до 2000000:");
            foreach (var country in extraArea) Console.WriteLine($"Название: {country.Title}, Площадь: {country.Area}");
        }
        public static void PrintExtraPopulation(DbContextOptions<Context> options, Context context) {
            var extraPopulation = context.Сountries.Where(c => c.Population > 1100000).ToList();
            Console.WriteLine("Страны с населением больше 1100000 человек:");
            foreach (var country in extraPopulation) Console.WriteLine($"Название: {country.Title}, Население: {country.Population}");
        }
        public static void Main(string[] args) {
            try { 
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("settings.json");
                var config = builder.Build();
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                var options = optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
                Console.WriteLine("Успешное подключение к БД!\n");

                using (Context context = new Context(options)) {
                    PrintAllInfo(options, context);
                    PrintTitles(options, context);
                    PrintCapitals(options, context);
                    PrintEuropean(options, context);
                    PrintExtraCountries(options, context);
                    PrintAECountries(options, context);
                    PrintStartA(options, context);
                    PrintExtraArea(options, context);
                    PrintExtraPopulation(options, context);
                }
            }
            catch(Exception ex) {
                Console.Write("Ошибка: " + ex.Message);
                if (ex.InnerException != null) Console.WriteLine("Внутренняя ошибка: " + ex.InnerException.Message);
            }
        }
    }
}