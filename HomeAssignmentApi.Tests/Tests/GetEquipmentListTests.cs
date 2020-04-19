using System;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignmentAPI.Controllers;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;
using HomeAssignmentAPI.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HomeAssignmentApi.Tests.Tests
{
    public class GetEquipmentListTests
    {
        private readonly HomeAssignmentAPIContext _context;
        private readonly EquipmentListsController _controller;

        public GetEquipmentListTests()
        {
            var options = new DbContextOptionsBuilder<HomeAssignmentAPIContext>()
                .UseInMemoryDatabase(databaseName: "HomeAssignmentAPIDB")
                .Options;
            _context = new HomeAssignmentAPIContext(options);

            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Heavy", Name = "testtypeList1" });
            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Regular", Name = "testtypeList2" });
            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Specialized", Name = "testtypeList3" });
            _context.SaveChanges();
            _controller = new EquipmentListsController(_context);
        }

        [Test]
        public async Task GetEquipmentList()
        {
            var result = await _controller.GetEquipmentList();
            Assert.That(result.Value.Count(),Is.EqualTo(3),"Can't get all equipment list" );
        }

    }
}