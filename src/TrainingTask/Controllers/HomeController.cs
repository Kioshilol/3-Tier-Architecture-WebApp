using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Models;

namespace TrainingTask.Controllers
{
    public class HomeController : Controller
    {
        private Project project;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Task()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
