using BLayer.DTO;
using BLayer.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class TaskMapper : IBaseMapper<TaskDTO, TaskViewModel>
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
                TypeStatus = (TaskDTO.EnumTypeOfStatus)item.TypeStatus,
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
                TypeStatus = (TaskViewModel.EnumTypeOfStatus)item.TypeStatus,
                ProjectId = item.ProjectId
            };
        }
    }
}
