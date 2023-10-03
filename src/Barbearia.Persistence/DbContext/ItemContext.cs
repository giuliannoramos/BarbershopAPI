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
        public DbSet<Appointment> Appointments {get; set; } = null!;
        public DbSet<AppointmentService> AppointmentServices { get; set; } = null!;
        public DbSet<Service> Services {get; set; } = null!;
        public DbSet<ServiceCategory> ServiceCategories{get; set; } = null!;
        public DbSet<RoleServiceCategory> RoleServiceCategories{get; set;} = null!;
        public DbSet<AppointmentOrder> AppointmentOrders{get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var item = modelBuilder.Entity<Item>().UseTptMappingStrategy();
            var product = modelBuilder.Entity<Product>();
            var productCategory = modelBuilder.Entity<ProductCategory>();
            var stockHistory = modelBuilder.Entity<StockHistory>();
            var appointment = modelBuilder.Entity<Appointment>();
            var appointmentService = modelBuilder.Entity<AppointmentService>();
            var service = modelBuilder.Entity<Service>();
            var serviceCategory = modelBuilder.Entity<ServiceCategory>();
            var roleServiceCategory = modelBuilder.Entity<RoleServiceCategory>();
            var appointmentOrder = modelBuilder.Entity<AppointmentOrder>();

            modelBuilder.Entity<Person>().ToTable("Person", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Order>().ToTable("Order", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Role>().ToTable("Role", t=>t.ExcludeFromMigrations());
            modelBuilder.Entity<Schedule>().ToTable("Schedule", t=>t.ExcludeFromMigrations());
            

            modelBuilder.Ignore<Address>();
            modelBuilder.Ignore<Telephone>();
            modelBuilder.Ignore<Coupon>();
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<RoleEmployee>();
            // modelBuilder.Ignore<Role>();
            modelBuilder.Ignore<WorkingDay>();
            // modelBuilder.Ignore<Schedule>();
            modelBuilder.Ignore<TimeOff>();


            item
                .HasKey(p => p.ItemId);

            item
                .Property(i => i.Name)
                .HasMaxLength(80)
                .IsRequired();

            item
                .Property(i => i.Description)
                .IsRequired();

            item
                .Property(i=>i.Price)
                .IsRequired()
                .HasPrecision(8,2);

            product
            .ToTable("Product");

            product
                .HasMany(s => s.StockHistories)
                .WithOne(p => p.Product)
                .HasForeignKey(s => s.ProductId)
                .IsRequired();

            product
                .HasOne(s => s.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(s => s.PersonId)
                .IsRequired();

            product
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

            product
                .HasOne(s => s.ProductCategory)
                .WithMany(p => p.Product)
                .HasForeignKey(s => s.ProductCategoryId)
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
                .Property(p=>p.Status)
                .IsRequired();

            appointment
                .ToTable("Appointment");

            appointment
                .Property(a =>a.StartDate)
                .IsRequired();

            appointment
                .Property(a =>a.FinishDate)
                .IsRequired();

            appointment
                .Property(a =>a.Status)
                .IsRequired();

            //demais atributos acho que não devem ser obrigatórios. Por exemplo: o cliente pode nunca confirmar.

            appointment
                .HasOne(a =>a.Schedule)
                .WithMany(s=>s.Appointments)
                .HasForeignKey(a=>a.ScheduleId);

            appointment
                .HasOne(a => a.Person)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.CustomerId);

            appointment
                .HasMany(a => a.Services)
                .WithMany(s => s.Appointments)
                .UsingEntity<AppointmentService>();

            appointment
                .HasMany(a => a.Orders)
                .WithMany(o => o.Appointments)
                .UsingEntity<AppointmentOrder>();

            appointmentService
                .ToTable("AppointmentService");

            appointmentOrder
                .ToTable("AppointmentOrder");

            appointmentService
                .Property(a=>a.Name)
                .IsRequired();

            appointmentService
                .Property(a=>a.DurationMinutes)
                .IsRequired();

            appointmentService
                .Property(a=>a.CurrentPrice)
                .IsRequired();
            
            service
                .ToTable("Service");

            service
                .Property(s => s.DurationMinutes)
                .IsRequired();

            service
                .Property(s => s.ServiceCategoryId)
                .IsRequired();

            service
                .HasMany(s => s.Persons)
                .WithMany(e => e.Services)
                .UsingEntity<EmployeeService>();

            service
                .HasOne(s => s.ServiceCategory)
                .WithMany(sc => sc.Services);

            serviceCategory
                .ToTable("ServiceCategory");

            serviceCategory
                .Property(s => s.Name)
                .IsRequired();

            serviceCategory
                .HasMany(s=> s.Roles)
                .WithMany(r=>r.ServiceCategories)
                .UsingEntity<RoleServiceCategory>();

            roleServiceCategory
                .ToTable("RoleServiceCategory");
            

            product
                .HasData(
                    new Product()
                    {
                        ItemId = 1,
                        Name = "Bombomzinho de energético",
                        Description = "é bom e te deixa ligadão",
                        Brand = "Josefa doces para gamers",
                        SKU = "G4M3R5",
                        Price = 20m,
                        Status = 1,
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
                        Price = 40m,
                        Status = 2,
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
                .HasPrecision(8,2)
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

            stockHistory
                .HasOne(s => s.Supplier)
                .WithMany(s => s.StockHistories)
                .HasForeignKey(s => s.PersonId)
                .IsRequired();


            stockHistory
                .HasData(
                    new StockHistory()
                    {
                        StockHistoryId = 1,
                        Operation = 1,
                        CurrentPrice = 23.5m,
                        Amount = 20,
                        Timestamp = DateTime.UtcNow,
                        LastStockQuantity = 10,
                        PersonId = 3,
                        ProductId = 1,
                        OrderId = 1
                    },
                    new StockHistory()
                    {
                        StockHistoryId = 2,
                        Operation = 3,
                        CurrentPrice = 200.2m,
                        Amount = 40,
                        Timestamp = DateTime.UtcNow,
                        LastStockQuantity = 32,
                        PersonId = 4,
                        ProductId = 2,
                        OrderId = 2
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}