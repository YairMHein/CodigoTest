using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySql.EntityFrameworkCore.Extensions;

namespace CMSAPI.DBContext
{
    public class MyDbContext: DbContext
    {
        public DbSet<Buyer> tb_buyer { get; set; }
        public DbSet<Purchase> tb_purchase { get; set; }
        public DbSet<EVoucher> tb_evoucher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;database=codigo_test;user=root;password=root");
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public MyDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Buyer>().ToTable("tb_buyer");
            modelBuilder.Entity<Purchase>().ToTable("tb_purchase");
            modelBuilder.Entity<EVoucher>().ToTable("tb_evoucher");

            modelBuilder.Entity<Buyer>().HasKey(x => x.id).HasName("id");
            modelBuilder.Entity<Purchase>().HasKey(x => x.id).HasName("id");
            modelBuilder.Entity<EVoucher>().HasKey(x => x.id).HasName("id");

            modelBuilder.Entity<Buyer>().Property(x => x.id).HasColumnType("varchar(36)").IsRequired();
            modelBuilder.Entity<Buyer>().Property(x => x.name).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<Buyer>().Property(x => x.phone).HasColumnType("varchar(45)").IsRequired();
            modelBuilder.Entity<Buyer>().Property(x => x.maxlimit).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Buyer>().Property(x => x.giftuserlimit).HasColumnType("int").IsRequired(false);

            modelBuilder.Entity<Purchase>().Property(x => x.id).HasColumnType("int").ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.evoucherid).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.buyerid).HasColumnType("varchar(36)").IsRequired(false);
            modelBuilder.Entity<Purchase>().Property(x => x.buydate).HasColumnType("DateTime").IsRequired(false);
            modelBuilder.Entity<Purchase>().Property(x => x.buyquantity).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.totalamount).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<Purchase>().Property(x => x.promocode).HasColumnType("varchar(45)").IsRequired(false);

            modelBuilder.Entity<EVoucher>().Property(x => x.id).HasColumnType("int").ValueGeneratedNever().IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.maxlimitbuy).HasColumnType("int").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.maxlimitgift).HasColumnType("int").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.title).HasColumnType("varchar(45)").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.description).HasColumnType("varchar(1000)").IsRequired(false);
            modelBuilder.Entity<EVoucher>().Property(x => x.expirydate).HasColumnType("DateTime").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.amount).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.paymentmethod).HasColumnType("varchar(45)").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.discountpercentage).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<EVoucher>().Property(x => x.quantity).HasColumnType("int").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.buytype).HasColumnType("int").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.image).HasColumnType("varchar(1000)").IsRequired(false);
            modelBuilder.Entity<EVoucher>().Property(x => x.isactive).HasColumnType("tinyint").IsRequired();
            modelBuilder.Entity<EVoucher>().Property(x => x.createdate).HasColumnType("DateTime").IsRequired();
        }
    }
}
