namespace Practic1_2024.Models
{
    public class Product
    {
        public int id { get; set; }

        public int? Вес { get; set; }

        public string Название { get; set; }

        public int Цена { get; set; }

        public string? Описание { get; set; }

        public int? category_id { get; set; }
        public Category Category { get; set; }

        public int? manufacturer_id { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public string? SKU { get; set; }

        public int Количество_на_складе { get; set; } = 0;

        public DateTime? Дата_создания { get; set; }

        public DateTime? Дата_обновления { get; set; }

        // Навигационные свойства
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }


    }

}
