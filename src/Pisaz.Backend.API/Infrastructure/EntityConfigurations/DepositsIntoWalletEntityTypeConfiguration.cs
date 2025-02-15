using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Transactions;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class DepositsIntoWalletEntityTypeConfiguration : IEntityTypeConfiguration<DepositsIntoWallet>
    {
        public void Configure(EntityTypeBuilder<DepositsIntoWallet> builder)
        {
            builder.ToTable("DepositsIntoWallet");

            builder.HasNoKey();
        }  
    }
}