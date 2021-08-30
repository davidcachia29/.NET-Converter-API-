using System.Collections.Generic;
using System.Threading.Tasks;
using MyConvertor.Core.Models;

namespace MyConvertor.Core.Repositories
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Task<Currency> GetAllDataAsync();

        Task<Currency> GetAllDataDate(int days);

        Task<Currency> GetAllDataTarget(string target);

        Task<ConvertorDataGet> DisplayConvertDataAsync(ConvertorResource resource);
    }
}