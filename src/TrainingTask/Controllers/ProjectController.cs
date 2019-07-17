using System.Collections.Generic;
using System.Diagnostics;
using BLayer.DTO;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Mapping;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectService projectService;
        private ProjectMapper projectMapper;
        public ProjectController()
        {
            this.projectMapper = new ProjectMapper();
            this.projectService = new ProjectService();
        }

        public IActionResult Index()
        {
            var projectViewModelList = new List<ProjectViewModel>();
            IEnumerable<ProjectDTO> projectList = projectService.GetAll();
            foreach (var project in projectList)
            {
                var projectViewModel = this.projectMapper.Map(project);
                projectViewModelList.Add(projectViewModel);
            }
            return View(projectViewModelList);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectViewModel project)
        {
            var projectDTO = projectMapper.Map(project);
            projectService.Add(projectDTO);
            return RedirectToAction("Index");
        }

        public IActionResult EditProject(int id)
        {
            var projectDTO = projectService.GetById(id);
            var projectModelView = projectMapper.Map(projectDTO);
            return View(projectModelView);
        }

        [HttpPost]
        public IActionResult EditProject(ProjectViewModel project)
        {
            var projectDTO = projectMapper.Map(project);
            projectService.Edit(projectDTO);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProject(int id)
        {
            var projectDTO = projectService.GetById(id);
            var projectModelView = projectMapper.Map(projectDTO);
            return View(projectModelView);
        }

        [HttpPost, ActionName("DeleteProject")]
        public IActionResult DeleteConfirmed(int id)
        {
            projectService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult ShowProject(int id)
        {
            var projectDTO = projectService.GetById(id);
            var projectModelView = projectMapper.Map(projectDTO);
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
