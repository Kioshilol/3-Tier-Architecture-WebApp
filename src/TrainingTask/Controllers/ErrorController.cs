using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TrainingTask.Controllers
{
    public class ErrorController : Controller
    {
        [Route ("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch(statusCode)
            {
                case 404:
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.Message = "Sorry, resource you requested could not be found";
                    break;
            }

            return View("NotFound");
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ErrorMessage = exceptionDetails.Error.Message;

            return View("Error");
        }
    }
}