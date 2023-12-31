﻿// <auto-generated />
using System;
using LicitatiiService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LicitatiiService.Data.Migrations
{
    [DbContext(typeof(LicitatiiDBContext))]
    [Migration("20230906114115_PrimaRealizare")]
    partial class PrimaRealizare
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LicitatiiService.Models.ItemDB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("An")
                        .HasColumnType("integer");

                    b.Property<string>("Culoare")
                        .HasColumnType("text");

                    b.Property<string>("ImagineUrl")
                        .HasColumnType("text");

                    b.Property<int>("Kilomegtraj")
                        .HasColumnType("integer");

                    b.Property<Guid>("LicitatieId")
                        .HasColumnType("uuid");

                    b.Property<string>("Make")
                        .HasColumnType("text");

                    b.Property<string>("ModelMasina")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LicitatieId")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LicitatiiService.Models.Licitatie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Castigator")
                        .HasColumnType("text");

                    b.Property<int>("CelMaiMareBid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LicitatieEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PretRezervare")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Vanzator")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Licitatii");
                });

            modelBuilder.Entity("LicitatiiService.Models.ItemDB", b =>
                {
                    b.HasOne("LicitatiiService.Models.Licitatie", "Licitatie")
                        .WithOne("Item")
                        .HasForeignKey("LicitatiiService.Models.ItemDB", "LicitatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Licitatie");
                });

            modelBuilder.Entity("LicitatiiService.Models.Licitatie", b =>
                {
                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
