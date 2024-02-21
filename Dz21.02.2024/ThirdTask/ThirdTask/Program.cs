public class Employee {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public int DepId { get; set; }
}
public class Department {
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}
public static class Program {
    static List<Department> departments = new List<Department>() {
        new Department(){ Id = 1, Country = "Ukraine", City = "Odesa"},
        new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
        new Department(){ Id = 3, Country = "France", City = "Paris" },
        new Department(){ Id = 4, Country = " Ukraine ", City = "Lviv"}
    };
    static List<Employee> employees = new List<Employee>() {
        new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
        new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
        new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
        new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
        new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
        new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
        new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
    };
    public static void FirstSelect() {
        var result1 = (from emp in employees join dep in departments on emp.DepId equals dep.Id
        where dep.Country?.Trim().Equals("Ukraine", StringComparison.OrdinalIgnoreCase) == true
        orderby emp.FirstName, emp.LastName select emp).ToList();
        foreach (var employee in result1) Console.WriteLine($"{employee.FirstName} {employee.LastName}");

        Console.Write("\n");

        var result2 = employees .Where(emp => departments.Any(dep => dep.Id == emp.DepId && dep.Country?
        .Trim().Equals("Ukraine", StringComparison.OrdinalIgnoreCase) == true)).OrderBy(emp => emp.FirstName)
        .ThenBy(emp => emp.LastName).ToList();
        foreach (var employee in result2) Console.WriteLine($"{employee.FirstName} {employee.LastName}");
    }
    public static void SecondSelect() {
        var result1 = (from emp in employees orderby emp.Age descending select new 
        { emp.Id, emp.FirstName, emp.LastName, emp.Age }).ToList();
        foreach (var employee in result1) Console.WriteLine($"{employee.Id}, {employee.FirstName}, {employee.LastName}, {employee.Age}");

        Console.Write("\n");

        var result2 = employees.OrderByDescending(emp => emp.Age).Select(emp => new 
        { emp.Id, emp.FirstName, emp.LastName, emp.Age }).ToList();
        foreach (var employee in result2) Console.WriteLine($"{employee.Id}, {employee.FirstName}, {employee.LastName}, {employee.Age}");
    }
    public static void ThirdSelect() {
        var result1 = (from emp in employees group emp by emp.Age into ageGroup
        select new { Age = ageGroup.Key, Count = ageGroup.Count() }).ToList();
        foreach (var ageGroup in result1) Console.WriteLine($"Возраст: {ageGroup.Age}, Кол-во: {ageGroup.Count}");

        Console.Write("\n");

        var result2 = employees.GroupBy(emp => emp.Age).Select(group => new { Age = group.Key, 
        Count = group.Count() }).ToList();
        foreach (var ageGroup in result2) Console.WriteLine($"Возраст: {ageGroup.Age}, Кол-во: {ageGroup.Count}");
    }
    public static void Main() {
        FirstSelect();
        SecondSelect();
        ThirdSelect();
    }
}