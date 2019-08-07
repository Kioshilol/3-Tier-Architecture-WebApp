using BLayer.DTO;
using BLayer.Interfaces;
using Core;
using Core.Interfaces;
using DLayer.Entities;
using DLayer.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BLayer.Services
{
    public class EmployeeService :BaseMapper, IService<EmployeeDTO>
    {
        private IUnitOfWork _dataBase { get; set; }
        private IMapper<Employee,EmployeeDTO> _employeeMapper;
        private IMapper<Task, TaskDTO> _taskMapper;
        private ILogger<EmployeeService> _logger;
        public EmployeeService(IUnitOfWork dataBase, IMapper<Employee, EmployeeDTO> employeeMapper,
            ILogger<EmployeeService> logger, IMapper<Task, TaskDTO> taskMapper)
        {
            _dataBase = dataBase;
            _employeeMapper = employeeMapper;
            _logger = logger;
            _taskMapper = taskMapper;
        }

        public int Add(EmployeeDTO entity)
        {
            _logger.LogInformation($" Add entity. Name: {entity.Name}");
            int id;

            try
            {
                var employee = SaveImage(entity);
                id = _dataBase.Employees.Insert(employee);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }

            return id;
        }

        public void Edit(EmployeeDTO entity)
        {
            _logger.LogInformation($"Editing of project. Id:{entity.Id}");

            try
            {
                var employee = SaveImage(entity);
                _dataBase.Employees.Edit(employee);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public IEnumerable<EmployeeDTO> GetAllWithPaging(int pageNumber)
        {
            _logger.LogInformation($"Get employees by page number {pageNumber}");
            IEnumerable<EmployeeDTO> employeesDTO;

            try
            {
                employeesDTO = Map(_employeeMapper, _dataBase.Employees.GetAllWithPaging(pageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }

            return employeesDTO;
        }

        public EmployeeDTO GetById(int id)
        {
            _logger.LogInformation($"Get employee by id {id}");
            EmployeeDTO employeeDTO;

            try
            {
                var employee = _dataBase.Employees.GetById(id);
                employeeDTO = _employeeMapper.Map(employee);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }

            return employeeDTO;
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete employee by id {id}");

            try
            {
                _dataBase.Employees.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            _logger.LogInformation("Get all employees");
            IEnumerable<EmployeeDTO> employeesDTO;

            try
            {
                employeesDTO = Map(_employeeMapper, _dataBase.Employees.GetAll());
                EmployeeInitialization(employeesDTO);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, "Stopped program because of exception");
                throw;
            }

            return employeesDTO;
        }

        private Employee SaveImage(EmployeeDTO employeeDTO)
        {
            _logger.LogInformation($"Save image of employee. Name: {employeeDTO.Name}");

            if (employeeDTO.UploadedFile != null)
            {
                string filePath = AppSetting.GetFullPathToPictures() + employeeDTO.UploadedFile.FileName;

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employeeDTO.UploadedFile.CopyTo(stream);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    throw;
                }

                employeeDTO.FilePath = AppSetting.GetPicturesFilePath() + employeeDTO.UploadedFile.FileName;
            }
            else
            {
                if (employeeDTO.FilePath == null)
                    employeeDTO.FilePath = AppSetting.SetDefaultAvatar();
            }

            var employee = _employeeMapper.Map(employeeDTO);
            return employee;
        }

        private IEnumerable<EmployeeDTO> EmployeeInitialization(IEnumerable<EmployeeDTO> employeesDTO)
        {
            foreach (var employeeDTO in employeesDTO)
            {
                var tasksDTO = Map(_taskMapper, _dataBase.Tasks.GetTasksByEmployeeId(employeeDTO.Id.Value));
                foreach (var taskDTO in tasksDTO)
                {
                    var list = new List<TaskDTO>();
                    employeeDTO.EmployeeTasks.Add(new EmployeeTasksDTO { Task = taskDTO });
                }
            }
            return employeesDTO;
        }
    }
}
