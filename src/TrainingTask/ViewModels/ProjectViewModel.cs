using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Models
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
        public string Description { get; set; }
        public ICollection<TaskViewModel> Tasks { get; set; }
    }
}
