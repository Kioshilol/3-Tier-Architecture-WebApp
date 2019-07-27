using System.Collections.Generic;
using System.Diagnostics;
using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Mapper;
using TrainingTask.Mapping;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private IService<ProjectDTO> _projectService;
        private ITaskService<TaskDTO> _taskService;
        private ProjectMapper projectMapper;
        private TaskMapper taskMapper;
        public ProjectController(IService<ProjectDTO> projectService, ITaskService<TaskDTO> taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
            projectMapper = new ProjectMapper();
            taskMapper = new TaskMapper();
        }
        public IActionResult Index(int page = 1)
        {
            var projectViewModelList = new List<ProjectViewModel>();
            IEnumerable<ProjectDTO> projectPagingList = _projectService.GetAllWithPaging(page);
            var allProjects = new List<ProjectViewModel>();
            IEnumerable<ProjectDTO> projectList = _projectService.GetAll();

            foreach (var project in projectList)
            {
                var projectViewModel = this.projectMapper.Map(project);
                allProjects.Add(projectViewModel);
            }

            foreach (var project in projectPagingList)
            {
                var projectViewModel = this.projectMapper.Map(project);
                projectViewModelList.Add(projectViewModel);
            }

            var pageViewModel = new PageViewModel
            {
                PageNumber = page,
                RowsPerPage = PageSetting.GetRowsPerPage(),
                TotalRecords = allProjects.Count,
                TotalPages = allProjects.Count / PageSetting.GetRowsPerPage()
            };

            var indexViewModel = new IndexViewModel<ProjectViewModel>
            {
                ViewModelList = projectViewModelList,
                Page = pageViewModel
            };

            return View(indexViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var projectDTO = _projectService.GetById(id.Value);
                var projectModelView = projectMapper.Map(projectDTO);
                return View(projectModelView);
            }
            else
            {
                return View(new ProjectViewModel());
            }
        }

        [HttpPost]
        public IActionResult Edit(ProjectViewModel project)
        {
            if (project.Id.HasValue)
            {
                var projectDTO = projectMapper.Map(project);
                _projectService.Edit(projectDTO);
            }
            else
            {
                var projectDTO = projectMapper.Map(project);
                _projectService.Add(projectDTO);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var projectDTO = _projectService.GetById(id);
            var projectModelView = projectMapper.Map(projectDTO);
            return View(projectModelView);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("projects/{id}")]
        public IActionResult Details(int id)
        {
            var projectDTO = _projectService.GetById(id);
            var projectModelView = projectMapper.Map(projectDTO);
            var tasksDTO = _taskService.GetAllTasksByProjectId(id);
            projectModelView.Tasks = new List<TaskViewModel>();

            foreach(var task in tasksDTO)
            {
                var taskViewModel = taskMapper.Map(task);
                projectModelView.Tasks.Add(taskViewModel);
            }
            TempData["ProjectId"] = projectModelView.Id;
            return View(projectModelView);
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
