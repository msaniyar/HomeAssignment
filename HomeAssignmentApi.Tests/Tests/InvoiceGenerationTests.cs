using System;
using HomeAssignmentAPI.Controllers;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;
using HomeAssignmentAPI.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HomeAssignmentApi.Tests.Tests
{
    [TestFixture]
    public class InvoiceGenerationTests
    {
        private readonly IBackendServices _services;
        private readonly HomeAssignmentAPIContext _context;


        public InvoiceGenerationTests()
        {
            var options = new DbContextOptionsBuilder<HomeAssignmentAPIContext>()
                .UseInMemoryDatabase(databaseName: "HomeAssignmentAPIDB")
                .Options;
            _context = new HomeAssignmentAPIContext(options);
            _services = new BackendServices(new HomeAssignmentAPIContext(options));

            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Heavy", Name = "testtype1" });
            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Regular", Name = "testtype2" });
            _context.EquipmentList.Add(new EquipmentList() { EquipmentType = "Specialized", Name = "testtype3" });

        }

        [Test]
        public void HeavyInvoice()
        {

            var rentController = new RentedHistoryController(_context, _services);

            var rent = new RentedHistory
            {
                RentedDays = 3,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype1",
                UserName = "testuser"
            };

            var result = rentController.PostRentedHistory(rent);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rent.Name}");

            var invoiceResult = _services.GetInvoice("testuser");

            Assert.That(invoiceResult.Contains("testtype1 280"), "Equipment type and price are wrong");
            Assert.That(invoiceResult.Contains("Total Price: 280"), "Total price calcuation is wrong");
            Assert.That(invoiceResult.Contains("Total Bonus: 2"), "Total bonus calcuation is wrong");

        }

        [Test]
        public void RegularInvoice()
        {
            var rentController = new RentedHistoryController(_context, _services);

            var rent = new RentedHistory
            {
                RentedDays = 3,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype2",
                UserName = "testuser2"
            };

            var result = rentController.PostRentedHistory(rent);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rent.Name}");

            var invoiceResult = _services.GetInvoice("testuser2");

            Assert.That(invoiceResult.Contains("testtype2 260"), "Equipment type and price are wrong");
            Assert.That(invoiceResult.Contains("Total Price: 260"), "Total price calcuation is wrong");
            Assert.That(invoiceResult.Contains("Total Bonus: 1"), "Total bonus calcuation is wrong");

        }


        [Test]
        public void SpecializedInvoice()
        {
            var rentController = new RentedHistoryController(_context, _services);

            var rent = new RentedHistory
            {
                RentedDays = 4,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype3",
                UserName = "testuser3"
            };

            var result = rentController.PostRentedHistory(rent);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rent.Name}");

            var invoiceResult = _services.GetInvoice("testuser3");

            Assert.That(invoiceResult.Contains("testtype3 220"), "Equipment type and price are wrong");
            Assert.That(invoiceResult.Contains("Total Price: 220"), "Total price calcuation is wrong");
            Assert.That(invoiceResult.Contains("Total Bonus: 1"), "Total bonus calcuation is wrong");

        }

        [Test]
        public void TotalInvoice()
        {
            var rentController = new RentedHistoryController(_context, _services);

            var rentheavy = new RentedHistory
            {
                RentedDays = 4,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype1",
                UserName = "testuser4"
            };

            var result = rentController.PostRentedHistory(rentheavy);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rentheavy.Name}");


            var rentregular = new RentedHistory
            {
                RentedDays = 4,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype2",
                UserName = "testuser4"
            };

            result = rentController.PostRentedHistory(rentregular);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rentregular.Name}");


            var rentSpecialized = new RentedHistory
            {
                RentedDays = 4,
                EquipmentList = null,
                Id = new Guid(),
                Name = "testtype3",
                UserName = "testuser4"
            };

            result = rentController.PostRentedHistory(rentSpecialized);
            Assert.That(result.IsCompletedSuccessfully.Equals(true), $"New rent cannot be added.  Name: {rentSpecialized.Name}");

            var invoiceResult = _services.GetInvoice("testuser4");

            Assert.That(invoiceResult.Contains("testtype1 340"), "Equipment type and price are wrong for testtype1");
            Assert.That(invoiceResult.Contains("testtype2 300"), "Equipment type and price are wrong for testtype2");
            Assert.That(invoiceResult.Contains("testtype3 220"), "Equipment type and price are wrong for testtype3");
            Assert.That(invoiceResult.Contains("Total Price: 860"), "Total price calcuation is wrong");
            Assert.That(invoiceResult.Contains("Total Bonus: 4"), "Total bonus calcuation is wrong");



        }


    }
}