using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.Transactions;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class SubscribesEntityTypeConfiguration : IEntityTypeConfiguration<Subscribes>
    {
        public void Configure(EntityTypeBuilder<Subscribes> builder)
        {
            builder.ToTable("Subscribes");

            builder.HasNoKey();
        }  
    }
}