using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameAPI.Migrations
{
    /// <inheritdoc />
    public partial class addtournamenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveJSONString",
                table: "Player");

            migrationBuilder.CreateTable(
                name: "GameSave",
                columns: table => new
                {
                    GameSaveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CashEarned = table.Column<int>(type: "int", nullable: false),
                    CashSpent = table.Column<int>(type: "int", nullable: false),
                    GoldEarned = table.Column<int>(type: "int", nullable: false),
                    GoldSpent = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSave", x => x.GameSaveID);
                    table.ForeignKey(
                        name: "FK_GameSave_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSave_PlayerID",
                table: "GameSave",
                column: "PlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSave");

            migrationBuilder.AddColumn<string>(
                name: "SaveJSONString",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
