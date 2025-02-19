using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class CaseEntityTypeConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Case");

            builder.HasNoKey();
        }
    }
}