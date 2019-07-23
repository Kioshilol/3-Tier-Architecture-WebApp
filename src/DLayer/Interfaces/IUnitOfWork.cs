using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Staff> Staff { get; }
        IRepository<Task> Task { get; }
        IGetAllById<Task> TasksById { get; }
        IInsert<Task> InsertStaff { get; }
        void Save();
    }
}
