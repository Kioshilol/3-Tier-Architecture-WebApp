using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace DLayer.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
