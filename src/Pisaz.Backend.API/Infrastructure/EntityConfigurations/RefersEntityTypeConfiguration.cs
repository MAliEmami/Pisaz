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

            builder.HasNoKey();
        }
    }
}