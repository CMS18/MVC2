using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityFromEmpty.Migrations
{
    public partial class ShoeSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoeSize",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoeSize",
                table: "AspNetUsers");
        }
    }
}
