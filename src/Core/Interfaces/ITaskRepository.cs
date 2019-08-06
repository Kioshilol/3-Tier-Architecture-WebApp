using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ITaskRepository<Task> : IRepository<Task>
    {
        IEnumerable<Task> GetTasksByProjectId(int id);
        IEnumerable<Task> GetTasksByEmployeeId(int id);

    }
}
