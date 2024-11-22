namespace Practic1_2024.Models
{
    public class OrderItem
    {
        public int id { get; set; } // Идентификатор элемента заказа
        public int order_id { get; set; } // Идентификатор заказа
        public int product_id { get; set; } // Идентификатор товара
        public int Количество { get; set; } // Количество товара в заказе
        public decimal Цена_за_единицу { get; set; } // Цена за единицу товара на момент заказа

        public decimal Итог_по_пункту => Количество * Цена_за_единицу; // Общая цена за этот элемент заказа

        public Order Order { get; set; } // Навигационное свойство к заказу
        public Product Product { get; set; } // Навигационное свойство к продукту
    }

}
