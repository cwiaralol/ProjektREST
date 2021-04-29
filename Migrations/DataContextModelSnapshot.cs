﻿// <auto-generated />
using System;
using AplikacjaKurierska.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplikacjaKurierska.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("AplikacjaKurierska.API.Models.DeliveryWindow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DeliveryWindow");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Modul", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeliveryWindowId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryWindowId");

                    b.ToTable("Moduls");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModulId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ModulId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.TransitTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DeliveryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DispatchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TransitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("DispatchId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TransitId");

                    b.ToTable("TransitTime");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.TransitTimeAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Friday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Monday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Saturday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Sunday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Thursday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TransitTimeAvailability");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Modul", b =>
                {
                    b.HasOne("AplikacjaKurierska.API.Models.DeliveryWindow", "DeliveryWindow")
                        .WithMany()
                        .HasForeignKey("DeliveryWindowId");

                    b.Navigation("DeliveryWindow");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Service", b =>
                {
                    b.HasOne("AplikacjaKurierska.API.Models.Modul", null)
                        .WithMany("Services")
                        .HasForeignKey("ModulId");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.TransitTime", b =>
                {
                    b.HasOne("AplikacjaKurierska.API.Models.TransitTimeAvailability", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId");

                    b.HasOne("AplikacjaKurierska.API.Models.TransitTimeAvailability", "Dispatch")
                        .WithMany()
                        .HasForeignKey("DispatchId");

                    b.HasOne("AplikacjaKurierska.API.Models.Service", null)
                        .WithMany("TransitTimes")
                        .HasForeignKey("ServiceId");

                    b.HasOne("AplikacjaKurierska.API.Models.TransitTimeAvailability", "Transit")
                        .WithMany()
                        .HasForeignKey("TransitId");

                    b.Navigation("Delivery");

                    b.Navigation("Dispatch");

                    b.Navigation("Transit");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Modul", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("AplikacjaKurierska.API.Models.Service", b =>
                {
                    b.Navigation("TransitTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
