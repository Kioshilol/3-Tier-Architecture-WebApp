using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Text;

namespace DLayer.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TaskTime { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public  Status TypeStatus { get; set; }
        public Project Project { get; set; }
        [NotMapped]
        public int[] StaffId { get; set; }
        public ICollection<EmployeeTasks> StaffTasks { get; set; }
        public int? ProjectId { get; set; }
    }
}
