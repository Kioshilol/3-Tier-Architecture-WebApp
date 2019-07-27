using BLayer.DTO;
using BLayer.Mapper;
using Core.Interfaces;
using TrainingTask.Models;

namespace TrainingTask.Mapping
{
    public class ProjectMapper : IMapper<ProjectDTO, ProjectViewModel>
    {
        public ProjectDTO Map(ProjectViewModel item)
        {
            return new ProjectDTO()
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }

        public ProjectViewModel Map(ProjectDTO item)
        {
            return new ProjectViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                ShortName = item.ShortName,
                Description = item.Description
            };
        }
    }
}
