using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class SSDEntityTypeConfiguration : IEntityTypeConfiguration<SSD>
    {
        public void Configure(EntityTypeBuilder<SSD> builder)
        {
            builder.ToTable("SSD");

            builder.HasNoKey();
        }  
    }
}