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
        public ICollection<Task> TaskList { get; set; }
        public ICollection<EmployeeTasks> StaffTasks { get; set; }
    }
}
