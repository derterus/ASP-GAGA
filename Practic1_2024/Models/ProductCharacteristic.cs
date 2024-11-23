using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Practic1_2024.Models
{
    public class ProductCharacteristic
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int characteristic_id { get; set; }
        public string value { get; set; }
        public Product Product { get; set; }
        public Characteristic Characteristic { get; set; }
    }

}
