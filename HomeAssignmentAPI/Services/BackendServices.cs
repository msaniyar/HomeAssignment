using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignmentAPI.Data;
using HomeAssignmentAPI.Interfaces;
using HomeAssignmentAPI.Models;
using HomeAssignmentAPI.Services.Types;
using Microsoft.EntityFrameworkCore;

namespace HomeAssignmentAPI.Services
{
    public class BackendServices : IBackendServices
    {
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

        public IQueryable<RentedHistory> GetInvoice(string userName)
        {
            var rentedList = _db.RentedHistory.Where(x => x.UserName == userName);
            SetPrices(rentedList);
            return rentedList;
        }


        private void SetPrices(IEnumerable<RentedHistory> rentedRecord)
        {
            var rentedHistories = rentedRecord as RentedHistory[] ?? rentedRecord.ToArray();

            var heavyRecords = rentedHistories.Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new {r, h})
                .Where(@t => @t.h.EquipmentType == EquipmentTypes.Heavy.ToString())
                .Select(@t => @t.r);

            var regularRecords = rentedHistories.Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new { r, h })
                .Where(@t => @t.h.EquipmentType == EquipmentTypes.Regular.ToString())
                .Select(@t => @t.r);

            var specializedRecords = rentedHistories.Join(_db.EquipmentList, r => r.Name, h => h.Name, (r, h) => new { r, h })
                .Where(@t => @t.h.EquipmentType == EquipmentTypes.Specialized.ToString())
                .Select(@t => @t.r);

            foreach (var heavy in heavyRecords)
            {
                CalculateHeavy(heavy);
            }

            foreach (var regular in regularRecords)
            {
                CalculateRegular(regular);
            }

            foreach (var specialized in specializedRecords)
            {
                CalculateSpecialized(specialized);
            }

        }


        private void CalculateHeavy(RentedHistory rented)
        {
            rented.InvoiceModel = new InvoiceModel
            {
                TotalPrice = OneTimeFee + (rented.RentedDays * PremiumDailyFee), TotalBonus = HeavyLoyaltyPoint
            };
        }

        private void CalculateRegular(RentedHistory rented)
        {
            rented.InvoiceModel = new InvoiceModel
            {
                TotalPrice = rented.RentedDays > 2
                    ? OneTimeFee + (PremiumDailyFee * 2) + (RegularDailyFee * (rented.RentedDays - 2))
                    : OneTimeFee + PremiumDailyFee * rented.RentedDays,
                TotalBonus = OtherLoyaltyPoing
            };
        }

        private void CalculateSpecialized(RentedHistory rented)
        {
            rented.InvoiceModel = new InvoiceModel
            {
                TotalPrice = rented.RentedDays > 3
                    ? (PremiumDailyFee * 3) + (RegularDailyFee * (rented.RentedDays - 3))
                    : (PremiumDailyFee * rented.RentedDays),
                TotalBonus = OtherLoyaltyPoing
            };
        }
    }
}