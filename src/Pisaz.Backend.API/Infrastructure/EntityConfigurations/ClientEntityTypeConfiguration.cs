using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
{
    public class ClientEntityTypeConfiguration  : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasNoKey();
        }
    }
        
}
