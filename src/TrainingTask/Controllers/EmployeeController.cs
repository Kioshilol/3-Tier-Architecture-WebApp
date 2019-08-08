using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private IService<EmployeeDTO> _employeeService;
        private IMapper<EmployeeDTO,EmployeeViewModel> _employeeMapper;
        private ILogger<EmployeeController> _logger;
        private IExportToXML<EmployeeDTO> _exportToXML;
        private IExportToExcel<EmployeeDTO> _exportToExcel;
        public EmployeeController(IService<EmployeeDTO> employeeService, IMapper<EmployeeDTO, EmployeeViewModel> employeeMapper,
            ILogger<EmployeeController> logger, IExportToXML<EmployeeDTO> exportToXML, IExportToExcel<EmployeeDTO> exportToExcel)
        {
            _employeeService = employeeService;
            _employeeMapper = employeeMapper;
            _logger = logger;
            _exportToXML = exportToXML;
            _exportToExcel = exportToExcel;
        }
        [HttpGet()]
        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation($"{page}");
            IndexViewModel<EmployeeViewModel> indexViewModel;

            try
            {
                var employeesVM = new List<EmployeeViewModel>();
                var employeesDTO = _employeeService.GetAllWithPaging(page);
                var allEmployeeVM = new List<EmployeeViewModel>();
                var allEmployeesDTO = _employeeService.GetAll();

                foreach (var employeeDTO in allEmployeesDTO)
                {
                    var employeeVM = _employeeMapper.Map(employeeDTO);
                    allEmployeeVM.Add(employeeVM);
                }

                foreach (var employee in employeesDTO)
                {
                    var employeeVM = _employeeMapper.Map(employee);
                    employeesVM.Add(employeeVM);
                }

                var pageViewModel = new PageViewModel
                {
                    PageNumber = page,
                    RowsPerPage = PageSetting.GetRowsPerPage(),
                    TotalRecords = allEmployeeVM.Count,
                    TotalPages = PageSetting.GetTotalPages(allEmployeeVM)
                };

                indexViewModel = new IndexViewModel<EmployeeViewModel>
                {
                    ViewModelList = employeesVM,
                    Page = pageViewModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return View(indexViewModel);
        }

        public IActionResult Edit(int? id)
        {
            _logger.LogInformation($"{id}");
            if (id.HasValue)
            {
                try
                {
                    var employeeDTO = _employeeService.GetById(id.Value);

                    if (employeeDTO != null)
                    {
                        var employeeViewModel = _employeeMapper.Map(employeeDTO);
                        return View(employeeViewModel);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    throw;
                }

                return NotFound();
            }
            else
            {
                return View(new EmployeeViewModel());
            }
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employee)
        {
            _logger.LogInformation($"{employee}");

            if (employee.Id.HasValue)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var employeeDTO = _employeeMapper.Map(employee);
                        _employeeService.Edit(employeeDTO);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, "Stopped program because of exception");
                        throw;
                    }
                }
                else
                    return View(employee);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var employeeDTO = _employeeMapper.Map(employee);
                        _employeeService.Add(employeeDTO);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, "Stopped program because of exception");
                        throw;
                    }
                }
                else
                    return View(employee);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id != null) 
            {
                try
                {
                    var employeeDTO = _employeeService.GetById(id.Value);

                    if (employeeDTO != null)
                    {
                        var employeeViewModel = _employeeMapper.Map(employeeDTO);
                        return View(employeeViewModel);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    throw;
                }
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id != null)
            {
                try
                {
                    _employeeService.Delete(id.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    throw;
                }

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public IActionResult Details(int? id)
        {
            _logger.LogInformation($"{id}");

            if (id != null)
            {
                try
                {
                    var employeeDTO = _employeeService.GetById(id.Value);

                    if (employeeDTO != null)
                    {
                        var employeeViewModel = _employeeMapper.Map(employeeDTO);
                        return View(employeeViewModel);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Stopped program because of exception");
                    throw;
                }
            }

            return NotFound();
        }

        public IActionResult UploadToXML()
        {
            MemoryStream memoryStream;

            try
            {
                var employeesDTO = _employeeService.GetAll();
                memoryStream = _exportToXML.Export(employeesDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return File(memoryStream.ToArray(), "application/xml", "Projects.xml");
        }

        public IActionResult UploadToExcel()
        {
            IEnumerable<EmployeeDTO> employeesDTO;

            try
            {
                employeesDTO = _employeeService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }

            return File(_exportToExcel.Export(employeesDTO).ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @"Employees.xlsx");
        }


    }
}