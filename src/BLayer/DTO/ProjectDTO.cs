using System.Collections.Generic;

namespace BLayer.DTO
{
    public class ProjectDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public ICollection<TaskDTO> Tasks { get; set; }
    }
}
