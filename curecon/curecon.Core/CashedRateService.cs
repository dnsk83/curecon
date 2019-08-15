using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace curecon.Core
{
    public class CashedRateService : IRateService
    {
        IRateService RateService;
        public CashedRateService()
        {
            RateService = new RateService();
        }
        public async Task<double> GetRateAsync(string convertFrom, string convertTo)
        {
            RateItem rate;
            rate = await GetCachedRateAsync(convertFrom, convertTo);
            if (rate.LastCached < DateTime.Now.AddHours(-1))
            {
                rate.Rate = await RateService.GetRateAsync(convertFrom, convertTo);
                rate.LastCached = DateTime.Now;
                CacheRateAsync(rate);
            }
            return rate.Rate;
        }

        private async Task<RateItem> GetCachedRateAsync(string convertFrom, string convertTo)
        {
            RateItem item = new RateItem() { CurrencyFrom = convertFrom, CurrencyTo = convertTo, LastCached = DateTime.MinValue };
            await Task.Run(() =>
            {
                XmlSerializer formatter = new XmlSerializer(typeof(RateItem));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{convertFrom}_{convertTo}.xml");
                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(path))
                    {
                        item = (RateItem)formatter.Deserialize(sr);
                    } 
                }
            });
            return item;
        }

        private async Task CacheRateAsync(RateItem itemToCache)
        {
            await Task.Run(() =>
            {
                XmlSerializer formatter = new XmlSerializer(typeof(RateItem));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{itemToCache.CurrencyFrom}_{itemToCache.CurrencyTo}.xml");
                using (var writer = new StreamWriter(path))
                {
                    itemToCache.LastCached = DateTime.Now;
                    formatter.Serialize(writer, itemToCache);
                }
            });
        }


        [Serializable]
        public class RateItem
        {
            public string CurrencyFrom { get; set; }
            public string CurrencyTo { get; set; }
            public double Rate { get; set; }
            public DateTime LastCached { get; set; }
        }
    }
}
