using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class AddedTotEntityTypeConfiguration : IEntityTypeConfiguration<AddedTo>
    {
        public void Configure(EntityTypeBuilder<AddedTo> builder)
        {
            builder.ToTable("AddedTo");

            builder.HasNoKey();
        }
    }
}