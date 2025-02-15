using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class IssuedForEntityTypeConfiguration : IEntityTypeConfiguration<IssuedFor>
    {
        public void Configure(EntityTypeBuilder<IssuedFor> builder)
        {
            builder.ToTable("IssuedFor");

            builder.HasNoKey();
        }  
    }
}