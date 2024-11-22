using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;

namespace Practic1_2024.Models
{
    public class Order
    {
        public int id { get; set; } // Идентификатор заказа
        public int user_id { get; set; } // Идентификатор пользователя, который сделал заказ
        public DateTime Дата_создания { get; set; } // Дата создания заказа
        public DateTime? Дата_обновления { get; set; } // Дата отгрузки
        public string Статус { get; set; } // Статус заказа (например, "Ожидает отправки", "Отправлен", "Завершен" и т. д.)
        public decimal Сумма { get; set; } // Общая сумма заказа (можно рассчитывать на основе OrderItems)

        // Навигационное свойство
        public ICollection<OrderItem> OrderItems { get; set; }
        public User User { get; set; } // Навигационное свойство к пользователю

    }

}
