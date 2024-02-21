public class Person {
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? City { get; set; }
}
public static class Program {
    static List<Person> person = new List<Person>() {
        new Person(){ Name = "Andrey", Age = 24, City = "Kyiv"},
        new Person(){ Name = "Liza", Age = 18, City = "Odesa" },
        new Person(){ Name = "Oleg", Age = 15, City = "London" },
        new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
        new Person(){ Name = "Sergey", Age = 32, City = "Lviv" }
    };
    public static void Older25() {
        var selectedPersons1 = from person in person where person.Age > 25 select person;
        foreach (var person in selectedPersons1) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        Console.Write("\n");
        var selectedPersons2 = person.Where(person => person.Age > 25);
        foreach (var person in selectedPersons2) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
    }
    public static void NotInLondon() {
        var selectedPersons1 = from person in person where person.City != "London" select person;
        foreach (var person in selectedPersons1) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        Console.Write("\n");
        var selectedPersons2 = person.Where(person => person.City != "London");
        foreach (var person in selectedPersons2) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
    }
    public static void LiveInKyiv() {
        var selectedPersons1 = from person in person where person.City == "Kyiv" select person.Name;
        foreach (var name in selectedPersons1) Console.WriteLine(name);
        Console.Write("\n");
        var namesInKyiv = person.Where(person => person.City == "Kyiv").Select(person => person.Name);
        foreach (var name in namesInKyiv) Console.WriteLine(name);
    }
    public static void Older35Sergey() {
        var selectedPersons1 = from person in person where person.Name == "Sergey" 
        && person.Age > 35 select person;
        foreach (var person in selectedPersons1) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        Console.Write("\n");
        var selectedPersons2 = person.Where(person => person.Name == "Sergey" && person.Age > 35);
        foreach (var person in selectedPersons2) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
    }
    public static void LiveInOdesa() {
        var selectedPersons1 = from person in person where person.City == "Odesa" select person;
        foreach (var person in selectedPersons1) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        Console.Write("\n");
        var residentsOfOdesa = person.Where(person => person.City == "Odesa");
        foreach (var person in residentsOfOdesa) Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
    }
    public static void Main(string[] Args) {
        Older25();
        NotInLondon();
        LiveInKyiv();
        Older35Sergey();
        LiveInOdesa();
    }
}