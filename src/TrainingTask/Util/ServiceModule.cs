using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using BLayer.Mapping;
using BLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using TrainingTask.Models;

namespace TrainingTask.Util
{
    public static class ServiceModule
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IService<ProjectDTO>, ProjectService>();
            services.AddTransient<ITaskService<TaskDTO>, TaskService>();
            services.AddTransient<IService<EmployeeDTO>, EmployeeService>();
        }
    }
}
