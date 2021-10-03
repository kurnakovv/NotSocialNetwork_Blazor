﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotSocialNetwork.DBContexts;

namespace NotSocialNetwork.DBContexts.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210822090424_Favorites")]
    partial class Favorites
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ImageEntityPublicationEntity", b =>
                {
                    b.Property<Guid>("ImagesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublicationsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ImagesId", "PublicationsId");

                    b.HasIndex("PublicationsId");

                    b.ToTable("ImageEntityPublicationEntity");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.FavoritesEntity", b =>
                {
                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PublicationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.ImageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.PublicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateOfCreate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ImageEntityPublicationEntity", b =>
                {
                    b.HasOne("NotSocialNetwork.Application.Entities.ImageEntity", null)
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotSocialNetwork.Application.Entities.PublicationEntity", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.FavoritesEntity", b =>
                {
                    b.HasOne("NotSocialNetwork.Application.Entities.PublicationEntity", "Publication")
                        .WithMany("FavoritesEntities")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NotSocialNetwork.Application.Entities.UserEntity", "User")
                        .WithMany("FavoritesEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.PublicationEntity", b =>
                {
                    b.HasOne("NotSocialNetwork.Application.Entities.UserEntity", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.UserEntity", b =>
                {
                    b.HasOne("NotSocialNetwork.Application.Entities.ImageEntity", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.PublicationEntity", b =>
                {
                    b.Navigation("FavoritesEntities");
                });

            modelBuilder.Entity("NotSocialNetwork.Application.Entities.UserEntity", b =>
                {
                    b.Navigation("FavoritesEntities");
                });
#pragma warning restore 612, 618
        }
    }
}