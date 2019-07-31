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
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<Task, TaskDTO>(task);
        }

        public Task Map(TaskDTO taskDTO)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<TaskDTO, Task>(taskDTO);
        }
    }
}
