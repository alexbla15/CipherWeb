using CipherData.Models;
using System.Xml;

namespace CipherData
{
    public interface ICipherInfo
    {
        Task<List<Event>> GetEvents();
        Task<List<Models.StorageSystem>> GetSystems();
        Task<List<Category>> GetSubCategories();
        Task<List<Vessel>> GetVessels();
    }
    
    public class ExcelService
    {
        public byte[] GenerateExcel<T>(IEnumerable<T> data)
        {
            // Create a MemoryStream to write the Excel XML
            using (MemoryStream stream = new MemoryStream())
            {
                // Create an XML document
                XmlDocument xmlDoc = new XmlDocument();

                // Create XML declaration
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDeclaration);

                // Create root element for workbook
                XmlElement workbookElement = xmlDoc.CreateElement("Workbook");
                workbookElement.SetAttribute("xmlns", "urn:schemas-microsoft-com:office:spreadsheet");
                workbookElement.SetAttribute("xmlns:o", "urn:schemas-microsoft-com:office:office");
                workbookElement.SetAttribute("xmlns:x", "urn:schemas-microsoft-com:office:excel");
                workbookElement.SetAttribute("xmlns:ss", "urn:schemas-microsoft-com:office:spreadsheet");
                workbookElement.SetAttribute("xmlns:html", "http://www.w3.org/TR/REC-html40");

                // Create worksheet element
                XmlElement worksheetElement = xmlDoc.CreateElement("Worksheet");
                worksheetElement.SetAttribute("ss:Name", "Sheet1");

                // Create table element
                XmlElement tableElement = xmlDoc.CreateElement("Table");

                // Assuming data is a collection of objects with properties to be exported
                var properties = typeof(T).GetProperties();

                // Add header row
                XmlElement headerRowElement = xmlDoc.CreateElement("Row");
                for (int i = 0; i < properties.Length; i++)
                {
                    XmlElement cellElement = xmlDoc.CreateElement("Cell");
                    XmlElement dataElement = xmlDoc.CreateElement("Data");
                    dataElement.SetAttribute("ss:Type", "String");
                    dataElement.InnerText = properties[i].Name;
                    cellElement.AppendChild(dataElement);
                    headerRowElement.AppendChild(cellElement);
                }
                tableElement.AppendChild(headerRowElement);

                // Add data rows
                foreach (var item in data)
                {
                    XmlElement dataRowElement = xmlDoc.CreateElement("Row");
                    for (int i = 0; i < properties.Length; i++)
                    {
                        XmlElement cellElement = xmlDoc.CreateElement("Cell");
                        XmlElement dataElement = xmlDoc.CreateElement("Data");
                        dataElement.SetAttribute("ss:Type", "String");
                        dataElement.InnerText = Convert.ToString(properties[i].GetValue(item));
                        cellElement.AppendChild(dataElement);
                        dataRowElement.AppendChild(cellElement);
                    }
                    tableElement.AppendChild(dataRowElement);
                }

                worksheetElement.AppendChild(tableElement);
                workbookElement.AppendChild(worksheetElement);
                xmlDoc.AppendChild(workbookElement);

                // Save XML document to MemoryStream
                xmlDoc.Save(stream);

                // Return the byte array of the MemoryStream
                return stream.ToArray();
            }
        }
    }
}