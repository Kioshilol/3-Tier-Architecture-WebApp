using BLayer.DTO;
using BLayer.Interfaces;
using Core;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BLayer.Exporters
{
    public class ExportProjectsToExcel : IExportToExcel<ProjectDTO>
    {
        public void ExportToExcel(IEnumerable<ProjectDTO> collection)
        {
            Type type = typeof(ProjectDTO);
            string fileName = $"{type.Name}Details{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(AppSetting.SetExcelFilesPath(), fileName));
            PropertyInfo[] propertyInfos = type.GetProperties();

            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add($"{type.Name}");
                int column = 1;
                int row = 1;
                int count = 1;

                foreach (var property in propertyInfos)
                {
                    excelWorksheet.Cells[row, count].Value = property.Name;
                    count++;
                }
                row++;

                foreach (var item in collection)
                {
                    var totalTasks = item.Tasks.Count;
                    foreach (var property in propertyInfos)
                    {
                        if(totalTasks > 1)
                        {
                            excelWorksheet.Cells[row, column, row - 1  + totalTasks, column].Merge = true;
                        }
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
