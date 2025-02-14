using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.Infrastructure.EntityConfigurations;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Models.Discount;
using Pisaz.Backend.API.Models.Product.Cart;
using Pisaz.Backend.API.Models.Product.PCpart;
using Pisaz.Backend.API.Models.Product.PCpart.Sazgaryab;
using Pisaz.Backend.API.Models.Transactions;

namespace Pisaz.Backend.API.DbContextes
{
    public class PisazDB(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<VIPClient> VIPClients { get; set; }
        public DbSet<Refers> Refers { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<PrivateCode> PrivateCodes { get; set; }
        public DbSet<PublicCode> PublicCodes { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Subscribes> Subscribes { get; set; }
        public DbSet<DepositsIntoWallet> DepositsIntoWallets { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LockedShoppingCart> LockedShoppingCarts { get; set; }
        public DbSet<AppliedTo> AppliedTos { get; set; }
        public DbSet<IssuedFor> IssuedFors { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<AddedTo> AddedTos { get; set; }
        public DbSet<HDD> HDDs { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<SSD> SSDs { get; set; }
        public DbSet<RAM_Stick> RAM_Sticks { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<Cooler> Coolers { get; set; }
        public DbSet<ConnectorCompatibleWith> ConnectorCompatibleWiths { get; set; }
        public DbSet<SmSlotCompatibleWith> SmSlotCompatibleWiths { get; set; }
        public DbSet<GmSlotCompatibleWith> GmSlotCompatibleWiths { get; set; }
        public DbSet<RmSlotCompatibleWith> RmSlotCompatibleWiths { get; set; }
        public DbSet<McSlotCompatibleWith> McSlotCompatibleWiths { get; set; }
        public DbSet<CcSlotCompatibleWith> CcSlotCompatibleWiths { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder.ApplyConfiguration(new AddressEntityTypeConfiguration());
        //     modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
        // }

    }
}