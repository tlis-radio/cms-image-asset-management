﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence;

#nullable disable

namespace Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ImageAssetManagementDbContext))]
    [Migration("20240609130345_ShowImage")]
    partial class ShowImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.ToTable("image", "cms_image_asset_management");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.ShowImage", b =>
                {
                    b.HasBaseType("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image");

                    b.Property<Guid>("ShowId")
                        .HasColumnType("uuid")
                        .HasColumnName("show_id");

                    b.HasIndex("ShowId")
                        .HasDatabaseName("ix_show_image_show_id");

                    b.ToTable("show_image", "cms_image_asset_management");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.UserProfileImage", b =>
                {
                    b.HasBaseType("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_profile_image_user_id");

                    b.ToTable("user_profile_image", "cms_image_asset_management");
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

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.ShowImage", b =>
                {
                    b.HasOne("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", null)
                        .WithOne()
                        .HasForeignKey("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.ShowImage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_show_image_image_id");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.UserProfileImage", b =>
                {
                    b.HasOne("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", null)
                        .WithOne()
                        .HasForeignKey("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.UserProfileImage", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_profile_image_image_id");
                });

            modelBuilder.Entity("Tlis.Cms.ImageAssetManagement.Domain.Entities.Images.Image", b =>
                {
                    b.Navigation("Crops");
                });
#pragma warning restore 612, 618
        }
    }
}