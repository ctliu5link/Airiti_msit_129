using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchemaNote_A11085.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11085.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult ConnectionString()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult ConnectionString(string DbConn)
        //{
        //    if (DbConn != null)
        //    {
        //        string DbConnString = DbConn.Replace(@"""", "");
        //        TempData["Entry"] = DbConnString;
        //        return RedirectToAction("List");
        //    }
        //    return View("ConnectionString");
        //}

        public IActionResult Index()
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
