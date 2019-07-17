using BLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainingTask.Mapper;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class StaffController : Controller
    {
        private StaffService staffService;
        private StaffMapper staffMapper;
        public StaffController()
        {
            this.staffService = new StaffService();
            this.staffMapper = new StaffMapper();
        }
        public IActionResult Staff()
        {
            var staffViewModelList = new List<StaffViewModel>();
            var staffList = staffService.GetAll();
            foreach(var staff in staffList)
            {
                var staffViewModel = staffMapper.Map(staff);
                staffViewModelList.Add(staffViewModel);
            }
            return View(staffViewModelList);
        }
        public IActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStaff(StaffViewModel staff)
        {
            var staffDTO = staffMapper.Map(staff);
            staffService.Add(staffDTO);
            return RedirectToAction("Staff");
        }

        public IActionResult EditStaff(int id)
        {
            var staffDTO = staffService.GetById(id);
            var staffModelView = staffMapper.Map(staffDTO);
            return View(staffModelView);
        }

        [HttpPost]
        public IActionResult EditStaff(StaffViewModel staff)
        {
            var staffDTO = staffMapper.Map(staff);
            staffService.Edit(staffDTO);
            return RedirectToAction("Staff");
        }

        public IActionResult DeleteStaff(int id)
        {
            var staff = staffService.GetById(id);
            var StaffViewModel = staffMapper.Map(staff);
            return View(StaffViewModel);
        }

        [HttpPost, ActionName("DeleteStaff")]
        public IActionResult DeleteConfirmed(int id)
        {
            staffService.Delete(id);
            return RedirectToAction("Staff");
        }

        public IActionResult ShowStaff(int id)
        {
            var staffDTO = staffService.GetById(id);
            var staffModelView = staffMapper.Map(staffDTO);
            return View(staffModelView);
        }
    }
}