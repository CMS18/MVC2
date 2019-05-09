using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_Battles_BattleId",
                table: "Samurais");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_BattleId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "Samurais");

            migrationBuilder.CreateTable(
                name: "SamuraiBattle",
                columns: table => new
                {
                    SamuraiId = table.Column<int>(nullable: false),
                    BattleId = table.Column<int>(nullable: false),
                    NumberOfKills = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamuraiBattle", x => new { x.BattleId, x.SamuraiId });
                    table.ForeignKey(
                        name: "FK_SamuraiBattle_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SamuraiBattle_Samurais_SamuraiId",
                        column: x => x.SamuraiId,
                        principalTable: "Samurais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SamuraiBattle_SamuraiId",
                table: "SamuraiBattle",
                column: "SamuraiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SamuraiBattle");

            migrationBuilder.AddColumn<int>(
                name: "BattleId",
                table: "Samurais",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_BattleId",
                table: "Samurais",
                column: "BattleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_Battles_BattleId",
                table: "Samurais",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
