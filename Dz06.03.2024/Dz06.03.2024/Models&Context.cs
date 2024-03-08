using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dz06._03._2024 {
    public class PositionsM {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public virtual ICollection<EmployeesM> Employees { get; set; } = new List<EmployeesM>();
    }
    public class EmployeesM {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public virtual PositionsM Position { get; set; }
    }
    public class Context : DbContext {
        private static DbContextOptions<Context> options;
        private readonly ILogger<Context> logger;
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
        public Context() : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<EmployeesM>().Property(e => e.FullName).IsRequired();
            modelBuilder.Entity<EmployeesM>().HasOne(e => e.Position).WithMany(p => p.Employees).HasForeignKey(e => e.PositionId);
            modelBuilder.Entity<PositionsM>().Property(p => p.Title).IsRequired();
            InitializeData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void InitializeData(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PositionsM>().HasData(
                new PositionsM { Id = 1, Title = "Менеджер" },
                new PositionsM { Id = 2, Title = "Разработчик" },
                new PositionsM { Id = 3, Title = "Дизайнер" },
                new PositionsM { Id = 4, Title = "Тестер" }
            );
            modelBuilder.Entity<EmployeesM>().HasData(
                new EmployeesM { Id = 1, FullName = "Джон Лин", PositionId = 1 },
                new EmployeesM { Id = 2, FullName = "Григорий Лепс", PositionId = 2 },
                new EmployeesM { Id = 3, FullName = "Агата Лемберг", PositionId = 3 },
                new EmployeesM { Id = 4, FullName = "Том Пот", PositionId = 4 }
            );
        }
    }
}
