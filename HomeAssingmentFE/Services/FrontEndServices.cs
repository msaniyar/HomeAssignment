using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HomeAssingmentFE.Interfaces;
using HomeAssingmentFE.Models;
using Newtonsoft.Json;

namespace HomeAssingmentFE.Services
{
    public class FrontEndServices : IFrontEndServices
    {
        private HttpClient _client;
        public FrontEndServices()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:58529/") };
        }

        public async Task<string> AddNewRent(RentModel model)
        {
            var requestUri = "api/rentedhistories";
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(requestUri, stringContent);
            return response.IsSuccessStatusCode ? "Rent is Successfully Added" : "An error occured. Please try again later.";
        }

        public async Task<IEnumerable<ListModel>> GetEquipmentList()
        {
            //var client = new HttpClient {BaseAddress = new Uri("http://localhost:58529/")};
            var response = await _client.GetAsync("api/equipmentlists");
            if (!response.IsSuccessStatusCode) return null;
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ListModel>>(stringResponse);
        }

        public InvoiceModel GetInvoiceList()
        {
            throw new System.NotImplementedException();
        }
    }
}