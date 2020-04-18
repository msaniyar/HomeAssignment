using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssignmentAPI.Models;

namespace HomeAssignmentAPI.Interfaces
{
    public interface IBackendServices
    {
        IQueryable<RentedHistory> GetInvoice(string userName);

    }
}