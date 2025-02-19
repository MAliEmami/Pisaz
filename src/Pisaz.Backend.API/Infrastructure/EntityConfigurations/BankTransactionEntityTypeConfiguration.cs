using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Transactions;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class BankTransactionEntityTypeConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransaction");

            builder.HasNoKey();
        }
    }
}