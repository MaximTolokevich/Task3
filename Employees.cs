using System.Text.Json.Serialization;

namespace Task2
{
    public class Employees
    {
        [JsonPropertyName ("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Age")]
        public int Age { get; set; }
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Car")]
        public Car Car { get; set; }
    }
}
