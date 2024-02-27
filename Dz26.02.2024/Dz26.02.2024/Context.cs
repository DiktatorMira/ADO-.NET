using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz26._02._2024 {
    public class Context : DbContext{
        public DbSet<Countries> countries { get; set; }
        public DbSet<Continent> сontinents { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) {
            if (Database.EnsureCreated()) {
                var cont = new List<Continent> {
                    new Continent { Title = "Европа" },
                    new Continent { Title = "Северная Америка" },
                    new Continent { Title = "Африка" },
                    new Continent { Title = "Азия" }
                };
                var count = new List<Countries> {
                    new Countries {
                        Title = "Украина",
                        Capital = "Киев",
                        Population = 37937820,
                        Area = 603628,
                        Continent = cont.FirstOrDefault(c => c.Title == "Европа")
                    },
                    new Countries {
                        Title = "Германия",
                        Capital = "Берлин",
                        Population = 83200000,
                        Area = 357592,
                        Continent = cont.FirstOrDefault(c => c.Title == "Европа")
                    },
                    new Countries {
                        Title = "Мексика",
                        Capital = "Мехико",
                        Population = 126700000,
                        Area = 1973000,
                        Continent = cont.FirstOrDefault(c => c.Title == "Северная Америка")
                    },
                    new Countries {
                        Title = "Египет",
                        Capital = "Каир",
                        Population = 109300000,
                        Area = 1002000,
                        Continent = cont.FirstOrDefault(c => c.Title == "Африка")
                    },
                    new Countries{
                        Title = "Китай",
                        Capital = "Пекин",
                        Population = 1412000000,
                        Area = 9597000,
                        Continent = cont.FirstOrDefault(c => c.Title == "Азия")
                    }
                };
                сontinents?.AddRange(cont);
                countries?.AddRange(count);
                SaveChanges();
            }
        }
    }
}
