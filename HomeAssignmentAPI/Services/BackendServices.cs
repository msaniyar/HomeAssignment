using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;
using HomeAssignmentAPI.Services.Types;

namespace HomeAssignmentAPI.Services
{
    public class BackendServices : IBackendServices
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private readonly HomeAssignmentAPIContext _db;

        //Constant Fees
        private const int OneTimeFee = 100;
        private const int PremiumDailyFee = 60;
        private const int RegularDailyFee = 40;

        //Constant Loyalty Point
        private const int HeavyLoyaltyPoint = 2;
        private const int OtherLoyaltyPoing = 1;


        public BackendServices(HomeAssignmentAPIContext db)
        {
            _db = db;
        }

        public string GetInvoice(string userName)
        {
            var rentedList = _db.RentedHistory.Where(x => x.UserName == userName);
            if (rentedList.FirstOrDefault() == null) return null;
            var invoice = GenerateInvoice(rentedList);
            invoice.Title = $"{userName} Rent Invoice";
            return BuildResponse(invoice);

        }

        private static string BuildResponse(GeneratedInvoice invoice)
        {
            try
            {
                var build = new StringBuilder();
                build.Append(invoice.Title);
                build.AppendLine();
                build.AppendLine();
                build.Append("Rented Items Name and Price");
                build.AppendLine();
                foreach (var item in invoice.Items)
                {
                    build.Append($"{item.Name} {item.Price}");
                    build.AppendLine();
                }

                build.AppendLine();
                build.Append($"Total Price: {invoice.TotalPrice}");
                build.AppendLine();
                build.Append($"Total Bonus: {invoice.TotalBonus}");
                Log.Info($"Text is built {build.ToString()}");
                return build.ToString();

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in string building: {ex.Message}");
                return null;
            }

        }


        private GeneratedInvoice GenerateInvoice(IEnumerable<RentedHistory> rentedRecord)
        {
            var invoiceModel = new GeneratedInvoice { Items = new List<InvoiceItems>() };
            
            var rentedHistories = rentedRecord as RentedHistory[] ?? rentedRecord.ToArray();

            try
            {
                var heavyRecords = rentedHistories.Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new { r, h })
                    .Where(@t => @t.h.EquipmentType == EquipmentTypes.Heavy.ToString())
                    .Select(@t => @t.r).AsEnumerable();

                var regularRecords = rentedHistories.Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new { r, h })
                    .Where(@t => @t.h.EquipmentType == EquipmentTypes.Regular.ToString())
                    .Select(@t => @t.r).AsEnumerable();

                var specializedRecords = rentedHistories
                    .Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new { r, h })
                    .Where(@t => @t.h.EquipmentType == EquipmentTypes.Specialized.ToString())
                    .Select(@t => @t.r).AsEnumerable();

                foreach (var heavy in heavyRecords)
                {
                    var heavyPrice = CalculateHeavy(heavy);
                    invoiceModel.TotalPrice += heavyPrice;
                    invoiceModel.TotalBonus += HeavyLoyaltyPoint;
                    var item = new InvoiceItems { Name = heavy.Name, Price = heavyPrice };
                    invoiceModel.Items.Add(item);
                }

                foreach (var regular in regularRecords)
                {
                    var regularPrice = CalculateRegular(regular);
                    invoiceModel.TotalPrice += regularPrice;
                    invoiceModel.TotalBonus += OtherLoyaltyPoing;
                    var item = new InvoiceItems { Name = regular.Name, Price = regularPrice };
                    invoiceModel.Items.Add(item);
                }

                foreach (var specialized in specializedRecords)
                {
                    var specializedPrice = CalculateSpecialized(specialized);
                    invoiceModel.TotalPrice += specializedPrice;
                    invoiceModel.TotalBonus += OtherLoyaltyPoing;
                    var item = new InvoiceItems { Name = specialized.Name, Price = specializedPrice };
                    invoiceModel.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error in Invoice generation: {ex.Message}");
                return new GeneratedInvoice();
            }

            return invoiceModel;
        }


        private static int CalculateHeavy(RentedHistory rented) => OneTimeFee + (rented.RentedDays * PremiumDailyFee);

        private static int CalculateRegular(RentedHistory rented) =>
            rented.RentedDays > 2
                ? OneTimeFee + (PremiumDailyFee * 2) + (RegularDailyFee * (rented.RentedDays - 2))
                : OneTimeFee + PremiumDailyFee * rented.RentedDays;

        private static int CalculateSpecialized(RentedHistory rented) =>
            rented.RentedDays > 3
                ? (PremiumDailyFee * 3) + (RegularDailyFee * (rented.RentedDays - 3))
                : (PremiumDailyFee * rented.RentedDays);
    }
}