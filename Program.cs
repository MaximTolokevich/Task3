using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Task2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employees");
            EmployeeRepository repository = new();
            var path = @"C:\Users\Trolo\source\repos\EducSenama\Task2\data.txt";

            while (true)
            {
                var name = string.Empty;
                int age = 0;

                Console.WriteLine("Enter the command");
                var command = Console.ReadLine().Trim();
                var json = string.Empty;
                switch (command)
                {

                    case "e":
                        Console.WriteLine("Enter the name");
                        name = ReturnValidName(Console.ReadLine().Trim());

                        Console.WriteLine("Enter the age");

                        age = ReturnValidAge(Console.ReadLine().Trim());
                        Console.WriteLine("Enter the car");
                        Car car = ReturnValidCar(Console.ReadLine().Trim());

                        repository.AddEmployee(name, age, car);

                        var opt = new JsonSerializerOptions() { WriteIndented = true };

                        json = JsonSerializer.Serialize(repository, opt);

                        File.WriteAllText(path, json);
                        break;
                    case "v":
                        json = File.ReadAllText(path);

                        var vResult = JsonSerializer.Deserialize<EmployeeRepository>(json);

                        foreach (var item in vResult._employees)
                        {
                            Console.WriteLine($"Id - {item.Id} Name - {item.Name}  Age - {item.Age} Car {item.Car}");
                        }

                        break;
                    case "f":

                        json = File.ReadAllText(path);

                        var dataForFind = JsonSerializer.Deserialize<EmployeeRepository>(json);
                        var dataFindResult = repository._employees;

                        Console.WriteLine("enter the name to find if needed else insert \"skip\"");

                        var nameToFind = Console.ReadLine().Trim();

                        if (!string.Equals(nameToFind, "skip"))
                        {
                            nameToFind = ReturnValidName(nameToFind);
                            dataFindResult = repository.FindAllByName(nameToFind).ToList();
                        }

                        Console.WriteLine("enter the age to find if needed else insert \"skip\"");

                        var input = Console.ReadLine().Trim();
                        var ageToFind = 0;

                        if (!input.Equals("skip"))
                        {
                            ageToFind = ReturnValidAge(input);
                            dataFindResult = repository.FindByAge(ageToFind).ToList();
                        }
                        Console.WriteLine("enter the Car to find if needed else insert \"skip\"");

                        var carInput = Console.ReadLine().Trim();

                        if (!carInput.Equals("skip"))
                        {
                            var carToFind = ReturnValidCar(carInput);
                            dataFindResult = repository.FindAllByCar(carToFind).ToList();
                        }

                        foreach (var item in dataFindResult)
                        {
                            Console.WriteLine($"Id - {item.Id} Name - {item.Name}  Age - {item.Age} Car {item.Car}");
                        }
                        break;
                    case "d":
                        Console.WriteLine("Enter th Name to delete");
                        var nameToDelete = Console.ReadLine().Trim();
                        repository.RemoveByName(nameToDelete);
                        opt = new JsonSerializerOptions() { WriteIndented = true };
                        json = JsonSerializer.Serialize(repository, opt);
                        File.WriteAllText(path, json);
                        break;
                    default:
                        break;
                }
            }
        }

        public static string ReturnValidName(string name)
        {
            while (!name.All(char.IsLetter) || string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can contains only letters");
                name = Console.ReadLine().Trim();
            }
            return name;
        }

        public static int ReturnValidAge(string input)
        {
            int age;
            bool succes = int.TryParse(input, out age);

            while (!succes)
            {
                succes = !int.TryParse(Console.ReadLine().Trim(), out age);
                if (succes)
                {
                    Console.WriteLine("Your age should be a number");
                    succes = false;
                    continue;
                }
                if (age < 18 || age > 99)
                {
                    Console.WriteLine("Your age should be between 18 and 99");
                    succes = false;
                    continue;
                }
                succes = true;
            }
            return age;
        }

        public static Car ReturnValidCar(string input)
        {
            bool succesCar = Enum.TryParse(input, out Car car);

            while (!succesCar)
            {
                succesCar = Enum.TryParse(Console.ReadLine(), out Car tryCar);
                car = tryCar;
                if (!succesCar)
                {
                    Console.WriteLine("this machine is not supported");
                }
            }
            return car;
        }
    }
}
