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
            return GetConfiguration().Map<Project, ProjectDTO>(project);
        }

        public Project Map(ProjectDTO projectDTO)
        {
            return GetConfiguration().Map<ProjectDTO, Project>(projectDTO);
        }
    }
}
