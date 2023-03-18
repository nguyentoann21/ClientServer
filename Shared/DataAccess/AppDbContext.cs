using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;Database=Stores;Trusted_Connection=true;Encrypt=false");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manufacturer>().HasKey(m => m.ManufacturerID);
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ManufacturerID = "OP", ManufacturerName = "Once Piece" },
                new Manufacturer { ManufacturerID = "DRB", ManufacturerName = "Dragon Ball" },
                new Manufacturer { ManufacturerID = "PO", ManufacturerName = "Pokemon" },
                new Manufacturer { ManufacturerID = "AD", ManufacturerName = "Adventure" },
                new Manufacturer { ManufacturerID = "BR", ManufacturerName = "Bear Rich" }
                );

            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    ProductID = 1,
                    ProductName = "Luffy Gear 3",
                    ProductDescription = "The toys of Luffy Gear 3",
                    ProductPrice = 120000,
                    Quantity = 100,
                    CreateOn = new DateTime(2023, 02, 14),
                    ProductImage = "https://cf.shopee.vn/file/3a0bbf3c2605bcabeadbf7ae252ff056&zimken.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Son Goku",
                    ProductDescription = "The toys of Son Goku",
                    ProductPrice = 150000,
                    Quantity = 50,
                    CreateOn = new DateTime(2023, 01, 14),
                    ProductImage = "https://d3nt9em9l1urz8.cloudfront.net/media/catalog/product/cache/3/image/1100x/040ec09b1e35df139433887a97daa66f/b/i/bibas60208-3-edited.jpg",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Pikachu",
                    ProductDescription = "The toys of Pikachu",
                    ProductPrice = 125000,
                    Quantity = 90,
                    CreateOn = new DateTime(2023, 02, 10),
                    ProductImage = "https://xuhishop.vn/upload/products/4381645371475_xuhi//pikachu_0.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "Superman",
                    ProductDescription = "The toys of Superman",
                    ProductPrice = 100000,
                    Quantity = 80,
                    CreateOn = new DateTime(2023, 02, 02),
                    ProductImage = "https://cdn-amz.woka.io/images/I/61hfcBE4kWL.jpg",
                    ManufacturerID = "AD"
                });
            modelBuilder.Entity<Product>().HasOne(p => p.Manufacturers).WithMany(p => p.Products).HasForeignKey(p => p.ManufacturerID);
        }
    }
}
