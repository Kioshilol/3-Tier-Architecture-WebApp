using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mapper
{
    public class TaskMapper : IMapper<Task,TaskDTO>
    {
        public Task Map(TaskDTO item)
        {
            return new Task()
            {
                Id = item.Id,
                Name = item.Name,
                TaskTime = item.TaskTime,
                DateOfStart = item.DateOfStart,
                DateOfEnd = item.DateOfEnd,
                TypeStatus = item.TypeStatus,
                ProjectId = item.ProjectId.Value,
                EmployeeId = item.EmployeeId
            };
        }

        public TaskDTO Map(Task item)
        {
            return new TaskDTO()
            {
                Id = item.Id,
                Name = item.Name,
                TaskTime = item.TaskTime,
                DateOfStart = item.DateOfStart,
                DateOfEnd = item.DateOfEnd,
                TypeStatus = item.TypeStatus,
                ProjectId = item.ProjectId,
                EmployeeId = item.EmployeeId
            };
        }
    }
}
