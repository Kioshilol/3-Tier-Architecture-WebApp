using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BLayer.DTO
{
    public class EmployeeTasksDTO
    {
        [IgnoreDataMember]
        public int? EmployeeId { get; set; }
        [IgnoreDataMember]
        public int? TaskId { get; set; }
        [IgnoreDataMember]
        public int Id { get; set; }
        public EmployeeDTO Employee { get; set; }
        public TaskDTO Task { get; set; }
    }
}
