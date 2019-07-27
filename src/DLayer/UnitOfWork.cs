using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using DLayer.Repositories;
using System.Data.SqlClient;

namespace DLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Employee> _employee;
        private ITaskRepository<Task> _task;
        private IRepository<Project> _project;
        public UnitOfWork(IRepository<Employee> employee, IRepository<Project> project, ITaskRepository<Task> task)
        {
            _employee = employee;
            _project = project;
            _task = task;
        }

        public IRepository<Project> Projects
        {
            get
            {
                return _project;
            }
        }

        public IRepository<Employee> Employee
        {
            get
            {
                return _employee;
            }
        }

        public ITaskRepository<Task> Task
        {
            get
            {
                return _task;
            }
        }
        public void Dispose(){   }
        public void Save(){   }
    }
}

