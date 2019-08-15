using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace curecon.Core
{
    public interface IRateService
    {
        Task<double> GetRateAsync(string convertFrom, string convertTo);
    }
}
