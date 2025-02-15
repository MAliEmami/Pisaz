using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.PCpart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class RAM_StickEntityTypeConfiguration : IEntityTypeConfiguration<RAM_Stick>
    {
        public void Configure(EntityTypeBuilder<RAM_Stick> builder)
        {
            builder.ToTable("RAM_Stick");

            builder.HasNoKey();
        }  
    }
}