using DLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingTask.ViewModels;

namespace TrainingTask.Models
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string Position { get; set; }
        public IEnumerable<EmployeeTasksViewModel> EmployeeTasks { get; set; }
    }
}
