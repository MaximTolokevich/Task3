using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Task2.Models;
using Task2.Repositories;

namespace Task2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employees");
            
            var path = Environment.CurrentDirectory + "\\data.txt";
            EmployeeRepository repository = new();
            var file = string.Empty;
            if (File.Exists(path))
            {
                file = File.ReadAllText(path);
                var savedRepository = JsonSerializer.Deserialize<IEnumerable<Employees>>(file);
                foreach (var item in savedRepository)
                {
                    repository.AddEmployee(item.Name, item.Age, item.Car);
                }
            }
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console().MinimumLevel.Error().CreateLogger();
            
            while (true)
            {
                Console.WriteLine("Enter the command");
                var command = Console.ReadLine().Trim();
                string json;
                switch (command)
                {

                    case "e":
                        

                        string name = string.Empty;
                        while (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Enter the name");
                            name = ReturnValidName(Console.ReadLine().Trim());
                            if (string.IsNullOrEmpty(name))
                            {
                                Log.Error("Name can't be empty");
                            }
                        }    
                        

                        Console.WriteLine("Enter the age");

                        int age = 0;
                        while (age<18 || age>99)
                        {
                           age = ReturnValidAge(Console.ReadLine().Trim());
                        }
                            
                        Car car = Car.None;
                        while (car == Car.None)
                        {
                            Console.WriteLine("Enter the car");
                            
                            car = ReturnValidCar(Console.ReadLine().Trim());
                            if (car == Car.None)
                            {
                                Log.Error("This car is not valid");
                            }
                        }

                        repository.AddEmployee(name, age, car);

                        var opt = new JsonSerializerOptions() { WriteIndented = true };
                        try
                        {
                            json = JsonSerializer.Serialize(repository.GetAllEmployees(), opt);

                            File.WriteAllText(path, json);
                        }
                        catch (Exception e)
                        {
                            Log.Error(e.Message);
                        }
                        break;
                    case "v":
                        try
                        {

                            json = File.ReadAllText(path);

                            var vResult = JsonSerializer.Deserialize<IEnumerable<Employees>>(json);

                            foreach (var item in vResult)
                            {
                                Console.WriteLine($"Id - {item.Id} Name - {item.Name}  Age - {item.Age} Car {item.Car}");
                            }
                        }
                        catch (JsonException e)
                        {
                            Log.Error("data has been corrupted ,try to enter new data");
                            Log.Error(e.Message);
                        }


                        break;
                    case "f":
                        var dataFindResult = repository.GetAllEmployees();

                        Console.WriteLine("enter the name to find if needed else press Enter");
                         
                        var nameToFind = Console.ReadLine().Trim();

                        if (!nameToFind.Equals(string.Empty))
                        {
                            nameToFind = ReturnValidName(nameToFind);
                            if (!string.IsNullOrEmpty(nameToFind))
                            {
                                dataFindResult = repository.FindAllByName(nameToFind).ToList();
                            } 
                        }

                        Console.WriteLine("enter the age to find if needed else press Enter");

                        var input = Console.ReadLine().Trim();
                        if (!input.Equals(string.Empty))
                        {
                            int ageToFind = ReturnValidAge(input);
                            if (ageToFind !=0)
                            {
                                dataFindResult = repository.FindByAge(ageToFind).ToList();
                            }
                            
                        }

                        Console.WriteLine("enter the Car to find if needed else press Enter\\0\\none");

                        var carInput = Console.ReadLine().Trim();

                        if (!carInput.Equals(string.Empty))
                        {
                            var carToFind = ReturnValidCar(carInput);
                            if (carToFind != Car.None)
                            {
                                dataFindResult = repository.FindAllByCar(carToFind).ToList();
                            }
                        }

                        foreach (var item in dataFindResult)
                        {
                            Console.WriteLine($"Id - {item.Id} Name - {item.Name}  Age - {item.Age} Car {item.Car}");
                        }
                        break;

                    case "d":

                        Console.WriteLine("Enter the Name to delete");
                        var nameToDelete = Console.ReadLine().Trim();
                        repository.RemoveAllByName(nameToDelete);

                        opt = new JsonSerializerOptions() { WriteIndented = true };
                        json = JsonSerializer.Serialize(repository.GetAllEmployees(), opt);
                        File.WriteAllText(path, json);

                        break;

                    default:
                        Log.Error($"operation \"{command}\" not supported");
                        break;
                }
            }
        }

        private static string ReturnValidName(string name)
        {
            while (!name.All(char.IsLetter))
            {
                Log.Error("Name can contains only letters");
                name = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(name))
                {
                    return name;
                }
            }
            return name;
        }

        private static int ReturnValidAge(string input)
        {
            bool succes = int.TryParse(input, out int age);
            if (age < 18 || age > 99)
            {
                succes = false;
            }

            while (!succes)
            {
                Log.Error("Input should be a number and be between 18 and 99");
                string str = Console.ReadLine().Trim();
                succes = int.TryParse(str, out age);
                
                if (succes)
                {
                    if (age < 18 || age > 99)
                    {
                        succes = false; ;
                    }
                    
                }
                if (string.IsNullOrEmpty(str))
                {
                    return 0;
                }
            }

            return age;
        }

        private static Car ReturnValidCar(string input)
        {
            bool succesCar = Enum.TryParse(input,true, out Car car);
            if (Enum.IsDefined(car) && succesCar)
            {
                return car;
            }
            succesCar = false;
            while (!succesCar)
            {
                Log.Error("this machine is not supported");
                var str = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(str))
                {
                    return Car.None;
                }
                succesCar = Enum.TryParse(str,true, out Car tryCar);
                car = tryCar;
                if (Enum.IsDefined<Car>(car) && succesCar)
                {
                    return car;
                }
                succesCar = false;
            }
            return car;
        }
    }
}
