using BLayer.DTO;
using BLayer.Interfaces;
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
    public class TaskService :BaseMapper, IService<TaskDTO>
    {
        private IUnitOfWork _dataBase { get; set; }
        private IMapper<Task, TaskDTO> _taskMapper;
        private IMapper<Employee, EmployeeDTO> _employeeMapper;
        private IMapper<Project, ProjectDTO> _projectMapper;
        private ILogger<TaskService> _logger;
        public TaskService(IUnitOfWork dataBase, IMapper<Task, TaskDTO> taskMapper,
            ILogger<TaskService> logger, IMapper<Employee, EmployeeDTO> employeeMapper,
            IMapper<Project, ProjectDTO> projectMapper)
        {
            _dataBase = dataBase;
            _taskMapper = taskMapper;
            _employeeMapper = employeeMapper;
            _logger = logger;
            _projectMapper = projectMapper;
        }
        public int Add(TaskDTO entity)
        {
            _logger.LogInformation($"{entity}");
            int id;

            try
            {
                entity.DateOfStart = DateTime.UtcNow;
                TimeSpan timeOfTask = entity.DateOfEnd - entity.DateOfStart;
                long timeOfTaskDays = timeOfTask.Days;

                if (timeOfTaskDays < 1)
                {
                    _logger.LogError($"{timeOfTaskDays} less than 1");
                    throw new Exception("Wrong Number(less than 1)");
                }

                entity.TaskTime = timeOfTaskDays;
                var task = _taskMapper.Map(entity);
                id = _dataBase.Tasks.Insert(task);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return id;
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete task by id: {id}");
            try
            {
                _dataBase.Tasks.Delete(id);
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

            try
            {
                var task = _taskMapper.Map(entity);
                _dataBase.Tasks.Edit(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public IEnumerable<TaskDTO> GetAllWithPaging(int pageNumber)
        {
            _logger.LogInformation($"Get all tasks by page number: {pageNumber}");
            IEnumerable<TaskDTO> tasksDTO;

            try
            {
                tasksDTO = Map(_taskMapper, _dataBase.Tasks.GetAllWithPaging(pageNumber));
                TaskInitialization(tasksDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return tasksDTO;
        }

        public TaskDTO GetById(int id)
        {
            _logger.LogInformation($"Get task by id:{id}");
            TaskDTO taskDTO;
            try
            {
                var task = _dataBase.Tasks.GetById(id);
                taskDTO = _taskMapper.Map(task);
                var tasksDTO = new List<TaskDTO>();
                tasksDTO.Add(taskDTO);
                TaskInitialization(tasksDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return taskDTO;
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            _logger.LogInformation($"Get all tasks");
            IEnumerable<TaskDTO> tasksDTO;

            try
            {
                tasksDTO = Map(_taskMapper, _dataBase.Tasks.GetAll());
                TaskInitialization(tasksDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return tasksDTO;
        }

        private IEnumerable<TaskDTO> TaskInitialization(IEnumerable<TaskDTO> tasksDTO)
        {
            foreach (var taskDTO in tasksDTO)
            {
                var employeesDTO = Map(_employeeMapper, _dataBase.Employees.GetEmployeesByTaskId(taskDTO.Id));

                foreach (var employee in employeesDTO)
                {
                    taskDTO.EmployeeTasks.Add(new EmployeeTasksDTO { Employee = employee });
                }
            }

            foreach(var taskDTO in tasksDTO)
            {
                var projectsDTO = Map(_projectMapper, _dataBase.Projects.GetAll());

                foreach(var projectDTO in projectsDTO)
                {
                    if(taskDTO.ProjectId == projectDTO.Id)
                        taskDTO.Project = projectDTO;
                }
            }
            return tasksDTO;
        }
    }
}
