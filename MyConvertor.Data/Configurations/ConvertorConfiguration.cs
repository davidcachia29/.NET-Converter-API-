using MyConvertor.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyConvertor.Data.Configurations
{
    public class ConvertorConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {        

            builder
                .Property(m => m.timestamp)
                .IsRequired()
                .HasMaxLength(50);  

            builder
                .Property(m => m.success)
                .IsRequired()
                .HasMaxLength(50);            
            builder
                .ToTable("Convertor");
        }
    }
}