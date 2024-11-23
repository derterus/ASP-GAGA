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
            var sb = new StringBuilder();
            AppendCsvData(sb);
            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "text/csv", "export.csv");
        }

        // Метод для экспорта в TXT
        public IActionResult ExportToTxt()
        {
            var sb = new StringBuilder();
            AppendCsvData(sb); // Тот же формат, но для TXT.
            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "text/plain", "export.txt");
        }

        // Метод для экспорта в XML
        public IActionResult ExportToXml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<Tables>");

            AppendXmlData(sb);

            sb.AppendLine("</Tables>");
            var fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(fileBytes, "application/xml", "export.xml");
        }

        // Метод для экспорта в YAML
        public IActionResult ExportToYaml()
        {
            var serializer = new Serializer();
            var exportData = PrepareExportData();

            var yamlExportData = new Dictionary<string, List<Dictionary<string, string>>>();

            foreach (var table in exportData)
            {
                var tableList = new List<Dictionary<string, string>>();

                // Для каждой таблицы добавляем строки с соответствующими колонками
                foreach (var row in table.Value.Rows)
                {
                    var rowDict = new Dictionary<string, string>();

                    for (int i = 0; i < table.Value.Headers.Length; i++)
                    {
                        // Используем Column1, Column2 и т.д. в качестве ключей
                        rowDict[$"Column{i + 1}"] = row[i]?.ToString() ?? "N/A";
                    }

                    tableList.Add(rowDict);
                }

                // Добавляем данные таблицы в результат
                yamlExportData[table.Key] = tableList;
            }

            // Сериализуем данные в YAML
            var yaml = serializer.Serialize(yamlExportData);
            var fileBytes = Encoding.UTF8.GetBytes(yaml);

            return File(fileBytes, "application/x-yaml", "export.yaml");
        }



        // Общая логика для добавления данных в CSV или TXT
        private void AppendCsvData(StringBuilder sb)
        {
            // Экспорт каждой таблицы
            var data = PrepareExportData();

            foreach (var table in data)
            {
                sb.AppendLine(table.Key); // Имя таблицы
                sb.AppendLine(string.Join(";", table.Value.Headers)); // Заголовки

                foreach (var row in table.Value.Rows)
                {
                    sb.AppendLine(string.Join(";", row));
                }

                sb.AppendLine(); // Отделяем таблицы
            }
        }

        // Общая логика для добавления данных в XML
        private void AppendXmlData(StringBuilder sb)
        {
            var data = PrepareExportData();

            foreach (var table in data)
            {
                sb.AppendLine($"<{table.Key}>");

                foreach (var row in table.Value.Rows)
                {
                    sb.AppendLine("  <Row>");
                    for (int i = 0; i < table.Value.Headers.Length; i++)
                    {
                        sb.AppendLine($"    <{table.Value.Headers[i]}>{row[i]}</{table.Value.Headers[i]}>");
                    }
                    sb.AppendLine("  </Row>");
                }

                sb.AppendLine($"</{table.Key}>");
            }
        }

            // Подготовка данных для экспорта
            private Dictionary<string, ExportTable> PrepareExportData()
            {
                return new Dictionary<string, ExportTable>
                {
                    ["categories"] = new ExportTable
                    {
                        Headers = new[] { "Название", "Описание" },
                        Rows = _context.Categories
                            .Select(c => new object[] { c.Название, c.Описание })
                            .ToList()
                    },
                    ["manufacturers"] = new ExportTable
                    {
                        Headers = new[] { "Название", "Страна" },
                        Rows = _context.Manufacturers
                            .Select(m => new object[] { m.Название, m.Страна })
                            .ToList()
                    },
                    ["characteristics"] = new ExportTable
                    {
                        Headers = new[] { "Название", "Описание" },
                        Rows = _context.Characteristics
                            .Select(ch => new object[] { ch.Название, ch.Описание })
                            .ToList()
                    },
                    ["products"] = new ExportTable
                    {
                        Headers = new[]
                        {
                            "Вес", "Название", "Цена", "Описание", "category_id", "manufacturer_id", "SKU",
                            "Количество_на_складе", "Дата_создания", "Дата_обновления"
                        },
                        Rows = _context.Products
                            .Select(p => new object[]
                            {
                                p.Вес, p.Название, p.Цена, p.Описание, p.category_id, p.manufacturer_id, p.SKU,
                                p.Количество_на_складе,  p.Дата_создания.HasValue ? p.Дата_создания.Value.ToString("yyyy-MM-dd") : "N/A",
            p.Дата_обновления.HasValue ? p.Дата_обновления.Value.ToString("yyyy-MM-dd") : "N/A"
                            })
                            .ToList()
                    },
                    ["users"] = new ExportTable
                    {
                        Headers = new[] { "UserName", "Password", "PasswordResetToken", "Email", "Status", "created_at" },
                        Rows = _context.Users
        .Select(u => new object[]
        {
            u.UserName,
            u.Password,
            u.PasswordResetToken,
            u.Email,
            u.Status,
            // Преобразуем Unix timestamp в дату в формате yyyy-MM-dd
            DateTimeOffset.FromUnixTimeSeconds(u.created_at).DateTime.ToString("yyyy-MM-dd")
        })
        .ToList()
                    },

                    ["reviews"] = new ExportTable
                    {
                        Headers = new[] { "Дата", "user_id", "product_id", "Оценка", "Текст" },
                        Rows = _context.Reviews
                            .Select(r => new object[]
                            {
                             r.Дата.HasValue ? r.Дата.Value.ToString("yyyy-MM-dd") : "N/A", r.user_id, r.product_id, r.Оценка, r.Текст
                            })
                            .ToList()
                    },
                    ["product_characteristics"] = new ExportTable
                    {
                        Headers = new[] { "product_id", "characteristic_id", "value" },
                        Rows = _context.ProductCharacteristics
                            .Select(pc => new object[] { pc.product_id, pc.characteristic_id, pc.value })
                            .ToList()
                    },
                    ["product_images"] = new ExportTable
                    {
                        Headers = new[] { "product_id", "URL_изображения", "Основное_изображение" },
                        Rows = _context.ProductImages
                            .Select(pi => new object[] { pi.product_id, pi.URL_изображения, pi.Основное_изображение })
                            .ToList()
                    },
                    ["orders"] = new ExportTable
                    {
                        Headers = new[] { "user_id", "Дата_создания", "Дата_обновления", "Сумма", "Статус" },
                        Rows = _context.Orders
                            .Select(o => new object[]
                            {
                                o.user_id, o.Дата_создания.ToString("yyyy-MM-dd"),  o.Дата_обновления.HasValue ? o.Дата_обновления.Value.ToString("yyyy-MM-dd") : "N/A",
                                o.Сумма, o.Статус
                            })
                            .ToList()
                    },
                    ["order_items"] = new ExportTable
                    {
                        Headers = new[] { "order_id", "product_id", "Количество", "Цена_за_единицу", "Итог_по_пункту" },
                        Rows = _context.OrderItems
                            .Select(oi => new object[]
                            {
                                oi.order_id, oi.product_id, oi.Количество, oi.Цена_за_единицу, oi.Итог_по_пункту
                            })
                            .ToList()
                    }
                };
            }

        // Вспомогательный класс для хранения данных таблицы
        private class ExportTable
        {
            public string[] Headers { get; set; }
            public List<object[]> Rows { get; set; }
        }
    }
}
