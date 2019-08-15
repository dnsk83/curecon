using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using curecon.Core.DTO;

namespace curecon.Core
{
    public class CachedCountriesService : ICountriesService
    {
        ICountriesService CountriesService;

        public CachedCountriesService()
        {
            CountriesService = new CountriesService();
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var cached = new CachedCountries();
            cached = await GetCachedCounriesAsync();
            if (cached.LastCached < DateTime.Now.AddDays(-7))
            {
                var countries = await CountriesService.GetCountriesAsync();
                foreach (var c in countries)
                {
                    cached.Countries.Add(c);
                }
                cached.LastCached = DateTime.Now;
                CacheCountriesAsync(cached);
            }
            return cached.Countries;
        }

        private async Task CacheCountriesAsync(CachedCountries toCache)
        {
            await Task.Run(() =>
            {
                XmlSerializer formatter = new XmlSerializer(typeof(CachedCountries));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "countries.xml");
                using (var writer = new StreamWriter(path))
                {
                    formatter.Serialize(writer, toCache);
                }
            });
        }

        private async Task<CachedCountries> GetCachedCounriesAsync()
        {
            CachedCountries cached = new CachedCountries() { LastCached = DateTime.MinValue };
            await Task.Run(() =>
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(CachedCountries));
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "countries.xml");
                    if (File.Exists(path))
                    {
                        using (var sr = new StreamReader(path))
                        {
                            cached = (CachedCountries)formatter.Deserialize(sr);
                        }
                    }
                }
                catch (Exception e )
                {
                    var x = e.Message;
                    throw;
                }
            });
            return cached;
        }

        [Serializable]
        public class CachedCountries
        {
            public DateTime LastCached { get; set; }
            public List<Country> Countries { get; set; }
            public CachedCountries()
            {
                Countries = new List<Country>();
            }
        }
    }
}
