using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TrainingTask.Models;

namespace TrainingTask.ViewModels
{
    public class EmployeeTasksViewModel
    {
        public int? EmployeeId { get; set; }
        [XmlIgnore]
        public int? TaskId { get; set; }
        [XmlIgnore]
        public int Id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public TaskViewModel Task { get; set; }
    }
}
