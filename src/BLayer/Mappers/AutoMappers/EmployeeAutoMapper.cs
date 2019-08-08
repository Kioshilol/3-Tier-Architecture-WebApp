using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mappers.AutoMappers
{
    public class EmployeeAutoMapper : AutoMapperConfiguration, IMapper<Employee, EmployeeDTO>
    {
        public EmployeeDTO Map(Employee employee)
        {
            return GetConfiguration().Map<Employee, EmployeeDTO>(employee);
        }

        public Employee Map(EmployeeDTO employeeDTO)
        {
            return GetConfiguration().Map<EmployeeDTO, Employee>(employeeDTO);
        }
    }
}
