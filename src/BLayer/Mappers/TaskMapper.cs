using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;
using System.Linq;

namespace BLayer.Mappers
{
    public class TaskMapper : IMapper<Task,TaskDTO>
    {
        public Task Map(TaskDTO taskDTO)
        {

            return new Task()
            {
                Id = taskDTO.Id,
                Name = taskDTO.Name,
                TaskTime = taskDTO.TaskTime,
                DateOfStart = taskDTO.DateOfStart,
                DateOfEnd = taskDTO.DateOfEnd,
                TypeStatus = taskDTO.TypeStatus,
                ProjectId = taskDTO.ProjectId.Value,
                EmployeeId = taskDTO.EmployeeId,
            };
        }

        public TaskDTO Map(Task task)
        {
            return new TaskDTO()
            {
                Id = task.Id,
                Name = task.Name,
                TaskTime = task.TaskTime,
                DateOfStart = task.DateOfStart,
                DateOfEnd = task.DateOfEnd,
                TypeStatus = task.TypeStatus,
                ProjectId = task.ProjectId,
                EmployeeId = task.EmployeeId,
            };
        }
    }
}
