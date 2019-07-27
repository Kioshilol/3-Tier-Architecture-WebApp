using Core.Interfaces;
using DLayer.EF.EfEntities;
using DLayer.EF.EfRepositories;
using DLayer.Entities;
using DLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DLayer.EF
{
    public static class RepositoryModule
    {
        public static void AddServiceRepository(this IServiceCollection services)
        {
            if (AppSetting.EfConnect())
            {
                services.AddTransient<IRepository<Employee>, EfEmployeeRepository>();
                services.AddTransient<IRepository<Project>, EfProjectRepository>();
                services.AddTransient<ITaskRepository<Task>, EfTaskRepository>();
                services.AddTransient<TrainingTaskContext>();
            }
            else
            {
                services.AddTransient<IRepository<Project>, ProjectRepository>();
                services.AddTransient<ITaskRepository<Task>, TaskRepository>();
                services.AddTransient<IRepository<Employee>, EmployeeRepository>();
            }
        }
    }
}
