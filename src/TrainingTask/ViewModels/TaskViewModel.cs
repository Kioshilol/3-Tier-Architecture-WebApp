using BLayer.DTO;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingTask.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
        public long TaskTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfEnd { get; set; }
        public Status TypeStatus { get; set; }
    }
}
