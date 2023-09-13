using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Barbearia.Persistence.DbContexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options){}

        public DbSet<Customer> Customers { get; set; } = null!;        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customer");

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.BirthDate)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Gender)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address) // Relacionamento um-para-um com Address
                .WithOne(a => a.Customer)
                .HasForeignKey<Address>(a => a.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Telephone) // Relacionamento um-para-um com Telephone
                .WithOne(t => t.Customer)
                .HasForeignKey<Telephone>(t => t.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .ToTable("Address");

            modelBuilder.Entity<Address>()
                .Property(c => c.Street)
                .HasMaxLength(80);                

            modelBuilder.Entity<Address>()
                .Property(c => c.Number);                

            modelBuilder.Entity<Address>()
                .Property(c => c.District)
                .HasMaxLength(60);                

            modelBuilder.Entity<Address>()
                .Property(c => c.City)
                .HasMaxLength(60);                

            modelBuilder.Entity<Address>()
                .Property(c => c.State)
                .HasMaxLength(2);                

            modelBuilder.Entity<Address>()
                .Property(c => c.Cep)
                .HasMaxLength(8);                

            modelBuilder.Entity<Address>()
                .Property(c => c.Complement)
                .HasMaxLength(80);                

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer) // Relacionamento um-para-um com Customer
                .WithOne(c => c.Address)
                .HasForeignKey<Customer>(c => c.AddressId);                

            modelBuilder.Entity<Telephone>()
                .ToTable("Telephone");

            modelBuilder.Entity<Telephone>()
                .Property(c => c.Number)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<Telephone>()
                .Property(c => c.Type)
                .IsRequired();

            modelBuilder.Entity<Telephone>()
                .HasOne(t => t.Customer) // Relacionamento um-para-um com Customer
                .WithOne(c => c.Telephone)
                .HasForeignKey<Customer>(c => c.TelephoneId); 
                

            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer()
                    {
                        CustomerId = 1,
                        Name = "Linus Torvalds",
                        BirthDate = new DateTime(1999, 8, 7),
                        Gender = 1,
                        Cpf = "73473943096",
                        Email = "veio@hotmail.com",
                        AddressId = 1,
                        TelephoneId = 1
                    },
                    new Customer()
                    {
                        CustomerId = 2,
                        Name = "Bill Gates",
                        BirthDate = new DateTime(2000, 1, 1),
                        Gender = 2,
                        Cpf = "73473003096",
                        Email = "bill@gmail.com",
                        AddressId = 2,
                        TelephoneId = 2
                    });

            modelBuilder.Entity<Address>()
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
                        CustomerId = 1
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
                        CustomerId = 2
                    });

            modelBuilder.Entity<Telephone>()
                .HasData(
                    new Telephone()
                    {
                        TelephoneId = 1,
                        Number = "47988887777",
                        Type = 1,
                        CustomerId = 1                        
                    },
                    new Telephone()
                    {
                        TelephoneId = 2,
                        Number = "47988887777",
                        Type = 2,
                        CustomerId = 2  
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}