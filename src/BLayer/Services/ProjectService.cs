using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using BLayer.Mapping;
using DLayer;
using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Services
{
    public class ProjectService : IService<ProjectDTO>
    {
        IUnitOfWork DataBase { get; set; }
        private IBaseMapper<Project, ProjectDTO> projectMapper;
        public ProjectService()
        {
            this.DataBase = new UnitOfWork();
            this.projectMapper = new ProjectMapper();
        }

        public ProjectDTO GetById(int id)
        {
            var project = DataBase.Projects.GetById(id);
            return this.projectMapper.Map(project);            
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var projects = new List<ProjectDTO>();
            IEnumerable<Project> projectList = DataBase.Projects.GetAll();
            foreach(var project in projectList)
            {
                var projectDto = this.projectMapper.Map(project);
                projects.Add(projectDto);
            }
            return projects;
        }

        public void Add(ProjectDTO entity)
        {
            var project = projectMapper.Map(entity);
            DataBase.Projects.Insert(project);
        }

        public void Edit(ProjectDTO entity)
        {
            var project = projectMapper.Map(entity);
            DataBase.Projects.Edit(project);
        }

        public void Delete(int id)
        {
            DataBase.Projects.Delete(id);
        }
    }
}
