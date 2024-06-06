using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAPI.Migrations
{
    /// <inheritdoc />
    public partial class changenameplayertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "GameSave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "GameSave",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
