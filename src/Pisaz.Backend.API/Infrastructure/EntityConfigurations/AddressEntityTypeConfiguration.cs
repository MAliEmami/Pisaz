// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Pisaz.Backend.API.Models.ClientModels;

// namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
// {
//     public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
//     {
//         public void Configure(EntityTypeBuilder<Address> builder)
//         {
//             builder.ToTable("Address");

//             // builder.HasKey(a => new { a.ClientId, a.Province, a.Remainder });
//             builder.HasKey(a => a.Id); // shuold be remove


//             // builder.HasOne(a => a.Client)
//             //     .WithMany(c => c.Address)
//             //     .HasForeignKey(a => a.ClientId)
//             //     .OnDelete(DeleteBehavior.Cascade);

//             builder.Property(a => a.Province)
//                 .HasMaxLength(20)
//                 .HasDefaultValue("Theran")
//                 .IsRequired();

//             builder.Property(a => a.Remainder)
//                 .HasMaxLength(255)
//                 .IsRequired();
//         }
//     }
// }
