using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dz06._03._2024 {
    public class PositionsM {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string? Title { get; set; }
        public virtual ICollection<EmployeesM> Employees { get; set; } = new List<EmployeesM>();
    }
    public class EmployeesM {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        public string? FullName { get; set; }
        public virtual PositionsM? Position { get; set; }
    }
    public class Context : DbContext {
        private static DbContextOptions<Context> options;
        public DbSet<EmployeesM> Employees { get; set; }
        public DbSet<PositionsM> Positions { get; set; }
        static Context() {
            if (options == null) {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                options = optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
            }
        }
        public Context() : base(options) {
            if (Database.EnsureCreated()) {
                var positions = new List<PositionsM>() {
                    new PositionsM { Title = "Менеджер" },
                    new PositionsM { Title = "Разработчик" },
                    new PositionsM { Title = "Дизайнер" },
                    new PositionsM { Title = "Тестер" }
                };
                var employees = new List<EmployeesM>() {
                    new EmployeesM { FullName = "Джон Лин", Position = positions[0] },
                    new EmployeesM { FullName = "Григорий Лепс", Position = positions[1] },
                    new EmployeesM { FullName = "Агата Лемберг", Position = positions[2] },
                    new EmployeesM { FullName = "Том Пот", Position = positions[3] }
                };
                Positions?.AddRange(positions);
                Employees?.AddRange(employees);
                SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<EmployeesM>().Property(e => e.FullName).IsRequired();
            modelBuilder.Entity<EmployeesM>().HasOne(e => e.Position).WithMany(p => p.Employees).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PositionsM>().Property(p => p.Title).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
