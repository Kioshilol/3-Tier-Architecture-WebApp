using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Services;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using TrainingTask.Mapper;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private IService<EmployeeDTO> _employeeService;
        private IMapper<EmployeeDTO,EmployeeViewModel> _employeeMapper;
        public EmployeeController(IService<EmployeeDTO> employeeService, IMapper<EmployeeDTO, EmployeeViewModel> employeeMapper)
        {
            _employeeService = employeeService;
            _employeeMapper = employeeMapper;
        }
        [HttpGet()]
        public IActionResult Index(int page = 1)
        {
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
            if (id.HasValue)
            {
                var employeeDTO = _employeeService.GetById(id.Value);
                if(employeeDTO != null)
                {
                    var employeeDTOList = _employeeMapper.Map(employeeDTO);
                    return View(employeeDTOList);
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
            if(id != null) 
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
            if (id != null)
            {
                _employeeService.Delete(id.Value);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Details(int? id)
        {
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
    }
}