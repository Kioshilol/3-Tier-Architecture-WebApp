using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BLayer.Exporters
{
    public abstract class  BaseExcelExporter
    {
        protected int FillCell(ExcelWorksheet excelWorksheet , dynamic property, int row, int column, int totalTasks)
        {
            excelWorksheet.Cells[row, column].Value = property;

            if(totalTasks > 0)
            {
                excelWorksheet.Cells[row, column, row + totalTasks - 1, column].Merge = true;
            }

            return ++column;
        }
    }
}
