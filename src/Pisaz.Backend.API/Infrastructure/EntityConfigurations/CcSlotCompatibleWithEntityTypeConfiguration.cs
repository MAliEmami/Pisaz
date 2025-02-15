using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart.Sazgaryab;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class CcSlotCompatibleWithEntityTypeConfiguration : IEntityTypeConfiguration<CcSlotCompatibleWith>
    {
        public void Configure(EntityTypeBuilder<CcSlotCompatibleWith> builder)
        {
            builder.ToTable("CcSlotCompatibleWith");

            builder.HasNoKey();
        }
    }
}