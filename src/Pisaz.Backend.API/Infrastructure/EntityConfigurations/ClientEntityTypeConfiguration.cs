// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Pisaz.Backend.API.Models.ClientModels;

// namespace Pisaz.Backend.API.Infrastructure.EntityConfigurations
// {
//     public class ClientEntityTypeConfiguration  : IEntityTypeConfiguration<Client>
//     {
//         public void Configure(EntityTypeBuilder<Client> builder)
//         {
//             builder.ToTable("Client");

//             builder.HasKey(c => c.Id);

//             builder.Property(c => c.Id)
//                 .ValueGeneratedOnAdd();

//             builder.Property(c => c.PhoneNumber)
//                 .HasColumnType("CHAR(11)")
//                 .IsRequired()
//                 .IsUnicode(false);

//             builder.HasIndex(c => c.PhoneNumber)
//                 .IsUnique();

//             builder.Property(c => c.FirstName)
//                 .HasMaxLength(40)
//                 .IsRequired();

//             builder.Property(c => c.LastName)
//                 .HasMaxLength(40)
//                 .IsRequired();

//             builder.Property(c => c.WalletBalance)
//                 .HasColumnType("DECIMAL")
//                 .HasDefaultValue(0)
//                 .IsRequired()
//                 .HasAnnotation("CheckConstraint", "WalletBalance >= 0");


//             builder.Property(c => c.ReferralCode)
//                 .HasColumnType("CHAR(10)")
//                 .IsUnicode(false);

//             builder.HasIndex(c => c.ReferralCode)
//                 .IsUnique();

//             builder.Property(c => c.SignUpDate)
//                 .HasColumnType("DATETIME")
//                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
//                 .IsRequired();

//             //builder.HasCheckConstraint("CK_Client_PhoneNumber", "PhoneNumber LIKE '09%'");
//         }
//     }
        
// }
