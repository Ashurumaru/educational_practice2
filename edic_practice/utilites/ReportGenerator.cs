using System;
using System.Linq;
using Novacode;
using System.Data.Entity;


namespace edic_practice.utilites
{
    internal class ReportGenerator
    {
        public static void GenerateCarsReport()
        {
            string filePath = "AutoparkCarsReport.docx";

            using (var doc = DocX.Create(filePath))
            {
                doc.InsertParagraph("Отчет по автомобилям в автопарке")
                   .FontSize(16)
                   .Bold()
                   .Alignment = Alignment.center;
                string currentDate = DateTime.Now.ToString("«dd» MMMM yyyy г.");
                doc.InsertParagraph($"Дата создания отчета: {currentDate}")
                    .FontSize(12)
                    .Italic()
                    .Alignment = Alignment.center;


                using (var context = new ed_practiceEntities())
                {
                    var cars = context.Cars.Include(c => c.RentalPrices).ToList();

                    if (cars.Any())
                    {
                        var table = doc.AddTable(cars.Count + 1, 5);

                        table.Rows[0].Cells[0].Paragraphs.First().Append("ID");
                        table.Rows[0].Cells[1].Paragraphs.First().Append("Марка");
                        table.Rows[0].Cells[2].Paragraphs.First().Append("Модель");
                        table.Rows[0].Cells[3].Paragraphs.First().Append("Год");
                        table.Rows[0].Cells[4].Paragraphs.First().Append("Статус");


                        for (int i = 0; i < cars.Count; i++)
                        {
                            table.Rows[i + 1].Cells[0].Paragraphs.First().Append(cars[i].CarID.ToString());
                            table.Rows[i + 1].Cells[1].Paragraphs.First().Append(cars[i].Brand);
                            table.Rows[i + 1].Cells[2].Paragraphs.First().Append(cars[i].Model);
                            table.Rows[i + 1].Cells[3].Paragraphs.First().Append(cars[i].CarYears.ToString());
                            table.Rows[i + 1].Cells[4].Paragraphs.First().Append(cars[i].CarStatuses.StatusName); 

                        }

                        doc.InsertTable(table);
                    }
                    else
                    {
                        doc.InsertParagraph("Нет данных об автомобилях для отображения.");
                    }
                }
                doc.InsertParagraph("\n");
                doc.InsertParagraph("\n");
                var signatureTable = doc.AddTable(3, 3);
                string signaturePath = @"E:\workSpace\development\C#\repos\edic_practice\edic_practice\Images\подпись.png";
                Image image = doc.AddImage(signaturePath);
                Picture signaturePicture = image.CreatePicture(30, 100);
                signatureTable.Rows[0].Cells[0].Paragraphs.First().Append("Сдал Исполнитель: Автопарк «Автоэлита»").Bold().FontSize(12);
                signatureTable.Rows[1].Cells[0].Paragraphs.First().Append(currentDate).FontSize(12);
                signatureTable.Rows[2].Cells[0].Paragraphs.First().Append("М.П.").FontSize(12);
                signatureTable.Rows[2].Cells[0].Paragraphs.First().AppendPicture(signaturePicture);

                signatureTable.Rows[0].Cells[2].Paragraphs.First().Append("Принял Заказчик:").Bold().FontSize(12);
                signatureTable.Rows[1].Cells[2].Paragraphs.First().Append("«_____»___________20____г.").FontSize(12);
                signatureTable.Rows[2].Cells[2].Paragraphs.First().Append("М.П.").FontSize(12);



                signatureTable.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));


                signatureTable.SetColumnWidth(0, 3200);
                signatureTable.SetColumnWidth(1, 3600);
                signatureTable.SetColumnWidth(2, 3200);

                doc.InsertTable(signatureTable);

                doc.Save();

            }
        }

        public static void GenerateRevenueReport(DateTime startDate, DateTime endDate)
        {
            string filePath = $"RevenueReport_{startDate:yyyy-MM-dd}_to_{endDate:yyyy-MM-dd}.docx";

            using (var doc = DocX.Create(filePath))
            {
                var title = doc.InsertParagraph("ОТЧЕТ ОБ ОКАЗАННЫХ УСЛУГАХ")
                                   .FontSize(16)
                                   .Bold()
                                   .Alignment = Alignment.center;
                doc.InsertParagraph("\n");

                var contractInfo = doc.InsertParagraph("по договору №123 от «01» января 2024 г.")
                   .FontSize(12)
                   .Alignment = Alignment.both;
                doc.InsertParagraph("\n");

                var periodInfo = doc.InsertParagraph($"В соответствии с Договором №123 от «01» января 2024 года  в отчетном периоде с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}")
                   .FontSize(12)
                   .Alignment = Alignment.both;

                var context = new ed_practiceEntities();

                var transactions = context.RentalPayments.Include("Rentals")
                        .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                        .ToList();

                var totalRevenue = transactions.Sum(p => p.Amount);

                doc.InsertParagraph($"Общая выручка за указанный период составила: ")
                    .SpacingAfter(10)
                    .FontSize(12);

                if (transactions.Any())
                {
                    var table = doc.AddTable(transactions.Count + 1, 6);
                    table.Rows[0].Cells[0].Paragraphs.First().Append("Дата платежа");
                    table.Rows[0].Cells[1].Paragraphs.First().Append("Дней аренды");
                    table.Rows[0].Cells[2].Paragraphs.First().Append("Арендатор");
                    table.Rows[0].Cells[3].Paragraphs.First().Append("Сумма");
                    table.Rows[0].Cells[4].Paragraphs.First().Append("Автомобиль");
                    table.Rows[0].Cells[5].Paragraphs.First().Append("Комментарий");

                    for (int i = 0; i < transactions.Count; i++)
                    {
                        var transaction = transactions[i];
                        var rentalDays = (transaction.Rentals.RentalEndDate - transaction.Rentals.RentalStartDate).Days; 
                        table.Rows[i + 1].Cells[0].Paragraphs.First().Append(transaction.PaymentDate.ToString("dd.MM.yyyy"));
                        table.Rows[i + 1].Cells[1].Paragraphs.First().Append(rentalDays.ToString()); 
                        table.Rows[i + 1].Cells[2].Paragraphs.First().Append(transaction.Rentals.Users.Username);
                        table.Rows[i + 1].Cells[3].Paragraphs.First().Append($"{transaction.Amount:C2}");
                        table.Rows[i + 1].Cells[4].Paragraphs.First().Append(transaction.Rentals.Cars.Model);
                    }

                    doc.InsertTable(table);
                    table.Design = TableDesign.TableGrid;
                    table.Alignment = Alignment.center;
                    table.AutoFit = AutoFit.Contents;
                }
                else
                {
                    doc.InsertParagraph("За выбранный период транзакции отсутствуют.");
                }

                var totalCost = doc.InsertParagraph($"Всего оказано услуг на сумму {totalRevenue:C2} руб., в том числе НДС 20% в размере {totalRevenue * 0.2m:C2} руб.")
                    .FontSize(12)
                    .Alignment = Alignment.both;
                doc.InsertParagraph("\n");


                var signatureTable = doc.AddTable(3, 3);
                string currentDate = DateTime.Now.ToString("«dd» MMMM yyyy г.");
                string signaturePath = @"E:\workSpace\development\C#\repos\edic_practice\edic_practice\Images\подпись.png";
                Image image = doc.AddImage(signaturePath);
                Picture signaturePicture = image.CreatePicture(30, 100);
                signatureTable.Rows[0].Cells[0].Paragraphs.First().Append("Сдал Исполнитель: Автопарк «Автоэлита»").Bold().FontSize(12);
                signatureTable.Rows[1].Cells[0].Paragraphs.First().Append(currentDate).FontSize(12);
                signatureTable.Rows[2].Cells[0].Paragraphs.First().Append("М.П.").FontSize(12);
                signatureTable.Rows[2].Cells[0].Paragraphs.First().AppendPicture(signaturePicture);

                signatureTable.Rows[0].Cells[2].Paragraphs.First().Append("Принял Заказчик:").Bold().FontSize(12);
                signatureTable.Rows[1].Cells[2].Paragraphs.First().Append("«_____»___________20____г.").FontSize(12);
                signatureTable.Rows[2].Cells[2].Paragraphs.First().Append("М.П.").FontSize(12);



                signatureTable.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));
                signatureTable.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, 0, 0, System.Drawing.Color.White));


                signatureTable.SetColumnWidth(0, 3200);
                signatureTable.SetColumnWidth(1, 3600);
                signatureTable.SetColumnWidth(2, 3200);

                doc.InsertTable(signatureTable);

                doc.Save();

            }
        }    
    }
}
