using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;

namespace Barbearia.Persistence.DbContexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
        : base(options) { }

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;

        public DbSet<Coupon> Coupons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var order = modelBuilder.Entity<Order>();
            var payment = modelBuilder.Entity<Payment>();
            var coupon = modelBuilder.Entity<Coupon>();
            var orderProduct = modelBuilder.Entity<OrderProduct>();

            modelBuilder.Entity<Person>().ToTable("Person", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Address>().ToTable("Address", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Telephone>().ToTable("Telephone", t => t.ExcludeFromMigrations()); // criada em CustomerContext 
            // modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct", t => t.ExcludeFromMigrations());  
            modelBuilder.Ignore<Supplier>();
            modelBuilder.Ignore<Product>();
            modelBuilder.Ignore<StockHistory>();   
            modelBuilder.Ignore<OrderProduct>();   

     

            order
            .ToTable("Order");

            order
            .Property(o => o.Number)
            .IsRequired();

            order
            .Property(o => o.Status)
            .IsRequired();

            order
            .Property(o => o.BuyDate)
            .IsRequired();

            order
            .Property(o => o.PersonId)
            .IsRequired();


            order
            .HasOne(o => o.Person)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.PersonId)
            .IsRequired();

            order
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

            payment
            .ToTable("Payment");

            payment
            .Property(p => p.BuyDate)
            .IsRequired();

            payment
            .Property(p => p.GrossTotal)
            .IsRequired();

            payment
            .Property(p => p.PaymentMethod)
            .IsRequired();

            payment
            .Property(p => p.Description)
            .IsRequired(false);

            payment
            .Property(p => p.Status)
            .IsRequired();

            payment
            .Property(p => p.NetTotal)
            .IsRequired();

            payment
            .Property(p => p.OrderId)
            .IsRequired();

            payment
            .HasOne(p => p.Coupon)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.CouponId)
            .IsRequired(false);

            coupon
            .ToTable("Coupon");

            coupon
            .Property(c => c.CouponCode)
            .HasMaxLength(30)
            .IsRequired();

            coupon
            .Property(c => c.DiscountPercent)
            .IsRequired();

            coupon
            .Property(c => c.CreationDate)
            .IsRequired();

            coupon
            .Property(c => c.ExpirationDate)
            .IsRequired();

            order
            .HasData(
                new Order()
                {
                    OrderId = 1,
                    Number = 500,
                    PersonId = 1,
                    Status = 2,
                    BuyDate = DateTime.UtcNow,
                    StockHistories = new List<StockHistory>()
                    {
                        new StockHistory()
                        {
                            StockHistoryId = 1,
                            Operation = 1,
                            CurrentPrice = 23.5m,
                            Amount = 20,
                            Timestamp = DateTime.UtcNow,
                            LastStockQuantity = 10,
                            PersonId = 3,
                            ItemId = 1,
                            OrderId = 1
                        }
                    },
                    Products = new List<Product>()
                    {
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
                    }
                },
                new Order()
                {
                    OrderId = 2,
                    Number = 501,
                    PersonId = 2,
                    Status = 2,
                    BuyDate = DateTime.UtcNow,
                    StockHistories = new List<StockHistory>()
                    {
                        new StockHistory()
                        {
                            StockHistoryId = 2,
                            Operation = 3,
                            CurrentPrice = 200.2m,
                            Amount = 40,
                            Timestamp = DateTime.UtcNow,
                            LastStockQuantity = 32,
                            PersonId = 4,
                            ItemId = 2,
                            OrderId = 2
                        }
                    },
                    Products = new List<Product>()
                    {
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
                        }
                    }
                }
            );

            payment
            .HasData(
                new Payment()
                {
                    PaymentId = 1,
                    BuyDate = DateTime.UtcNow,
                    GrossTotal = 80,
                    PaymentMethod = "Dinheiro",
                    Description = "Para de ler isso aqui e vai programar",
                    Status = 1,
                    NetTotal = 60,
                    OrderId = 1
                }
            );

            coupon
            .HasData(
                new Coupon()
                {
                    CouponId = 1,
                    CouponCode = "teste3",
                    DiscountPercent = 10,
                    CreationDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow
                }
            );

            base.OnModelCreating(modelBuilder);


        }
    }
}