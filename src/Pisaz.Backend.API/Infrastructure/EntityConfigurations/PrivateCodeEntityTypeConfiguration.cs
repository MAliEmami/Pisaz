using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class PrivateCodeEntityTypeConfiguration : IEntityTypeConfiguration<PrivateCode>
    {
        public void Configure(EntityTypeBuilder<PrivateCode> builder)
        {
            builder.ToTable("PrivateCode");

            builder.HasNoKey();
        }  
    }
}