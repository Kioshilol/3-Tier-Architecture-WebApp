using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AutoMapper;
using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using TrainingTask.Mapper;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private IService<ProjectDTO> _projectService;
        private IService<TaskDTO> _taskService;
        private IMapper<ProjectDTO, ProjectViewModel> _projectMapper;
        private IMapper<TaskDTO, TaskViewModel> _taskMapper;
        private ILogger<ProjectController> _logger;
        private IExportToXML<ProjectDTO> _exportToXML;
        private IExportToExcel<ProjectDTO> _exportToExcel;
        private IHostingEnvironment _hostingEnvironment;
        public ProjectController(IService<ProjectDTO> projectService, IService<TaskDTO> taskService,
            IMapper<ProjectDTO, ProjectViewModel> projectMapper, IMapper<TaskDTO, TaskViewModel> taskMapper,
            ILogger<ProjectController> logger, IExportToXML<ProjectDTO> exportToXML,
            IExportToExcel<ProjectDTO> exportToExcel, IHostingEnvironment hostingEnvironment)
        {
            _projectService = projectService;
            _taskService = taskService;
            _projectMapper = projectMapper;
            _taskMapper = taskMapper;
            _logger = logger;
            _exportToXML = exportToXML;
            _exportToExcel = exportToExcel;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation($"{page}");
            var allProjectsVM = new List<ProjectViewModel>();
            var allProjects = _projectService.GetAll();

            foreach (var project in allProjects)
            {
                var projectViewModel = _projectMapper.Map(project);
                allProjectsVM.Add(projectViewModel);
            }

            var projects = _projectService.GetAllWithPaging(page);
            var projectsVM = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                var projectViewModel = _projectMapper.Map(project);
                projectsVM.Add(projectViewModel);
            }

            var pageViewModel = new PageViewModel
            {
                PageNumber = page,
                RowsPerPage = PageSetting.GetRowsPerPage(),
                TotalRecords = allProjectsVM.Count,
                TotalPages = PageSetting.GetTotalPages(allProjectsVM)
            };

            var indexViewModel = new IndexViewModel<ProjectViewModel>
            {
                ViewModelList = projectsVM,
                Page = pageViewModel
            };

            return View(indexViewModel);
        }

        public IActionResult Edit(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id.HasValue)
            {
                var projectDTO = _projectService.GetById(id.Value);
                if(projectDTO != null)
                {
                    var projectModelView = _projectMapper.Map(projectDTO);
                    return View(projectModelView);
                }
                return NotFound();
            }

            else
            {
                return View(new ProjectViewModel());
            }
        }

        [HttpPost]
        public IActionResult Edit(ProjectViewModel project)
        {
            _logger.LogInformation($"{project}");

            if (project.Id.HasValue)
            {
                if (ModelState.IsValid)
                {
                    var projectDTO = _projectMapper.Map(project);
                    _projectService.Edit(projectDTO);
                }
                else
                    return View(project);
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var projectDTO = _projectMapper.Map(project);
                    _projectService.Add(projectDTO);
                }
                else
                    return View(project);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id != null)
            {
                var projectDTO = _projectService.GetById(id.Value);
                if (projectDTO != null)
                {
                    var projectModelView = _projectMapper.Map(projectDTO);
                    return View(projectModelView);
                }
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation($"{id}");

            try
            {
                _projectService.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception ");
                throw;
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id != null)
            {
                var projectDTO = _projectService.GetById(id.Value);
                var projectModelView = _projectMapper.Map(projectDTO);
                return View(projectModelView);
            }
            return NotFound();
        }

        public IActionResult UploadToXML()
        {
            MemoryStream memoryStream;

            try
            {
                var projectsDTO =  _projectService.GetAll();
                memoryStream = _exportToXML.Export(projectsDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception ");
                throw;
            }

            return File(memoryStream.ToArray(), "application/xml", "Projects.xml");
        }

        public  IActionResult UploadToExcel()
        {
            IEnumerable<ProjectDTO> projectsDTO;
            try
            {
                projectsDTO = _projectService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception ");
                throw;
            }

            return File(_exportToExcel.Export(projectsDTO).ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @"Projects.xlsx");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
