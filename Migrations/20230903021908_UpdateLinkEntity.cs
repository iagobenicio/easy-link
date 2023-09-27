using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easy_link.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLinkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClicksCount",
                table: "link");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClicksCount",
                table: "link",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
