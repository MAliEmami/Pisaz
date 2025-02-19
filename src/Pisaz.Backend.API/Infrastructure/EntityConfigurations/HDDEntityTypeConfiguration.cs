using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class HDDEntityTypeConfiguration : IEntityTypeConfiguration<HDD>
    {
        public void Configure(EntityTypeBuilder<HDD> builder)
        {
            builder.ToTable("HDD");

            builder.HasNoKey();
        }  
    }
}