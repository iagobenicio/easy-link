using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easy_link.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageProfile",
                table: "page",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "page");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageProfile",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
