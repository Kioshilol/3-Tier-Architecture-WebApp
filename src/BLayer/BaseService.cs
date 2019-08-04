using Core;
using Core.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;

namespace BLayer
{
    public class BaseService<T,U>
    {
        protected IEnumerable<U> GetAll(IMapper<T, U> mapper, IEnumerable<T> list)
        {
            IEnumerable<T> TList = list;
            var TListDTO = new List<U>();

            foreach (dynamic item in TList)
            {
                var itemDTO = mapper.Map(item);
                TListDTO.Add(itemDTO);
            }

            return TListDTO;
        }

        protected DataTable ConvertToDataTable<T>(IEnumerable<T> collection)
        {

            DataTable dataTable = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach(PropertyInfo property in propertyInfos)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(
            property.PropertyType) ?? property.PropertyType);
            }

            foreach(T item in collection)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow.BeginEdit();
                foreach(PropertyInfo property in propertyInfos)
                {
                    dataRow[property.Name] = property.GetValue(item, null);
                }
                dataRow.EndEdit();
                dataTable.Rows.Add(dataRow);
            }

            dataTable.TableName = $"{type.Name}_details{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xml";
            return dataTable;
        }

        protected void WriteAndSaveXMLFile(DataTable dataTable)
        {
            string filePath = Path.Combine(AppSetting.SetXMLFilesPath(), dataTable.TableName);

            using (XmlTextWriter writer = new XmlTextWriter(filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                dataTable.WriteXml(writer);
            }

        }

        protected void ExportToExcel<T>(IEnumerable<T> collection)
        {
            List<T> list = new List<T>();

            foreach(var item in collection)
            {
                list.Add(item);
            }

            Type type = typeof(T);
            string fileName = $"{type.Name}Details{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(AppSetting.SetExcelFilesPath(), fileName));
            PropertyInfo[] propertyInfos = type.GetProperties();

            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add($"{type.Name}");
                int totalRows = list.Count;
                int column = 1;
                int row = 2;
                int count = 1;

                foreach (var property in propertyInfos)
                {
                    excelWorksheet.Cells[1, count].Value = property.Name;
                    count++;
                }

                foreach (var item in list)
                {
                    foreach(var property in propertyInfos)
                    {
                        excelWorksheet.Cells[row, column].Value = property.GetValue(item, null);
                        column++;
                    }
                    column = 1;
                    row++;
                }

                excelPackage.Save();
            }
        }

    }
}
