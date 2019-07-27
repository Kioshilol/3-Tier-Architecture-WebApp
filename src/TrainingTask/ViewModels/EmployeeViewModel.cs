using Core.Enum;
using System.Collections.Generic;

namespace TrainingTask.Models
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Position { get; set; }
        public IEnumerable<EmployeeViewModel> Staff { get; set; }
    }
}
