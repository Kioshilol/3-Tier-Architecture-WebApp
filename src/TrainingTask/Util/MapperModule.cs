using BLayer.DTO;
using Core;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TrainingTask.Mapper;
using TrainingTask.Models;

namespace TrainingTask.Util
{
    public static class MapperModule
    {
        public static void AddMapperModule(this IServiceCollection services)
        {
            if (AppSetting.isAutoMapperEnable())
            {
                services.AddTransient<IMapper<ProjectDTO, ProjectViewModel>, AutoProjectMapper>();
                services.AddTransient<IMapper<TaskDTO, TaskViewModel>, AutoTaskMapper>();
                services.AddTransient<IMapper<EmployeeDTO, EmployeeViewModel>, AutoEmployeeMapper>();
            }
            else
            {
                services.AddTransient<IMapper<ProjectDTO, ProjectViewModel>, ProjectMapper>();
                services.AddTransient<IMapper<TaskDTO, TaskViewModel>, TaskMapper>();
                services.AddTransient<IMapper<EmployeeDTO, EmployeeViewModel>, EmployeeMapper>();
            }
        }
    }
}
