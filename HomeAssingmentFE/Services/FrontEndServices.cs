using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HomeAssingmentFE.Interfaces;
using HomeAssingmentFE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HomeAssingmentFE.Services
{
    public class FrontEndServices : IFrontEndServices
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();


        public FrontEndServices(IConfiguration configuration)
        {
            _configuration = configuration;
            var endpoint = configuration.GetSection("apiendpoint").Value;
            _client = new HttpClient { BaseAddress = new Uri(endpoint) };
        }

        public async Task<string> AddNewRent(RentModel model)
        {
            var requestUri = _configuration.GetSection("renthistory").Value;
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(requestUri, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return "Successful";
            }
            else
            {
                Log.Error($"Status code is not ok. {response.StatusCode} ");
                return "Unsuccessful";
            }
        }

        public async Task<IEnumerable<ListModel>> GetEquipmentList()
        {
            var equipmentEndPoint = _configuration.GetSection("equipmentlist").Value;
            var response = await _client.GetAsync(equipmentEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ListModel>>(stringResponse);
            }
            else
            {
                Log.Error($"Status code is not ok. {response.StatusCode} ");
                return null;
            }
        }

        public async Task<IEnumerable<RentModel>> GetHistory(string username)
        {
            var requestUri = _configuration.GetSection("renthistory").Value;
            var response = await _client.GetAsync($"{requestUri}?username={username}");
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<RentModel>>(stringResponse);
            }
            else
            {
                Log.Error($"Status code is not ok. {response.StatusCode} ");
                return null;
            }
        }

        public async Task<string> GetInvoice(string username)
        {
            var requestUri = _configuration.GetSection("renthistory").Value;
            var response = await _client.GetAsync($"{requestUri}/getinvoice?username={username}");
            if(!response.IsSuccessStatusCode)
                Log.Error($"Status code is not ok. {response.StatusCode} ");
            return await response.Content.ReadAsStringAsync();
        }
    }
}