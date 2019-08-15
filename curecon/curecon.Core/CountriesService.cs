using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using curecon.Core.DTO;
using Newtonsoft.Json;

namespace curecon.Core
{
    public class CountriesService : ICountriesService
    {

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://restcountries.eu/rest/v2/all?fields=name;alpha2Code;currencies");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                var ret = JsonConvert.DeserializeObject<IEnumerable<Country>>(json);
                return ret;
            }
            return null;
        }
    }
}
