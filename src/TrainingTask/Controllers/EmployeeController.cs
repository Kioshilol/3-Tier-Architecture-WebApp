using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private IService<EmployeeDTO> _employeeService;
        private IMapper<EmployeeDTO,EmployeeViewModel> _employeeMapper;
        private ILogger<EmployeeController> _logger;
        private IExportToXML<EmployeeViewModel> _exportToXML;
        public EmployeeController(IService<EmployeeDTO> employeeService, IMapper<EmployeeDTO, EmployeeViewModel> employeeMapper,
            ILogger<EmployeeController> logger, IExportToXML<EmployeeViewModel> exportToXML)
        {
            _employeeService = employeeService;
            _employeeMapper = employeeMapper;
            _logger = logger;
            _exportToXML = exportToXML;
        }
        [HttpGet()]
        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation($"{page}");
            var employeesVM = new List<EmployeeViewModel>();
            var employeesDTO = _employeeService.GetAllWithPaging(page);
            var allEmployeeVM = new List<EmployeeViewModel>();
            var allEmployeesDTO = _employeeService.GetAll();

            foreach(var employeeDTO in allEmployeesDTO)
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

            var indexViewModel = new IndexViewModel<EmployeeViewModel>
            {
                ViewModelList = employeesVM,
                Page = pageViewModel
            };

            return View(indexViewModel);
        }

        public IActionResult Edit(int? id)
        {
            _logger.LogInformation($"{id}");
            if (id.HasValue)
            {
                var employeeDTO = _employeeService.GetById(id.Value);
                if(employeeDTO != null)
                {
                    var employeeViewModel = _employeeMapper.Map(employeeDTO);
                    return View(employeeViewModel);
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
                    var employeeDTO = _employeeMapper.Map(employee);
                    _employeeService.Edit(employeeDTO);
                }
                else
                    return View(employee);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var employeeDTO = _employeeMapper.Map(employee);
                    _employeeService.Add(employeeDTO);
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
                var employeeDTO = _employeeService.GetById(id.Value);
                if (employeeDTO != null)
                {
                    var employeeViewModel = _employeeMapper.Map(employeeDTO);
                    return View(employeeViewModel);
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
                _employeeService.Delete(id.Value);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Details(int? id)
        {
            _logger.LogInformation($"{id}");
            if (id != null)
            {
                var employeeDTO = _employeeService.GetById(id.Value);
                if (employeeDTO != null)
                {

                    var employeeViewModel = _employeeMapper.Map(employeeDTO);
                    return View(employeeViewModel);
                }
            }
            return NotFound();
        }

        public IActionResult UploadToXML()
        {
            try
            {
                var employeesDTO = _employeeService.GetAll();
                var eployeesVM = new List<EmployeeViewModel>();

                foreach (var employeeDTO in employeesDTO)
                {
                    var employeeVM = _employeeMapper.Map(employeeDTO);
                    eployeesVM.Add(employeeVM);
                }

                _exportToXML.ExportToXML(eployeesVM);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
            return RedirectToAction("Index");
        }

        public IActionResult UploadToExcel()
        {
            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped program because of exception");
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}