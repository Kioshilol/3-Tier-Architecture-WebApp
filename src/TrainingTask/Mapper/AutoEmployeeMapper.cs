using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class AutoEmployeeMapper : IMapper<EmployeeDTO,EmployeeViewModel>
    {
        public EmployeeDTO Map(EmployeeViewModel employeeViewModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<EmployeeViewModel, EmployeeDTO>(employeeViewModel);
        }

        public EmployeeViewModel Map(EmployeeDTO employeeDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<EmployeeDTO, EmployeeViewModel>(employeeDTO);
        }
    }
}
