using System;
using System.Collections.Generic;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingTask.Mapper;
using TrainingTask.Mapping;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class TaskController : Controller
    {
        private TaskService taskService;
        private TaskService taskServiceForStaff;
        private TaskMapper taskMapper;
        private ProjectMapper projectMapper;
        private ProjectService projectService;
        private StaffMapper staffMapper;
        private StaffService staffService;
        public TaskController()
        {
            this.taskService = new TaskService();
            this.taskServiceForStaff = new TaskService();
            this.taskMapper = new TaskMapper();
            this.projectService = new ProjectService();
            this.projectMapper = new ProjectMapper();
            this.staffService = new StaffService();
            this.staffMapper = new StaffMapper();
        }
        public IActionResult Task()
        {
            var projectsDTO = projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();
            foreach (var project in projectsDTO)
            {
                var projectViewModel = projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);
            }

            var tasksViewModel = new List<TaskViewModel>();
            var taskList = taskService.GetAll();
            foreach (var task in taskList)
            {
                var taskViewModel = taskMapper.Map(task);
                taskViewModel.Project = new ProjectViewModel();
                tasksViewModel.Add(taskViewModel);
            }
            
            foreach (var project in projectsViewModel)
            {
                foreach(var task in tasksViewModel)
                {
                    if(task.ProjectId == project.Id)
                    {
                        task.Project = project;
                    }
                }
            }
            return View(tasksViewModel);
        }
        [HttpGet("CreateTask")]
        public IActionResult CreateTask()
        {
            var staffDTO = staffService.GetAll();
            var staffViewModel = new List<StaffViewModel>();
            foreach(var staff in staffDTO)
            {
                var staffViewM = staffMapper.Map(staff);
                staffViewModel.Add(staffViewM);
            }
            ViewBag.Staff = staffViewModel;

            var projectsDTO = projectService.GetAll();
            var projectsViewModel = new List<ProjectViewModel>();
            foreach(var project in projectsDTO)
            {
                var projectViewModel = projectMapper.Map(project);
                projectsViewModel.Add(projectViewModel);
            }
            SelectList projects = new SelectList(projectsViewModel, "Id","Name");
            ViewBag.Projects = projects;
            var model = new TaskViewModel()
            {
                DateOfEnd = DateTime.Now,
            };

            return View(model);
        }

        [HttpGet("projects/{projectid}/CreateTask/")]
        public IActionResult CreateTask(int projectid)
        {
            var model = new TaskViewModel()
            {
                DateOfEnd = DateTime.Now
            };

            var projectDto = projectService.GetById(projectid);
            if (projectDto != null)
            {
                model.ProjectId = projectid;
                model.Project = projectMapper.Map(projectDto);

                return View(model);
            }

            return new BadRequestResult();
        }

        [HttpPost]
        public IActionResult CreateTask(TaskViewModel task,int[] selectedStaff)
        {
            var staffDTO = staffService.GetAll();
            var staffViewModel = new List<StaffViewModel>();
            foreach (var staff in staffDTO)
            {
                var staffViewM = staffMapper.Map(staff);
                staffViewModel.Add(staffViewM);
            }

            var taskDTO = taskMapper.Map(task);
            taskDTO.staffId = selectedStaff;
            taskService.Add(taskDTO);
            taskServiceForStaff.InsertStaff(taskDTO);
            return RedirectToAction("Task");
        }

        public IActionResult EditTask(int id)
        {
            var taskDTO = taskService.GetById(id);
            taskDTO.DateOfEnd = DateTime.Now;
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }

        [HttpPost]
        public IActionResult EditTask(TaskViewModel task)
        {
            var taskDTO = taskMapper.Map(task);
            taskService.Edit(taskDTO);
            return RedirectToAction("Task");
        }

        public IActionResult DeleteTask(int id)
        {
            var task = taskService.GetById(id);
            var taskViewModel = taskMapper.Map(task);
            return View(taskViewModel);
        }

        [HttpPost, ActionName("DeleteTask")]
        public IActionResult DeleteConfirmed(int id)
        {
            taskService.Delete(id);
            return RedirectToAction("Task");
        }

        public IActionResult ShowTask(int id)
        {
            var taskDTO = taskService.GetById(id);
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }
    }
}