using System.Threading.Tasks;
using MyConvertor.Core;
using MyConvertor.Core.Repositories;
using MyConvertor.Data.Repositories;

namespace MyConvertor.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyCurrencyDbContext _context;
        private ConvertorRepository _convertorRepository;
        
        public UnitOfWork(MyCurrencyDbContext context)
        {
            this._context = context;
        }

        public ICurrencyRepository Currencys => _convertorRepository = _convertorRepository ?? new ConvertorRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}