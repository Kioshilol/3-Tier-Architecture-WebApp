using BLayer.DTO;
using DLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Mapper
{
    public class TaskMapper : IBaseMapper<Task,TaskDTO>
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
                TypeStatus = (Task.EnumTypeOfStatus)item.TypeStatus
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
                TypeStatus = (TaskDTO.EnumTypeOfStatus)item.TypeStatus
            };
        }
    }
}
