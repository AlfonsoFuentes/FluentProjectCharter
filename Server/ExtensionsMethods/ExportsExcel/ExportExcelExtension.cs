﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Shared.Models.FileResults;
using System.Globalization;
namespace Server.ExtensionsMethods.ExportsExcel
{
    public static class ExportExcelExtension
    {

        public static FileResult ToExcel(IQueryable query, string fileName, string SheetName)
        {
            var columns = ExportHelpers.GetProperties(query.ElementType);
            var stream = new MemoryStream();
            try
            {
                using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet();

                    var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                    GenerateWorkbookStylesPartContent(workbookStylesPart);

                    var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                    var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = SheetName };
                    sheets.Append(sheet);

                    workbookPart.Workbook.Save();

                    var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                    var headerRow = new Row();
                    headerRow.Append(new Cell()
                    {
                        CellValue = new CellValue("Item"),
                        DataType = new EnumValue<CellValues>(CellValues.String)
                    });
                    foreach (var column in columns)
                    {
                        headerRow.Append(new Cell()
                        {
                            CellValue = new CellValue(column.Key),
                            DataType = new EnumValue<CellValues>(CellValues.String)
                        });
                    }

                    sheetData.AppendChild(headerRow);
                    int serialNumber = 0;
                    foreach (var item in query)
                    {
                        var row = new Row();
                        var cellId = new Cell();
                        serialNumber += 1;
                        var stringValueID = Convert.ToString(serialNumber, CultureInfo.InvariantCulture);
                        cellId.CellValue = new CellValue(stringValueID);
                        cellId.DataType = new EnumValue<CellValues>(CellValues.String);
                        row.Append(cellId);
                        foreach (var column in columns)
                        {
                            var value = ExportHelpers.GetValue(item, column.Key);
                            var stringValue = $"{value}".Trim();

                            var cell = new Cell();

                            var underlyingType = column.Value.IsGenericType &&
                                column.Value.GetGenericTypeDefinition() == typeof(Nullable<>) ?
                                Nullable.GetUnderlyingType(column.Value) : column.Value;

                            var typeCode = Type.GetTypeCode(underlyingType);

                            if (typeCode == TypeCode.DateTime)
                            {
                                if (!string.IsNullOrWhiteSpace(stringValue))
                                {
                                    cell.CellValue = new CellValue() { Text = ((DateTime)value).ToOADate().ToString(CultureInfo.InvariantCulture) };
                                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                    cell.StyleIndex = (UInt32Value)1U;
                                }
                            }
                            else if (typeCode == TypeCode.Boolean)
                            {
                                cell.CellValue = new CellValue(stringValue.ToLower());
                                cell.DataType = new EnumValue<CellValues>(CellValues.Boolean);
                            }
                            else if (ExportHelpers.IsNumeric(typeCode))
                            {
                                if (value != null)
                                {
                                    stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                                }
                                cell.CellValue = new CellValue(stringValue!);
                                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                            }
                            else
                            {
                                cell.CellValue = new CellValue(stringValue);
                                cell.DataType = new EnumValue<CellValues>(CellValues.String);
                            }

                            row.Append(cell);
                        }

                        sheetData.AppendChild(row);
                    }


                    workbookPart.Workbook.Save();

                }

            }
            catch (Exception ex)
            {

                string exm = ex.Message;
            }


            if (stream?.Length > 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }


            FileResult newresult = new()
            {
                Data = stream?.ToArray(),
                ExportFileName = $"{fileName}.xlsx",
                ContentType = FileResult.OpenxmlExcelContentType,
            };
            return newresult;
        }




        private static void GenerateWorkbookStylesPartContent(WorkbookStylesPart workbookStylesPart1)
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac x16r2 xr" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            stylesheet1.AddNamespaceDeclaration("x16r2", "http://schemas.microsoft.com/office/spreadsheetml/2015/02/main");
            stylesheet1.AddNamespaceDeclaration("xr", "http://schemas.microsoft.com/office/spreadsheetml/2014/revision");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);

            fonts1.Append(font1);

            Fills fills1 = new Fills() { Count = (UInt32Value)2U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            fills1.Append(fill1);
            fills1.Append(fill2);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)2U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)14U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };

            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");

            StylesheetExtension stylesheetExtension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");

            OpenXmlUnknownElement openXmlUnknownElement4 = workbookStylesPart1.CreateUnknownElement("<x15:timelineStyles defaultTimelineStyle=\"TimeSlicerStyleLight1\" xmlns:x15=\"http://schemas.microsoft.com/office/spreadsheetml/2010/11/main\" />");



            stylesheetExtension2.Append(openXmlUnknownElement4);

            stylesheetExtensionList1.Append(stylesheetExtension1);
            stylesheetExtensionList1.Append(stylesheetExtension2);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);

            workbookStylesPart1.Stylesheet = stylesheet1;
        }
    }
}
