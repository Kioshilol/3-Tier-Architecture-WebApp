using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;

namespace DLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmployeeRepository<Employee> _employee;
        private ITaskRepository<Task> _task;
        private IRepository<Project> _project;
        public UnitOfWork(IEmployeeRepository<Employee> employee, IRepository<Project> project, ITaskRepository<Task> task)
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

        public IEmployeeRepository<Employee> Employees
        {
            get
            {
                return _employee;
            }
        }

        public ITaskRepository<Task> Tasks
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

