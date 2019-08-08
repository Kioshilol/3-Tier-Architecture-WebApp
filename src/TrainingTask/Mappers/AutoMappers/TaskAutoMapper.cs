using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Mappers.AutoMappers;
using TrainingTask.Models;

namespace TrainingTask.AutoMappers
{
    public class TaskAutoMapper : AutoMapperConfiguration, IMapper<TaskDTO,TaskViewModel>
    {
        public TaskDTO Map(TaskViewModel taskViewModel)
        {
            return GetConfiguration().Map<TaskViewModel, TaskDTO>(taskViewModel);
        }

        public TaskViewModel Map(TaskDTO taskDTO)
        {
            return GetConfiguration().Map<TaskDTO, TaskViewModel>(taskDTO);
        }
    }
}
