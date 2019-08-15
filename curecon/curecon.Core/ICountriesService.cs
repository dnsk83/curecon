using curecon.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace curecon.Core
{
    public interface ICountriesService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}
