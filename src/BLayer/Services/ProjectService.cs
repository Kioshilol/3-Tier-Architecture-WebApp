using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BLayer.Services
{
    public class ProjectService : BaseMapper, IService<ProjectDTO>
    {
        private IUnitOfWork _dataBase { get; set; }
        private IMapper<Project, ProjectDTO> _projectMapper;
        private IMapper<Task, TaskDTO> _taskMapper;
        private ILogger<ProjectService> _logger;
        public ProjectService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper, IMapper<Project, ProjectDTO> projectMapper, ILogger<ProjectService> logger)
        {
            _dataBase = dataBase;
            _projectMapper = projectMapper;
            _taskMapper = taskMapper;
            _logger = logger;
        }

        public ProjectDTO GetById(int id)
        {
            _logger.LogInformation($"Get project by id: {id}");
            ProjectDTO projectdDTO;

            try
            {
                var project = _dataBase.Projects.GetById(id);
                var tasks = Map(_taskMapper, _dataBase.Tasks.GetTasksByProjectId(id));
                projectdDTO = _projectMapper.Map(project);

                foreach(var task in tasks)
                {
                    projectdDTO.Tasks.Add(task);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return projectdDTO;
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            _logger.LogInformation($"Get all projects");
            IEnumerable<ProjectDTO> projectsDTO;

            try
            {
                projectsDTO = Map(_projectMapper, _dataBase.Projects.GetAll());
                var tasksDTO = Map(_taskMapper, _dataBase.Tasks.GetAll());
                ProjectInitialization(tasksDTO, projectsDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
            return projectsDTO;

        }

        public int Add(ProjectDTO entity)
        {
            _logger.LogInformation($"Add project {entity.Name}");
            var project = _projectMapper.Map(entity);
            return _dataBase.Projects.Insert(project);
        }

        public void Edit(ProjectDTO entity)
        {
            _logger.LogInformation($"Editing of project. Id:{entity.Id}");

            try
            {
                var project = _projectMapper.Map(entity);
                _dataBase.Projects.Edit(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete project by id: {id}");

            try
            {
                _dataBase.Projects.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            } 
        }

        public IEnumerable<ProjectDTO> GetAllWithPaging(int pageNumber)
        {
            _logger.LogInformation($"Get all projects by page number: {pageNumber}");
            IEnumerable<ProjectDTO> projectsDTO;

            try
            {
                projectsDTO = Map(_projectMapper, _dataBase.Projects.GetAllWithPaging(pageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return projectsDTO;
        }

        private IEnumerable<ProjectDTO> ProjectInitialization(IEnumerable<TaskDTO> tasksDTO, IEnumerable<ProjectDTO> projectsDTO)
        {
            foreach (var projectDTO in projectsDTO)
            {
                foreach (var taskDTO in tasksDTO)
                {
                    if (projectDTO.Id == taskDTO.ProjectId)
                        projectDTO.Tasks.Add(taskDTO);
                }
            }
            return projectsDTO;
        }
    }
}
