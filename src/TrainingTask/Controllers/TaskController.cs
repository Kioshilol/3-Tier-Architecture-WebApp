using System.Collections.Generic;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Mapper;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class TaskController : Controller
    {
        private TaskService taskService;
        private TaskMapper taskMapper;
        public TaskController()
        {
            this.taskService = new TaskService();
            this.taskMapper = new TaskMapper();
        }
        public IActionResult Task()
        {
            var taskViewModelList = new List<TaskViewModel>();
            var taskList = taskService.GetAll();
            foreach (var task in taskList)
            {
                var taskViewModel = taskMapper.Map(task);
                taskViewModelList.Add(taskViewModel);
            }
            return View(taskViewModelList);
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(TaskViewModel task)
        {
            var taskDTO = taskMapper.Map(task);
            taskService.Add(taskDTO);
            return RedirectToAction("Task");
        }

        public IActionResult EditTask(int id)
        {
            var taskDTO = taskService.GetById(id);
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }

        [HttpPost]
        public IActionResult EditTask(TaskViewModel task)
        {
            var taskDTO = taskMapper.Map(task);
            taskService.Edit(taskDTO);
            return RedirectToAction("Staff");
        }

        public IActionResult DeleteStaff(int id)
        {
            var task = taskService.GetById(id);
            var taskViewModel = taskMapper.Map(task);
            return View(taskViewModel);
        }

        [HttpPost, ActionName("DeleteStaff")]
        public IActionResult DeleteConfirmed(int id)
        {
            taskService.Delete(id);
            return RedirectToAction("Staff");
        }

        public IActionResult ShowStaff(int id)
        {
            var taskDTO = taskService.GetById(id);
            var taskModelView = taskMapper.Map(taskDTO);
            return View(taskModelView);
        }
    }
}