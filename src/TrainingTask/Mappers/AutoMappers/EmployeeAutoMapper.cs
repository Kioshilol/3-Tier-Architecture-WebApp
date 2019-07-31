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
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<EmployeeViewModel, EmployeeDTO>(employeeViewModel);
        }

        public EmployeeViewModel Map(EmployeeDTO employeeDTO)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<EmployeeDTO, EmployeeViewModel>(employeeDTO);
        }
    }
}
