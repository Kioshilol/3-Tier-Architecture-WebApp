using BLayer.Interfaces;
using Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BLayer
{
    public class ExportToXML<T> : IExportToXML<T>
    {
        public MemoryStream Export(IEnumerable<T> collection)
        {
            var stream = new MemoryStream();
            Type type = typeof(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            foreach (var item in collection)
            {
                xmlSerializer.Serialize(stream, item);
            }

            return stream;
        }
    }
}
