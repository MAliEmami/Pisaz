using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart.Sazgaryab;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class RmSlotCompatibleWithEntityTypeConfiguration : IEntityTypeConfiguration<RmSlotCompatibleWith>
    {
        public void Configure(EntityTypeBuilder<RmSlotCompatibleWith> builder)
        {
            builder.ToTable("RmSlotCompatibleWith");

            builder.HasNoKey();
        }  
    }
}