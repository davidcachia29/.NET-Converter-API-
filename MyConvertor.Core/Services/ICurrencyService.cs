using System.Collections.Generic;
using System.Threading.Tasks;
using MyConvertor.Core.Models;

namespace MyConvertor.Core.Services
{
    public interface ICurrencyService
    {
        Task<Currency> DisplayDataAsync();

        Task<Currency> DisplayTargetDataAsync(string target);

        Task<Currency> DisplayTargetDateAsync(int days);

        Task<ConvertorDataGet> DisplayConvertDataAsync(ConvertorResource resource);
        
    }
}