﻿// <auto-generated />
using System;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barbearia.Persistence.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20230922201109_NovaMigration")]
    partial class NovaMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("BuyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CustomerPersonId")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerPersonId");

                    b.ToTable("Orders", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
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
                        .HasColumnType("text");

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

                    b.ToTable("Persons", (string)null);

                    b.HasDiscriminator<int>("PersonType").HasValue(1);

                    b.UseTphMappingStrategy();
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
                            Type = 1
                        },
                        new
                        {
                            TelephoneId = 2,
                            Number = "47988887777",
                            PersonId = 2,
                            Type = 2
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

            modelBuilder.Entity("Barbearia.Domain.Entities.Address", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Person", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Order", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerPersonId");

                    b.Navigation("Customer");
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

            modelBuilder.Entity("Barbearia.Domain.Entities.Person", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Telephones");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
