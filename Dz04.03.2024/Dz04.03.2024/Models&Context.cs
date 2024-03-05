using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.ComponentModel;

namespace Dz04._03._2024 {
    public class AuthorsM {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public virtual ICollection<BooksM>? Books { get; set; } = new List<BooksM>();
    }
    public class BooksM {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual AuthorsM? Author { get; set; }
    }
    public class Context : DbContext {
        static DbContextOptions<Context> options;
        public virtual DbSet<AuthorsM> authors { get; set; }
        public virtual DbSet<BooksM> books { get; set; }
        static Context() {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("settings.json");
            var config = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")).Options;
        }
        public Context() : base(options) => InitializeDatabase();
        public async Task InitializeDatabase() {
            if (Database.EnsureCreated()) {
                var authorsList = new List<AuthorsM> {
                    new AuthorsM { Id = 1, FullName = "Мартин Фаулер" },
                    new AuthorsM { Id = 2, FullName = "Роберт Мартин" },
                    new AuthorsM { Id = 3, FullName = "Агата Кристи" },
                    new AuthorsM { Id = 4, FullName = "Артур Конан Дойл" },
                    new AuthorsM { Id = 5, FullName = "Дэн Браун" },
                    new AuthorsM { Id = 6, FullName = "Айзек Азимов" }
                };
                var booksList = new List<BooksM> {
                    new BooksM { Id = 1, Title = "Рефакторинг: Улучшение дизайна существующего кода", AuthorId = 1 },
                    new BooksM { Id = 2, Title = "Чистый код: Руководство по написанию приемлемого кода", AuthorId = 2 },
                    new BooksM { Id = 3, Title = "Убийство в Восточном экспрессе", AuthorId = 3 },
                    new BooksM { Id = 4, Title = "Собака Баскервилей", AuthorId = 4 },
                    new BooksM { Id = 5, Title = "Код да Винчи", AuthorId = 5 },
                    new BooksM { Id = 6, Title = "Я, робот", AuthorId = 6 },
                    new BooksM { Id = 7, Title = "Дизайн паттерны", AuthorId = 1 },
                    new BooksM { Id = 8, Title = "Мифы о языках программирования", AuthorId = 2 }
                };
                authors?.AddRange(authorsList);
                books?.AddRange(booksList);
                await SaveChangesAsync();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<BooksM>().HasOne(a => a.Author).WithMany(b => b.Books).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
