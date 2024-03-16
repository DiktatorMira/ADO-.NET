using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Dz13._03._2024 {
    public class Game {
        [Key]
        public int GameId { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        public virtual Studio? Studio { get; set; }
        public virtual Genre? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? GameMode { get; set; }
        public ulong SoldCopies { get; set; }
    }
    public class Studio {
        [Key]
        public int StudioId { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
    }
    public class Genre {
        [Key]
        public int GenreId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
    }
    public class Context : DbContext {
        private static DbContextOptions<Context> options;
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Genre> Genres { get; set; }
        static Context() {
            if (options == null) {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions => {
                    sqlOptions.EnableRetryOnFailure();
                }).Options;
            }
        }
        public Context() : base(options) { }
    }
    public static class Program {
        public static void Main(string[] args) {
            // Пока что проект только в 1 файле, т.к. были проблемы с подключением зависимостей, в txt будет описано подробнее
            try { 
                using (var db = new Context()) {
                    List<Game> list1 = db.Games.Include(g => g.Studio).ToList();
                    List<Game> list2 = db.Games.Include(g => g.Genre).ToList();
                    if ((list1 == null || list1.Count == 0) && (list2 == null || list2.Count == 0)) {
                        var studios = new List<Studio> {
                            new Studio { Name = "Ubisoft" },
                            new Studio { Name = "Nintendo" },
                            new Studio { Name = "Naughty Dog" },
                            new Studio { Name = "CD Projekt Red" },
                            new Studio { Name = "Mojang" },
                            new Studio { Name = "Valve" },
                            new Studio { Name = "Mundfish" }
                        };
                        var genres = new List<Genre> {
                            new Genre { Name = "Action" },
                            new Genre { Name = "Role-playing" },
                            new Genre { Name = "Adventure" },
                            new Genre { Name = "Simulation" },
                            new Genre { Name = "Sandbox" },
                            new Genre { Name = "Shooter" }
                        };
                        var games = new List<Game> {
                            new Game {
                                Title = "Assassin's Creed",
                                ReleaseDate = new DateTime(2007, 11, 13),
                                Studio = studios[0],
                                Genre = genres[0],
                                GameMode = "multiplayer",
                                SoldCopies = 100000000
                            },
                            new Game {
                                Title = "The Legend of Zelda",
                                ReleaseDate = new DateTime(1986, 2, 21),
                                Studio = studios[1],
                                Genre = genres[1],
                                GameMode = "singleplayer",
                                SoldCopies = 21000000
                            },
                            new Game {
                                Title = "The Last of Us",
                                ReleaseDate = new DateTime(2013, 6, 14),
                                Studio = studios[2],
                                Genre = genres[2],
                                GameMode = "multiplayer",
                                SoldCopies = 37000000
                            },
                            new Game {
                                Title = "Cyberpunk 2077",
                                ReleaseDate = new DateTime(2020, 12, 10),
                                Studio = studios[3],
                                Genre = genres[3],
                                GameMode = "singleplayer",
                                SoldCopies = 25000000
                            },
                            new Game {
                                Title = "Minecraft",
                                ReleaseDate = new DateTime(2011, 11, 18),
                                Studio = studios[4],
                                Genre = genres[4],
                                GameMode = "multiplayer",
                                SoldCopies = 300000000
                            },
                            new Game {
                                Title = "Half-life",
                                ReleaseDate = new DateTime(1998, 11, 19),
                                Studio = studios[5],
                                Genre = genres[5],
                                GameMode = "singleplayer",
                                SoldCopies = 9300000
                            },
                            new Game {
                                Title = "Atomic Heart",
                                ReleaseDate = new DateTime(2022, 12, 31),
                                Studio = studios[6],
                                Genre = genres[5],
                                GameMode = "singleplayer",
                                SoldCopies = 12000000
                            }
                        };
                        db.Studios?.AddRange(studios);
                        db.Genres?.AddRange(genres);
                        db.Games?.AddRange(games);
                        db.SaveChanges();
                    }
                    foreach (var game in db.Games.ToList()) {
                        Console.WriteLine($"Название: {game.Title}");
                        Console.WriteLine($"Студия: {game.Studio?.Name ?? "Нет информации"}");
                        Console.WriteLine($"Жанр: {game.Genre?.Name ?? "Нет информации"}");
                        Console.WriteLine($"Дата выхода: {game.ReleaseDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"Режим игры: {game.GameMode ?? "Нет информации"}");
                        Console.WriteLine($"Копий продано: {game.SoldCopies}");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); }
        }
    }
}