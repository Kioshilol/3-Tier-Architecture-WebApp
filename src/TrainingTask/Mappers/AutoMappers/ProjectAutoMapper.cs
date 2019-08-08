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
            return GetConfiguration().Map<ProjectViewModel, ProjectDTO>(projectViewModel);
        }

        public ProjectViewModel Map(ProjectDTO projectDTO)
        {
            return GetConfiguration().Map<ProjectDTO, ProjectViewModel>(projectDTO);
        }
    }
}
