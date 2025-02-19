using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class DiscountCodeEntityTypeConfiguration : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.ToTable("DiscountCode");

            builder.HasNoKey();
        }
    }
}