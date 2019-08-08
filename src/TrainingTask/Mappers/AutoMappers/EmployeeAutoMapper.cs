using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Mappers.AutoMappers;
using TrainingTask.Models;

namespace TrainingTask.AutoMappers
{
    public class EmployeeAutoMapper : AutoMapperConfiguration, IMapper<EmployeeDTO,EmployeeViewModel>
    {
        public EmployeeDTO Map(EmployeeViewModel employeeViewModel)
        {
            return GetConfiguration().Map<EmployeeViewModel, EmployeeDTO>(employeeViewModel);
        }

        public EmployeeViewModel Map(EmployeeDTO employeeDTO)
        {
            return GetConfiguration().Map<EmployeeDTO, EmployeeViewModel>(employeeDTO);
        }
    }
}
