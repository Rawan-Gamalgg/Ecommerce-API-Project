using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        /*------------------------------------------------------------------*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var createdAt = new DateTime(2026, 5, 10, 17, 12, 15, 908);

            //Data Seeding (Inserting initial data into the database) for testing purposes or to provide default values.
            var Categories = new List<Category>()
            {
                new Category() {Id = 1, Name = "Home", CreatedAt = createdAt },
                new Category() {Id = 2, Name = "Electronics", CreatedAt = createdAt },
                new Category() {Id = 3, Name = "Fashion",  CreatedAt = createdAt },
            };


            var Products = new List<Product>()
            {
               new() { Id = 1, Name = "Wireless Noise-Cancelling Headphones", Description = "Over-ear Bluetooth headphones with 30-hour battery and active noise cancellation.", Price = 149.99m, Count = 42, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 2, Name = "Mechanical Gaming Keyboard", Description = "TKL layout with Cherry MX Red switches and per-key RGB backlight.", Price = 89.99m, Count = 18, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 3, Name = "4K Webcam", Description = "Ultra HD webcam with autofocus and built-in stereo microphone for streaming.", Price = 119.99m, Count = 7, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 4, Name = "USB-C Hub 7-in-1", Description = "Expands to HDMI 4K, 3x USB-A, SD card, microSD, and 100W PD charging.", Price = 49.99m, Count = 65, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 5, Name = "Ergonomic Desk Chair", Description = "Lumbar support, adjustable armrests, and breathable mesh back for all-day comfort.", Price = 299.00m, Count = 3, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 6, Name = "27-inch IPS Monitor", Description = "2560×1440 resolution, 144Hz refresh rate, 1ms response time with HDR400.", Price = 399.99m, Count = 11, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 7, Name = "Portable SSD 1TB", Description = "USB 3.2 Gen 2 with read speeds up to 1050 MB/s in a compact, shockproof case.", Price = 109.95m, Count = 34, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 8, Name = "Smart LED Desk Lamp", Description = "Touch-dimmable with 5 color temperatures and wireless charging base.", Price = 59.99m, Count = 28, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 9, Name = "Vertical Wireless Mouse", Description = "Ergonomic vertical design reducing wrist strain, 2.4GHz with 3-level DPI.", Price = 39.99m, Count = 2, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
               new() { Id = 10, Name = "Laptop Stand Adjustable", Description = "Aluminum foldable riser with 6 height levels, compatible with 10–17 inch devices.", Price = 34.99m, Count = 55, CategoryId = 2, ExpiryDate = new DateTime(2024, 12, 31) },
            };
            /*--------------------------------------------------*/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);//for c
            base.OnModelCreating(modelBuilder);
            /*--------------------------------------------------*/

            modelBuilder.Entity<Category>().HasData(Categories);//Seeding the Categories table with initial data.

            modelBuilder.Entity<Product>().HasData(Products);//Seeding the Products table with initial data.

        }
        /*------------------------------------------------------------------*/
        public override int SaveChanges() //sync with the database and persist changes made to the entities in the context. It returns the number of state entries written to the database.
        {
            AuditLog();
            return base.SaveChanges();
        }
        /*------------------------------------------------------------------*/
        private void AuditLog()
        {
            var dateTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<IAuditEntity>())//ChangeTracker is a property of the DbContext that provides access to the entities being tracked by the context and their state (Added, Modified, Deleted, Unchanged).
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = dateTime;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = dateTime;
                }
            }
        }
        /*------------------------------------------------------------------*/

        /*--------------------------------------------------*/
        public DbSet<Category> Categories => Set<Category>(); 
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        /*--------------------------------------------------*/
    }
}
