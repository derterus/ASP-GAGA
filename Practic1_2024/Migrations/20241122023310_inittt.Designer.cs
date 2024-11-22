﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Practic1_2024.Data;

#nullable disable

namespace Practic1_2024.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20241122023310_inittt")]
    partial class inittt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Practic1_2024.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Название")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Описание")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Practic1_2024.Models.Characteristic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Название")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Описание")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Practic1_2024.Models.Manufacturer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Название")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Страна")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Practic1_2024.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("user_id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Дата_обновления")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Дата_создания")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Статус")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Сумма")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Practic1_2024.Models.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("order_id")
                        .HasColumnType("integer");

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.Property<int>("Количество")
                        .HasColumnType("integer");

                    b.Property<decimal>("Цена_за_единицу")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.HasIndex("order_id");

                    b.HasIndex("product_id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Practic1_2024.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("SKU")
                        .HasColumnType("text");

                    b.Property<int?>("category_id")
                        .HasColumnType("integer");

                    b.Property<int?>("manufacturer_id")
                        .HasColumnType("integer");

                    b.Property<int?>("Вес")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Дата_обновления")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Дата_создания")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Количество_на_складе")
                        .HasColumnType("integer");

                    b.Property<string>("Название")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Описание")
                        .HasColumnType("text");

                    b.Property<int>("Цена")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.HasIndex("manufacturer_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Practic1_2024.Models.ProductCharacteristic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("Characteristicid")
                        .HasColumnType("integer");

                    b.Property<int>("characteristic_id")
                        .HasColumnType("integer");

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Characteristicid");

                    b.HasIndex("product_id");

                    b.ToTable("ProductCharacteristics");
                });

            modelBuilder.Entity("Practic1_2024.Models.ProductImage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("URL_изображения")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.Property<bool>("Основное_изображение")
                        .HasColumnType("boolean");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Practic1_2024.Models.Review", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.Property<int>("user_id")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Дата")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Оценка")
                        .HasColumnType("integer");

                    b.Property<string>("Текст")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Practic1_2024.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("text");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("created_at")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Practic1_2024.Models.Order", b =>
                {
                    b.HasOne("Practic1_2024.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Practic1_2024.Models.OrderItem", b =>
                {
                    b.HasOne("Practic1_2024.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practic1_2024.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Practic1_2024.Models.Product", b =>
                {
                    b.HasOne("Practic1_2024.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Practic1_2024.Models.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("manufacturer_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Category");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Practic1_2024.Models.ProductCharacteristic", b =>
                {
                    b.HasOne("Practic1_2024.Models.Characteristic", "Characteristic")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("Characteristicid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practic1_2024.Models.Product", "Product")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characteristic");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Practic1_2024.Models.ProductImage", b =>
                {
                    b.HasOne("Practic1_2024.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Practic1_2024.Models.Review", b =>
                {
                    b.HasOne("Practic1_2024.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practic1_2024.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Practic1_2024.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Practic1_2024.Models.Characteristic", b =>
                {
                    b.Navigation("ProductCharacteristics");
                });

            modelBuilder.Entity("Practic1_2024.Models.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Practic1_2024.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Practic1_2024.Models.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("ProductCharacteristics");

                    b.Navigation("ProductImages");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Practic1_2024.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
