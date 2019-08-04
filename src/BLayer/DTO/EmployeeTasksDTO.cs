using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BLayer.DTO
{
    public class EmployeeTasksDTO
    {
        public int? EmployeeId { get; set; }
        public int? TaskId { get; set; }
        public int Id { get; set; }
        public EmployeeDTO Employee { get; set; }
        public TaskDTO Task { get; set; }
    }
}
