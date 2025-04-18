﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftTouchSearch.Data.Migrations.Search
{
    /// <inheritdoc />
    public partial class AddThumbnails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailGuid",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Thumbnail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_ThumbnailGuid",
                table: "Episodes",
                column: "ThumbnailGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Thumbnail_ThumbnailGuid",
                table: "Episodes",
                column: "ThumbnailGuid",
                principalTable: "Thumbnail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Thumbnail_ThumbnailGuid",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "Thumbnail");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_ThumbnailGuid",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ThumbnailGuid",
                table: "Episodes");
        }
    }
}
