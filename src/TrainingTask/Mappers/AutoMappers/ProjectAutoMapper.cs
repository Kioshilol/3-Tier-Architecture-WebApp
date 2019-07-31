using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using TrainingTask.Mappers.AutoMappers;
using TrainingTask.Models;

namespace TrainingTask.AutoMappers
{
    public class ProjectAutoMapper : AutoMapperConfiguration, IMapper<ProjectDTO,ProjectViewModel>
    {
        public ProjectDTO Map(ProjectViewModel projectViewModel)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<ProjectViewModel, ProjectDTO>(projectViewModel);
        }

        public ProjectViewModel Map(ProjectDTO projectDTO)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<ProjectDTO, ProjectViewModel>(projectDTO);
        }
    }
}
