using curecon.Core.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace curecon.Core
{
    public class RateService : IRateService
    {
        public async Task<double> GetRateAsync(string convertFrom, string convertTo)
        {
            const string API_KEY = "1ebdb3c687255f0c87f6";
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri($"https://free.currconv.com/api/v7/convert?q={convertFrom}_{convertTo}&compact=ultra&apiKey={API_KEY}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync(); //{"USD_PHP":52.127065}

                var index1 = json.IndexOf(':') + 1;
                var index2 = json.IndexOf('}');
                var strValue = json.Substring(index1, index2 - index1);

                if (double.TryParse(strValue, out var value))
                {
                    return value;
                }
                else if (double.TryParse(strValue.Replace('.', ','), out value))
                {
                    return value;
                }
            }
            return 0;
        }
    }
}
