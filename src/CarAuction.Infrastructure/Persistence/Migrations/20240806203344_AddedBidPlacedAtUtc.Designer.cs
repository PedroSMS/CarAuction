﻿// <auto-generated />
using System;
using CarAuction.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarAuction.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(CarAuctionContext))]
    [Migration("20240806203344_AddedBidPlacedAtUtc")]
    partial class AddedBidPlacedAtUtc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarAuction.Domain.Entities.Auction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("FinishedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Auction");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Bid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PlacedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.ToTable("Bid");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StartingBid")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("Vehicle");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Hatchback", b =>
                {
                    b.HasBaseType("CarAuction.Domain.Entities.Vehicle");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("int");

                    b.ToTable("HatchBack");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Sedan", b =>
                {
                    b.HasBaseType("CarAuction.Domain.Entities.Vehicle");

                    b.Property<int>("NumberOfDoors")
                        .HasColumnType("int");

                    b.ToTable("Sedan");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Suv", b =>
                {
                    b.HasBaseType("CarAuction.Domain.Entities.Vehicle");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.ToTable("Suv");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Truck", b =>
                {
                    b.HasBaseType("CarAuction.Domain.Entities.Vehicle");

                    b.Property<int>("LoadCapacity")
                        .HasColumnType("int");

                    b.ToTable("Truck");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Auction", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Vehicle", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Bid", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Auction", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Hatchback", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarAuction.Domain.Entities.Hatchback", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Sedan", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarAuction.Domain.Entities.Sedan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Suv", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarAuction.Domain.Entities.Suv", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Truck", b =>
                {
                    b.HasOne("CarAuction.Domain.Entities.Vehicle", null)
                        .WithOne()
                        .HasForeignKey("CarAuction.Domain.Entities.Truck", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarAuction.Domain.Entities.Auction", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
