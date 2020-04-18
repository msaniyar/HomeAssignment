using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAssingmentFE.Models;

namespace HomeAssingmentFE.Interfaces
{
    public interface IFrontEndServices
    {
        Task<IEnumerable<ListModel>> GetEquipmentList();
        InvoiceModel GetInvoiceList();
        Task<string> AddNewRent(RentModel model);

    }
}