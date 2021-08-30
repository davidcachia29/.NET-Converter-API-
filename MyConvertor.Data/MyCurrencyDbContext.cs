using Microsoft.EntityFrameworkCore;
using MyConvertor.Core.Models;
using MyConvertor.Data.Configurations;

namespace MyConvertor.Data
{
    public class MyCurrencyDbContext : DbContext
    {
        public DbSet<Currency> Currencys { get; set; }
        
        public MyCurrencyDbContext(DbContextOptions<MyCurrencyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ConvertorConfiguration());           
        }
    }
}