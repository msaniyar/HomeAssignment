using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeAssingmentFE.Interfaces;
using HomeAssingmentFE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssingmentFE.Controllers
{
    public class EquipmentController : Controller
    {

        private readonly IFrontEndServices _services;
        [ViewData]
        public string ResultMessage { get; set; }

        public EquipmentController(IFrontEndServices services)
        {
            _services = services;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _services.GetEquipmentList();
            if (TempData["ViewMessage"] != null)
                ResultMessage = TempData["ViewMessage"].ToString();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> List(RentModel model)
        {
            model.UserName = "MyUser";
            model.Id = new Guid();
            TempData["ViewMessage"] = await _services.AddNewRent(model);
            return RedirectToAction(nameof(List));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}