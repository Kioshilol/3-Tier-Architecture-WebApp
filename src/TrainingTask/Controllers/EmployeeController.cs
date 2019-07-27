using BLayer.DTO;
using BLayer.Interfaces;
using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainingTask.Mapper;
using TrainingTask.Models;
using TrainingTask.ViewModels;

namespace TrainingTask.Controllers
{
    public class EmployeeController : Controller
    {
        private IService<EmployeeDTO> _employeeService;
        private EmployeeMapper _employeeMapper;
        public EmployeeController(IService<EmployeeDTO> employeeService)
        {
            _employeeService = employeeService;
            _employeeMapper = new EmployeeMapper();
        }
        [HttpGet()]
        public IActionResult Index(int page = 1)
        {
            var staffViewModelListPaging = new List<EmployeeViewModel>();
            var staffListPaging = _employeeService.GetAllWithPaging(page);
            var staffViewModelList = new List<EmployeeViewModel>();
            var staffList = _employeeService.GetAll();

            foreach(var employee in staffList)
            {
                var employeeDTO = _employeeMapper.Map(employee);
                staffViewModelList.Add(employeeDTO);
            }

            foreach (var employee in staffListPaging)
            {
                var employeeDTO = _employeeMapper.Map(employee);
                staffViewModelListPaging.Add(employeeDTO);
            }

            var pageViewModel = new PageViewModel
            {
                PageNumber = page,
                RowsPerPage = PageSetting.GetRowsPerPage(),
                TotalRecords = staffViewModelList.Count,
                TotalPages = staffViewModelList.Count / PageSetting.GetRowsPerPage()
            };

            var indexViewModel = new IndexViewModel<EmployeeViewModel>
            {
                ViewModelList = staffViewModelListPaging,
                Page = pageViewModel
            };

            return View(indexViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var employeeDTO = _employeeService.GetById(id.Value);
                var staffModelView = _employeeMapper.Map(employeeDTO);
                return View(staffModelView);
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
                var staffDTO = _employeeMapper.Map(employee);
                _employeeService.Edit(staffDTO);
            }
            else
            {
                var staffDTO = _employeeMapper.Map(employee);
                _employeeService.Add(staffDTO);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            var StaffViewModel = _employeeMapper.Map(employee);
            return View(StaffViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var staffDTO = _employeeService.GetById(id);
            var staffModelView = _employeeMapper.Map(staffDTO);
            return View(staffModelView);
        }
    }
}