using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class AutoTaskMapper : IMapper<TaskDTO,TaskViewModel>
    {
        public TaskDTO Map(TaskViewModel taskViewModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TaskViewModel, TaskDTO>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<TaskViewModel, TaskDTO>(taskViewModel);
        }

        public TaskViewModel Map(TaskDTO taskDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TaskDTO, TaskViewModel>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<TaskDTO, TaskViewModel>(taskDTO);
        }
    }
}
