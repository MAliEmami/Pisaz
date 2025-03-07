using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class PublicCodeEntityTypeConfiguration : IEntityTypeConfiguration<PublicCode>
    {
        public void Configure(EntityTypeBuilder<PublicCode> builder)
        {
            builder.ToTable("PublicCode");

            builder.HasNoKey();
        }
        
    }
}