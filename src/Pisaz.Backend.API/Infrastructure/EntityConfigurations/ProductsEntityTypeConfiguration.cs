using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class ProductsEntityTypeConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");

            builder.HasNoKey();
        }  
    }
}