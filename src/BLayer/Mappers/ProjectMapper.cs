using BLayer.DTO;
using BLayer.Mappers;
using Core.Interfaces;
using DLayer.Entities;

namespace BLayer.Mappers
{
    public class ProjectMapper : IMapper<Project, ProjectDTO>
    {
        public  ProjectDTO Map(Project item)
        {
            return new ProjectDTO()
            {
                Id = item.Id.Value,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }

        public Project Map(ProjectDTO item)
        {
            return new Project()
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
    }
}
