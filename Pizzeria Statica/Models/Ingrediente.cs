using System.Text.Json.Serialization;

namespace Pizzeria_Statica.Models
{
    public class Ingrediente
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public Ingrediente()
        {

        }
    }
}
