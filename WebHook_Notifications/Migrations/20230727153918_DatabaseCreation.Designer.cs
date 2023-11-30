﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebHook_Notifications;

#nullable disable

namespace WebHook_Notifications.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230727153918_DatabaseCreation")]
    partial class DatabaseCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebHook_Notifications.Models.NotificationDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("WebHook_Notifications.Models.PayerDB", b =>
                {
                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Document");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Payer");
                });

            modelBuilder.Entity("WebHook_Notifications.Models.TransactionDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("WebHook_Notifications.Models.NotificationDB", b =>
                {
                    b.HasOne("WebHook_Notifications.Models.TransactionDB", "Transaction")
                        .WithOne("Notification")
                        .HasForeignKey("WebHook_Notifications.Models.NotificationDB", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("WebHook_Notifications.Models.PayerDB", b =>
                {
                    b.HasOne("WebHook_Notifications.Models.TransactionDB", "Transaction")
                        .WithOne("Payer")
                        .HasForeignKey("WebHook_Notifications.Models.PayerDB", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("WebHook_Notifications.Models.TransactionDB", b =>
                {
                    b.Navigation("Notification");

                    b.Navigation("Payer");
                });
#pragma warning restore 612, 618
        }
    }
}