using Core;
using Core.Interfaces;
using DLayer.EFContext.EfEntities;
using DLayer.EFContext.EfRepositories;
using DLayer.Entities;
using DLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DLayer.EFContext
{
    public static class RepositoryModule
    {
        public static void AddServiceRepository(this IServiceCollection services)
        {
            if (AppSetting.IsEfConnect())
            {
                services.AddTransient<IEmployeeRepository<Employee>, EmployeeEfRepository>();
                services.AddTransient<IRepository<Project>, ProjectEFRepository>();
                services.AddTransient<ITaskRepository<Task>, TaskEfRepository>();
                services.AddTransient<TrainingTaskContext>();
            }
            else
            {
                services.AddTransient<IRepository<Project>, ProjectRepository>();
                services.AddTransient<ITaskRepository<Task>, TaskRepository>();
                services.AddTransient<IEmployeeRepository<Employee>, EmployeeRepository>();
            }
        }
    }
}
