using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftTouchSearch.Data.Migrations.Search
{
    /// <inheritdoc />
    public partial class AddIsFirstInChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirstEpisodeInChapter",
                table: "Episodes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstEpisodeInChapter",
                table: "Episodes");
        }
    }
}
