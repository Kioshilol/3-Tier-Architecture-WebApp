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

namespace BLayer.Services
{
    public class EmployeeService :BaseService<Employee,EmployeeDTO>, IService<EmployeeDTO>
    {
        private IUnitOfWork _DataBase { get; set; }
        private IMapper<Employee,EmployeeDTO> _employeeMapper;
        private ILogger<EmployeeService> _logger;
        public EmployeeService(IUnitOfWork dataBase, IMapper<Employee, EmployeeDTO> employeeMapper, ILogger<EmployeeService> logger)
        {
            _DataBase = dataBase;
            _employeeMapper = employeeMapper;
            _logger = logger;
        }

        public int Add(EmployeeDTO entity)
        {
            _logger.LogInformation($" Add entity. Name: {entity.Name}");
            var employee = SaveImage(entity);
            return _DataBase.Employee.Insert(employee);
        }

        public void Edit(EmployeeDTO entity)
        {
            _logger.LogInformation($"Editing of project. Id:{entity.Id}");
            var employee = SaveImage(entity);

            try
            {
                _DataBase.Employee.Edit(employee);
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
            return GetAll(_employeeMapper, _DataBase.Employee.GetAllWithPaging(pageNumber));
        }

        public EmployeeDTO GetById(int id)
        {
            _logger.LogInformation($"Get employee by id {id}");
            var employee = _DataBase.Employee.GetById(id);
            return _employeeMapper.Map(employee);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Delete employee by id {id}");
            try
            {
                _DataBase.Employee.Delete(id);
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
            return GetAll(_employeeMapper, _DataBase.Employee.GetAll());
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

        public void ExportToXML()
        {
            var employeesDTO = GetAll(_employeeMapper, _DataBase.Employee.GetAll());
            _logger.LogInformation($"Export {employeesDTO} to XML");

            foreach (var item in employeesDTO)
            {
                item.EmployeeTasks = null;
            }

            var employeesDataTable = ConvertToDataTable(employeesDTO);

            try
            {
                WriteAndSaveXMLFile(employeesDataTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }

        public void ExportToExcel()
        {
            var employeesDTO = GetAll(_employeeMapper, _DataBase.Employee.GetAll());
            _logger.LogInformation($"Export {employeesDTO} to excel");

            try
            {
                ExportToExcel(employeesDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
        }
    }
}
