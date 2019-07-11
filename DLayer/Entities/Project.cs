using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Entities
{
    class Project
    {
        string Id { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        string Description { get; set; }
        List<Task> TaskList { get; set; }
    }
}
