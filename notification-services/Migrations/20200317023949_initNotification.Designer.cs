﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using notification_services.Infrastructure;

namespace notification_services.Migrations
{
    [DbContext(typeof(NotifContext))]
    [Migration("20200317023949_initNotification")]
    partial class initNotification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("notification_services.Domain.Entities.LogsEn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email_Destination")
                        .HasColumnType("text");

                    b.Property<int>("From")
                        .HasColumnType("integer");

                    b.Property<int>("Notification_Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReadAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Notification_Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.NotifEn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Notifs");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email_Destination")
                        .HasColumnType("text");

                    b.Property<int?>("LogsEnId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LogsEnId");

                    b.ToTable("Target");
                });

            modelBuilder.Entity("notification_services.Domain.Entities.LogsEn", b =>
                {
                    b.HasOne("notification_services.Domain.Entities.NotifEn", "notif")
                        .WithMany()
                        .HasForeignKey("Notification_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("notification_services.Domain.Entities.Target", b =>
                {
                    b.HasOne("notification_services.Domain.Entities.LogsEn", null)
                        .WithMany("Target")
                        .HasForeignKey("LogsEnId");
                });
#pragma warning restore 612, 618
        }
    }
}