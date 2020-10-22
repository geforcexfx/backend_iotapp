﻿// <auto-generated />
using System;
using Iot_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Iot_app.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201022121031_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Iot_app.Models.BME680", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("bmetemperature")
                        .HasColumnType("float");

                    b.Property<float>("casio")
                        .HasColumnType("float");

                    b.Property<float>("gas")
                        .HasColumnType("float");

                    b.Property<float>("humidity")
                        .HasColumnType("float");

                    b.Property<float>("noaa")
                        .HasColumnType("float");

                    b.Property<float>("pressure")
                        .HasColumnType("float");

                    b.Property<float>("wiki")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("bME680s");
                });

            modelBuilder.Entity("Iot_app.Models.DHT22", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("comfort")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("dew_point")
                        .HasColumnType("float");

                    b.Property<float>("heat_index")
                        .HasColumnType("float");

                    b.Property<float>("humidity")
                        .HasColumnType("float");

                    b.Property<float>("light")
                        .HasColumnType("float");

                    b.Property<float>("temperature")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("dHT22s");
                });
#pragma warning restore 612, 618
        }
    }
}
