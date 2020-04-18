using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;

namespace HomeAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedHistoriesController : ControllerBase
    {
        private readonly HomeAssignmentAPIContext _context;

        private readonly IBackendServices _backendServices;

        public RentedHistoriesController(HomeAssignmentAPIContext context, IBackendServices backendServices)
        {
            _context = context;
            _backendServices = backendServices;
        }

        // GET: api/RentedHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedHistory>>> GetRentedHistory()
        {
            return await _context.RentedHistory.ToListAsync();
        }

        // GET: api/RentedHistories
        [HttpGet]
        [Route("getinvoice")]
        public async Task<ActionResult<IEnumerable<RentedHistory>>> GetInvoice(string userName)
        {
            return await _backendServices.GetInvoice(userName).ToListAsync();
        }

        

        // POST: api/RentedHistories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RentedHistory>> PostRentedHistory(RentedHistory rentedHistory)
        {
            _context.RentedHistory.Add(rentedHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentedHistory", new { id = rentedHistory.Name }, rentedHistory);
        }

    }
}
