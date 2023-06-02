using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Task2
{
    public class EmployeeRepository
    {
        private List<Employees> Employees { get; set; } = new List<Employees>();
        [JsonIgnore]
        private int _id = 1;

        public void AddEmployee(string name, int age, Car car)
        {
            Employees.Add(new Employees() { Name = name, Age = age, Car = car, Id = _id++ });
        }
        public void RemoveById(int id)
        {
            Employees.Remove(Employees.FirstOrDefault(x => x.Id == id));
        }
        public void RemoveByName(string name)
        {
            Employees.Remove(Employees.FirstOrDefault(x => x.Name == name));
        }
        public void RemoveByCar(Car car)
        {
            Employees.Remove(Employees.FirstOrDefault(x => x.Car == car));
        }
        public Employees FindById(int id)
        {
            return Employees.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Employees> FindAllByName(string name)
        {
            return Employees.Where(x => x.Name == name);
        }
        public IEnumerable<Employees> FindAllByCar(Car car)
        {
            return Employees.Where(x => x.Car == car);
        }
        public IEnumerable<Employees> FindByAgeRange(int from, int to)
        {
            return Employees.Where(x => x.Age >= from && x.Age <= to);
        }
        public IEnumerable<Employees> FindByAge(int age)
        {
            return Employees.Where(x => x.Age == age);
        }
        public IEnumerable<Employees> GetAllEmployees()
        {
            return Employees;
        }
        public Employees FindLast()
        {
            return Employees.Last();
        }
    }
}
