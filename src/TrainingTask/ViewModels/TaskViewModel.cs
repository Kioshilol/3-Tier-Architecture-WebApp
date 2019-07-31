using BLayer.DTO;
using Core.Enum;
using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingTask.ViewModels;

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
        public ICollection<EmployeeTasksViewModel> EmployeeTasks { get; set; }
        public EmployeeViewModel[] SelectedEmployee { get; set; }
    }
}
