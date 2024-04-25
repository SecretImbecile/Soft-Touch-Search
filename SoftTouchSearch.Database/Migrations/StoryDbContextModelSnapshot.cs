﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftTouchSearch.Data;

#nullable disable

namespace SoftTouchSearch.Data.Migrations
{
    [DbContext(typeof(StoryDbContext))]
    partial class StoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Chapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ChapterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentHtml")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DescriptionHtml")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlExternal")
                        .HasColumnType("TEXT");

                    b.Property<int>("UrlId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.ExclusionRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExclusionRules");
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Episode", b =>
                {
                    b.HasOne("SoftTouchSearch.Data.Models.Chapter", "Chapter")
                        .WithMany("Episodes")
                        .HasForeignKey("ChapterId");

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Chapter", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
