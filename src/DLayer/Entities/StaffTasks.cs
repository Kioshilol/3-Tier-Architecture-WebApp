using DLayer.Entities;
using System;
using System.Collections.Generic;

namespace DLayer.Entities
{
    public partial class EmployeeTasks
    {
        public int? StaffId { get; set; }
        public int? TaskId { get; set; }
        public int Id { get; set; }
        public Employee Staff { get; set; }
        public Task Task { get; set; }
    }
}
