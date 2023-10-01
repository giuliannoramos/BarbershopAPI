﻿// <auto-generated />
using System;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barbearia.Persistence.Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Barbearia.Domain.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AddressId"));

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("AddressId");

                    b.HasIndex("PersonId");

                    b.ToTable("Address", (string)null);

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            Cep = "88888888",
                            City = "Bc",
                            Complement = "Perto de la",
                            District = "Teste",
                            Number = 100,
                            PersonId = 1,
                            State = "SC",
                            Street = "Rua logo ali"
                        },
                        new
                        {
                            AddressId = 2,
                            Cep = "88888888",
                            City = "Itajaí",
                            Complement = "Longe de la",
                            District = "Perto",
                            Number = 300,
                            PersonId = 2,
                            State = "SC",
                            Street = "Rua longe"
                        },
                        new
                        {
                            AddressId = 3,
                            Cep = "80888088",
                            City = "Bc",
                            Complement = "Perto",
                            District = "Asilo",
                            Number = 100,
                            PersonId = 3,
                            State = "SC",
                            Street = "Rua velha"
                        },
                        new
                        {
                            AddressId = 4,
                            Cep = "88123888",
                            City = "Floripa",
                            Complement = "Longe",
                            District = "soft",
                            Number = 300,
                            PersonId = 4,
                            State = "SC",
                            Street = "Rua micro"
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("ItemId");

                    b.ToTable("Item", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonId"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("PersonType")
                        .HasColumnType("integer");

                    b.HasKey("PersonId");

                    b.ToTable("Person", (string)null);

                    b.HasDiscriminator<int>("PersonType").HasValue(1);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.StockHistory", b =>
                {
                    b.Property<int>("StockHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StockHistoryId"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("LastStockQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("Operation")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("StockHistoryId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ProductId");

                    b.ToTable("StockHistory", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Telephone", b =>
                {
                    b.Property<int>("TelephoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TelephoneId"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("TelephoneId");

                    b.HasIndex("PersonId");

                    b.ToTable("Telephone", (string)null);

                    b.HasData(
                        new
                        {
                            TelephoneId = 1,
                            Number = "47988887777",
                            PersonId = 1,
                            Type = 0
                        },
                        new
                        {
                            TelephoneId = 2,
                            Number = "47988887777",
                            PersonId = 2,
                            Type = 1
                        },
                        new
                        {
                            TelephoneId = 3,
                            Number = "47944887777",
                            PersonId = 3,
                            Type = 0
                        },
                        new
                        {
                            TelephoneId = 4,
                            Number = "55988844777",
                            PersonId = 4,
                            Type = 1
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Product", b =>
                {
                    b.HasBaseType("Barbearia.Domain.Entities.Item");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityReserved")
                        .HasColumnType("integer");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasIndex("PersonId");

                    b.ToTable("Product", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Customer", b =>
                {
                    b.HasBaseType("Barbearia.Domain.Entities.Person");

                    b.HasDiscriminator().HasValue(2);

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            BirthDate = new DateOnly(1999, 8, 7),
                            Cnpj = "",
                            Cpf = "73473943096",
                            Email = "veio@hotmail.com",
                            Gender = 1,
                            Name = "Linus Torvalds"
                        },
                        new
                        {
                            PersonId = 2,
                            BirthDate = new DateOnly(2000, 1, 1),
                            Cnpj = "",
                            Cpf = "73473003096",
                            Email = "bill@gmail.com",
                            Gender = 2,
                            Name = "Bill Gates"
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Supplier", b =>
                {
                    b.HasBaseType("Barbearia.Domain.Entities.Person");

                    b.HasDiscriminator().HasValue(3);

                    b.HasData(
                        new
                        {
                            PersonId = 3,
                            BirthDate = new DateOnly(1973, 2, 1),
                            Cnpj = "",
                            Cpf = "73473943096",
                            Email = "josefacraft@hotmail.com",
                            Gender = 2,
                            Name = "Josefina"
                        },
                        new
                        {
                            PersonId = 4,
                            BirthDate = new DateOnly(1975, 4, 4),
                            Cnpj = "73473003096986",
                            Cpf = "",
                            Email = "micro@so.ft",
                            Gender = 0,
                            Name = "Microsoft"
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Address", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Person", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.StockHistory", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Person", "Supplier")
                        .WithMany("StockHistories")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbearia.Domain.Entities.Product", "Product")
                        .WithMany("StockHistories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Telephone", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Person", "Person")
                        .WithMany("Telephones")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Product", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Item", null)
                        .WithOne()
                        .HasForeignKey("Barbearia.Domain.Entities.Product", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbearia.Domain.Entities.Person", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Person", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Products");

                    b.Navigation("StockHistories");

                    b.Navigation("Telephones");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Product", b =>
                {
                    b.Navigation("StockHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
