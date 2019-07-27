using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mapper;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BLayer.Services
{
    public class TaskService :BaseService<Task,TaskDTO>, ITaskService<TaskDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Task, TaskDTO> taskMapper;

        public TaskService(IUnitOfWork dataBase)
        {
            _DataBase = dataBase;
            taskMapper = new TaskMapper();
        }
        public int Add(TaskDTO entity)
        {
            entity.DateOfStart = DateTime.UtcNow;
            TimeSpan timeOfTask = entity.DateOfEnd.Subtract(entity.DateOfStart);
            long timeOfTaskDays = timeOfTask.Days;
            if (timeOfTaskDays < 1)
                throw new Exception("Wrong Number(less than 1)");
            else
                entity.TaskTime = timeOfTaskDays;
            var task = taskMapper.Map(entity);
            return _DataBase.Task.Insert(task);
        }

        public void Delete(int id)
        {
            _DataBase.Task.Delete(id);
        }

        public void Edit(TaskDTO entity)
        {
            var task = taskMapper.Map(entity);
            _DataBase.Task.Edit(task);
        }

        public IEnumerable<TaskDTO> GetAllWithPaging(int pageNumber)
        {
            return GetPaging(taskMapper, _DataBase.Task.GetAllWithPaging(pageNumber));
        }

        public TaskDTO GetById(int id)
        {
            var task = _DataBase.Task.GetById(id);
            return taskMapper.Map(task);
        }

        public IEnumerable<TaskDTO> GetAllTasksByProjectId(int id)
        {
            return GetPaging(taskMapper, _DataBase.Task.GetAllTasksByProjectId(id));
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return GetPaging(taskMapper, _DataBase.Task.GetAll());
        }
    }
}
