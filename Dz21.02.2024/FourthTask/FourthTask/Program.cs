public class Good {
    public int Id { get; set; }
    public string? Title { get; set; }
    public double Price { get; set; }
    public string? Category { get; set; }
}
public static class Program {
    static List<Good> goods = new List<Good>() {
        new Good() { Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
        new Good() { Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
        new Good() { Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
        new Good() { Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
        new Good() { Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" },
        new Good() { Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
        new Good() { Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
        new Good() { Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
        new Good() { Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
        new Good() { Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
    };
    public static void FirstSelect() {
        var result1 = (from g in goods where g.Category == "Mobile" && g.Price > 1000 select g).ToList();
        foreach (var good in result1) Console.WriteLine($"{good.Id} {good.Title} {good.Price} {good.Category}");
        Console.Write("\n");
        var result2 = goods.Where(g => g.Category == "Mobile" && g.Price > 1000).ToList();
        foreach (var good in result2) Console.WriteLine($"{good.Id} {good.Title} {good.Price} {good.Category}");
    }
    public static void SecondSelect() {
        var result1 = (from g in goods where g.Category != "Kitchen" && g.Price > 1000 
        select new { g.Title, g.Price }).ToList();
        foreach (var selectedGood in result1) Console.WriteLine($"{selectedGood.Title} {selectedGood.Price}");

        Console.Write("\n");

        var result2 = goods.Where(g => g.Category != "Kitchen" && g.Price > 1000)
        .Select(g => new { g.Title, g.Price }).ToList();
        foreach (var selectedGood in result2)Console.WriteLine($"{selectedGood.Title} {selectedGood.Price}");
    }
    public static void ThirdSelect() {
        double result1 = (from g in goods select g.Price).Average();
        Console.WriteLine($"Средняя цена товаров: {result1}\n");
        double result2 = goods.Select(g => g.Price).Average();
        Console.WriteLine($"Средняя цена товаров: {result2}");
    }
    public static void FourthSelect() {
        var result1 = (from g in goods select g.Category).Distinct().ToList();
        foreach (var category in result1) Console.WriteLine(category);
        Console.Write("\n");
        var result2 = goods.Select(g => g.Category).Distinct().ToList();
        foreach (var category in result2)Console.WriteLine(category);
    }
    public static void FifthSelect() {
        var result1 = (from g in goods orderby g.Title select new { g.Title, g.Category }).ToList();
        foreach (var orderedGood in result1) Console.WriteLine($"{orderedGood.Title} {orderedGood.Category}");
        Console.Write("\n");
        var result2 = goods.OrderBy(g => g.Title).Select(g => new { g.Title, g.Category }).ToList();
        foreach (var orderedGood in result2) Console.WriteLine($"{orderedGood.Title} {orderedGood.Category}");
    }
    public static void SixthSelect() {
        int result1 = (from g in goods where g.Category == "Car" || g.Category == "Mobile" select g).Count();
        Console.WriteLine($"Всего товаров: {result1}\n");
        int result2 = goods.Where(g => g.Category == "Car" || g.Category == "Mobile").Count();
        Console.WriteLine($"Всего товаров: {result2}");
    }
    public static void SeventhSelect() {
        var result1 = (from g in goods group g by g.Category into grouped
        select new { Category = grouped.Key, Count = grouped.Count() }).ToList();
        foreach (var categoryCount in result1)Console.WriteLine($"{categoryCount.Category} {categoryCount.Count}");
        
        Console.Write("\n");

        var result2 = goods.GroupBy(g => g.Category).Select(group => new { Category = group.Key, 
        Count = group.Count() }).ToList();і
        foreach (var categoryCount in result2) Console.WriteLine($"{categoryCount.Category} {categoryCount.Count}");
    }
    public static void Main(string[] args) {
        FirstSelect();
        SecondSelect();
        ThirdSelect();
        FourthSelect();
        FifthSelect();
        SixthSelect();
        SeventhSelect();
    }
}