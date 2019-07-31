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
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<Employee, EmployeeDTO>(employee);
        }

        public Employee Map(EmployeeDTO employeeDTO)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<EmployeeDTO, Employee>(employeeDTO);
        }
    }
}
