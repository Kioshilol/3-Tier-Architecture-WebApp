using BLayer.DTO;
using BLayer.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BLayer.Exporters
{
    public class ExportProjectsToExcel : BaseExcelExporter, IExportToExcel<ProjectDTO>
    {
        public MemoryStream Export(IEnumerable<ProjectDTO> collection)
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
                    var totalTasks = item.Tasks.Count;
                    column = FillCell(excelWorksheet, item.Id, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Name, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.ShortName, row, column, totalTasks);
                    column = FillCell(excelWorksheet, item.Description, row, column, totalTasks);

                    foreach (var task in item.Tasks)
                    {
                        excelWorksheet.Cells[row, column].Value = task.Name;
                        row++;
                    }
                }

                excelPackage.SaveAs(stream);
            }

            return stream;
        }
    }
}
