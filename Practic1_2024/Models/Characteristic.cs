using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Practic1_2024.Models
{
    public class Characteristic
    {
        public int id { get; set; }

        public string Название { get; set; }

        public string? Описание { get; set; }

        // Навигационное свойство
        public ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
    }

}
