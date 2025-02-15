using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.ClientModels;
namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class RefersEntityTypeConfiguration  : IEntityTypeConfiguration<Refers>
    {
        public void Configure(EntityTypeBuilder<Refers> builder)
        {

            builder.ToTable("Refers");

            builder.HasKey(c => c.Referee);

            /*
            builder.HasOne<Client>()
                   .WithMany()
                   .HasForeignKey(r => r.Referee)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Client>()
                   .WithMany()
                   .HasForeignKey(r => r.Referrer)
                   .OnDelete(DeleteBehavior.NoAction);
            */
        }
    }
}