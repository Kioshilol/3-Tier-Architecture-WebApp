using System;
using System.Collections.Generic;
using System.Text;
using TrainingTask.Models;

namespace TrainingTask.ViewModels
{
    public class EmployeeTasksViewModel
    {
        public int? EmployeeId { get; set; }
        public int? TaskId { get; set; }
        public int Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public TaskViewModel Task { get; set; }
    }
}
