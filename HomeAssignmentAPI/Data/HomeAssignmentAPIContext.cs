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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EquipmentList>().HasData(
                new EquipmentList[]
                {
                    new EquipmentList() { Name = "Caterpillar bulldozer", EquipmentType = "Heavy" },
                    new EquipmentList() { Name = "KamAZ truck", EquipmentType = "Regular" },
                    new EquipmentList() { Name = "Komatsu crane", EquipmentType = "Heavy" },
                    new EquipmentList() { Name = "Volvo steamroller", EquipmentType = "Regular" },
                    new EquipmentList() { Name = "Bosch jackhammer", EquipmentType = "Specialized" }
                }
            );


        }
    }
}
