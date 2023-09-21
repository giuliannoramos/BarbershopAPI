using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;

namespace Barbearia.Persistence.DbContexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
        : base(options) { }

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Payment> Payments {get; set; } = null!;

        public DbSet<Coupon> Coupons {get; set; } = null!;

        public DbSet<Person> Persons {get; set;} = null!;

        public DbSet<Address> Addresses {get; set;} = null!;

        public DbSet<Telephone> Telephones {get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            var order = modelBuilder.Entity<Order>();
            var payment = modelBuilder.Entity<Payment>();
            var coupon = modelBuilder.Entity<Coupon>();

            modelBuilder.Entity<Person>().ToTable("Persons", t => t.ExcludeFromMigrations()); 
            modelBuilder.Entity<Address>().ToTable("Address", t => t.ExcludeFromMigrations()); 
            modelBuilder.Entity<Telephone>().ToTable("Telephone", t => t.ExcludeFromMigrations()); // criada em CustomerContext

            order
            .ToTable("Order");

            order
            .Property(o => o.Number)
            .IsRequired();

            order
            .Property(o=>o.Status)
            .IsRequired();

            order
            .Property(o=>o.BuyDate)
            .IsRequired();

            order
            .Property(o=>o.PersonId)
            .IsRequired();


            order
            .HasOne(o=>o.customer)
            .WithMany(c=>c.Orders)
            .HasForeignKey(o=>o.PersonId);

            order
            .HasOne(o=>o.payment)
            .WithOne(p=>p.order)
            .HasForeignKey<Payment>(p=>p.OrderId)
            .IsRequired(false);

            payment
            .ToTable("Payments");

            payment
            .Property(p=>p.BuyDate)
            .IsRequired();

            payment
            .Property(p=>p.GrossTotal)
            .IsRequired();

            payment
            .Property(p=>p.PaymentMethod)
            .IsRequired();

            payment
            .Property(p=>p.Description)
            .IsRequired(false);

            payment
            .Property(p=>p.Status)
            .IsRequired();

            payment
            .Property(p=>p.NetTotal)
            .IsRequired();

            payment
            .Property(p=>p.OrderId)
            .IsRequired();

            payment
            .HasOne(p=>p.coupon)
            .WithMany(c=>c.Payments)
            .HasForeignKey(p=>p.CouponId)
            .IsRequired(false);

            coupon
            .ToTable("Coupons");

            coupon
            .Property(c=>c.CouponCode)
            .HasMaxLength(30)
            .IsRequired();

            coupon
            .Property(c=>c.DiscountPercent)
            .IsRequired();

            coupon
            .Property(c=>c.CreationDate)
            .IsRequired();

            coupon
            .Property(c=>c.ExpirationDate)
            .IsRequired();

            order
            .HasData(
                new Order()
                {
                    OrderId = 1,
                    Number = 500,
                    PersonId = 1,
                    Status = 2,
                    BuyDate = DateTime.UtcNow
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