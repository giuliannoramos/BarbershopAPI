using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;

namespace Barbearia.Persistence.DbContexts;

public class CustomerContext : DbContext
{
    public DbSet<Customer> customer { get; set; } = null!;

    public CustomerContext(DbContextOptions<CustomerContext> options)
    : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var customer = modelBuilder.Entity<Customer>(); 

        customer
            .ToTable("Customer");

        customer.Property(c => c.Name)
            .HasMaxLength(80)
            .IsRequired();
            
        customer.Property(c => c.Cpf)
            .HasMaxLength(11)
            .IsRequired();
        
        customer.Property(c => c.BirthDate)
            .IsRequired();
        
        customer.Property(c => c.Email)
            .HasMaxLength(80)
            .IsRequired();
        
        customer.Property(c => c.Gender)
            .IsRequired();


        // modelBuilder.Entity<User>()
        //     .HasData(
        //         new User()
        //         {
        //             Id = 1,
        //             Nome = "Linus Torvalds",
        //             Telefone = "(47)94002-8922",
        //             Email = "veio@hotmal.com",
        //             Cpf = "73473943096",
        //             Endereco = "Logo ali",
        //             Status = 2,
        //             QntPasses = 0,
        //         },
        //         new User()
        //         {
        //             Id = 2,
        //             Nome = "Ele",
        //             Telefone = "(47)94002-8922",
        //             Email = "ele@local.domain",
        //             Cpf = "73473003096",
        //             Endereco = "Virando a esquerda",
        //             Status = 2,
        //             QntPasses = 0,
        //         });

        base.OnModelCreating(modelBuilder);
    }

}