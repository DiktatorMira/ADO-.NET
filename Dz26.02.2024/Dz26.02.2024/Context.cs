using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz26._02._2024 {
    public class Context : DbContext{
        public DbSet<Countries> Сountries { get; set; }
        public DbSet<Continent> Сontinents { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) {
            Continent europe = new Continent { Title = "Европа" };
            Continent northAmerica = new Continent { Title = "Северная Америка" };
            Continent africa = new Continent { Title = "Африка" };
            Continent asia = new Continent { Title = "Азия" };
            Сontinents?.AddRange(europe, northAmerica, africa, asia);
            SaveChanges();
            Countries ukraine = new Countries {
                Title = "Украина",
                Capital = "Киев",
                Population = 37937820,
                Area = 603628,
                Continent = europe
            };
            Countries germany = new Countries {
                Title = "Германия",
                Capital = "Берлин",
                Population = 83200000,
                Area = 357592,
                Continent = europe
            };
            Countries mexico = new Countries {
                Title = "Мексика",
                Capital = "Мехико",
                Population = 126700000,
                Area = 1973000,
                Continent = northAmerica
            };
            Countries egypt = new Countries {
                Title = "Египет",
                Capital = "Каир",
                Population = 109300000,
                Area = 1002000,
                Continent = africa
            };
            Countries china = new Countries{
                Title = "Китай",
                Capital = "Пекин",
                Population = 1412000000,
                Area = 9597000,
                Continent = asia
            };
            Сountries?.AddRange(ukraine, germany, mexico, egypt, china);
            SaveChanges();
        }
    }
}
