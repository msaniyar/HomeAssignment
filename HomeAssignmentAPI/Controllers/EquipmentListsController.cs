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
    [Route("[controller]")]
    [ApiController]
    public class EquipmentListsController : ControllerBase
    {
        private readonly HomeAssignmentAPIContext _context;

        public EquipmentListsController(HomeAssignmentAPIContext context)
        {
            _context = context;
        }

        // GET: EquipmentLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentList>>> GetEquipmentList()
        {
            return await _context.EquipmentList.ToListAsync();
        }

    }
}
