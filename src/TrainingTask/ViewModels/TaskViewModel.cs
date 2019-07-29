using Core.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingTask.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        public long TaskTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfEnd { get; set; }
        [Required]
        public Status TypeStatus { get; set; }
        public int[] EmployeeId { get; set; }
    }
}
