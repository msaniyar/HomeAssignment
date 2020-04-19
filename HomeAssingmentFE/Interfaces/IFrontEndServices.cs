using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssingmentFE.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssingmentFE.Interfaces
{
    public interface IFrontEndServices
    {
        Task<IEnumerable<ListModel>> GetEquipmentList();
        Task<IEnumerable<RentModel>> GetHistory(string username);
        Task<string> AddNewRent(RentModel model);
        Task<string> GetInvoice(string username);

    }
}