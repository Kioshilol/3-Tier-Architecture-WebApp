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
    public class ExportTasksToExcel : BaseExcelExporter, IExportToExcel<TaskDTO>
    {
        public MemoryStream Export(IEnumerable<TaskDTO> collection)
        {
            MemoryStream stream;
            Type type = typeof(TaskDTO);
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
                    column = FillCell(excelWorksheet, item.DateOfStart, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.DateOfEnd, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.TaskTime, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.TypeStatus, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Project.Name, row, column, totalTasks);

                    foreach (var employeeTasks in item.EmployeeTasks)
                    {
                        excelWorksheet.Cells[row, column].Value = employeeTasks.Employee.Name;
                        row++;
                    }
                }

                excelPackage.SaveAs(stream);
            }

            return stream;
        }
    }
}
