using System;
using System.Collections.Generic;
using System.Text;
using MotorPlantBusinessLogic.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MotorPlantBusinessLogic.BuisnessLogics
{
    static class SaveToWord
    {
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="info"></param>
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                foreach (var engine in info.Engines)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { ($"{engine.EngineName}: ", new WordTextProperties { Size = "24", Bold = true }), (engine.Price.ToString(), new WordTextProperties { Size = "24" }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                }
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }

        public static void CreateStoresDoc(StoreWordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                Table table = new Table();
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "20", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "20",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                TableRow tableRowHeader = new TableRow();

                TableCell cellHeaderName = new TableCell();
                cellHeaderName.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                cellHeaderName.Append(new Paragraph(new Run(new Text("Название"))));

                TableCell cellHeaderFIO = new TableCell();
                cellHeaderFIO.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                cellHeaderFIO.Append(new Paragraph(new Run(new Text("ФИО ответственного"))));

                TableCell cellHeaderDateCreation = new TableCell();
                cellHeaderDateCreation.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                cellHeaderDateCreation.Append(new Paragraph(new Run(new Text("Дата создания"))));

                tableRowHeader.Append(cellHeaderName);
                tableRowHeader.Append(cellHeaderFIO);
                tableRowHeader.Append(cellHeaderDateCreation);

                table.Append(tableRowHeader);

                foreach (var store in info.Stores)
                {
                    TableRow tableRow = new TableRow();

                    TableCell cellName = new TableCell();
                    cellName.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                    cellName.Append(new Paragraph(new Run(new Text(store.StoreName))));

                    TableCell cellFIO = new TableCell();
                    cellFIO.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                    cellFIO.Append(new Paragraph(new Run(new Text(store.ResponsibleName))));

                    TableCell cellDateCreation = new TableCell();
                    cellDateCreation.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                    cellDateCreation.Append(new Paragraph(new Run(new Text(store.DateCreation.ToString()))));

                    tableRow.Append(cellName);
                    tableRow.Append(cellFIO);
                    tableRow.Append(cellDateCreation);

                    table.Append(tableRow);
                }

                docBody.AppendChild(table);
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
    }
}
