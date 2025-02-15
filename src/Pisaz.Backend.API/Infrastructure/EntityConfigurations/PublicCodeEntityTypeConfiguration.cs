using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            builder.HasKey(c => c.Code);

            /*
            builder.HasOne<DiscountCode>()
                   .WithMany()
                   .HasForeignKey(r => r.Code)
                   .OnDelete(DeleteBehavior.NoAction);
            */
        }
        
    }
}