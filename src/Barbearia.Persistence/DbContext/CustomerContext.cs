using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Barbearia.Persistence.DbContexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options) { }

        public DbSet<Person> Persons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var person = modelBuilder.Entity<Person>();
            var customer = modelBuilder.Entity<Customer>();

            person
            .ToTable("Persons")
            .HasDiscriminator<int>("PersonType")
            .HasValue<Person>(1)
            .HasValue<Customer>(2);

            person
                .Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            person
                .Property(p => p.Cpf)
                .HasMaxLength(11)
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
                .Property(p => p.Gender)
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
                .HasOne(p => p.Person) 
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.PersonId);

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
                .HasOne(t => t.Person) 
                .WithMany(c => c.Telephones)
                .HasForeignKey(t => t.PersonId);


            modelBuilder.Entity<Customer>()
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
                    });

            modelBuilder.Entity<Telephone>()
                .HasData(
                    new Telephone()
                    {
                        TelephoneId = 1,
                        Number = "47988887777",
                        Type = 1,
                        PersonId = 1
                    },
                    new Telephone()
                    {
                        TelephoneId = 2,
                        Number = "47988887777",
                        Type = 2,
                        PersonId = 2
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}

// dotnet ef migrations add InitialMigration --startup-project ../barbearia.api
// dotnet ef database update --startup-project ../barbearia.api