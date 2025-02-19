using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Transactions;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class TransactionsEntityTypeConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable("Transactions");

            builder.HasNoKey();
        }  
    }
}