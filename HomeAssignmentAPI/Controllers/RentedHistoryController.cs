using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;

namespace HomeAssignmentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentedHistoryController : ControllerBase
    {
        private readonly HomeAssignmentAPIContext _context;

        private readonly IBackendServices _backendServices;

        public RentedHistoryController(HomeAssignmentAPIContext context, IBackendServices backendServices)
        {
            _context = context;
            _backendServices = backendServices;
        }

        // GET: RentedHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedHistory>>> GetRentedHistory(string userName)
        {
            return await _context.RentedHistory.Where(a => a.UserName == userName).ToListAsync();
        }

        // GET: RentedHistories?username
        [HttpGet]
        [Route("getinvoice")]
        public ActionResult GetInvoice(string userName)
        {
            var result = _backendServices.GetInvoice(userName);
            return !string.IsNullOrEmpty(result) ? (ActionResult) Ok(result) : NoContent();
        }

        

        // POST: RentedHistory
        [HttpPost]
        public async Task<ActionResult<RentedHistory>> PostRentedHistory(RentedHistory rentedHistory)
        {
            _context.RentedHistory.Add(rentedHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentedHistory", new { id = rentedHistory.Name }, rentedHistory);
        }

    }
}
