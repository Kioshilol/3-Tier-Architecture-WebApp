using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Interfaces
{
    public interface IExportToExcel<T>
    {
        void ExportToExcel(IEnumerable<T> collection);
    }
}
