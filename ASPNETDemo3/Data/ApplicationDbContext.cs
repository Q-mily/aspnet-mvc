using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASPNETDemo3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ApplicationUser>  User { get; set; }
        public DbSet<OrderTable> OrderTables { get; set; }
        public DbSet<Service> Services { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lobby>()
                .Property(l => l.ImagePath)
                .HasDefaultValue("./");

            modelBuilder.Entity<Food>()
                .Property(p => p.status)
                .HasDefaultValue(1);
            modelBuilder.Entity<Food>()
               .Property(p => p.createdAt)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Lobby>()
                .Property(p => p.status)
                .HasDefaultValue(1);
            modelBuilder.Entity<Lobby>()
               .Property(p => p.createdAt)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Order>()
               .Property(p => p.status)
               .HasDefaultValue(1);
            modelBuilder.Entity<Order>()
               .Property(p => p.createdAt)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Lobby)
                .WithMany(l => l.Orders)
                .HasForeignKey(o => o.lobbyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .Property(o => o.status)
                .HasDefaultValue(1);
            modelBuilder.Entity<Order>()
               .Property(p => p.createdAt)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<OrderTable>()
                .Property(p => p.status)
                .HasDefaultValue(1);
            modelBuilder.Entity<OrderTable>()
               .Property(p => p.createdAt)
               .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<OrderTable>()
                .HasOne(p => p.Order)
                .WithMany(o => o.OrderTables)
                .HasForeignKey(o => o.OrderId)
                .IsRequired(false);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.OrderTables)
                .WithMany(c => c.Foods);

            modelBuilder.Entity<Service>()
               .Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}