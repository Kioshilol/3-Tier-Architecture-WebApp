using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Entities
{
    public class Project
    {
        public Project(string name, string shortName, string description)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
        }

        string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        List<Task> TaskList { get; set; }
    }
}
