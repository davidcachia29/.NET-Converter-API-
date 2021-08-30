using System;
using System.Threading.Tasks;
using MyConvertor.Core.Repositories;

namespace MyConvertor.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICurrencyRepository Currencys { get; }
        
        Task<int> CommitAsync();
    }
}