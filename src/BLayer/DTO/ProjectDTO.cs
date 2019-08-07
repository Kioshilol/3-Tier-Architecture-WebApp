using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BLayer.DTO
{
    [Serializable]
    public class ProjectDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        [XmlArray]
        public ICollection<TaskDTO> Tasks { get; set; }

    }
}
