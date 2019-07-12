using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Entities
{
    public class Task
    {
        string Id { get; set; }
        string Name { get; set; }
        int TaskTime { get; set; }
        DateTime DateOfStart { get; set; }
        DateTime DateOfEnd { get; set; }
        List<Staff> StaffList { get; set; }
    }
}
