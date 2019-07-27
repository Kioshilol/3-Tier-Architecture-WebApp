using BLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Interfaces
{
    public interface ITaskService<T> : IService<TaskDTO>
    {
        IEnumerable<TaskDTO> GetAllTasksByProjectId(int id);
    }
}
