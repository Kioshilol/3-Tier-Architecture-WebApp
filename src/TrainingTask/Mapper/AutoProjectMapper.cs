using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapper
{
    public class AutoProjectMapper : IMapper<ProjectDTO,ProjectViewModel>
    {
        public ProjectDTO Map(ProjectViewModel projectViewModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProjectViewModel, ProjectDTO>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<ProjectViewModel, ProjectDTO>(projectViewModel);
        }

        public ProjectViewModel Map(ProjectDTO projectDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProjectDTO, ProjectViewModel>();
            });
            IMapper iMapper = config.CreateMapper();
            return iMapper.Map<ProjectDTO, ProjectViewModel>(projectDTO);
        }
    }
}
