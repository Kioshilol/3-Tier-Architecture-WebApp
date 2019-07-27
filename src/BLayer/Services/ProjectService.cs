using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using BLayer.Mapping;
using Core.Interfaces;
using DLayer;
using DLayer.Entities;
using DLayer.Interfaces;
using System.Collections.Generic;

namespace BLayer.Services
{
    public class ProjectService : BaseService<Project,ProjectDTO>, IService<ProjectDTO>
    {
        IUnitOfWork _DataBase { get; set; }
        private IMapper<Project, ProjectDTO> projectMapper;
        private IMapper<Task, TaskDTO> taskMapper;
        public ProjectService(IUnitOfWork dataBase)
        {
            _DataBase = dataBase;
            projectMapper = new ProjectMapper();
            taskMapper = new TaskMapper();
        }

        public ProjectDTO GetById(int id)
        {
            var project = _DataBase.Projects.GetById(id);
            return this.projectMapper.Map(project);            
        }

        public IEnumerable<ProjectDTO> GetAll()
        {

            return GetPaging(projectMapper, _DataBase.Projects.GetAll()); ;
        }

        public int Add(ProjectDTO entity)
        {
            var project = projectMapper.Map(entity);
            return _DataBase.Projects.Insert(project);
        }

        public void Edit(ProjectDTO entity)
        {
            var project = projectMapper.Map(entity);
            _DataBase.Projects.Edit(project);
        }

        public void Delete(int id)
        {
            _DataBase.Projects.Delete(id);
        }

        public IEnumerable<ProjectDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(projectMapper, _DataBase.Projects.GetAllWithPaging(pageNumber));
        }
    }
}
