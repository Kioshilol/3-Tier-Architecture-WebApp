using BLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BLayer
{
    public class BaseExporter<T> : IExportToXML<T>
    {
        public void ExportToXML(List<T> collection)
        {
            Type type = typeof(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(type.Name);
            foreach (var item in collection)
            {
                xmlSerializer.Serialize(writer, item);
            }
        }
    }
}
