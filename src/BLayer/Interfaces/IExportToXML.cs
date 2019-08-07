using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLayer.Interfaces
{
    public interface IExportToXML<T>
    {
        MemoryStream Export(IEnumerable<T> collection);
    }
}
