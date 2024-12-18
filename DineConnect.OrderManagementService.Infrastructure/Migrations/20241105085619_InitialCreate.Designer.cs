﻿// <auto-generated />
using System;
using DineConnect.OrderManagementService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DineConnect.OrderManagementService.Infrastructure.Migrations
{
    [DbContext(typeof(DineOutOrderDbContext))]
    [Migration("20241105085619_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Customers.Entities.DeliveryAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("City");

                    b.Property<Guid>("DeliveryAddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("PostalCode");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("Street");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryAddressId")
                        .IsUnique();

                    b.ToTable("DeliveryAddress", (string)null);
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Orders.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Restaurants", (string)null);
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Customers.Entities.DeliveryAddress", b =>
                {
                    b.HasOne("DineConnect.OrderManagementService.Domain.Customers.Customer", null)
                        .WithOne("DeliveryAddress")
                        .HasForeignKey("DineConnect.OrderManagementService.Domain.Customers.Entities.DeliveryAddress", "DeliveryAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Orders.Order", b =>
                {
                    b.HasOne("DineConnect.OrderManagementService.Domain.Customers.Customer", null)
                        .WithOne()
                        .HasForeignKey("DineConnect.OrderManagementService.Domain.Orders.Order", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("DineConnect.OrderManagementService.Domain.Orders.Entities.OrderItem", "OrderItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("OrderItemId");

                            b1.Property<string>("ItemName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Name");

                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Price")
                                .HasColumnType("decimal(5,2)");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.ToTable("OrderOrderItemIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("DineConnect.OrderManagementService.Domain.Orders.ValueObjects.Payment", "Payment", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<int>("PaymentMethod")
                                .HasColumnType("integer");

                            b1.Property<int>("Status")
                                .HasColumnType("integer");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("OrderItems");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Orders.Restaurant", b =>
                {
                    b.OwnsMany("DineConnect.OrderManagementService.Domain.Orders.ValueObjects.OrderId", "OrderIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("IdValue")
                                .HasColumnType("uuid")
                                .HasColumnName("OrderItemId");

                            b1.Property<Guid>("ResturantID")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("ResturantID");

                            b1.ToTable("ResturantOrderIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ResturantID");
                        });

                    b.Navigation("OrderIds");
                });

            modelBuilder.Entity("DineConnect.OrderManagementService.Domain.Customers.Customer", b =>
                {
                    b.Navigation("DeliveryAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
