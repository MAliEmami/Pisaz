using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class VIPClientEntityTypeConfiguration : IEntityTypeConfiguration<VIPClient>
    {
        public void Configure(EntityTypeBuilder<VIPClient> builder)
        {

            builder.ToTable("VIPClients");

            builder.HasNoKey();
        }
    
    }
}