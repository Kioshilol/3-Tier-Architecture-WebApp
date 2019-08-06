using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TrainingTask.Util
{
    public static class ServiceModule
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IService<ProjectDTO>, ProjectService>();
            services.AddTransient<IService<TaskDTO>, TaskService>();
            services.AddTransient<IService<EmployeeDTO>, EmployeeService>();
        }
    }
}
