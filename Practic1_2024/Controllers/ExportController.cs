using Microsoft.AspNetCore.Mvc;
using Practic1_2024.Data;
using Practic1_2024.Models;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace Practic1_2024.Controllers
{
    public class ExportController : Controller
    {
        private readonly StoreDbContext _context;

        public ExportController(StoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Метод для экспорта в CSV
        public IActionResult ExportToCsv()
        {
            var categories = _context.Categories.ToList();
            var manufacturers = _context.Manufacturers.ToList();
            var products = _context.Products.ToList();
            var reviews = _context.Reviews.ToList();
            var users = _context.Users.ToList();

            var sb = new StringBuilder();

            // Экспорт "Категории"
            sb.AppendLine("Категории");
            sb.AppendLine("id;Название;Описание");
            foreach (var category in categories)
            {
                sb.AppendLine($"{category.id};{category.Название};{category.Описание}");
            }

            // Экспорт "Производители"
            sb.AppendLine("\nПроизводители");
            sb.AppendLine("id;Название;Страна");
            foreach (var manufacturer in manufacturers)
            {
                sb.AppendLine($"{manufacturer.id};{manufacturer.Название};{manufacturer.Страна}");
            }

            // Экспорт "Товары"
            sb.AppendLine("\nТовары");
            sb.AppendLine("id;Вес;Название;Цена;Описание;category_id;manufacturer_id;SKU;Количество_на_складе;Дата_создания;Дата_обновления");
            foreach (var product in products)
            {
                sb.AppendLine($"{product.id};{product.Вес};{product.Название};{product.Цена};{product.Описание};" +
                              $"{product.category_id};{product.manufacturer_id};{product.SKU};{product.Количество_на_складе};" +
                              $"{product.Дата_создания:yyyy-MM-dd};{product.Дата_обновления:yyyy-MM-dd}");
            }

            // Экспорт "Отзывы"
            sb.AppendLine("\nОтзывы");
            sb.AppendLine("id;Дата;user_id;product_id;Оценка;Текст");
            foreach (var review in reviews)
            {
                sb.AppendLine($"{review.id};{review.Дата:yyyy-MM-dd};{review.user_id};{review.product_id};{review.Оценка};{review.Текст}");
            }

            // Экспорт "Пользователи"
            sb.AppendLine("\nПользователи");
            sb.AppendLine("id;Имя_пользователя;Email;Статус;Дата_создания");
            foreach (var user in users)
            {
                sb.AppendLine($"{user.id};{user.UserName};{user.Email};{user.Status};{user.created_at}");
            }

            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "text/csv", "export.csv");
        }

        // Метод для экспорта в TXT
        public IActionResult ExportToTxt()
        {
            var categories = _context.Categories.ToList();
            var manufacturers = _context.Manufacturers.ToList();
            var users = _context.Users.ToList();
            var products = _context.Products.ToList();
            var characteristics = _context.Characteristics.ToList();
            var orders = _context.Orders.ToList();
            var orderItems = _context.OrderItems.ToList();
            var reviews = _context.Reviews.ToList();

            var sb = new StringBuilder();

            // Categories
            sb.AppendLine("Categories");
            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Название};{category.Описание}");
            }

            // Manufacturers
            sb.AppendLine("\nManufacturers");
            foreach (var manufacturer in manufacturers)
            {
                sb.AppendLine($"{manufacturer.Название};{manufacturer.Страна}");
            }



            // Products
            sb.AppendLine("\nProducts");
            foreach (var product in products)
            {
                sb.AppendLine($"{product.Название};{product.Цена};{product.Описание}");
            }

            // Characteristics
            sb.AppendLine("\nCharacteristics");
            foreach (var characteristic in characteristics)
            {
                sb.AppendLine($"{characteristic.Название};{characteristic.Описание}");
            }

            // Orders
            sb.AppendLine("\nOrders");
            foreach (var order in orders)
            {
                sb.AppendLine($"{order.user_id};{order.Сумма};{order.Статус}");
            }

            // Order Items
            sb.AppendLine("\nOrder Items");
            foreach (var orderItem in orderItems)
            {
                sb.AppendLine($"{orderItem.product_id};{orderItem.Количество};{orderItem.Цена_за_единицу}");
            }

            // Reviews
            sb.AppendLine("\nReviews");
            foreach (var review in reviews)
            {
                sb.AppendLine($"{review.user_id};{review.Оценка};{review.Текст}");
            }
            // Users
            sb.AppendLine("\nUsers");
            foreach (var user in users)
            {
                sb.AppendLine($"{user.UserName};{user.Email};{user.Password};{user.PasswordResetToken};{user.Status};{user.created_at}");
            }

            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "text/plain", "export.txt");
        }


        // Метод для экспорта в XML
        public IActionResult ExportToXml()
        {
            var categories = _context.Categories.ToList();
            var manufacturers = _context.Manufacturers.ToList();

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<Tables>");

            sb.AppendLine("<Категории>");
            foreach (var category in categories)
            {
                sb.AppendLine($"  <Row><id>{category.id}</id><Название>{category.Название}</Название><Описание>{category.Описание}</Описание></Row>");
            }
            sb.AppendLine("</Категории>");

            sb.AppendLine("<Производители>");
            foreach (var manufacturer in manufacturers)
            {
                sb.AppendLine($"  <Row><id>{manufacturer.id}</id><Название>{manufacturer.Название}</Название><Страна>{manufacturer.Страна}</Страна></Row>");
            }
            sb.AppendLine("</Производители>");

            sb.AppendLine("</Tables>");
            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "application/xml", "export.xml");
        }

        // Метод для экспорта в YAML
        public IActionResult ExportToYaml()
        {
            var categories = _context.Categories.ToList();
            var manufacturers = _context.Manufacturers.ToList();

            var serializer = new Serializer();
            var exportData = new Dictionary<string, object>
            {
                { "Категории", categories },
                { "Производители", manufacturers }
            };

            var yaml = serializer.Serialize(exportData);
            var fileBytes = Encoding.UTF8.GetBytes(yaml);
            return File(fileBytes, "application/x-yaml", "export.yaml");
        }
    }
}
