public class Employee {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string?LastName { get; set; }
    public int Age { get; set; }
    public int DepId { get; set; }
}
public class Department {
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}
public class Program {
    static List<Department> departments = new List<Department>() {
        new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
        new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
        new Department(){ Id = 3, Country = "France", City = "Paris" },
        new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
    };
    static List<Employee> employees = new List<Employee>() {
        new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
        new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
        new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
        new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
        new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
        new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
        new Employee() { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27, DepId = 4 }
    };
    public static void FirstSelect() {
        var result1 = from employee in employees join department in departments on employee.DepId 
        equals department.Id where department.Country == "Ukraine" && department.City != "Odesa"
        select new { employee.FirstName, employee.LastName };
        foreach (var item in result1) Console.WriteLine($"{item.FirstName} {item.LastName}");

        Console.Write("\n");

        var result2 = employees.Join(departments, employee => employee.DepId, department => department.Id,
        (employee, department) => new { Employee = employee, Department = department }).Where(joinResult =>
        joinResult.Department.Country == "Ukraine" && joinResult.Department.City != "Odesa").Select(joinResult => 
        new { Name = joinResult.Employee.FirstName, LastName = joinResult.Employee.LastName });
        foreach (var person in result2) Console.WriteLine($"{person.Name} {person.LastName}");
    }
    public static void SecondSelect() {
        var result1 = (from department in departments select department.Country).Distinct();
        foreach (var country in result1) Console.WriteLine(country);
        Console.Write("\n");
        var result2 = departments.Select(department => department.Country).Distinct();
        foreach (var country in result2) Console.WriteLine(country);
    }
    public static void ThirdSelect() {
        var result1 = (from employee in employees where employee.Age > 25 select employee).Take(3);
        foreach (var employee in result1) Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Age}");
        Console.Write("\n");
        var result2 = employees.Where(employee => employee.Age > 25).Take(3);
        foreach (var employee in result2) Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Age}");
    }
    public static void FourthSelect() {
        var result1 = from employee in employees join department in departments on employee.DepId equals department.Id
        where department.City == "Kyiv" && employee.Age > 23 select new { employee.FirstName, 
        employee.LastName, employee.Age };
        foreach (var employee in result1) Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Age}");
        
        Console.Write("\n");

        var result2 = employees.Join(departments, employee => employee.DepId, department => department.Id,
        (employee, department) => new { Employee = employee, Department = department }).Where(joinResult => 
        joinResult.Department.City == "Kyiv" && joinResult.Employee.Age > 23).Select(joinResult => new 
        { joinResult.Employee.FirstName, joinResult.Employee.LastName, joinResult.Employee.Age });
        foreach (var employee in result2) Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Age}");
    }
    public static void Main(string[] Args) {
        FirstSelect();
        SecondSelect();
        ThirdSelect();
        FourthSelect();
    }
}