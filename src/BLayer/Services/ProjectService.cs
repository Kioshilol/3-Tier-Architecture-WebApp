using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using System.Collections.Generic;

namespace BLayer.Services
{
    public class ProjectService : BaseService<Project,ProjectDTO>, IService<ProjectDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Project, ProjectDTO> _projectMapper;
        private IMapper<Task, TaskDTO> _taskMapper;
        public ProjectService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper, IMapper<Project, ProjectDTO> projectMapper)
        {
            _DataBase = dataBase;
            _projectMapper = projectMapper;
            _taskMapper = taskMapper;
        }

        public ProjectDTO GetById(int id)
        {
            var project = _DataBase.Projects.GetById(id);
            return this._projectMapper.Map(project);            
        }

        public IEnumerable<ProjectDTO> GetAll()
        {

            return GetPaging(_projectMapper, _DataBase.Projects.GetAll()); ;
        }

        public int Add(ProjectDTO entity)
        {
            var project = _projectMapper.Map(entity);
            return _DataBase.Projects.Insert(project);
        }

        public void Edit(ProjectDTO entity)
        {
            var project = _projectMapper.Map(entity);
            _DataBase.Projects.Edit(project);
        }

        public void Delete(int id)
        {
            _DataBase.Projects.Delete(id);
        }

        public IEnumerable<ProjectDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(_projectMapper, _DataBase.Projects.GetAllWithPaging(pageNumber));
        }
    }
}
