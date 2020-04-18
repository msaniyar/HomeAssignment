using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Models;

namespace HomeAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedHistoriesController : ControllerBase
    {
        private readonly HomeAssignmentAPIContext _context;

        public RentedHistoriesController(HomeAssignmentAPIContext context)
        {
            _context = context;
        }

        // GET: api/RentedHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentedHistory>>> GetRentedHistory()
        {
            return await _context.RentedHistory.ToListAsync();
        }

        // GET: api/RentedHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentedHistory>> GetRentedHistory(int id)
        {
            var rentedHistory = await _context.RentedHistory.FindAsync(id);

            if (rentedHistory == null)
            {
                return NotFound();
            }

            return rentedHistory;
        }

        // PUT: api/RentedHistories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentedHistory(int id, RentedHistory rentedHistory)
        {
            if (id != rentedHistory.Name)
            {
                return BadRequest();
            }

            _context.Entry(rentedHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentedHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        // DELETE: api/RentedHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RentedHistory>> DeleteRentedHistory(int id)
        {
            var rentedHistory = await _context.RentedHistory.FindAsync(id);
            if (rentedHistory == null)
            {
                return NotFound();
            }

            _context.RentedHistory.Remove(rentedHistory);
            await _context.SaveChangesAsync();

            return rentedHistory;
        }

        private bool RentedHistoryExists(int id)
        {
            return _context.RentedHistory.Any(e => e.Name == id);
        }
    }
}
