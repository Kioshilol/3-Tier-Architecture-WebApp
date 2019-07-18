using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using DLayer;
using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Services
{
    public class TaskService : IService<TaskDTO>
    {
        private IUnitOfWork DataBase { get; set; }
        private IBaseMapper<Task, TaskDTO> taskMapper;
        public TaskService()
        {
            DataBase = new UnitOfWork();
            taskMapper = new TaskMapper();
        }
        public void Add(TaskDTO entity)
        {
            entity.DateOfStart = DateTime.Now;
            TimeSpan timeOfTask = entity.DateOfEnd.Subtract(entity.DateOfStart);
            long timeOfTaskDays = timeOfTask.Days;
            if (timeOfTaskDays < 1)
                throw new Exception("Wrong Number(less than 1)");
            else
                entity.TaskTime = timeOfTaskDays;
            var task = taskMapper.Map(entity);
            DataBase.Task.Insert(task);
        }

        public void Delete(int id)
        {
            DataBase.Task.Delete(id);
        }

        public void Edit(TaskDTO entity)
        {
            var task = taskMapper.Map(entity);
            DataBase.Task.Edit(task);
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            IEnumerable<Task> taskList = DataBase.Task.GetAll();
            var taskDTOList = new List<TaskDTO>();
            foreach (var tasks in taskList)
            {
                var taskDTO = this.taskMapper.Map(tasks);
                taskDTOList.Add(taskDTO);
            }
            return taskDTOList;
        }

        public TaskDTO GetById(int id)
        {
            var task = DataBase.Task.GetById(id);
            var taskDTO = taskMapper.Map(task);
            return taskDTO;
        }
    }
}
