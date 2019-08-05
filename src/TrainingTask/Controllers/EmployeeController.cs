using BLayer.DTO;
using BLayer.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private IService<EmployeeDTO> _employeeService;
        private IMapper<EmployeeDTO,EmployeeViewModel> _employeeMapper;
        private ILogger<EmployeeController> _logger;
        public EmployeeController(IService<EmployeeDTO> employeeService, IMapper<EmployeeDTO, EmployeeViewModel> employeeMapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _employeeMapper = employeeMapper;
            _logger = logger;
        }
        [HttpGet()]
        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation($"{page}");
            var employeeViewModelListPaging = new List<EmployeeViewModel>();
            var employeeListPaging = _employeeService.GetAllWithPaging(page);
            var employeeViewModelList = new List<EmployeeViewModel>();
            var employeeList = _employeeService.GetAll();

            foreach(var employee in employeeList)
            {
                var employeeDTO = _employeeMapper.Map(employee);
                employeeViewModelList.Add(employeeDTO);
            }

            foreach (var employee in employeeListPaging)
            {
                var employeeDTO = _employeeMapper.Map(employee);
                employeeViewModelListPaging.Add(employeeDTO);
            }

            var pageViewModel = new PageViewModel
            {
                PageNumber = page,
                RowsPerPage = PageSetting.GetRowsPerPage(),
                TotalRecords = employeeViewModelList.Count,
                TotalPages = employeeViewModelList.Count / PageSetting.GetRowsPerPage()
            };

            var indexViewModel = new IndexViewModel<EmployeeViewModel>
            {
                ViewModelList = employeeViewModelListPaging,
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
                _employeeService.ExportToXML();
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
                _employeeService.ExportToExcel();
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