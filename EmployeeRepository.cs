using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Task2
{
    public class EmployeeRepository
    {
        public List<Employees> _employees { get; set; } = new List<Employees>();
        [JsonIgnore]
        private int _id = 1;

        public void AddEmployee(string name, int age, Car car)
        {
            _employees.Add(new Employees() { Name = name, Age = age, Car = car, Id = _id++ });
        }
        public void RemoveById(int id)
        {
            _employees.Remove(_employees.FirstOrDefault(x => x.Id == id));
        }
        public void RemoveByName(string name)
        {
            _employees.Remove(_employees.FirstOrDefault(x => x.Name == name));
        }
        public void RemoveByCar(Car car)
        {
            _employees.Remove(_employees.FirstOrDefault(x => x.Car == car));
        }
        public Employees FindById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Employees> FindAllByName(string name)
        {
            return _employees.Where(x => x.Name == name);
        }
        public IEnumerable<Employees> FindAllByCar(Car car)
        {
            return _employees.Where(x => x.Car == car);
        }
        public IEnumerable<Employees> FindByAgeRange(int from, int to)
        {
            return _employees.Where(x => x.Age >= from && x.Age <= to);
        }
        public IEnumerable<Employees> FindByAge(int age)
        {
            return _employees.Where(x => x.Age == age);
        }
        public IEnumerable<Employees> GetAllEmployees()
        {
            return _employees;
        }
        public Employees FindLast()
        {
            return _employees.Last();
        }
    }
}
