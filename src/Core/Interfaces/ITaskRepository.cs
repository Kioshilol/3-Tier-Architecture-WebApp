using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ITaskRepository<Task> : IRepository<Task>
    {
        IEnumerable<Task> GetAllTasksByProjectId(int id);
    }
}
