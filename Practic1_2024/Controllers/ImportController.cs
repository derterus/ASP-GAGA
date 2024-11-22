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



        // Пример импорта CSV (если файл в формате CSV)
        private async Task ImportFromCsv(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var row = line.Split(';').ToList();

                if (row.Count >= 2)
                {
                    if (row[0].Equals("categories", StringComparison.OrdinalIgnoreCase))
                    {
                        var category = new Category
                        {
                            Название = row[1],
                            Описание = row.ElementAtOrDefault(2)
                        };

                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                    }
                    else if (row[0].Equals("manufacturers", StringComparison.OrdinalIgnoreCase))
                    {
                        var manufacturer = new Manufacturer
                        {
                            Название = row[1],
                            Страна = row.ElementAtOrDefault(2)
                        };

                        _context.Manufacturers.Add(manufacturer);
                        await _context.SaveChangesAsync();
                    }
                    // Дополнительно можно обработать другие таблицы, такие как "products" или "reviews"
                }
            }
        }
            



        // Остальные методы для XML и YAML могут быть аналогичными
    }
}


//// Импорт данных из YAML файла
//private async Task ImportFromYaml(string filePath)
//        {
//            var yamlContent = System.IO.File.ReadAllText(filePath);
//            var deserializer = new DeserializerBuilder()
//                .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
//                .Build();

//            // Десериализация содержимого YAML в словарь
//            var data = deserializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(yamlContent);

//            // Импортируем данные в соответствующие таблицы
//            if (data.ContainsKey("Categories"))
//            {
//                foreach (var item in data["Categories"])
//                {
//                    var category = new Category
//                    {
//                        Name = item["Name"]
//                    };
//                    _context.Categories.Add(category);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("Brands"))
//            {
//                foreach (var item in data["Brands"])
//                {
//                    var brand = new Product
//                    {
//                        Name = item["Name"]
//                    };
//                    _context.Brands.Add(brand);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("Smartphones"))
//            {
//                foreach (var item in data["Smartphones"])
//                {
//                    var smartphone = new Review
//                    {
//                        Name = item["Name"],
//                        BrandId = int.Parse(item["BrandId"]),
//                        Description = item["Description"],
//                        Price = decimal.Parse(item["Price"]),
//                        ReleaseYear = int.Parse(item["ReleaseYear"]),
//                        SimCount = int.Parse(item["SimCount"]),
//                        MemoryOptions = item["MemoryOptions"],
//                        ColorOptions = item["ColorOptions"],
//                        CategoryId = int.Parse(item["CategoryId"]),
//                        ImageUrl = item["ImageUrl"]
//                    };
//                    _context.Smartphones.Add(smartphone);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("SmartphoneCharacteristics"))
//            {
//                foreach (var item in data["SmartphoneCharacteristics"])
//                {
//                    var characteristic = new ProductCharacteristic
//                    {
//                        SmartphoneId = int.Parse(item["SmartphoneId"]),
//                        Characteristic = item["Characteristic"],
//                        Value = item["Value"]
//                    };
//                    _context.SmartphoneCharacteristics.Add(characteristic);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("Users"))
//            {
//                foreach (var item in data["Users"])
//                {
//                    var user = new User
//                    {
//                        Name = item["Name"],
//                        Email = item["Email"],
//                        Password = item["Password"],
//                        Role = item["Role"],
//                        Phone = item["Phone"],
//                        Address = item["Address"]
//                    };
//                    _context.Users.Add(user);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("Orders"))
//            {
//                foreach (var item in data["Orders"])
//                {
//                    var order = new Order
//                    {
//                        UserId = int.Parse(item["UserId"]),
//                        TotalPrice = decimal.Parse(item["TotalPrice"]),
//                        Status = item["Status"],
//                        CreatedAt = DateOnly.Parse(item["CreatedAt"]),
//                        UpdatedAt = DateOnly.Parse(item["UpdatedAt"])
//                    };
//                    _context.Orders.Add(order);
//                }
//                await _context.SaveChangesAsync();
//            }

//            if (data.ContainsKey("OrderItems"))
//            {
//                foreach (var item in data["OrderItems"])
//                {
//                    var orderItem = new OrderItem
//                    {
//                        OrderId = int.Parse(item["OrderId"]),
//                        SmartphoneId = int.Parse(item["SmartphoneId"]),
//                        Quantity = int.Parse(item["Quantity"]),
//                        Price = decimal.Parse(item["Price"])
//                    };
//                    _context.OrderItems.Add(orderItem);
//                }
//                await _context.SaveChangesAsync();
//            }

//            // Сохраняем все изменения в базе данных
//            await _context.SaveChangesAsync();
//        }
//        // Пример импорта данных для Categories (категорий)
//        private async Task ImportCategories(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");

//                // Пропускаем строку, если это заголовок
//                if (columns.Count == 1 && columns[0].ToLower() == "name")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }
//                // Пропускаем строки с пустыми или неверными данными

//                // Создаем новый объект категории
//                var category = new Category
//                {
//                    Name = columns[0].Trim() // Имя категории
//                };

//                // Добавляем категорию в базу данных
//                _context.Categories.Add(category);
//                await _context.SaveChangesAsync(); // Сохраняем изменения
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка при импорте категории: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }

//        private async Task ImportBrands(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");

//                // Пропускаем строку, если это заголовок
//                if (columns.Count == 1 && columns[0].ToLower() == "name")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }

//                // Создаем новый объект бренда
//                var brand = new Product
//                {
//                    Name = columns[0].Trim() // Имя бренда
//                };

//                // Добавляем бренд в базу данных
//                _context.Brands.Add(brand);
//                await _context.SaveChangesAsync(); // Сохраняем изменения
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка при импорте бренда: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }


//        // Пример импорта данных для Users (пользователей)
//        private async Task ImportUsers(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");

//                // Пропускаем строку, если это заголовок
//                if (columns.Count > 1 && columns[0].ToLower() == "name" && columns[1].ToLower() == "email")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }

//                if (columns.Count < 6) return; // Пропускаем строки с недостаточными данными

//                var user = new User
//                {
//                    Name = columns[0].Trim(),
//                    Email = columns[1].Trim(),
//                    Password = columns[2].Trim(),
//                    Role = columns[3].Trim(),
//                    Phone = columns[4].Trim(),
//                    Address = columns[5].Trim()
//                };

//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка при импорте пользователя: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }

//        // Пример импорта данных для Smartphones (смартфонов)
//        private async Task ImportSmartphones(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");

//                // Пропускаем строку, если это заголовок
//                if (columns.Count > 1 && columns[0].ToLower() == "name" && columns[1].ToLower() == "brandid")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }
//                // Проверка на достаточность данных в строке
//                if (columns.Count < 9) return; // Пропускаем строки с недостаточными данными

//                // Извлекаем значения из строк
//                var smartphone = new Review
//                {
//                    Name = columns[0].Trim(), // Название смартфона
//                    BrandId = Convert.ToInt32(columns[1].Trim()), // Идентификатор бренда
//                    Description = columns[2].Trim(), // Описание
//                    Price = Convert.ToDecimal(columns[3].Trim()), // Цена
//                    ReleaseYear = Convert.ToInt32(columns[4].Trim()), // Год выпуска
//                    SimCount = Convert.ToInt32(columns[5].Trim()), // Количество SIM-карт
//                    MemoryOptions = columns[6].Trim(), // Опции памяти (например, "128GB, 256GB")
//                    ColorOptions = columns[7].Trim(), // Опции цвета
//                    CategoryId = Convert.ToInt32(columns[8].Trim()), // Идентификатор категории
//                    ImageUrl = columns[9].Trim() // URL изображения
//                };

//                // Добавляем смартфон в базу данных
//                _context.Smartphones.Add(smartphone);
//                await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
//            }
//            catch (Exception ex)
//            {
//                // Логирование ошибок импорта
//                Console.WriteLine($"Ошибка при импорте смартфона: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }

//        // Пример импорта данных для SmartphoneCharacteristics (характеристик смартфонов)
//        private async Task ImportSmartphoneCharacteristics(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");
//                // Пропускаем строку, если это заголовок
//                if (columns.Count > 1 && columns[0].ToLower() == "smartphoneid" && columns[1].ToLower() == "characteristic")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }
//                // Проверка на достаточность данных в строке
//                if (columns.Count < 3) return; // Пропускаем строки с недостаточными данными

//                // Извлекаем значения из строки
//                var smartphoneCharacteristic = new ProductCharacteristic
//                {
//                    SmartphoneId = Convert.ToInt32(columns[0].Trim()), // Идентификатор смартфона
//                    Characteristic = columns[1].Trim(), // Характеристика (например, "Диагональ экрана")
//                    Value = columns[2].Trim() // Значение характеристики (например, "6.1 дюйма")
//                };

//                // Добавляем характеристику смартфона в базу данных
//                _context.SmartphoneCharacteristics.Add(smartphoneCharacteristic);
//                await _context.SaveChangesAsync(); // Сохраняем изменения в базе данных
//            }
//            catch (Exception ex)
//            {
//                // Логирование ошибок импорта
//                Console.WriteLine($"Ошибка при импорте характеристики смартфона: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }


//        // Пример импорта данных для Orders (заказов)
//        private async Task ImportOrders(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");
//                // Пропускаем строку, если это заголовок
//                if (columns.Count > 1 && columns[0].ToLower() == "userid" && columns[1].ToLower() == "totalprice")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }
//                if (columns.Count < 5) return; // Пропускаем строки с недостаточными данными

//                var order = new Order
//                {
//                    UserId = Convert.ToInt32(columns[0].Trim()), // Идентификатор пользователя
//                    TotalPrice = Convert.ToDecimal(columns[1].Trim()), // Общая стоимость
//                    Status = columns[2].Trim(), // Статус
//                    CreatedAt = DateOnly.Parse(columns[3].Trim()), // Дата создания
//                    UpdatedAt = DateOnly.Parse(columns[4].Trim()) // Дата обновления
//                };

//                _context.Orders.Add(order);
//                await _context.SaveChangesAsync();

//                // Если есть позиции для этого заказа, добавляем их
//                // Позиции будут добавлены в методе ImportOrderItems
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка при импорте заказа: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }
//        private async Task ImportOrderItems(List<string> columns)
//        {
//            try
//            {
//                Console.WriteLine($"Обрабатываем строку: {string.Join(";", columns)}");

//                // Пропускаем строку, если это заголовок
//                if (columns.Count > 1 && columns[0].ToLower() == "orderid" && columns[1].ToLower() == "smartphoneid")
//                {
//                    Console.WriteLine("Пропускаем строку с заголовками.");
//                    return; // Пропускаем строку, если это заголовок
//                }

//                // Пропускаем строки с недостаточными данными
//                if (columns.Count < 4) return;


//                // Создание нового элемента заказа
//                var orderItem = new OrderItem
//                {
//                    OrderId = Convert.ToInt32(columns[0].Trim()), // Идентификатор заказа
//                    SmartphoneId = Convert.ToInt32(columns[1].Trim()), // Идентификатор смартфона
//                    Quantity = Convert.ToInt32(columns[2].Trim()), // Количество товара
//                    Price = Convert.ToDecimal(columns[3].Trim()), // Цена товара
//                };

//                // Добавление позиции в заказ
//                _context.OrderItems.Add(orderItem);
//                await _context.SaveChangesAsync();

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Ошибка при импорте позиции в заказе: {string.Join(";", columns)}. Ошибка: {ex.Message}");
//            }
//        }
//    }
//}