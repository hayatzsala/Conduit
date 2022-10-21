﻿// <auto-generated />
using System;
using Conduit.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Conduit.Db.Migrations
{
    [DbContext(typeof(ConduitContext))]
    partial class ConduitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Conduit.Db.Article", b =>
                {
                    b.Property<Guid>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Conduit.Db.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Conduit.Db.Favourite", b =>
                {
                    b.Property<Guid>("FavouriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FavouriteId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Conduit.Db.Follower", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FollowerId");

                    b.HasIndex("UserId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Conduit.Db.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Conduit.Db.Article", b =>
                {
                    b.HasOne("Conduit.Db.User", null)
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Conduit.Db.Comment", b =>
                {
                    b.HasOne("Conduit.Db.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("Conduit.Db.User", null)
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Conduit.Db.Favourite", b =>
                {
                    b.HasOne("Conduit.Db.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("Conduit.Db.User", null)
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Conduit.Db.Follower", b =>
                {
                    b.HasOne("Conduit.Db.User", null)
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
