﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eCommerce.Products.Persistence.Context;

#nullable disable

namespace eCommerce.Products.Persistence.Migrations
{
    [DbContext(typeof(ProductsDbContext))]
    partial class ProductsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<JObject>("Characteristics")
                        .HasColumnType("jsonb")
                        .HasColumnName("characteristics");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean")
                        .HasColumnName("is_available");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision")
                        .HasColumnName("rating");

                    b.Property<string>("UnitOfMeasure")
                        .HasColumnType("text")
                        .HasColumnName("unit_of_measure");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_products_code");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_at");

                    b.HasKey("ProductId", "CategoryId")
                        .HasName("pk_product_categories");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_product_categories_category_id");

                    b.ToTable("product_categories", (string)null);
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.HasKey("Id")
                        .HasName("pk_product_images");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_images_product_id");

                    b.ToTable("product_images", (string)null);
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("review");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_product_reviews");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_product_reviews_product_id");

                    b.ToTable("product_reviews", (string)null);
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductCategory", b =>
                {
                    b.HasOne("eCommerce.Products.Domain.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_categories_categories_category_id");

                    b.HasOne("eCommerce.Products.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_categories_products_product_id");
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("eCommerce.Products.Domain.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_images_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.ProductReview", b =>
                {
                    b.HasOne("eCommerce.Products.Domain.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_product_reviews_products_product_id");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("eCommerce.Products.Domain.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
