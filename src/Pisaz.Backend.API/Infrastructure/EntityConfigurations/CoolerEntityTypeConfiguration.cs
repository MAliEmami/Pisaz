using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class CoolerEntityTypeConfiguration : IEntityTypeConfiguration<Cooler>
    {
        public void Configure(EntityTypeBuilder<Cooler> builder)
        {
            builder.ToTable("Cooler");

            builder.HasNoKey();
        }     
    }
}