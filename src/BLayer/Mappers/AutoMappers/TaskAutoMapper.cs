using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mappers.AutoMappers
{
    public class TaskAutoMapper : AutoMapperConfiguration, IMapper<Task, TaskDTO>
    {
        public TaskDTO Map(Task task)
        {
            return GetConfiguration().Map<Task, TaskDTO>(task);
        }

        public Task Map(TaskDTO taskDTO)
        {
            return GetConfiguration().Map<TaskDTO, Task>(taskDTO);
        }
    }
}
