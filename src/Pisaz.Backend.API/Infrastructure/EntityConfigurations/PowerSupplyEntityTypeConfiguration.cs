using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class PowerSupplyEntityTypeConfiguration : IEntityTypeConfiguration<PowerSupply>
    {
        public void Configure(EntityTypeBuilder<PowerSupply> builder)
        {
            builder.ToTable("PowerSupply");

            builder.HasNoKey();
        }  
    }
}