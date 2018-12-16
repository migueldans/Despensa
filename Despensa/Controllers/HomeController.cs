using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Despensa.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace Despensa.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "administrador")]
        public IActionResult Pantries()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Purchase()
        {
            return View();
        }

        public IActionResult Pantry()
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
