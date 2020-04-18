using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HomeAssingmentFE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeAssingmentFE.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HomeAssingmentFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFrontEndServices _services;

        public HomeController(ILogger<HomeController> logger, IFrontEndServices services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> List()
        {
            var list = await _services.GetEquipmentList();
            if (TempData["ViewMessage"] != null)
                ViewBag.Message = TempData["ViewMessage"].ToString();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> List(RentModel model )
        {
            model.UserName = "MyUser";
            model.Id = new Guid();
            TempData["ViewMessage"] = await _services.AddNewRent(model);
            return RedirectToAction(nameof(List));
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
