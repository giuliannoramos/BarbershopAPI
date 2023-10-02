﻿// <auto-generated />
using System;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barbearia.Persistence.Migrations.Item
{
    [DbContext(typeof(ItemContext))]
    [Migration("20231002023339_AddItem")]
    partial class AddItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)");

                    b.HasKey("ItemId");

                    b.ToTable("Item");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("BuyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int?>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("PersonId");

                    b.ToTable("Order", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("OrderProduct", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ItemId = 1
                        },
                        new
                        {
                            OrderId = 2,
                            ItemId = 1
                        },
                        new
                        {
                            OrderId = 1,
                            ItemId = 2
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
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("PersonId");

                    b.ToTable("Person", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductCategory", (string)null);

                    b.HasData(
                        new
                        {
                            ProductCategoryId = 1,
                            Name = "Comida"
                        },
                        new
                        {
                            ProductCategoryId = 2,
                            Name = "Gel"
                        });
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
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)");

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

                    b.HasIndex("OrderId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ProductId");

                    b.ToTable("StockHistory", (string)null);

                    b.HasData(
                        new
                        {
                            StockHistoryId = 1,
                            Amount = 20,
                            CurrentPrice = 23.5m,
                            LastStockQuantity = 10,
                            Operation = 1,
                            OrderId = 1,
                            PersonId = 3,
                            ProductId = 1,
                            Timestamp = new DateTime(2023, 10, 2, 2, 33, 39, 660, DateTimeKind.Utc).AddTicks(8041)
                        },
                        new
                        {
                            StockHistoryId = 2,
                            Amount = 40,
                            CurrentPrice = 200.2m,
                            LastStockQuantity = 32,
                            Operation = 3,
                            OrderId = 2,
                            PersonId = 4,
                            ProductId = 2,
                            Timestamp = new DateTime(2023, 10, 2, 2, 33, 39, 660, DateTimeKind.Utc).AddTicks(8048)
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Product", b =>
                {
                    b.HasBaseType("Barbearia.Domain.Entities.Item");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

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
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasIndex("PersonId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Description = "é bom e te deixa ligadão",
                            Name = "Bombomzinho de energético",
                            Price = 20m,
                            Brand = "Josefa doces para gamers",
                            PersonId = 3,
                            ProductCategoryId = 1,
                            QuantityInStock = 40,
                            QuantityReserved = 35,
                            SKU = "G4M3R5",
                            Status = 1
                        },
                        new
                        {
                            ItemId = 2,
                            Description = "deixa o cabelo duro",
                            Name = "Gel Mil Grau",
                            Price = 40m,
                            Brand = "Microsoft Cooporations",
                            PersonId = 4,
                            ProductCategoryId = 2,
                            QuantityInStock = 400,
                            QuantityReserved = 20,
                            SKU = "S0FT",
                            Status = 2
                        });
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Order", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Person", "Person")
                        .WithMany("Orders")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barbearia.Domain.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.StockHistory", b =>
                {
                    b.HasOne("Barbearia.Domain.Entities.Order", "Order")
                        .WithMany("StockHistories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("Supplier");
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

                    b.HasOne("Barbearia.Domain.Entities.ProductCategory", "ProductCategory")
                        .WithMany("Product")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("StockHistories");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Person", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");

                    b.Navigation("StockHistories");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.ProductCategory", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Barbearia.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("StockHistories");
                });
#pragma warning restore 612, 618
        }
    }
}