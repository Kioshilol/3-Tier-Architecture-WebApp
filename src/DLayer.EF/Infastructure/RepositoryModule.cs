﻿using Core.Interfaces;
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
