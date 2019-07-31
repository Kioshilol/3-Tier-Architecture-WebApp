using AutoMapper;
using BLayer.DTO;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Mappers.AutoMappers
{
    public class AutoMapperConfiguration
    {
        protected  MapperConfiguration GetConfiguration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeViewModel, EmployeeDTO>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<ProjectViewModel, ProjectDTO>();
                cfg.CreateMap<ProjectDTO, ProjectViewModel>();
                cfg.CreateMap<EmployeeTasksViewModel, EmployeeTasksDTO>();
                cfg.CreateMap<EmployeeTasksDTO, EmployeeTasksViewModel>();
                cfg.CreateMap<TaskViewModel, TaskDTO>().ForMember(dest => dest.EmployeeTasks, act => act.MapFrom(src => src.EmployeeTasks));
                cfg.CreateMap<TaskDTO, TaskViewModel>().ForMember(dest => dest.EmployeeTasks, act => act.MapFrom(src => src.EmployeeTasks));
            });
            return config;
        }
    }
}
