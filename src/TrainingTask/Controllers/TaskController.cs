using System;
using System.Collections.Generic;
using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.Mapper;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class TaskController : Controller
    {
        private ITaskService<TaskDTO> _taskService;
        private IService<ProjectDTO> _projectService;
        private IService<EmployeeDTO> _employeeService;
        private IMapper<TaskDTO, TaskViewModel> _taskMapper;
        private IMapper<ProjectDTO, ProjectViewModel> _projectMapper;
        private IMapper<EmployeeDTO, EmployeeViewModel> _employeeMapper;
        public TaskController(ITaskService<TaskDTO> taskService, IService<ProjectDTO> projectService,
            IService<EmployeeDTO> employeeService, IMapper<ProjectDTO, ProjectViewModel> projectMapper, IMapper<EmployeeDTO, EmployeeViewModel> employeeMapper,
            IMapper<TaskDTO, TaskViewModel> taskMapper)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _taskService = taskService;
            _taskMapper = taskMapper;
            _projectMapper = projectMapper;
            _employeeMapper = employeeMapper;
        }

        public IActionResult Index(int page = 1)
        {
            var tasksViewModelPaging = new List<TaskViewModel>();
            var taskListPaging = _taskService.GetAllWithPaging(page);

            foreach (var task in taskListPaging)
            {
                var taskViewModel = _taskMapper.Map(task);
                taskViewModel.Project = new ProjectViewModel();
                tasksViewModelPaging.Add(taskViewModel);
            }

            var tasksViewModel = new List<TaskViewModel>();
            var taskList = _taskService.GetAll();

            foreach(var task in taskList)
            {
                var taskViewModel = _taskMapper.Map(task);
                tasksViewModel.Add(taskViewModel);
            }

            var projectsDTO = _projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();

            foreach (var project in projectsDTO)
            {
                var projectViewModel = _projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);

                foreach (var task in tasksViewModelPaging)
                {
                    if(task.ProjectId == projectViewModel.Id)
                    {
                        task.Project = projectViewModel;
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

            foreach (var employee in employeeDTO)
            {
                var employeeViewM = _employeeMapper.Map(employee);
                employeeViewModel.Add(employeeViewM);
            }

            ViewBag.employee = employeeViewModel;
            var projectsDTO = _projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();

            foreach (var project in projectsDTO)
            {
                var projectViewModel = _projectMapper.Map(project);
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

            foreach (var employee in employeeDTO)
            {
                var employeeViewM = _employeeMapper.Map(employee);
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
                model.Project = _projectMapper.Map(projectDto);

                return View(model);
            }
            return new BadRequestResult();
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel task, int[] selectedEmployee)
        {
            var employeeDTO = _employeeService.GetAll();
            var employeeViewModel = new List<EmployeeViewModel>();

            foreach (var employee in employeeDTO)
            {
                var employeeViewM = _employeeMapper.Map(employee);
                employeeViewModel.Add(employeeViewM);
            }

            if (ModelState.IsValid)
            {
                var taskDTO = _taskMapper.Map(task);
                taskDTO.EmployeeId = selectedEmployee;
                _taskService.Add(taskDTO);
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.employee = employeeViewModel;
                return View(task);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var projectsDTO = _projectService.GetAll();
                var projectsViewModel = new List<ProjectViewModel>();

                foreach (var project in projectsDTO)
                {
                    var projectViewModel = _projectMapper.Map(project);
                    projectsViewModel.Add(projectViewModel);
                }

                SelectList projects = new SelectList(projectsViewModel, "Id", "Name");
                ViewBag.Projects = projects;
                var taskDTO = _taskService.GetById(id.Value);
                taskDTO.DateOfEnd = DateTime.Now;
                var taskModelView = _taskMapper.Map(taskDTO);
                return View(taskModelView);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskDTO = _taskMapper.Map(task);
                _taskService.Edit(taskDTO);
                return RedirectToAction("Index");
            }
            else
                return View(task);
        }

        public IActionResult Delete(int? id)
        {
            if(id != null)
            {
                var taskDTO = _taskService.GetById(id.Value);
                if(taskDTO != null)
                {
                    var taskViewModel = _taskMapper.Map(taskDTO);
                    return View(taskViewModel);
                }
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _taskService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if(id != null)
            {
                var taskDTO = _taskService.GetById(id.Value);
                if(taskDTO != null)
                {
                    var taskModelView = _taskMapper.Map(taskDTO);
                    return View(taskModelView);
                }
            }

            return NotFound();
        }
    }
}