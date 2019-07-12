using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Entities
{
    public class Staff
    {
        string Id { get; set; }
        string Surname { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        string Postition { get; set; }
        List<Task> TaskList { get; set; }
    }
}
