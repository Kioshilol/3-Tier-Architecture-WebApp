using AutoMapper;
using BLayer.DTO;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mappers.AutoMappers
{
    public class ProjectAutoMapper : AutoMapperConfiguration, IMapper<Project, ProjectDTO>
    {
        public ProjectDTO Map(Project project)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<Project, ProjectDTO>(project);
        }

        public Project Map(ProjectDTO projectDTO)
        {
            IMapper iMapper = GetConfiguration().CreateMapper();
            return iMapper.Map<ProjectDTO, Project>(projectDTO);
        }
    }
}
