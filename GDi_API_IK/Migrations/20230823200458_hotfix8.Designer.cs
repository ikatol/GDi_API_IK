﻿// <auto-generated />
using System;
using GDi_API_IK.Model.DContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GDi_API_IK.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230823200458_hotfix8")]
    partial class hotfix8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GDi_API_IK.Model.Entities.AssignementLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.ToTable("AssignementLog");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<int>("LoadCapacityKg")
                        .HasColumnType("int");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Registration")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.CarDriver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.ToTable("CarDriver");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.AssignementLog", b =>
                {
                    b.HasOne("GDi_API_IK.Model.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GDi_API_IK.Model.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.CarDriver", b =>
                {
                    b.HasOne("GDi_API_IK.Model.Entities.Car", "Car")
                        .WithOne("CarDriver")
                        .HasForeignKey("GDi_API_IK.Model.Entities.CarDriver", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GDi_API_IK.Model.Entities.Driver", "Driver")
                        .WithOne("CarDriver")
                        .HasForeignKey("GDi_API_IK.Model.Entities.CarDriver", "DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.Car", b =>
                {
                    b.Navigation("CarDriver");
                });

            modelBuilder.Entity("GDi_API_IK.Model.Entities.Driver", b =>
                {
                    b.Navigation("CarDriver");
                });
#pragma warning restore 612, 618
        }
    }
}
