﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftTouchSearch.Data;

#nullable disable

namespace SoftTouchSearch.Data.Migrations.Search
{
    [DbContext(typeof(SearchDbContext))]
    partial class SearchDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

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

                    b.Property<Guid>("ChapterId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFirstEpisodeInChapter")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNonStory")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlExternal")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlTapas")
                        .IsRequired()
                        .HasColumnType("TEXT");

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

                    b.ToTable("ExclusionRule");
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Chapter", b =>
                {
                    b.OwnsOne("SoftTouchSearch.Data.Models.MetadataChapter", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("ChapterId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Comments")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Likes")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Views")
                                .HasColumnType("INTEGER");

                            b1.HasKey("ChapterId");

                            b1.ToTable("Chapters");

                            b1.WithOwner()
                                .HasForeignKey("ChapterId");
                        });

                    b.Navigation("Metadata")
                        .IsRequired();
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Episode", b =>
                {
                    b.HasOne("SoftTouchSearch.Data.Models.Chapter", "Chapter")
                        .WithMany("Episodes")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SoftTouchSearch.Data.Models.MetadataEpisode", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("EpisodeId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Comments")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Likes")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("Mature")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Views")
                                .HasColumnType("INTEGER");

                            b1.HasKey("EpisodeId");

                            b1.ToTable("Episodes");

                            b1.WithOwner()
                                .HasForeignKey("EpisodeId");
                        });

                    b.Navigation("Chapter");

                    b.Navigation("Metadata")
                        .IsRequired();
                });

            modelBuilder.Entity("SoftTouchSearch.Data.Models.Chapter", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
