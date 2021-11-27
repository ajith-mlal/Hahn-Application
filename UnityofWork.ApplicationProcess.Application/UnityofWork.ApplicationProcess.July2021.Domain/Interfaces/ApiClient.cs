using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityofWork.ApplicationProcess.July2021.Domain.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Net.Http.Headers;

namespace UnityofWork.ApplicationProcess.July2021.Domain.Interfaces
{
    public class ApiClient:IApiClient
    {
        public async Task<CoinCapModel> GetCoinCap()
        {
            CoinCapModel coincap = new CoinCapModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.coincap.io/v2/assets"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    coincap = JsonConvert.DeserializeObject<CoinCapModel>(apiResponse);
                }
            }
            return coincap;
        }  
    }
}
