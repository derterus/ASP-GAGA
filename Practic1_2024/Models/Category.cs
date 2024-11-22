namespace Practic1_2024.Models
{
    public class Category
    {
        public int id { get; set; }

        public string Название { get; set; }

        public string? Описание { get; set; }

        // Навигационное свойство
        public ICollection<Product> Products { get; set; }
    }

}
