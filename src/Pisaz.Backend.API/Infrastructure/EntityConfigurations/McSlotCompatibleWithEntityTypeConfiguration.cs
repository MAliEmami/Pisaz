using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart.Sazgaryab;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class McSlotCompatibleWithEntityTypeConfiguration : IEntityTypeConfiguration<McSlotCompatibleWith>
    {
        public void Configure(EntityTypeBuilder<McSlotCompatibleWith> builder)
        {
            builder.ToTable("McSlotCompatibleWith");

            builder.HasNoKey();
        }  
    }
}
