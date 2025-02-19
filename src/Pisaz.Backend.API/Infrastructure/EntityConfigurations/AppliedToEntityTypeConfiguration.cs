using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class AppliedToEntityTypeConfiguration : IEntityTypeConfiguration<AppliedTo>
    {
        public void Configure(EntityTypeBuilder<AppliedTo> builder)
        {
            builder.ToTable("Applied");

            builder.HasNoKey();
        }
    }
}