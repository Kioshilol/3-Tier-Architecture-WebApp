using Core.Interfaces;
using DLayer.Entities;
using System;

namespace DLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IEmployeeRepository<Employee> Employees { get; }
        ITaskRepository<Task> Tasks { get; }
        void Save();
    }
}
