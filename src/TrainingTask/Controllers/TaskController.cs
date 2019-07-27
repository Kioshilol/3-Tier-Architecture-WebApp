using System;
using System.Collections.Generic;
using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.Mapper;
using TrainingTask.Mapping;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class TaskController : Controller
    {
        private ITaskService<TaskDTO> _taskService;
        private IService<ProjectDTO> _projectService;
        private IService<EmployeeDTO> _employeeService;
        private TaskMapper taskMapper;
        private ProjectMapper projectMapper;
        private EmployeeMapper employeeMapper;
        public TaskController(ITaskService<TaskDTO> taskService, IService<ProjectDTO> projectService, IService<EmployeeDTO> employeeService)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _taskService = taskService;
            taskMapper = new TaskMapper();
            projectMapper = new ProjectMapper();
            employeeMapper = new EmployeeMapper();
        }

        public IActionResult Index(int page = 1)
        {
            var projectsDTO = _projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();

            foreach (var project in projectsDTO)
            {
                var projectViewModel = projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);
            }

            var tasksViewModelPaging = new List<TaskViewModel>();
            var taskListPaging = _taskService.GetAllWithPaging(page);

            foreach (var task in taskListPaging)
            {
                var taskViewModel = taskMapper.Map(task);
                taskViewModel.Project = new ProjectViewModel();
                tasksViewModelPaging.Add(taskViewModel);
            }

            var tasksViewModel = new List<TaskViewModel>();
            var taskList = _taskService.GetAll();

            foreach(var task in taskList)
            {
                var taskViewModel = taskMapper.Map(task);
                tasksViewModel.Add(taskViewModel);
            }

            foreach (var project in projectsViewModel)
            {
                foreach(var task in tasksViewModelPaging)
                {
                    if(task.ProjectId == project.Id)
                    {
                        task.Project = project;
                    }
                }
            }

            var pageViewModel = new PageViewModel
            {
                PageNumber = page,
                RowsPerPage = PageSetting.GetRowsPerPage(),
                TotalRecords = tasksViewModel.Count,
                TotalPages = tasksViewModel.Count / PageSetting.GetRowsPerPage()
            };

            var indexViewModel = new IndexViewModel<TaskViewModel>
            {
                ViewModelList = tasksViewModelPaging,
                Page = pageViewModel
            };

            return View(indexViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var employeeDTO = _employeeService.GetAll();
            var employeeViewModel = new List<EmployeeViewModel>();

            foreach (var staff in employeeDTO)
            {
                var employeeViewM = employeeMapper.Map(staff);
                employeeViewModel.Add(employeeViewM);
            }

            ViewBag.employee = employeeViewModel;
            var projectsDTO = _projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();

            foreach (var project in projectsDTO)
            {
                var projectViewModel = projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);
            }

            SelectList projects = new SelectList(projectsViewModel, "Id", "Name");
            ViewBag.Projects = projects;

            var model = new TaskViewModel
            {
                DateOfEnd = DateTime.UtcNow
            };

            return View(model);
        }

        [HttpGet("projects/{projectid}/Create")]
        public IActionResult Create(int projectid)
        {
            var employeeDTO = _employeeService.GetAll();
            var employeeViewModel = new List<EmployeeViewModel>();

            foreach (var staff in employeeDTO)
            {
                var employeeViewM = employeeMapper.Map(staff);
                employeeViewModel.Add(employeeViewM);
            }

            ViewBag.Employee = employeeViewModel;

            var model = new TaskViewModel()
            {
                DateOfEnd = DateTime.UtcNow
            };

            var projectDto = _projectService.GetById(projectid);
            if (projectDto != null)
            {
                model.ProjectId = projectid;
                model.Project = projectMapper.Map(projectDto);

                return View(model);
            }
            return new BadRequestResult();
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel task, int[] selectedStaff)
        {
            var employeeDTO = _employeeService.GetAll();
            var employeeViewModel = new List<EmployeeViewModel>();

            foreach (var staff in employeeDTO)
            {
                var employeeViewM = employeeMapper.Map(staff);
                employeeViewModel.Add(employeeViewM);
            }

            var taskDTO = taskMapper.Map(task);
            taskDTO.StaffId = selectedStaff;
            _taskService.Add(taskDTO);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var projectsDTO = _projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();

            foreach (var project in projectsDTO)
            {
                var projectViewModel = projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);
            }

            SelectList projects = new SelectList(projectsViewModel, "Id", "Name");
            ViewBag.Projects = projects;
            var taskDTO = _taskService.GetById(id);
            taskDTO.DateOfEnd = DateTime.Now;
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }

        [HttpPost]
        public IActionResult Edit(TaskViewModel task)
        {
            var taskDTO = taskMapper.Map(task);
            _taskService.Edit(taskDTO);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var task = _taskService.GetById(id);
            var taskViewModel = taskMapper.Map(task);
            return View(taskViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _taskService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var taskDTO = _taskService.GetById(id);
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }
    }
}