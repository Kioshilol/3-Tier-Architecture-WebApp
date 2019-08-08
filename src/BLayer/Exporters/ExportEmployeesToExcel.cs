using BLayer.DTO;
using BLayer.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BLayer.Exporters
{
    public class ExportEmployeesToExcel : BaseExcelExporter, IExportToExcel<EmployeeDTO>
    {
        public MemoryStream Export(IEnumerable<EmployeeDTO> collection)
        {
            MemoryStream stream;
            Type type = typeof(ProjectDTO);
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add($"{type.Name}");
            int row = 1;

            using (stream = new MemoryStream())
            {
                foreach (var item in collection)
                {
                    var column = 1;
                    var totalTasks = item.EmployeeTasks.Count;
                    column = FillCell(excelWorksheet, item.Id, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Name, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Position, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.SecondName, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Surname, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.FilePath, row, column, totalTasks);

                    foreach (var employeeTasks in item.EmployeeTasks)
                    {
                        excelWorksheet.Cells[row, column].Value = employeeTasks.Task.Name;
                        row++;
                    }
                }

                excelPackage.SaveAs(stream);
            } 

            return stream;
        }
    }
}
