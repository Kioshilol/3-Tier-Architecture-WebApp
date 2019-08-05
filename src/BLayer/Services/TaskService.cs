using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mappers;
using BLayer.Mappers;
using BLayer.Mappers.AutoMappers;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace BLayer.Services
{
    public class TaskService :BaseService<Task,TaskDTO>, ITaskService<TaskDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Task, TaskDTO> _taskMapper;
        private ILogger<TaskService> _logger;
        public TaskService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper, ILogger<TaskService> logger)
        {
            _DataBase = dataBase;
            _taskMapper = taskMapper;
            _logger = logger;
        }
        public int Add(TaskDTO entity)
        {
            _logger.LogInformation($"{entity}");
            entity.DateOfStart = DateTime.UtcNow;

            try
            {
                TimeSpan timeOfTask = entity.DateOfEnd - entity.DateOfStart;
                long timeOfTaskDays = timeOfTask.Days;

                if (timeOfTaskDays < 1)
                {
                    _logger.LogError($"{timeOfTaskDays} less than 1");
                    throw new Exception("Wrong Number(less than 1)");
                }
                entity.TaskTime = timeOfTaskDays;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
            
            var task = _taskMapper.Map(entity);
            return _DataBase.Task.Insert(task);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete task by id: {id}");
            try
            {
                _DataBase.Task.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public void Edit(TaskDTO entity)
        {
            _logger.LogInformation($"Editing of task. Id:{entity.Id}");
            var task = _taskMapper.Map(entity);
            _DataBase.Task.Edit(task);
        }

        public IEnumerable<TaskDTO> GetAllWithPaging(int pageNumber)
        {
            _logger.LogInformation($"Get all tasks by page number: {pageNumber}");
            return GetAll(_taskMapper, _DataBase.Task.GetAllWithPaging(pageNumber));
        }

        public TaskDTO GetById(int id)
        {
            _logger.LogInformation($"Get task by id:{id}");
            var task = _DataBase.Task.GetById(id);
            return _taskMapper.Map(task);
        }

        public IEnumerable<TaskDTO> GetAllTasksByProjectId(int id)
        {
            _logger.LogInformation($"Get all tasks by project id: {id}");
            return GetAll(_taskMapper, _DataBase.Task.GetAllTasksByProjectId(id));
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            _logger.LogInformation($"Get all tasks");
            return GetAll(_taskMapper, _DataBase.Task.GetAll());
        }

        public void ExportToXML()
        {
            var tasksDTO = GetAll(_taskMapper, _DataBase.Task.GetAll());
            _logger.LogInformation($"Export {tasksDTO} to XML");

            foreach (var item in tasksDTO)
            {
                item.EmployeeTasks = null;
            }

            var tasksDataTable = ConvertToDataTable(tasksDTO);

            try
            {
                WriteAndSaveXMLFile(tasksDataTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public void ExportToExcel()
        {
            var tasksDTO = GetAll(_taskMapper, _DataBase.Task.GetAll());
            _logger.LogInformation($"Export {tasksDTO} to excel");

            try
            {
                ExportToExcel(tasksDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }
    }
}
