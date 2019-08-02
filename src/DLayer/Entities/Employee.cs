using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DLayer.Entities
{
    public class Employee
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public string FilePath { get; set; }
        public ICollection<EmployeeTasks> EmployeeTasks { get; set; }
    }
}
