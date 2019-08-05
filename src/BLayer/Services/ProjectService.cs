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

namespace BLayer.Services
{
    public class ProjectService : BaseService<Project,ProjectDTO>, IService<ProjectDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Project, ProjectDTO> _projectMapper;
        private IMapper<Task, TaskDTO> _taskMapper;
        private ILogger<ProjectService> _logger;
        public ProjectService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper, IMapper<Project, ProjectDTO> projectMapper, ILogger<ProjectService> logger)
        {
            _DataBase = dataBase;
            _projectMapper = projectMapper;
            _taskMapper = taskMapper;
            _logger = logger;
        }

        public ProjectDTO GetById(int id)
        {
            _logger.LogInformation($"Get project by id: {id}");
            var project = _DataBase.Projects.GetById(id);
            return _projectMapper.Map(project);            
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            _logger.LogInformation($"Get all projects");
            return GetAll(_projectMapper, _DataBase.Projects.GetAll());
        }

        public int Add(ProjectDTO entity)
        {
            _logger.LogInformation($"Add project {entity.Name}");
            var project = _projectMapper.Map(entity);
            return _DataBase.Projects.Insert(project);
        }

        public void Edit(ProjectDTO entity)
        {
            _logger.LogInformation($"Editing of project. Id:{entity.Id}");
            var project = _projectMapper.Map(entity);
            _DataBase.Projects.Edit(project);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete project by id: {id}");

            try
            {
                _DataBase.Projects.Delete(id);
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
            return GetAll(_projectMapper, _DataBase.Projects.GetAllWithPaging(pageNumber));
        }

        public void ExportToXML()
        {
            var projectsDTO = GetAll(_projectMapper, _DataBase.Projects.GetAll());
            var projectsDataTable = ConvertToDataTable(projectsDTO);
            _logger.LogInformation($"Export {projectsDTO} to XML");

            try
            {
                WriteAndSaveXMLFile(projectsDataTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public void ExportToExcel()
        {
            var projectsDTO = GetAll(_projectMapper, _DataBase.Projects.GetAll());
            _logger.LogInformation($"Export {projectsDTO} to excel");

            try
            {
                ExportToExcel(projectsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }
    }
}
