namespace Practic1_2024.Models
{
    public class ProductImage
    {
        public int id { get; set; }

        public int product_id { get; set; }
        public Product Product { get; set; }

        public string URL_изображения { get; set; }

        public bool Основное_изображение { get; set; } = false;
    }

}
