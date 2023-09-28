using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbearia.Persistence.DbContexts
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
        : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Item> Item { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategory { get; set; } = null!;
        public DbSet<StockHistory> StockHistories { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var item = modelBuilder.Entity<Item>().UseTptMappingStrategy();
            var product = modelBuilder.Entity<Product>();
            // var orderproduct = modelBuilder.Entity<OrderProduct>();
            var productCategory = modelBuilder.Entity<ProductCategory>();
            var stockHistory = modelBuilder.Entity<StockHistory>();

            modelBuilder.Entity<Person>().ToTable("Person", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Order>().ToTable("Order", t => t.ExcludeFromMigrations());

            modelBuilder.Ignore<Address>();
            modelBuilder.Ignore<Telephone>();
            modelBuilder.Ignore<Coupon>();
            modelBuilder.Ignore<Payment>();


            item
                .HasKey(p => p.ItemId);

            item
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsRequired();

            item
                .Property(s => s.Description)
                .IsRequired();

            // product.HasNoKey();

            product
                .ToTable("Product");

            // product
            //     .HasKey(p => p.ItemId);

            product
                .HasMany(s => s.StockHistories)
                .WithOne(p => p.Product)
                .HasForeignKey(s => s.ItemId)
                .IsRequired();

            product
                .HasOne(s => s.Suplier)
                .WithMany(s => s.Products)
                .HasForeignKey(s => s.PersonId)
                .IsRequired();

            // product
            //     .HasMany(s => s.Orders)
            //     .WithMany(p => p.Productsha)
            //     .UsingEntity<OrderProduct>(op => op.ToTable("OrderProduct"));

            // modelBuilder.Entity<OrderProduct>()
            // .HasNoKey();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithMany(e => e.Products)
                .UsingEntity<OrderProduct>(
                    j => j
                        .HasOne(op => op.Order)
                        .WithMany(o => o.OrderProducts)
                        .HasForeignKey(op => op.OrderId),
                    j => j
                        .HasOne(op => op.Product)
                        .WithMany(p => p.OrderProducts)
                        .HasForeignKey(op => op.ItemId),
                    j =>
                    {
                        j.HasKey(op => new { op.OrderId, op.ItemId });
                        j.ToTable("OrderProduct");
                    });



            modelBuilder.Entity<OrderProduct>()
                .HasData(
                    new OrderProduct()
                    {
                        OrderId = 1,
                        ItemId = 1
                    },
                    new OrderProduct()
                    {
                        OrderId = 2,
                        ItemId = 1
                    },
                    new OrderProduct()
                    {
                        OrderId = 1,
                        ItemId = 2
                    }
                );

            // modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.ItemId, op.OrderId });


            product
                .HasOne(s => s.ProductCategory)
                .WithOne(p => p.Product)
                .HasForeignKey<Product>(s => s.ProductCategoryId)
                .IsRequired();

            product
                .Property(s => s.Brand)
                .HasMaxLength(80)
                .IsRequired();

            product
                .Property(s => s.SKU)
                .HasMaxLength(50)
                .IsRequired();

            product
                .Property(s => s.QuantityInStock)
                .IsRequired();

            product
                .Property(s => s.QuantityReserved)
                .IsRequired();

            product
                .HasData(
                    new Product()
                    {
                        ItemId = 1,
                        Name = "Bombomzinho de energético",
                        Description = "é bom e te deixa ligadão",
                        Brand = "Josefa doces para gamers",
                        SKU = "G4M3R5",
                        QuantityInStock = 40,
                        QuantityReserved = 35,
                        ProductCategoryId = 1,
                        PersonId = 3
                    },
                    new Product()
                    {
                        ItemId = 2,
                        Name = "Gel Mil Grau",
                        Description = "deixa o cabelo duro",
                        Brand = "Microsoft Cooporations",
                        SKU = "S0FT",
                        QuantityInStock = 400,
                        QuantityReserved = 20,
                        ProductCategoryId = 2,
                        PersonId = 4
                    }
                );

            productCategory
                .ToTable("ProductCategory");

            productCategory
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            productCategory
                .HasData(
                    new ProductCategory()
                    {
                        ProductCategoryId = 1,
                        Name = "Comida"
                    },
                    new ProductCategory()
                    {
                        ProductCategoryId = 2,
                        Name = "Gel"
                    }
                );

            stockHistory
                .ToTable("StockHistory");

            stockHistory
                .Property(s => s.Operation)
                .IsRequired();

            stockHistory
                .Property(s => s.CurrentPrice)
                .IsRequired();

            stockHistory
                .Property(s => s.Amount)
                .IsRequired();

            stockHistory
                .Property(s => s.Timestamp)
                .IsRequired();

            stockHistory
                .Property(s => s.LastStockQuantity)
                .IsRequired();

            stockHistory
                .HasOne(s => s.Order)
                .WithMany(o => o.StockHistories)
                .HasForeignKey(o => o.OrderId);

            stockHistory//pelo que entendi, isso é pra dizer que StockHistory vai ser a que vai ter o suplier
                .HasOne(s => s.Suplier)
                .WithOne(s => s.StockHistory)
                .HasForeignKey<StockHistory>(s => s.PersonId)
                .IsRequired();


            stockHistory
                .HasData(
                    new StockHistory()
                    {
                        StockHistoryId = 1,
                        Operation = 1,
                        CurrentPrice = 23.5f,
                        Amount = 20,
                        Timestamp = DateTime.UtcNow,
                        LastStockQuantity = 10,
                        PersonId = 3,
                        ItemId = 1,
                        OrderId = 1
                    },
                    new StockHistory()
                    {
                        StockHistoryId = 2,
                        Operation = 3,
                        CurrentPrice = 200.2f,
                        Amount = 40,
                        Timestamp = DateTime.UtcNow,
                        LastStockQuantity = 32,
                        PersonId = 4,
                        ItemId = 2,
                        OrderId = 2
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}