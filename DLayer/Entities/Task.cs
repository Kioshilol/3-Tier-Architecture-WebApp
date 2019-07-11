using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Entities
{
    class Task
    {
        string Id { get; set; }
        string Name { get; set; }
        int TaskTime { get; set; }
        DateTime DateOfStart { get; set; }
        DateTime DateOfEnd { get; set; }
    }
}
