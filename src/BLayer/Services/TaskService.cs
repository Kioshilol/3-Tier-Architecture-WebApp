using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Mappers;
using BLayer.Mappers;
using BLayer.Mappers.AutoMappers;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
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

        public TaskService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper)
        {
            _DataBase = dataBase;
            _taskMapper = taskMapper;
        }
        public int Add(TaskDTO entity)
        {
            entity.DateOfStart = DateTime.UtcNow;
            TimeSpan timeOfTask = entity.DateOfEnd - entity.DateOfStart;
            long timeOfTaskDays = timeOfTask.Days;
            if (timeOfTaskDays < 1)
                throw new Exception("Wrong Number(less than 1)");
            else
                entity.TaskTime = timeOfTaskDays;
            var task = _taskMapper.Map(entity);
            return _DataBase.Task.Insert(task);
        }

        public void Delete(int id)
        {
            _DataBase.Task.Delete(id);
        }

        public void Edit(TaskDTO entity)
        {
            var task = _taskMapper.Map(entity);
            _DataBase.Task.Edit(task);
        }

        public IEnumerable<TaskDTO> GetAllWithPaging(int pageNumber)
        {
            return GetAll(_taskMapper, _DataBase.Task.GetAllWithPaging(pageNumber));
        }

        public TaskDTO GetById(int id)
        {
            var task = _DataBase.Task.GetById(id);
            return _taskMapper.Map(task);
        }

        public IEnumerable<TaskDTO> GetAllTasksByProjectId(int id)
        {
            return GetAll(_taskMapper, _DataBase.Task.GetAllTasksByProjectId(id));
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return GetAll(_taskMapper, _DataBase.Task.GetAll());
        }

        public void UploadToXML()
        {
            var tasksDTO = GetAll(_taskMapper, _DataBase.Task.GetAll());

            foreach (var item in tasksDTO)
            {
                item.EmployeeTasks = null;
            }

            var tasksDataTable = ConvertToDataTable(tasksDTO);
            WriteAndSaveXMLFile(tasksDataTable);
        }

        public void UploadToExcel()
        {
            var tasksDTO = GetAll(_taskMapper, _DataBase.Task.GetAll());
            ExportToExcel(tasksDTO);
        }
    }
}
