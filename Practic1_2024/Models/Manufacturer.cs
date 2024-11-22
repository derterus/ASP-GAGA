using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Practic1_2024.Models
{
    public class Manufacturer
    {
        public int id { get; set; }

        public string Название { get; set; }

        public string? Страна { get; set; }

        // Навигационное свойство
        public ICollection<Product> Products { get; set; }
    }
}
