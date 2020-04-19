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
    public class HistoryController : Controller
    {
        private readonly IFrontEndServices _services;

        public HistoryController(IFrontEndServices services)
        {
            _services = services;
        }


        public async Task<ActionResult> GetHistory()
        {
            var history = await _services.GetHistory("MyUser");
            if (!history.Any())
                ViewBag.Message = "No Invoice";
            return View(history);
        }

        public async Task<ActionResult> GenerateInvoice()
        {
            var result = await _services.GetInvoice("MyUser");
            if (string.IsNullOrEmpty(result))
            {
                return RedirectToAction(nameof(GetHistory));
            }
            var date = DateTime.Now.ToString("d");
            return File(System.Text.Encoding.UTF8.GetBytes(result), "text/plain", $"Invoice_{date}.txt");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}