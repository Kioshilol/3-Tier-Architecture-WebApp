using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLayer.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long TaskTime { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public EnumTypeOfStatus TypeStatus { get; set; }
        public ProjectDTO Project { get; set; }
        public int? ProjectId { get; set; }
        public int[] staffId { get; set; }
        public enum EnumTypeOfStatus
        {
            [Display(Name = "Not Started")]
            NotStarted,
            [Display(Name = "In Process")]
            InProcess,
            [Display(Name = "Completed")]
            Completed,
            [Display(Name = "Delayed")]
            Delayed
        }
    }
}
