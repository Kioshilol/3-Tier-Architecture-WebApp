using BLayer.DTO;
using BLayer.Mappers;
using BLayer.Mappers.AutoMappers;
using Core;
using Core.Interfaces;
using DLayer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingTask.Util
{
    public static class MapperModule
    {
        public static void AddEntityMapperModule(this IServiceCollection services)
        {
            if (AppSetting.isAutoMapperEnable())
            {
                services.AddTransient<IMapper<Project, ProjectDTO>, ProjectAutoMapper>();
                services.AddTransient<IMapper<Task, TaskDTO>, TaskAutoMapper>();
                services.AddTransient<IMapper<Employee, EmployeeDTO>, EmployeeAutoMapper>();
            }
            else
            {
                services.AddTransient<IMapper<Project, ProjectDTO>, ProjectMapper>();
                services.AddTransient<IMapper<Task, TaskDTO>, TaskMapper>();
                services.AddTransient<IMapper<Employee, EmployeeDTO>, EmployeeMapper>();
            }
        }
    }
}
