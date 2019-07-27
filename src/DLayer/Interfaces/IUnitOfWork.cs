using Core.Interfaces;
using DLayer.Entities;
using System;

namespace DLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Employee> Employee { get; }
        ITaskRepository<Task> Task { get; }
        void Save();
    }
}
