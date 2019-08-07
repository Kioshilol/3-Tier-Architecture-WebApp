using Core.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace BLayer.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TaskTime { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public Status TypeStatus { get; set; }
        public ProjectDTO Project { get; set; }
        public int ProjectId { get; set; }
        [NotMapped]
        public ICollection<EmployeeTasksDTO> EmployeeTasks { get; set; }

        public TaskDTO()
        {
            EmployeeTasks = new List<EmployeeTasksDTO>();
        }
    }
}
