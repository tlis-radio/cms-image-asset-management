﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence;

#nullable disable

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ImageAssetManagementDbContext))]
    partial class ImageAssetManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cms_image_asset_management")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Crop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uuid")
                        .HasColumnName("image_id");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<int>("Width")
                        .HasColumnType("integer")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_crop");

                    b.HasIndex("ImageId")
                        .HasDatabaseName("ix_crop_image_id");

                    b.ToTable("crop", "cms_image_asset_management");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<int>("Width")
                        .HasColumnType("integer")
                        .HasColumnName("width");

                    b.HasKey("Id")
                        .HasName("pk_image");

                    b.ToTable("image", "cms_image_asset_management");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Crop", b =>
                {
                    b.HasOne("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", null)
                        .WithMany("Crops")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_crop_image_image_id");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", b =>
                {
                    b.Navigation("Crops");
                });
#pragma warning restore 612, 618
        }
    }
}
