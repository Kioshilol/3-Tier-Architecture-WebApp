using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Interfaces
{
    public interface IExportToXML<T>
    {
        void ExportToXML(List<T> collection);
    }
}
