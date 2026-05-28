using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWeb.Models
{
    public class DSTruyen : IdentityDbContext<ApplicationUser>
    {
        public DSTruyen() : base("QLTruyen", throwIfV1Schema: false) 
        { 
        }

        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<Truyen> Truyens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public static DSTruyen Create()
        {
            return new DSTruyen();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ TheLoai - Truyen
            modelBuilder.Entity<Truyen>()
                .HasRequired(t => t.TheLoai)
                .WithMany(tl => tl.Truyens)
                .HasForeignKey(t => t.MaTL)
                .WillCascadeOnDelete(false);

            // Cấu hình quan hệ Order - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .WillCascadeOnDelete(true);

            // Cấu hình quan hệ Truyen - OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Truyen)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(od => od.MaTruyen)
                .WillCascadeOnDelete(false);

            // Cấu hình quan hệ User - Order
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            // Cấu hình quan hệ User - CartItem
            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .WillCascadeOnDelete(true);

            // Cấu hình quan hệ Truyen - CartItem
            modelBuilder.Entity<CartItem>()
                .HasRequired(ci => ci.Truyen)
                .WithMany(t => t.CartItems)
                .HasForeignKey(ci => ci.MaTruyen)
                .WillCascadeOnDelete(false);

            // Cấu hình Precision cho Decimal
            modelBuilder.Entity<Truyen>()
                .Property(t => t.GiaBan)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasPrecision(18, 2);
        }
    }
}