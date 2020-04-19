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
    public class GetRentListTests
    {
        private readonly IBackendServices _services;
        private readonly HomeAssignmentAPIContext _context;
        private readonly RentedHistoryController _controller;

        public GetRentListTests()
        {
            var options = new DbContextOptionsBuilder<HomeAssignmentAPIContext>()
                .UseInMemoryDatabase(databaseName: "HomeAssignmentAPIDB")
                .Options;
            _context = new HomeAssignmentAPIContext(options);
            _services = new BackendServices(new HomeAssignmentAPIContext(options));
            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Heavy", Name = "testTypeRent" });
            _context.SaveChanges();
            _context.RentedHistory.Add(new RentedHistory()
            {
                RentedDays = 3,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testTypeRent",
                UserName = "testuserRent"

            });
            _context.SaveChanges();

            _controller = new RentedHistoryController(_context, _services);
        }

        [Test]
        public async Task GetRentList()
        {
           var result = await _controller.GetRentedHistory("testuserRent");
           Assert.That(result.Value.Count(), Is.EqualTo(1), "Can't get rent history list");
        }
    }
}