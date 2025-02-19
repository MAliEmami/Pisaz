using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class LockedShoppingCartEntityTypeConfiguration : IEntityTypeConfiguration<LockedShoppingCart>
    {
        public void Configure(EntityTypeBuilder<LockedShoppingCart> builder)
        {
            builder.ToTable("LockedShoppingCart");

            builder.HasNoKey();
        }  
    }
}