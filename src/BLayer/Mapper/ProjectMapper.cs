using BLayer.DTO;
using BLayer.Mapper;
using DLayer.Entities;

namespace BLayer.Mapping
{
    public class ProjectMapper : IBaseMapper<Project, ProjectDTO>
    {
        public  ProjectDTO Map(Project item)
        {
            return new ProjectDTO()
            {
                Id = item.Id,
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
