using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public ICollection<TaskDTO> Tasks { get; set; }

    }
}
