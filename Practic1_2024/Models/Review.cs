using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Practic1_2024.Models
{
    public class Review
    {
        public int id { get; set; }

        public DateTime? Дата { get; set; }

        public int user_id { get; set; }
        public User User { get; set; }

        public int product_id { get; set; }
        public Product Product { get; set; }

        public int Оценка { get; set; }

        public string? Текст { get; set; }
    }

}
