using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Microsoft.Extensions.Configuration;
using FluentValidation.Validators;

namespace Barbearia.Persistence.DbContexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options) { }

        public DbSet<Person> Persons { get; set; } = null!;

        public DbSet<Telephone> Telephones { get; set; } = null!;

        public DbSet<Address> Addresses { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var person = modelBuilder.Entity<Person>();
            var customer = modelBuilder.Entity<Customer>();
            var product = modelBuilder.Entity<Product>();
            var item = modelBuilder.Entity<Item>();
            var Supplier = modelBuilder.Entity<Supplier>();
            var telephone = modelBuilder.Entity<Telephone>();
            var address = modelBuilder.Entity<Address>();

            modelBuilder.Entity<Order>().ToTable("Order", t => t.ExcludeFromMigrations());            
            modelBuilder.Entity<Item>().ToTable("Item", t => t.ExcludeFromMigrations());            
            modelBuilder.Entity<Product>().ToTable("Product", t => t.ExcludeFromMigrations());            
            modelBuilder.Entity<StockHistory>().ToTable("StockHistory", t => t.ExcludeFromMigrations());            
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<StockHistory>();
            modelBuilder.Ignore<Order>();
            modelBuilder.Ignore<Coupon>();
            modelBuilder.Ignore<ProductCategory>();
            modelBuilder.Ignore<OrderProduct>();

            person
            .ToTable("Person")
            .HasDiscriminator<int>("PersonType")
            .HasValue<Person>(1)
            .HasValue<Customer>(2)
            .HasValue<Supplier>(3);

            person
                .Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            person
                .Property(p => p.BirthDate)
                .HasColumnType("date")
                .IsRequired();

            person
                .Property(p => p.Email)
                .HasMaxLength(80)
                .IsRequired();

            person
                .HasMany(p => p.Addresses)
                .WithOne(a => a.Person)
                .HasForeignKey(a => a.PersonId);

            person
                .HasMany(p => p.Telephones)
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                .IsRequired();

            address
                .ToTable("Address");

            address
                .Property(c => c.Street)
                .HasMaxLength(80);

            address
                .Property(c => c.Number);

            address
                .Property(c => c.District)
                .HasMaxLength(60);

            address
                .Property(c => c.City)
                .HasMaxLength(60);

            address
                .Property(c => c.State)
                .HasMaxLength(2);

            address
                .Property(c => c.Cep)
                .HasMaxLength(8);

            address
                .Property(c => c.Complement)
                .HasMaxLength(80);

            address
                .HasOne(p => p.Person)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.PersonId);

            telephone
                .ToTable("Telephone");

            telephone
                .Property(c => c.Number)
                .HasMaxLength(80)
                .IsRequired();

            telephone
                .Property(c => c.Type)
                .IsRequired();

            telephone
                .HasOne(t => t.Person)
                .WithMany(c => c.Telephones)
                .HasForeignKey(t => t.PersonId);

            customer
                .Property(p => p.Gender)
                .IsRequired();

            customer
                .Property(p => p.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            customer
                .HasData(
                    new Customer()
                    {
                        PersonId = 1,
                        Name = "Linus Torvalds",
                        BirthDate = new DateOnly(1999, 8, 7),
                        Gender = 1,
                        Cpf = "73473943096",
                        Email = "veio@hotmail.com",
                    },
                    new Customer()
                    {
                        PersonId = 2,
                        Name = "Bill Gates",
                        BirthDate = new DateOnly(2000, 1, 1),
                        Gender = 2,
                        Cpf = "73473003096",
                        Email = "bill@gmail.com",
                    });

            // product
            //     .ToTable("Product");

            item
                .HasKey(p => p.ItemId);

            // product
            //     .HasKey(p => p.ItemId);

           

            Supplier
                .Property(p => p.Cnpj)
                .HasMaxLength(14);

            Supplier
                .Property(p => p.Gender);

            Supplier
                .Property(p => p.Cpf)
                .HasMaxLength(11);


            // Supplier
            //     .HasMany(s => s.Products)
            //     .WithOne(s => s.Supplier)
            //     .HasForeignKey(s => s.PersonId)
            //     .IsRequired();

            Supplier
                .HasData(
                    new Supplier()
                    {
                        PersonId = 3,
                        Name = "Josefina",
                        BirthDate = new DateOnly(1973, 2, 1),
                        Gender = 2,
                        Cpf = "73473943096",
                        Email = "josefacraft@hotmail.com",
                    },
                    new Supplier()
                    {
                        PersonId = 4,
                        Name = "Microsoft",
                        BirthDate = new DateOnly(1975, 4, 4),
                        Cnpj = "73473003096986",
                        Email = "micro@so.ft",
                    });

            address
                .HasData(
                    new Address()
                    {
                        AddressId = 1,
                        Street = "Rua logo ali",
                        Number = 100,
                        District = "Teste",
                        City = "Bc",
                        State = "SC",
                        Cep = "88888888",
                        Complement = "Perto de la",
                        PersonId = 1
                    },
                    new Address()
                    {
                        AddressId = 2,
                        Street = "Rua longe",
                        Number = 300,
                        District = "Perto",
                        City = "Itajaí",
                        State = "SC",
                        Cep = "88888888",
                        Complement = "Longe de la",
                        PersonId = 2
                    },
                    new Address()
                    {
                        AddressId = 3,
                        Street = "Rua velha",
                        Number = 100,
                        District = "Asilo",
                        City = "Bc",
                        State = "SC",
                        Cep = "80888088",
                        Complement = "Perto",
                        PersonId = 3
                    },
                    new Address()
                    {
                        AddressId = 4,
                        Street = "Rua micro",
                        Number = 300,
                        District = "soft",
                        City = "Floripa",
                        State = "SC",
                        Cep = "88123888",
                        Complement = "Longe",
                        PersonId = 4
                    });

            telephone
                .HasData(
                    new Telephone()
                    {
                        TelephoneId = 1,
                        Number = "47988887777",
                        Type = Telephone.TelephoneType.Mobile,
                        PersonId = 1
                    },
                    new Telephone()
                    {
                        TelephoneId = 2,
                        Number = "47988887777",
                        Type = Telephone.TelephoneType.Landline,
                        PersonId = 2
                    },
                    new Telephone()
                    {
                        TelephoneId = 3,
                        Number = "47944887777",
                        Type = Telephone.TelephoneType.Mobile,
                        PersonId = 3
                    },
                    new Telephone()
                    {
                        TelephoneId = 4,
                        Number = "55988844777",
                        Type = Telephone.TelephoneType.Landline,
                        PersonId = 4
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}

// dotnet ef migrations add Correcao --context CustomerContext --startup-project ../Barbearia.Api
// dotnet ef database update --context CustomerContext --startup-project ../Barbearia.Api