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
        public long TaskTime { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public EnumTypeOfStatus TypeStatus { get; set; }
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
