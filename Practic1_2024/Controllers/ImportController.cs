using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using Practic1_2024.Models;
using Microsoft.EntityFrameworkCore;
using Practic1_2024.Data;
using System.Globalization;
using System.Xml.Linq;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.RepresentationModel;

namespace Practic1_2024.Controllers
{
    public class ImportController : Controller
    {
        private readonly StoreDbContext _context; // Контекст базы данных
        private readonly string _importFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ImportController(StoreDbContext context)
        {
            _context = context;

            if (!Directory.Exists(_importFolder))
            {
                Directory.CreateDirectory(_importFolder);
            }
        }

        // Действие для отображения формы загрузки файла
        public IActionResult Index()
        {
            return View();
        }

        // Действие для обработки загрузки и импорта файла
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Пожалуйста, выберите файл для загрузки.");
                return View("Index");
            }

            // Читаем путь
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

            // Логируем путь
            Console.WriteLine($"Загружаемый файл будет сохранен по пути: {filePath}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Проверяем, был ли файл сохранен
            if (System.IO.File.Exists(filePath))
            {
                Console.WriteLine("Файл успешно сохранен.");
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            // В зависимости от расширения файла, обрабатываем его
            switch (fileExtension)
            {
                case ".txt":
                    await ImportFromTxt(filePath);
                    break;
                case ".csv":
                    await ImportFromCsv(filePath);
                    break;
                case ".xml":
                    await ImportFromXml(filePath);
                    break;

                case ".yaml":
                case ".yml":
                    await ImportFromYaml(filePath);
                    break;
                default:
                    ModelState.AddModelError("", "Неподдерживаемый формат файла.");
                    return View("Index");
            }

            TempData["Message"] = "Файл успешно импортирован!";
            return RedirectToAction("Index");
        }


        private async Task ImportFromTxt(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            string currentTable = null;
            List<string> headers = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var trimmedLine = line.Trim();

                // Определяем текущую таблицу
                if (trimmedLine.Equals("categories", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("manufacturers", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("characteristics", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("products", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("reviews", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("product_characteristics", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("product_images", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("users", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("orders", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("order_items", StringComparison.OrdinalIgnoreCase))
                {
                    currentTable = trimmedLine.ToLower();
                    headers = null;
                    continue;
                }

                if (headers == null)
                {
                    headers = trimmedLine.Split(';').ToList();
                    continue;
                }

                var row = trimmedLine.Split(';').ToList();

                // Обработка данных с использованием общего метода
                await ProcessRow(currentTable, row);
            }
        }

        // Вспомогательный метод для обработки строки в зависимости от текущей таблицы
        private async Task ProcessRow(string currentTable, List<string> row)
        {
            switch (currentTable)
            {
                case "categories":
                    if (row.Count >= 2)
                    {
                        var category = new Category
                        {
                            Название = row[0],
                            Описание = row.ElementAtOrDefault(1)
                        };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "manufacturers":
                    if (row.Count >= 2)
                    {
                        var manufacturer = new Manufacturer
                        {
                            Название = row[0],
                            Страна = row.ElementAtOrDefault(1)
                        };
                        _context.Manufacturers.Add(manufacturer);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "characteristics":
                    if (row.Count >= 2)
                    {
                        var characteristic = new Characteristic
                        {
                            Название = row[0],
                            Описание = row.ElementAtOrDefault(1)
                        };
                        _context.Characteristics.Add(characteristic);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "products":
                    if (row.Count >= 10)
                    {
                        var product = new Product
                        {
                            Вес = Convert.ToInt32(row[0]),
                            Название = row[1],
                            Цена = Convert.ToInt32(row[2]),
                            Описание = row[3],
                            category_id = Convert.ToInt32(row[4]),
                            manufacturer_id = Convert.ToInt32(row[5]),
                            SKU = row[6],
                            Количество_на_складе = Convert.ToInt32(row[7]),
                            Дата_создания = DateTime.SpecifyKind(DateTime.Parse(row[8]), DateTimeKind.Utc),
                            Дата_обновления = DateTime.SpecifyKind(DateTime.Parse(row[9]), DateTimeKind.Utc)
                        };
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "reviews":
                    if (row.Count >= 5)
                    {
                        var review = new Review
                        {
                            Дата = DateTime.SpecifyKind(DateTime.Parse(row[0]), DateTimeKind.Utc),
                            user_id = Convert.ToInt32(row[1]),
                            product_id = Convert.ToInt32(row[2]),
                            Оценка = Convert.ToInt32(row[3]),
                            Текст = row[4]
                        };
                        _context.Reviews.Add(review);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "product_characteristics":
                    if (row.Count >= 3)
                    {
                        var productCharacteristic = new ProductCharacteristic
                        {
                            product_id = Convert.ToInt32(row[0]),
                            characteristic_id = Convert.ToInt32(row[1]),
                            value = row[2]
                        };
                        _context.ProductCharacteristics.Add(productCharacteristic);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "product_images":
                    if (row.Count >= 3)
                    {
                        var productImage = new ProductImage
                        {
                            product_id = Convert.ToInt32(row[0]),
                            URL_изображения = row[1],
                            Основное_изображение = row[2].Equals("TRUE", StringComparison.OrdinalIgnoreCase)
                        };
                        _context.ProductImages.Add(productImage);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "users":
                    if (row.Count >= 6)
                    {
                        var user = new User
                        {
                            UserName = row[0],
                            Password = row[1], // Пароль как хэш
                            PasswordResetToken = row.ElementAtOrDefault(2),
                            Email = row[3],
                            Status = Convert.ToInt16(row[4]), // Status как short
                            created_at = (int)DateTime.SpecifyKind(DateTime.Parse(row[5]), DateTimeKind.Utc).ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds // Преобразуем в Unix timestamp
                        };
                        _context.Users.Add(user);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "orders":
                    if (row.Count >= 5)
                    {
                        var order = new Order
                        {
                            user_id = Convert.ToInt32(row[0]),
                            Дата_создания = DateTime.SpecifyKind(DateTime.Parse(row[1]), DateTimeKind.Utc),
                            Дата_обновления = DateTime.SpecifyKind(DateTime.Parse(row[2]), DateTimeKind.Utc),
                            Сумма = Convert.ToDecimal(row[3]),
                            Статус = row[4]
                        };
                        _context.Orders.Add(order);
                        await _context.SaveChangesAsync();
                    }
                    break;

                case "order_items":
                    if (row.Count >= 5)
                    {
                        var orderItem = new OrderItem
                        {
                            order_id = Convert.ToInt32(row[0]),
                            product_id = Convert.ToInt32(row[1]),
                            Количество = Convert.ToInt32(row[2]),
                            Цена_за_единицу = Convert.ToDecimal(row[3]),

                        };
                        _context.OrderItems.Add(orderItem);
                        await _context.SaveChangesAsync();
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Неизвестная таблица: {currentTable}");
            }
        }



        private async Task ImportFromCsv(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);

            string currentTable = null;
            List<string> headers = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var trimmedLine = line.Trim();

                // Определяем текущую таблицу
                if (trimmedLine.Equals("categories", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("manufacturers", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("characteristics", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("products", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("reviews", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("product_characteristics", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("product_images", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("users", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("orders", StringComparison.OrdinalIgnoreCase) ||
                    trimmedLine.Equals("order_items", StringComparison.OrdinalIgnoreCase))
                {
                    currentTable = trimmedLine.ToLower();
                    headers = null;
                    continue;
                }

                if (headers == null)
                {
                    headers = trimmedLine.Split(';').ToList();
                    continue;
                }

                var row = trimmedLine.Split(';').ToList();

                // Обработка данных с использованием общего метода
                await ProcessRow(currentTable, row);
            }
        }
        private async Task ImportFromXml(string filePath)
        {
            var doc = XDocument.Load(filePath);

            foreach (var tableElement in doc.Root.Elements())
            {
                var tableName = tableElement.Name.LocalName.ToLower();

                foreach (var rowElement in tableElement.Elements("Row"))
                {
                    // Collect values from all columns in the row
                    var rowValues = rowElement.Elements()
                                               .Select(column => column.Value)
                                               .ToList();

                    // Check if it's a header row and skip it
                    if (rowValues.All(value => value.StartsWith("Column")))
                    {
                        continue; // Skip header
                    }

                    // Validate or handle the row here
                    try
                    {
                        await ProcessRow(tableName, rowValues);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error processing row: {ex.Message}");
                        // Log the issue or handle it gracefully
                    }
                }
            }
        }


        private async Task ImportFromYaml(string filePath)
        {
            var yaml = new YamlStream();
            using (var reader = new StreamReader(filePath))
            {
                yaml.Load(reader);
            }

            var root = (YamlMappingNode)yaml.Documents[0].RootNode;

            foreach (var table in root.Children)
            {
                var tableName = table.Key.ToString().ToLower();
                var tableData = (YamlSequenceNode)table.Value;

                foreach (YamlMappingNode row in tableData)
                {
                    // Collect row values
                    var rowValues = row.Children.Values
                                         .Select(value => value.ToString())
                                         .ToList();

                    // Check if it's a header row and skip it
                    if (rowValues.All(value => value.StartsWith("Column")))
                    {
                        continue; // Skip header
                    }

                    // Validate or handle the row here
                    try
                    {
                        await ProcessRow(tableName, rowValues);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error processing row in table {tableName}: {ex.Message}");
                        // Log or handle the issue gracefully
                    }
                }
            }
        }


    }
}
