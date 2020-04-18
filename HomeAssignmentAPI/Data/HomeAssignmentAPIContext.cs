using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeAssignmentAPI.Models;

namespace HomeAssignmentAPI.Data
{
    public class HomeAssignmentAPIContext : DbContext
    {
        public HomeAssignmentAPIContext (DbContextOptions<HomeAssignmentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<HomeAssignmentAPI.Models.EquipmentList> EquipmentList { get; set; }

        public DbSet<HomeAssignmentAPI.Models.RentedHistory> RentedHistory { get; set; }
    }
}
