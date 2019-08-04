using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mappers
{
    public class EmployeeMapper : IMapper<Employee, EmployeeDTO>
    {
        public Employee Map(EmployeeDTO item)
        {
            return new Employee()
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }

        public EmployeeDTO Map(Employee item)
        {
            return new EmployeeDTO()
            {
                Id = item.Id.Value,
                Name = item.Name,
                Surname = item.Surname,
                SecondName = item.SecondName,
                Position = item.Position
            };
        }
    }
}
