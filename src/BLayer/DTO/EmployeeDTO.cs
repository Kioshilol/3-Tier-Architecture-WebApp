
using System.Collections.Generic;

namespace BLayer.DTO
{
    public class EmployeeDTO
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public ICollection<EmployeeTasksDTO> EmployeeTasks { get; set; }
    }
}
