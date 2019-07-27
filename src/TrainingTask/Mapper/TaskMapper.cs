using BLayer.DTO;
using Core.Enum;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class TaskMapper : IMapper<TaskDTO, TaskViewModel>
    {
        public TaskDTO Map(TaskViewModel item)
        {
            return new TaskDTO()
            {
                Id = item.Id,
                Name = item.Name,
                TaskTime = item.TaskTime,
                DateOfStart = item.DateOfStart,
                DateOfEnd = item.DateOfEnd,
                TypeStatus = (Status)item.TypeStatus,
                ProjectId = item.ProjectId.Value
            };
        }

        public TaskViewModel Map(TaskDTO item)
        {
            return new TaskViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                TaskTime = item.TaskTime,
                DateOfStart = item.DateOfStart,
                DateOfEnd = item.DateOfEnd,
                TypeStatus = (Status)item.TypeStatus,
                ProjectId = item.ProjectId
            };
        }
    }
}
