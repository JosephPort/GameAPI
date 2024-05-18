using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAPI.Migrations
{
    /// <inheritdoc />
    public partial class addeduid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemUID",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemUID",
                table: "Player");
        }
    }
}
