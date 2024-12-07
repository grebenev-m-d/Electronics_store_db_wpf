using OfficeOpenXml;
using OfficeOpenXml.Style;
using Electronics_store_db_wpf.Data.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Services.ExcelWriterService
{
    public class ExcelWriterService : IExcelWriterService
    {
        private readonly string _excelTemplateFolderPath;
        private readonly string _saveFolderPath;
        public ExcelWriterService(string SaveFolderPath)
        {
            _saveFolderPath = SaveFolderPath;
            _excelTemplateFolderPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))), "Resources", "ExcelTemplate");
        }

        public async Task CreateInvoiceAsync(List<Product> products)
        {
            // Путь к шаблону файла
            string templatePath = Path.Combine(_excelTemplateFolderPath, "invoice.xlsx");
            // Путь к новому файлу
            string newPath = Path.Combine(_saveFolderPath, $"Товарная накладная {DateTime.Today.ToString("d")}.xlsx");

            // Ряд, с которого нужно начать заполнение таблицы
            int startRow = 31;

            // Создание папки, если она не существует
            await Task.Run(() => Directory.CreateDirectory(_saveFolderPath));

            await Task.Run(() => File.Copy(templatePath, newPath, true));

           
            using (ExcelPackage package = new ExcelPackage(new FileInfo(newPath)))
            {
                // Получение рабочего листа
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                

                // Заполнение таблицы данными
                for (int i = 0; i < products.Count; i++)
                {
                    // Добавление нового ряда перед заполнением
                    worksheet.InsertRow(startRow + i, 1);

                  
                    int lastRow = startRow + i;

                    MergeCells(worksheet, lastRow, 1, 3);
                    MergeCells(worksheet, lastRow, 4, 19);
                    MergeCells(worksheet, lastRow, 20, 23);
                    MergeCells(worksheet, lastRow, 24, 28);
                    MergeCells(worksheet, lastRow, 29, 33);
                    MergeCells(worksheet, lastRow, 34, 38);
                    MergeCells(worksheet, lastRow, 39, 43);
                    MergeCells(worksheet, lastRow, 44, 48);
                    MergeCells(worksheet, lastRow, 49, 53);
                    MergeCells(worksheet, lastRow, 54, 59);
                    MergeCells(worksheet, lastRow, 60, 68);
                    MergeCells(worksheet, lastRow, 69, 75);
                    MergeCells(worksheet, lastRow, 76, 79);
                    MergeCells(worksheet, lastRow, 80, 86);
                    MergeCells(worksheet, lastRow, 87, 95);

                    var _amount = products[i].Quantity * products[i].Price;
                    var _vat = (_amount) * 0.18m;

                    // Заполнение ячеек таблицы данными из списка продуктов
                    worksheet.Cells[lastRow, 1].Value = i + 1;
                    worksheet.Cells[lastRow, 4].Value = products[i].Name;
                    worksheet.Cells[lastRow, 24].Value = "шт";
                    worksheet.Cells[lastRow, 54].Value = products[i].Quantity;
                    worksheet.Cells[lastRow, 60].Value = products[i].Price;
                    worksheet.Cells[lastRow, 69].Value = _amount;
                    worksheet.Cells[lastRow, 76].Value = "18%";
                    worksheet.Cells[lastRow, 80].Value = _vat;
                    worksheet.Cells[lastRow, 87].Value = _vat + _amount;
                }

                await package.SaveAsync();
                // Открытие сохраненного файла
                await Task.Run(() => Process.Start(new ProcessStartInfo { FileName = $"{newPath}", UseShellExecute = true }));

            }
        }
        public async Task CreateCustomerReportAsync(List<Client> clients)
        {
        
            string newPath = Path.Combine(_saveFolderPath, $"Отчет о клиентах {DateTime.Today.ToString("d")}.xlsx");
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Отчет о клиентах");

                int row = 1;

                int columnClient = 1;
                int columnOrders = 2;
                int columnOrderItems = 3;


                if (clients.Count>0)
                {
                    worksheet.Cells[row, columnClient].Value = "Фамилия";
                    worksheet.Cells[row, columnClient + 1].Value = "Имя";
                    worksheet.Cells[row, columnClient + 2].Value = "Отчество";
                    worksheet.Cells[row, columnClient + 3].Value = "Email";
                    worksheet.Cells[row, columnClient + 4].Value = "Телефон";
                    worksheet.Cells[row, columnClient + 5].Value = "Деньрождения";

                    Enumerable.Range(1, 6).ToList().ForEach(column => worksheet.Column(column).Width = 40);

                    SetRowProperties(worksheet, row, columnClient, columnClient + 5, "#0F0F0F", "#B6B6B6");
                    row++;
                }
                foreach (var client in clients)
                {
                    worksheet.Cells[row, columnClient + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                   
                    worksheet.Cells[row, columnClient].Value = client.Surname;
                    worksheet.Cells[row, columnClient + 1].Value = client.FirstName;
                    worksheet.Cells[row, columnClient + 2].Value = client.Patronymic;
                    worksheet.Cells[row, columnClient + 3].Value = client.Email;
                    worksheet.Cells[row, columnClient + 4].Value = client.Phone;
                    worksheet.Cells[row, columnClient + 5].Value = client.Birthday?.ToString("d");


                    SetRowProperties(worksheet, row, columnClient, columnClient + 5, "#272727", "#B6B6B6");

                    row ++;

                    if (client.Orders.Count > 0)
                    {
                        worksheet.Cells[row, columnOrders].Value = "Общая сумма";
                        worksheet.Cells[row, columnOrders + 1].Value = "Дата заказа";
                        SetRowProperties(worksheet, row, columnOrders, columnClient + 5, "#3D3D3D", "#B6B6B6");

                        row++;

                    }
                    foreach (var order in client.Orders)
                    {
                        worksheet.Cells[row, columnOrders].Value = order.TotalAmount;
                        worksheet.Cells[row, columnOrders + 1].Value = order.OrderDate?.ToString("d");

                        SetRowProperties(worksheet, row, columnOrders, columnClient + 5, "#4A4A4A", "#B6B6B6");

                        row++;

                        if (order.OrderItems.Count>0)
                        {
                            worksheet.Cells[row, columnOrderItems].Value = "Наименование товара";
                            worksheet.Cells[row, columnOrderItems + 1].Value = "Количество";
                            worksheet.Cells[row, columnOrderItems + 2].Value = "Сумма";
                           
                            SetRowProperties(worksheet, row, columnOrderItems, columnClient + 5, "#5E5E5E", "#000000");

                            row++;
                        }

                        foreach (var orderItem in order.OrderItems)
                        {
                            worksheet.Cells[row, columnOrderItems].Value = orderItem.Product.Name;
                            worksheet.Cells[row, columnOrderItems + 1].Value = orderItem.Quantity;
                            worksheet.Cells[row, columnOrderItems + 2].Value = orderItem.Amount;
                 
                            SetRowProperties(worksheet, row, columnOrderItems, columnClient + 5, "#868686", "#000000");

                            row++;

                        }
                    }
                }

                // Сохранение отчета в файл
                package.SaveAs(new FileInfo(newPath));

                // Открытие сохраненного файла
                await Task.Run(() => Process.Start(new ProcessStartInfo { FileName = $"{newPath}", UseShellExecute = true }));
            }
        }


        private void MergeCells(ExcelWorksheet worksheet, int rowStart, int colStart, int colEnd)
        {
            var cellRange = worksheet.Cells[rowStart, colStart, rowStart, colEnd];
            var mergedCell = worksheet.Cells[rowStart, colStart];
            cellRange.Merge = true;
            cellRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }


        private bool SetRowProperties(ExcelWorksheet worksheet, int row, int colStart, int colEnd, string backgroundColor , string fontColor)
        {
            for (int i = 1; i < colStart; i++)
            {
                var cell = worksheet.Cells[row, i];
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#8A8A8A"));
            }

            for (int i = colStart; i <= colEnd; i++)
            {
                var cell = worksheet.Cells[row, i];
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.Font.Size = 16;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(backgroundColor));
                cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(fontColor));

            }
            return true;
        }
    }
}
