using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HomeAssignmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignmentAPI.Interfaces
{
    public interface IBackendServices
    {
        string GetInvoice(string userName);

    }
}