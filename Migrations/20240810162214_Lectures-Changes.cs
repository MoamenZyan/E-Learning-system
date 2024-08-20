using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_Platform_API.Migrations
{
    /// <inheritdoc />
    public partial class LecturesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "Lectures",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "Lectures");
        }
    }
}
