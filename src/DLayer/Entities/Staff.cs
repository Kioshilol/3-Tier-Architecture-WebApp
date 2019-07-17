﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace DLayer.Entities
{
    public class Staff : IEntity
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public ICollection<Task> TaskList { get; set; }
    }
}
