using BLayer.Interfaces;
using Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BLayer
{
    public class ExportToXML<T> : IExportToXML<T>
    {
        public MemoryStream Export(IEnumerable<T> collection)
        {
            Type type = collection.GetType();
            var stream = new MemoryStream();

            DataContractSerializer serializer = new DataContractSerializer(type);

            using (XmlTextWriter writer = new XmlTextWriter(stream, null))
            {
                writer.Formatting = Formatting.Indented;
                foreach (var item in collection)
                {
                    serializer.WriteObject(writer, item);
                }
            }

            return stream;
        }
    }
}
